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
        private  List<Product> product;

        public  ManageCartService()
        {
             product = manageCartRepository.GetProduct();
        }
        public List<CartViewModel> AddtoCart(int ProductId, List<CartViewModel> item)
        {
            
                bool value = item.Any(x => x.ProductId == ProductId);
                if (value)
                {
                    foreach (var data in item)
                    {
                        if (data.ProductId == ProductId)
                        {
                            int quantity = data.Quantity;
                            data.Quantity = ++quantity;
                            break;
                        }
                    }

                }
                else
                {
                foreach (var data in product)
                {
                    if (data.ProductId == ProductId)
                    {
                        item.Add(new CartViewModel() { 
                            ProductId = ProductId, Quantity = 1, ProductName = data.ProductName, 
                            Price =Convert.ToDecimal(data.Price),Description=data.Description
                        });
                        break;
                    }
                }
                }

            return item;
        }

        public void UpdateCartDb(string Username,List<CartViewModel> cartViewModel)
        {
            foreach( var data in cartViewModel)
            {
                data.Username = Username;
            }
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CartViewModel, Cart>());
            var mapper = new Mapper(config);
            List<Cart> cart = mapper.Map<List<Cart>>(cartViewModel);
            foreach (var item in cart)
            {
                manageCartRepository.UpdateCartDb(item);
            }
        }

        public List<CartViewModel> GetCartDetails(string Username,List<CartViewModel> item)
        {
           List<Cart> cart = manageCartRepository.GetCartDetails(Username);
            if (cart.Count != 0)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Cart, CartViewModel>());
                var mapper = new Mapper(config);
                List<CartViewModel> cartViewModel = mapper.Map<List<CartViewModel>>(cart);
                foreach (var data in cartViewModel)
                { 
                   item.Add(data);
                }
            }
            return item;
        }
       
    }
}
