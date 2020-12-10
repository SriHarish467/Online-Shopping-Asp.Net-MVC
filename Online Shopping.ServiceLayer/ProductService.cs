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
        
        public IEnumerable<ProductViewModel> DisplayProduct()
        {
           IEnumerable<Product> product = productRepository.DisplayProduct();
           var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductViewModel>());
           var mapper = new Mapper(config);
           IEnumerable<ProductViewModel> productViewModel = mapper.Map<IEnumerable<ProductViewModel>>(product);
            return productViewModel;
        }

        public void CreateProduct(ProductViewModel productViewModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ProductViewModel, Product>());
            var mapper = new Mapper(config);
            Product product = mapper.Map<ProductViewModel, Product>(productViewModel);
            productRepository.AddProduct(product);
        }

        public ProductViewModel EditProduct(int ProductId)
        {
            Product product = productRepository.EditProduct(ProductId);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductViewModel>());
            var mapper = new Mapper(config);
            ProductViewModel productViewModel = mapper.Map<Product, ProductViewModel>(product);
            return productViewModel;
        }

        public void EditProduct(ProductViewModel productViewModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ProductViewModel, Product>());
            var mapper = new Mapper(config);
            Product product = mapper.Map<ProductViewModel, Product>(productViewModel);
            productRepository.EditProduct(product);
        }

        public void DeleteProduct(int ProductId)
        {
             productRepository.DeleteProduct(ProductId);
        }

    }
}
