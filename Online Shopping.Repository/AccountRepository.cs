using Online_Shopping.DomainModel;
using System.Data.Entity;
using System.Linq;

namespace Online_Shopping.Repository
{
    public class AccountRepository
    {
        ShoppingDbContext shoppingDbContext = new ShoppingDbContext();

        public bool ExistingUserSignUp(string Username)
        {
            return shoppingDbContext.Users.Any(x => x.Username == Username);
        }

        public bool ExistingEmailSignUp(string EmailId)
        {
            return shoppingDbContext.Users.Any(x => x.EmailId == EmailId);
        }

        public void NewUserSignUp(User user)
        {
            user.RoleId = 2;
            shoppingDbContext.Users.Add(user);
            shoppingDbContext.SaveChanges();
        }

        public bool Login(string Username,string Password)
        {
            return shoppingDbContext.Users.Any(x => x.Username == Username && x.Password == Password);
        }

        public User UpdateProfile(string name)
        {
            return shoppingDbContext.Users.Single(x => x.Username == name);
        }

        public void UpdateProfile(User user)
        {
            shoppingDbContext.Entry(user).State = EntityState.Modified;
            shoppingDbContext.SaveChanges();
        }
      
        public User NewPassword(string Email)
        {
            return shoppingDbContext.Users.Single(x => x.EmailId == Email);
        }
        public User Newpassword(string id)
        {
            return shoppingDbContext.Users.Single(x => x.ResetCode == id);
        }

        public void UpdateUser(string EmailId,string guid)
        {
            User user = shoppingDbContext.Users.Single(x => x.EmailId == EmailId);
            user.ResetCode = guid;
            shoppingDbContext.SaveChanges();
        }
    }
}
