using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace iStolo1.Models
{
    public class User : IdentityUser<string>
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AccountPassword { get; set; }
        public string PaymentMethod { get; set; }
        public string Adress { get; set; }
        public string Username { get; internal set; }
        public string Role { get; set; }
    }
}
