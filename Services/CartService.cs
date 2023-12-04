using CGullProject.Data;
using CGullProject.Models;
using CGullProject.Models.DTO;
using CGullProject.Services.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;
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
            Dictionary<String, Item> itemTable = 
                await _context.Inventory.ToDictionaryAsync<Item, String>(entry => entry.Id);
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



                        IEnumerable<String> bundledItemIds =
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

        
        
        
        public async Task<Guid> CreateNewCart(String cartName)
        {
            Cart cart = (await _context.Cart.AddAsync(new Cart {
                Name = cartName
            })).Entity;
            await _context.SaveChangesAsync();
            return cart.Id;
        }


        public async Task<TotalsDTO> GetTotals(Guid cartId)
        {
            Dictionary<String, Item> itemTable = 
                await _context.Inventory.ToDictionaryAsync<Item, String>(itm=> itm.Id);
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
            //create order
            var totals = await this.GetTotals(paymentInfo.cartID);
            Guid OrderId = Guid.NewGuid();
            Order newOrder = new Order(paymentInfo.cartID, OrderId, DateTime.Now,totals.TotalWithTax );
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
