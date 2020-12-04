using Online_Shopping.DomainModel;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace Online_Shopping.Repository
{
    public class ManageCartRepository
    {
        ShoppingDbContext shoppingDbContext = new ShoppingDbContext();

        public List<Product> GetProduct()
        {
            return shoppingDbContext.Products.ToList();
        }

        public void UpdateCartDb(Cart cart)
        {
            shoppingDbContext.Carts.Add(cart);
            shoppingDbContext.SaveChanges();
        }

        public List<Cart> GetCartDetails(string Username)
        {
            return shoppingDbContext.Carts.Where(x => x.Username == Username).ToList();
        }
    }
}
