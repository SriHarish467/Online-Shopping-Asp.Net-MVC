using AutoMapper;
using Online_Shopping.DomainModel;
using Online_Shopping.Repository;
using Online_Shopping.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Online_Shopping.ServiceLayer
{
    public class ManageCartService
    {
        ManageCartRepository manageCartRepository = new ManageCartRepository();

        public List<CartViewModel> AddCart(int ProductId, List<CartViewModel> item,decimal Price,string ProductName,string Description)
        {
            bool value = item.Any(x => x.ProductId == ProductId);
            if(value)
            {
                foreach(var data in item)
                {
                    if(data.ProductId == ProductId)
                    {
                        data.Quantity = ++data.Quantity;
                    }
                }
            }
            else
            {
                item.Add(new CartViewModel()
                {
                    Quantity = 1, ProductName = ProductName,Description = Description,Price=Price,ProductId=ProductId
                }) ;
            }
            return item;
        }

        public int UpdateCart(string Username,List<CartViewModel> cartViewModel)
        {
            List<Cart> cartDb = manageCartRepository.GetCartDetails(Username);
            User user = manageCartRepository.GetUserDetails(Username);
            if(cartDb.Count == 0)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<CartViewModel, Cart>());
                var mapper = new Mapper(config);
                List<Cart> cart = mapper.Map<List<Cart>>(cartViewModel);
                foreach (var data in cart)
                {
                    data.Username = Username;
                    data.UserId = user.UserId;
                    manageCartRepository.AddCartDb(data);
                }
                return cart.Count;
            }
            else
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<CartViewModel, Cart>());
                var mapper = new Mapper(config);
                List<Cart> cart = mapper.Map<List<Cart>>(cartViewModel);
                foreach (var data in cart)
                {
                    data.Username = Username; data.UserId = user.UserId;
                    Cart value = cartDb.Single(x=>x.ProductId==data.ProductId);
                    if (value != null)
                    {
                        value.Quantity = ++data.Quantity;
                        manageCartRepository.UpdateDb(value);
                    }
                    else
                    {
                        manageCartRepository.AddCartDb(data);
                    }

                }
                return cartDb.Count;
            }
            
        }

        public List<CartViewModel> DisplayCart(string Username)
        {
            List<Cart> cart = manageCartRepository.GetCartDetails(Username);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Cart, CartViewModel>());
            var mapper = new Mapper(config);
            List<CartViewModel> cartViewModel = mapper.Map<List<CartViewModel>>(cart);
            return cartViewModel;
        }

        public int CartDb(int ProductId,string Username)
        {
            List<Cart> cart = manageCartRepository.GetCartDetails(Username);
            User user = manageCartRepository.GetUserDetails(Username);
            bool value = cart.Any(x => x.ProductId == ProductId);
            if (value)
            {
                foreach (var data in cart)
                {
                    if (data.ProductId == ProductId)
                    {
                        data.Quantity = ++data.Quantity;
                        manageCartRepository.UpdateDb(data);
                        break;
                    }
                }
            }
            else
            {
                Cart Cart = new Cart();
                Cart.ProductId = ProductId;
                Cart.Username = Username;
                Cart.Quantity = 1;
                Cart.UserId = user.UserId;
                manageCartRepository.AddCartDb(Cart);
            }

            List<Cart> carts = manageCartRepository.GetCartDetails(Username);
            return carts.Count;
        }

        public int DeleteCartProduct(List<CartViewModel> cartViewModel,int ProductId)
        {
            CartViewModel cartviewModel = cartViewModel.Single(x => x.ProductId == ProductId);
            cartViewModel.Remove(cartviewModel);
            return cartViewModel.Count;
        }

        public int DeleteCartProduct(int ProductId,string Username)
        {
            manageCartRepository.DeleteProduct(ProductId, Username);
            List<Cart> cart = manageCartRepository.GetCartDetails(Username);
            return cart.Count;
        }

        public void DecreaseQuantity(List<CartViewModel> cartViewModel,int ProductId)
        {
            foreach(var item in cartViewModel)
            {
                if(item.ProductId == ProductId )
                {
                    if (item.Quantity > 1)
                    {
                        item.Quantity = --item.Quantity;
                        break;
                    }
                }
            }
         
        }

        public void DecreaseQuantity(int ProductId,string Username)
        {
            List<Cart> cart = manageCartRepository.GetCartDetails(Username);

            foreach (var item in cart)
            {
                if (item.ProductId == ProductId)
                {
                    item.Quantity = --item.Quantity;
                    manageCartRepository.UpdateDb(item);
                    break;
                }
            }
        }

        public void IncreaseQuantity(List<CartViewModel> cartViewModel,int ProductId)
        {
            foreach(var item in cartViewModel)
            {
                if(item.ProductId == ProductId)
                {
                    item.Quantity = ++item.Quantity;
                    break;
                }
            }
        }

        public void IncreaseQuantity(int ProductId,string Username)
        {
            List<Cart> cart = manageCartRepository.GetCartDetails(Username);
            
            foreach(var item in cart)
            {
                if(item.ProductId == ProductId)
                {
                    item.Quantity = ++item.Quantity;
                    manageCartRepository.UpdateDb(item);
                    break;
                }
            }
        }

        public void Checkout(string Username)
        {
            Order order = new Order();
            order.OrderDateTime = DateTime.Now;
            manageCartRepository.GetOrderDetails(order);
            int orderId = order.OrderId;
            List<Cart> cart = manageCartRepository.GetCartDetails(Username);
           // User user = manageCartRepository.GetUserDetails(Username);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Cart, OrderDetail>());
            var mapper = new Mapper(config);
            List<OrderDetail> orderDetail = mapper.Map<List<OrderDetail>>(cart);

            foreach (var item in orderDetail)
            {
                item.OrderId = orderId; item.Status = "Order";
                manageCartRepository.OrderDetail(item);
            }
        }

        public void RemoveCart(string Username)
        {
            List<Cart> cart = manageCartRepository.GetCartDetails(Username);
            foreach(var item in cart)
            {
                manageCartRepository.RemoveCart(item);
            }
        }

        public List<OrderDetailViewModel> YourOrder(string Username)
        {
           List<OrderDetail> orderDetail = manageCartRepository.YourOrder(Username);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<OrderDetail, OrderDetailViewModel>());
            var mapper = new Mapper(config);
            List<OrderDetailViewModel> orderDetailViewModel = mapper.Map<List<OrderDetailViewModel>>(orderDetail);
            return orderDetailViewModel;
        }

        public void CancelOrder(int OrderDetailId)
        {
            OrderDetail orderDetail = manageCartRepository.CancelOrder(OrderDetailId);
            orderDetail.Status = "Cancel";
            manageCartRepository.UpdateOrderDetail(orderDetail);
        }

        public List<OrderDetailViewModel> OrderHistory(string Username)
        {
            List<OrderDetail> orderDetail = manageCartRepository.YourOrder(Username);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<OrderDetail, OrderDetailViewModel>());
            var mapper = new Mapper(config);
            List<OrderDetailViewModel> orderDetailViewModel = mapper.Map<List<OrderDetailViewModel>>(orderDetail);
            return orderDetailViewModel;
        }

        public List<CartViewModel> Buy(int ProductId, string ProductName, decimal Price, string Description,string Username)
        {
            List<CartViewModel> cartViewModel = new List<CartViewModel>();
            CartViewModel cartviewModel = new CartViewModel();
                cartviewModel.ProductId = ProductId;
                cartviewModel.ProductName = ProductName;
                cartviewModel.Price = Price;
                cartviewModel.Description = Description;
                cartviewModel.Quantity = 1;
            cartViewModel.Add(cartviewModel);
            return cartViewModel;
        }
        public List<CartViewModel> DisplayBuy(string Username,List<CartViewModel> cartViewModel)
        {
            User user = manageCartRepository.GetUserDetails(Username);
            foreach (var item in cartViewModel)
            {
                item.Address = user.Address;
                item.PhoneNumber = user.PhoneNumber;
                item.Username = user.Username;
                item.UserId = user.UserId;
            }
            return cartViewModel;
        }

        public List<CartViewModel> BuyIncreaseQuantity(List<CartViewModel> cartViewModel)
        {
            foreach (var item in cartViewModel)
            {
                item.Quantity = ++item.Quantity;
            }
            return cartViewModel;
        }

        public List<CartViewModel> BuyDecreaseQuantity(List<CartViewModel> cartViewModel)
        {
            foreach (var item in cartViewModel)
            {
                item.Quantity = --item.Quantity;
            }
            return cartViewModel;
        }

        public UserDetailViewModel ShippingDetail(string Username)
        {
            User user = manageCartRepository.GetUserDetails(Username);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDetailViewModel>());
            var mapper = new Mapper(config);
            UserDetailViewModel userDetailViewModel = mapper.Map<UserDetailViewModel>(user);
            return userDetailViewModel;
        }

        public void ShippingDetail(UserDetailViewModel userDetailViewModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserDetailViewModel, User>());
            var mapper = new Mapper(config);
            User user = mapper.Map<User>(userDetailViewModel);
            manageCartRepository.ShippingDetail(user);
        }

        public void PlaceOrder(List<CartViewModel> cartViewModel)
        {
            Order order = new Order();
            order.OrderDateTime = DateTime.Now;
            manageCartRepository.GetOrderDetails(order);
            OrderDetail orderDetail = new OrderDetail();
            orderDetail.OrderId = order.OrderId;
            foreach (var item in cartViewModel)
            {
                orderDetail.UserId = item.UserId;
                orderDetail.Quantity = item.Quantity;
                orderDetail.Status = "Order";
                orderDetail.ProductId = item.ProductId;
            }
            manageCartRepository.OrderDetail(orderDetail);
        }

        public bool CheckDetail(string Username)
        {
           User user = manageCartRepository.GetUserDetails(Username);
            if(user.Address == null || user.PhoneNumber == null)
            {
                return true;
            }
            return false;
        }
    }
}