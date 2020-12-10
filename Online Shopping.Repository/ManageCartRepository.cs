using Online_Shopping.DomainModel;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace Online_Shopping.Repository
{
    public class ManageCartRepository
    {
        ShoppingDbContext shoppingDbContext = new ShoppingDbContext();

       public void AddCartDb(Cart cart)
        {
            shoppingDbContext.Carts.Add(cart);
            shoppingDbContext.SaveChanges();
        }

        public void UpdateDb(Cart cart)
        {
            shoppingDbContext.Entry(cart).State = EntityState.Modified;
            shoppingDbContext.SaveChanges();
        }

        public List<Cart> GetCartDetails(string Username)
        {
            return shoppingDbContext.Carts.Where(x => x.Username == Username).ToList();
        }

        public void DeleteProduct(int ProductId,string Username)
        {
            Cart cart = shoppingDbContext.Carts.Single(x => x.ProductId == ProductId && x.Username == Username);
            shoppingDbContext.Carts.Remove(cart);
            shoppingDbContext.SaveChanges();
        }

        public void GetOrderDetails(Order order)
        {
            shoppingDbContext.Orders.Add(order);
            shoppingDbContext.SaveChanges(); 
        }

        public void RemoveCart(Cart cart)
        {
            shoppingDbContext.Carts.Remove(cart);
            shoppingDbContext.SaveChanges();
        }

        public void OrderDetail(OrderDetail orderDetail)
        {
            shoppingDbContext.OrderDetails.Add(orderDetail);
            shoppingDbContext.SaveChanges();
        }

        public User GetUserDetails(string Username)
        {
            return shoppingDbContext.Users.Single(x => x.Username == Username);
        }
    }
}
