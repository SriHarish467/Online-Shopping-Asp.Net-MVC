using System.ComponentModel.DataAnnotations;



namespace Online_Shopping.ViewModel
{
    public class UpdateUserProfileViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string EmailId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int RoleId { get; set; }
    }
}
