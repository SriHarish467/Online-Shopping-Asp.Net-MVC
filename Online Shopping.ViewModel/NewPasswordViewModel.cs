using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Online_Shopping.ViewModel
{
    public class NewPasswordViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string EmailId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("New Password")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
        public int RoleId { get; set; }
    }
}
