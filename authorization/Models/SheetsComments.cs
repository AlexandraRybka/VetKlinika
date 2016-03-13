using System.Collections.Generic;

namespace authorization.Models
{
    public class SheetsComments
    {
        public IList<Models.Reviews> Reviews
        {
            get;
            set;
        }
        public string PersonalReview
        {
            get;
            set;
        }
    }
}