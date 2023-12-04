using CGullProject.Data;
using CGullProject.Models;
using CGullProject.Models.DTO;
using CGullProject.Services.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;

namespace CGullProject.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ShopContext _Context;

        public ReviewService(ShopContext shopContext)
        {
            _Context = shopContext;
        }

        public async Task<IEnumerable<Review>> GetReviewsById(string id)
        {
            var reviews = await _Context.Review.Where(c=> c.InventoryId == id).ToListAsync();
            return reviews;
        }

        public async Task<bool> AddReview(CreateReviewDTO review, string itemId)
        {
            Review r = new Review(review, itemId);
            try
            {
                await _Context.Review.AddAsync(r);
                await _Context.SaveChangesAsync();
                var avgRating = _Context.Review.Where(c => c.InventoryId == itemId).Average(c => c.Rating);
                var item = _Context.Inventory.First(c => c.Id == itemId);
                item.Rating = avgRating;
                await _Context.SaveChangesAsync();
            }
            catch(Exception e )
            {
                return false;
            }


            return true;
        }
    }
}
