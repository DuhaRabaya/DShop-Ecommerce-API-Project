using DSHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSHOP.DAL.Repository
{
    public interface IReviewRepository
    {
        Task<bool> HasReview(int productId, string userId);
        Task<Review> AddReview(Review review);
        Task RemoveReview(Review review);
        Task<List<Review>> GetReviews(int productId);
        Task<Review?> GetUserReviewForProduct(string userId, int productId);

    }
}
