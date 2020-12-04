using Online_Shopping.DomainModel;
using System.ComponentModel.DataAnnotations;


namespace Online_Shopping.ViewModel
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string EmailId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
