using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Online_Shopping.ViewModel
{
    public class UserDetailViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        [DisplayName("Email Id")]
        public string EmailId { get; set; }
        public string Password { get; set; }
        [DisplayName("Phone Number")]
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }

        public int RoleId { get; set; }
    }
}
