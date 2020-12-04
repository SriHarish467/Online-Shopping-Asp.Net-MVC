using Online_Shopping.DomainModel;


namespace Online_Shopping.ViewModel
{
    public class EditUserViewModel
    {
        public int UserId { get; set; }
        
        public string Username { get; set; }

        public string Password { get; set; }
        
        public string EmailId { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
