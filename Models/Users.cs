using System.ComponentModel.DataAnnotations;

namespace LostChildrenGP.Models
{
    public class Users
    {
        [Required(ErrorMessage ="Enter Your First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Enter Your Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Enter Your Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter Your Password")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }

    }
}
