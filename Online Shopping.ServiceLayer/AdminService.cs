using AutoMapper;
using Online_Shopping.DomainModel;
using Online_Shopping.Repository;
using Online_Shopping.ViewModel;
using System.Collections.Generic;

namespace Online_Shopping.ServiceLayer
{
    public class AdminService
    {
        AdminRepository adminRepository = new AdminRepository();

        public List<OrderViewModel> GetOrder()
        {
            List<Order> order = adminRepository.Order();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderViewModel>());
            var mapper = new Mapper(config);
            List<OrderViewModel> orderViewModel = mapper.Map<List<OrderViewModel>>(order);
            return orderViewModel;
        }

        public List<OrderDetailViewModel> OrderDetail(int OrderId)
        {
            List<OrderDetail> orderDetail = adminRepository.OrderDetail(OrderId);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<OrderDetail, OrderDetailViewModel>());
            var mapper = new Mapper(config);
            List<OrderDetailViewModel> orderDetailViewModel = mapper.Map<List<OrderDetailViewModel>>(orderDetail);
            return orderDetailViewModel;
        }

        public void CompleteOrder(int OrderId)
        {
            Order order = adminRepository.GetOrderId(OrderId);
            order.Status = "Complete";
            adminRepository.UpdateOrder(order);
            List<OrderDetail> orderDetail = adminRepository.OrderDetail(OrderId);
            foreach(var item in orderDetail)
            {
                if(item.Status == "Cancel")
                {
                    adminRepository.RemoveProduct(item);
                }
                else
                {
                    item.Status = "Complete";
                    adminRepository.UpdateOrderDetail(item);
                }
            }
        }

        public string GetUsername(int OrderId)
        {
            OrderDetail orderDetail = adminRepository.GetOrderDetail(OrderId);
            User user = adminRepository.GetUserDetail(orderDetail.UserId);
            return user.EmailId;
        }
    }
}
