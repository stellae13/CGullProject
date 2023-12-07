using CGullProject.Data;
using CGullProject.Models;
using CGullProject.Models.DTO;
using CGullProject.Services.ServiceInterfaces;
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

        public async Task<CartDTO> GetCart(Guid cartId)
        {
            Dictionary<string, Item> itemTable = 
                await _context.Inventory.ToDictionaryAsync<Item, string>(entry => entry.Id);
            Cart? cart =
                _context.Cart.Where(c => c.Id == cartId).Select(c => c).Include(c => c.CartItems).First() 
                    ?? throw new KeyNotFoundException($"Cart with Id {cartId} not found");

            IEnumerable<CartDTO.AbsCartItemView> contents = cart.CartItems.Select((Func<CartItem, CartDTO.AbsCartItemView>) 
                (entry => {
                    Item prod = itemTable[entry.ItemId];
                    // We can omit this part if we feel that we don't need to show the
                    // bundled items associated with a bundle to the end user.
                    if (prod.IsBundle)
                    {
                        Bundle bundle = 
                            _context.Bundle.Where(b => b.ItemId == prod.Id).Select(b => b).Include(b => b.BundleItems).First();



                        IEnumerable<string> bundledItemIds =
                        from bundleProd in bundle.BundleItems
                            select bundleProd.ItemId;

                        return new CartDTO.BundleView(prod.Id, entry.Quantity, entry.Quantity * prod.SalePrice, bundledItemIds);
                    }
                    return new CartDTO.ProductView(prod.Id, entry.Quantity, entry.Quantity * prod.SalePrice);
                }));

            return new CartDTO
            {
                Id = cart.Id,
                Name = cart.Name,
                Contents = contents
            };
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


            Dictionary<Tuple<Guid, string>, CartItem> cartItemTable =
                await _context.CartItem.ToDictionaryAsync<CartItem, Tuple<Guid, string>>(cItm => new(cItm.CartId, cItm.ItemId));
            Tuple<Guid, string> cartItemHandle = new(cartId, itemId);
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
                    ItemId = itemId,
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

        public async Task<bool> RemoveItemFromCart(Guid cartId, string itemId) {
            var record = await _context.CartItem.FindAsync(cartId, itemId);
            if (record == null) {
                return false;
            } else {
                _context.CartItem.Remove(record);
                await _context.SaveChangesAsync();

                return true;
            }
        }
        
        public async Task<Guid> CreateNewCart(string cartName)
        {
            Cart cart = (await _context.Cart.AddAsync(new Cart {
                Name = cartName
            })).Entity;
            await _context.SaveChangesAsync();
            return cart.Id;
        }


        public async Task<TotalsDTO> GetTotals(Guid cartId)
        {
            Dictionary<string, Item> itemTable = 
                await _context.Inventory.ToDictionaryAsync<Item, string>(itm=> itm.Id);
            // The items this cart contains tupled together with the quantity of the item in the cart
            Cart? cart = 
                _context.Cart.Where(c => c.Id == cartId).Select(c => c).Include(c => c.CartItems).First()
                    ?? throw new KeyNotFoundException($"Cart with ID {cartId} not found.");
            IEnumerable<Tuple<Item, int>> cartContents =
                cart.CartItems.Select((Func<CartItem, Tuple<Item, int>>) (
                itm => {
                    return new Tuple<Item, int>(itemTable[itm.ItemId], itm.Quantity);
                }));

            TotalsDTO ret = 
                new() { BundleTotal = 0, RegularTotal = 0, TotalWithTax = 0 };

            // Return outcome of Aggregate of the item totals, split by bundle and item totals.
            ret = cartContents.Aggregate<Tuple<Item, int>, TotalsDTO>(ret,
                (Func<TotalsDTO, Tuple<Item, int>, TotalsDTO>)((curr, nxt) => {

                    decimal toAdd;
                    if (nxt.Item1.OnSale)
                    {
                        toAdd = (nxt.Item1.SalePrice * nxt.Item2);
                    }
                    else
                    {
                        toAdd = (nxt.Item1.MSRP * nxt.Item2);
                    }
                    
                    if (nxt.Item1.IsBundle)
                        ret.BundleTotal += toAdd;
                    else
                        ret.RegularTotal += toAdd;

                    return ret;
                }));

            ret.TotalWithTax = (ret.RegularTotal + ret.BundleTotal) + (1 + TotalsDTO.FederalSalesTaxRate);

            return ret;

        }


        public async Task<bool> ProcessPayment(ProcessPaymentDTO paymentInfo)
        {
            IEnumerable<CartItem> cartItems = await _context.CartItem.Where(x => x.CartId == paymentInfo.CartId).ToListAsync();
            
            if
            (
                cartItems.IsNullOrEmpty() || 
                !ValidateCreditCard(paymentInfo.CardNumber) || 
                !ValidateSecurityCode(paymentInfo.Cvv) || 
                paymentInfo.Exp < DateOnly.FromDateTime(DateTime.Now)
            )
            {
                return false;
            }
            //create order
            var totals = await this.GetTotals(paymentInfo.CartId);
            Guid OrderId = Guid.NewGuid();
            Order newOrder = new Order(paymentInfo.CartId, OrderId, DateTime.Now,totals.TotalWithTax );
            _context.Order.Add(newOrder);
            foreach(CartItem cartitem in cartItems)
            {
                OrderItem i = new OrderItem(OrderId,cartitem.ItemId,cartitem.Quantity);
                _context.OrderItem.Add(i);

            }
            //clear cartitems 
            foreach(CartItem cartItem in cartItems)
            {
                _context.CartItem.Remove(cartItem);
            }
            
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<IEnumerable<Order>> GetOrdersById(Guid CartId)
        {
            var orders = await _context.Order.Where(c => c.CartId == CartId).Include(Order => Order.Items).ThenInclude(Items => Items.Item).ToListAsync();
            return orders;
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
