using CGullProject.Models;
using CGullProject.Models.DTO;

namespace CGullProject.Services.ServiceInterfaces
{
    public interface IReviewService
    {
        /// <summary>
        /// Get a list of Reviews corresponding to a <see cref="Item"/> id
        /// </summary>
        /// <param name="id">String id of <see cref="Item"/></param>
        /// <returns>IEnumerable&lt;Review&gt;</returns>
        public Task<IEnumerable<Review>> GetReviewsById(string id);


        /// <summary>
        /// Add a Review for a specific <see cref="Item"/>
        /// </summary>
        /// <param name="review">CreateReviewDTO review</param>
        /// <param name="itemId">string itemId</param>
        /// <returns>bool</returns>
        public Task<bool> AddReview(CreateReviewDTO review, string itemId);

        //update

        //delete
    }
}
