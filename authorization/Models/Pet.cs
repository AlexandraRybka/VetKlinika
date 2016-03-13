using System.Web.Mvc;

namespace authorization.Models
{
    public class Pet
    {
        [HiddenInput]
        public int Id { get; set; }
        
        public string Type { get; set; }        
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Diagnosis { get; set; }
    }
}