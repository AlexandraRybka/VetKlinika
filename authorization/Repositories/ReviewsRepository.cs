using authorization.Repositories.Interfaces;
using System.Collections.Generic;

namespace authorization.Repositories
{
    public class ReviewsRepository: IReviewsRepository
    {
        public IEnumerable<Models.Reviews> GetReviews()
        {
            using (DbEntities db = new DbEntities())
            {
                IList<Models.Reviews> reviews = new List<Models.Reviews>();
                foreach (var item in db.Reviews)
                {
                    reviews.Add(new Models.Reviews
                    {
                        Review = item.Review,
                        Name = item.Name                        
                    });
                }
                return reviews;
            }
        }

        public void AddReview(string userName, string review)
        {
            using (DbEntities db = new DbEntities())
            {
                db.Reviews.Add(new Reviews { Name = userName, Review = review });
                db.SaveChanges();
            }
        }
    }
}