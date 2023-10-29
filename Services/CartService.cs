using CGullProject.Data;
using CGullProject.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;

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
                await _context.CartItem.ToDictionaryAsync<CartItem, Tuple<Guid, String>>(cItm => new(cItm.CartId, cItm.InventoryId));
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
                    InventoryId = itemId,
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
            Dictionary<String, Inventory> inventoryById = await _context.Inventory.ToDictionaryAsync<Inventory, String>(itm => itm.Id);
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
                    else if (itm.InventoryId[0] == '1')
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
                    Inventory? productHandle = inventoryById.GetValueOrDefault(itm.InventoryId);
                    decimal total = (productHandle is null) ? 0M : productHandle.SalePrice;
                    total *= itm.Quantity;
                    return new CartItemView
                    {
                        InventoryId = itm.InventoryId, 
                        Quantity = itm.Quantity, 
                        Total = total 
                    };
                })).ToList<CartItemView>();
            foreach (CartItem bundle in bundles)
            {
                IEnumerable<Inventory> bundleContents = from BundleItem itm in _context.BundleItem
                                            where itm.BundleId.Equals(bundle.InventoryId) && inventoryById.ContainsKey(itm.InventoryId)
                                            select inventoryById[itm.InventoryId];
                Bundle? bundleHandle = await _context.Bundle.FindAsync(bundle.InventoryId);
                decimal total = 0;
                foreach (Inventory bundledItem in bundleContents)
                {
                    total += bundledItem.SalePrice;
                }
                total *= bundle.Quantity;
                // There should never be a case where the else operand gets chosen in this ternary operator expr.
                // It's just there to make the VS's language server stop warning me that bundlehandle may be null.
                total *= bundleHandle is not null ? bundleHandle.Discount : 1M;
                idQtyCost.Add(new CartItemView
                {
                    InventoryId = bundle.InventoryId,
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

        public Task<bool> ProcessPayment(Guid cartId, string cardNumber, DateOnly exp, string cardHolderName, string cvc)
        {
            throw new NotImplementedException();
        }
    }
}
