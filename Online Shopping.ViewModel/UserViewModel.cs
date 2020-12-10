using Online_Shopping.DomainModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Online_Shopping.ViewModel
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        [Required]
        //[RegularExpression(@"^(?=.{5,20}$)(([a-z0-9])\2?(?!\2))+$", ErrorMessage = "Invalid username")]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        [DisplayName("Email id")]
        public string EmailId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
