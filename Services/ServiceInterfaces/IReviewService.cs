using CGullProject.Models;
using CGullProject.Models.DTO;

namespace CGullProject.Services.ServiceInterfaces
{
    public interface IReviewService
    {

        //get by id
        public Task<IEnumerable<Review>> GetReviewsById(String id);


        //add

        public Task<bool> AddReview(CreateReviewDTO review, string productID);

        //update

        //delete
    }
}
