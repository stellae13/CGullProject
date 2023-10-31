using CGullProject.Models;
using CGullProject.Models.DTO;

namespace CGullProject.Services.ServiceInterfaces
{
    public interface IReviewService
    {
        /// <summary>
        /// Get a list of Reviews corresponding to a Product id
        /// </summary>
        /// <param name="id">String id of Product</param>
        /// <returns>IEnumerable&lt;Review&gt;</returns>
        public Task<IEnumerable<Review>> GetReviewsById(String id);


        /// <summary>
        /// Add a Review for a specific Product
        /// </summary>
        /// <param name="review">CreateReviewDTO review</param>
        /// <param name="productID">string productID</param>
        /// <returns>bool</returns>
        public Task<bool> AddReview(CreateReviewDTO review, string productID);

        //update

        //delete
    }
}
