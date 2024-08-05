using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_site1.Domain.Entities; //  Import this 
using Microsoft.AspNetCore.Identity;

namespace Web_site1.Domain.Entities
{
    public class User : IdentityUser
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // Store the hash
       

        public ICollection<Order> Orders { get; set; }
        public Administrator Administrator { get; set; } // For 1:1 relationship 
        public Collector Collector { get; set; }  // For 1:1 relationship 

    }
    
}