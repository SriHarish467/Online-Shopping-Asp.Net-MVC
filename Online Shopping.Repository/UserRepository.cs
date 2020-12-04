using Online_Shopping.DomainModel;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace Online_Shopping.Repository
{
   
    public class UserRepository
    {
        ShoppingDbContext shoppingDbContext = new ShoppingDbContext();

        public IEnumerable<User> DisplayUser()
        {
           IEnumerable<User> user = shoppingDbContext.Users.ToList();
            return user;
        }

        public IEnumerable<Role> DisplayRole()
        {
            IEnumerable<Role> role = shoppingDbContext.Roles.ToList();
            return role;
        }

        public User EditUser(int id)
        {
            User user = shoppingDbContext.Users.Find(id);
            return user;
        }

        public void EditUser(User user)
        {
            shoppingDbContext.Entry(user).State = EntityState.Modified;
            shoppingDbContext.SaveChanges();
        }

        public User DeleteUser(int id)
        {
            User user = shoppingDbContext.Users.Find(id);
            return user;
        }

        public void DeleteConfirmed(int id)
        {
            User user = shoppingDbContext.Users.Find(id);
            shoppingDbContext.Users.Remove(user);
            shoppingDbContext.SaveChanges();
        }
    }
}
