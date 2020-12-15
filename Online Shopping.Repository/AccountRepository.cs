using Online_Shopping.DomainModel;
using System.Data.Entity;
using System.Linq;

namespace Online_Shopping.Repository
{
    public class AccountRepository
    {
        ShoppingDbContext shoppingDbContext = new ShoppingDbContext();

        public bool ExistingUserSignUp(User user)
        {
            return shoppingDbContext.Users.Any(x => x.Username == user.Username);
        }

        public bool ExistingEmailSignUp(User user)
        {
            return shoppingDbContext.Users.Any(x => x.EmailId == user.EmailId);
        }

        public void NewUserSignUp(User user)
        {
            user.RoleId = 2;
            shoppingDbContext.Users.Add(user);
            shoppingDbContext.SaveChanges();
        }

        public bool Login(User user)
        {
            return shoppingDbContext.Users.Any(x => x.Username == user.Username && x.Password == user.Password);
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
    }
}
