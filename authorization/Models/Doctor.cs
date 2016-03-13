using System.Web.Mvc;

namespace authorization.Models
{
    public class Doctor
    {
        [HiddenInput]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public int Phone { get; set; }
        public string Position { get; set; }
        public string Category { get; set; }
        public string Qualification { get; set; }
    }
}