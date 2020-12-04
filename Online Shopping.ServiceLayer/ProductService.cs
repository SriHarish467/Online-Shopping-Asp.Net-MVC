using AutoMapper;
using Online_Shopping.DomainModel;
using Online_Shopping.Repository;
using Online_Shopping.ViewModel;
using System.Collections.Generic;


namespace Online_Shopping.ServiceLayer
{
    public class ProductService
    {
        ProductRepository productRepository = new ProductRepository();
        
        public IEnumerable<Product> DisplayProduct()
        {
           return productRepository.DisplayProduct();

        }

        public void CreateProduct(ProductViewModel productViewModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ProductViewModel, Product>());
            var mapper = new Mapper(config);
            Product product = mapper.Map<ProductViewModel, Product>(productViewModel);
            productRepository.AddProduct(product);
        }

        public Product EditProduct(int id)
        {
            return productRepository.EditProduct(id);
        }

        public void EditProduct(Product product)
        {
            productRepository.EditProduct(product);
        }

        public Product DeleteProduct(int id)
        {
            return productRepository.DeleteProduct(id);
        }

        

        public void DeleteConfirmed(int id)
        {
            productRepository.DeleteConfirmed(id);
        }
    }
}
