using System.Collections.Generic;

namespace authorization.Repositories.Interfaces
{
    public interface IReviewsRepository
    {
        IEnumerable<Models.Reviews> GetReviews();
        void AddReview(string userName, string review);
    }
}
