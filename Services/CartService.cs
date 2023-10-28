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
            this._context = context;
        }

        public Task<bool> AddItmeToCart(Guid cartID, string itemID, int quantity)
        {
            throw new NotImplementedException();
        }

        public Task<Cart> GetCart(Guid cartID)
        {
            throw new NotImplementedException();
        }

        public Task<TotalsDTO> GetTotals(Guid cartID)
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
