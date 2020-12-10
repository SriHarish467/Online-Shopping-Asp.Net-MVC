using Online_Shopping.DomainModel;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Online_Shopping.Repository
{
    public class AdminRepository
    {
        ShoppingDbContext shoppingDbContext = new ShoppingDbContext();
        public List<Order> Order()
        {
            return shoppingDbContext.Orders.ToList();
        }

        public List<OrderDetail> OrderDetail(int OrderId)
        {
            return shoppingDbContext.OrderDetails.Where(x => x.OrderId == OrderId).ToList();
        }
        public Order GetOrderId(int OrderId)
        {
            return shoppingDbContext.Orders.Single(x => x.OrderId == OrderId);
        }
        public void UpdateOrder(Order order)
        {
            shoppingDbContext.Entry(order).State = EntityState.Modified;
            shoppingDbContext.SaveChanges();
        }
        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            shoppingDbContext.Entry(orderDetail).State = EntityState.Modified;
            shoppingDbContext.SaveChanges();
        }
    }
}
