using CGullProject.Data;
using CGullProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace CGullProject.Services
{
    public class CartService : ICartService
    {
        private readonly ShopContext _context;

        public CartService(ShopContext context)
        {
            _context = context;
        }

        public async Task<bool> AddItemToCart(Guid cartId, string itemId, int quantity)
        {
            var cart = await _context.Cart.FindAsync(cartId);

            if (cart == null)
            {
                throw new KeyNotFoundException($"Cart with ID {cartId} not found");
            }
            else if (quantity == 0)
            {
                throw new BadHttpRequestException($"Cannot add 0 items to cart");
            }

            var itemQuantity = from i in _context.Inventory
                               where i.Id == itemId
                               select i.Stock;

            if (quantity > itemQuantity.First())
            {
                throw new BadHttpRequestException($"Tring to add too many of this item. This item only has {itemQuantity} left in stock");
            }


            Dictionary<Tuple<Guid, String>, CartItem> cartItemTable =
                await _context.CartItem.ToDictionaryAsync<CartItem, Tuple<Guid, String>>(cItm => new(cItm.CartId, cItm.ProductId));
            Tuple<Guid, String> cartItemHandle = new(cartId, itemId);
            // If cart already has a quantity of this item, fetch its associated CartItem entry from the database
            // and then update its quantity to reflect the adjusted qty after adding this new qty to cart.
            if (cartItemTable.ContainsKey(cartItemHandle)) 
            {

                cartItemTable[new(cartId, itemId)].Quantity += quantity;    
            }
            else
            {
                // Otherwise, make new entry in DB and add it to the CartItem DBSet
                CartItem cartItem = new CartItem
                {
                    CartId = cartId,
                    ProductId = itemId,
                    Quantity = quantity
                };
                await _context.CartItem.AddAsync(cartItem);

            }
            
            var item = from i in _context.Inventory
                       where i.Id == itemId
                       select i;

            
            item.First().Stock = item.First().Stock - quantity;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Tuple<Cart, IEnumerable<CartItemView>>> GetCart(Guid cartId)
        {
            Cart? result = await _context.Cart.FindAsync(cartId);
            if (result is null)
                throw new KeyNotFoundException($"Cart with ID {cartId} not found");
            IEnumerable<CartItemView> cartItemsQuantitiesAndRunningTotals = await GetCartItemDetails(cartId);
            return new(result, cartItemsQuantitiesAndRunningTotals);
        }

        // TODO: Will need to modify to account for DB changes
        // after change in how bundles are going to be structured in DB
        public async Task<IEnumerable<CartItemView>> GetItemTotals(Guid cartId)
        {
            return await GetCartItemDetails(cartId);
        }

        private async Task<IEnumerable<CartItemView>> GetCartItemDetails(Guid cartId)
        {
            Dictionary<String, Product> inventoryById = await _context.Inventory.ToDictionaryAsync<Product, String>(itm => itm.Id);
            List<CartItem> bundles = new();
            List<CartItemView> idQtyCost = 
                _context.CartItem.Where((Func<CartItem, bool>) (itm => 
                {
                    // Since we also need to sift out bundles,
                    // Preemptively skip the remaining bundle sifting logic if
                    // the cart ID of the current CartItem being queried is even
                    // associated with the Cart we're pulling the item data for.
                    if (itm.CartId != cartId)
                    {
                        return false;
                    }
                    else if (itm.ProductId[0] == '1')
                    {
                        bundles.Add(itm);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                })).Select((Func<CartItem, CartItemView>) (itm => 
                {
                    Product? productHandle = inventoryById.GetValueOrDefault(itm.ProductId);
                    decimal total = (productHandle is null) ? 0M : productHandle.SalePrice;
                    total *= itm.Quantity;
                    return new CartItemView
                    {
                        ProductId = itm.ProductId, 
                        Quantity = itm.Quantity, 
                        Total = total 
                    };
                })).ToList<CartItemView>();
            foreach (CartItem bundle in bundles)
            {
                IEnumerable<Product> bundleContents = from BundleItem itm in _context.BundleItem
                                            where itm.BundleId.Equals(bundle.ProductId) && inventoryById.ContainsKey(itm.ProductId)
                                            select inventoryById[itm.ProductId];
                Bundle? bundleHandle = await _context.Bundle.FindAsync(bundle.ProductId);
                decimal total = 0;
                foreach (Product bundledItem in bundleContents)
                {
                    total += bundledItem.SalePrice;
                }
                total *= bundle.Quantity;
                // There should never be a case where the else operand gets chosen in this ternary operator expr.
                // It's just there to make the VS's language server stop warning me that bundlehandle may be null.
                //total *= bundleHandle is not null ? bundleHandle.Discount : 1M;
                idQtyCost.Add(new CartItemView
                {
                    ProductId = bundle.ProductId,
                    Total = total,
                    Quantity = bundle.Quantity
                });
            }
            return idQtyCost;
        }
        
        public async Task<Guid> CreateNewCart(String cartName)
        {
            Cart cart = (await _context.Cart.AddAsync(new Cart {
                Name = cartName
            })).Entity;
            await _context.SaveChangesAsync();
            return cart.Id;
        }

        public Task<TotalsDTO> GetTotals(Guid cartId)
        {
            throw new NotImplementedException();
        }


        public async Task<bool> ProcessPayment(ProcessPaymentDTO paymentInfo)
        {
            IEnumerable<CartItem> cartItems = await _context.CartItem.Where(x => x.CartId == paymentInfo.cartID).ToListAsync();
            
            if
            (
                cartItems.IsNullOrEmpty() || 
                !ValidateCreditCard(paymentInfo.cardNumber) || 
                !ValidateSecurityCode(paymentInfo.cvv) || 
                paymentInfo.exp < DateOnly.FromDateTime(DateTime.Now)
            )
            {
                return false;
            }

            //clear cartitems 
            foreach(CartItem cartItem in cartItems)
            {
                _context.CartItem.Remove(cartItem);
            }
            
            await _context.SaveChangesAsync();
            return true;

        }

        //Uses luhn algroithm to check for valid credit card numbers 
        private static bool ValidateCreditCard(string cardNum)
        {
            if(!UInt64.TryParse(cardNum,out UInt64 x)){
                return false;
            }

            int[] nums = Array.ConvertAll(cardNum.ToCharArray(), c => (int)char.GetNumericValue(c));
            int sum = 0;

            for(int i = nums.Length - 1; i >= 0; i--)
            {

                if (i % 2 == 0)
                {
                    nums[i] = nums[i]*2;

                }

                sum += nums[i] / 10;
                sum += nums[i] % 10;
            }

            return(sum % 10 == 0); 
        }


        private static bool ValidateSecurityCode(string cvv)
        {
            Regex r = new Regex("^[0-9]{3,4}$");

            return r.IsMatch(cvv);
        }
    }
}
