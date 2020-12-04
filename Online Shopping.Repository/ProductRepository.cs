using Online_Shopping.DomainModel;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Online_Shopping.Repository
{
    public class ProductRepository
    {
        ShoppingDbContext shoppingDbContext = new ShoppingDbContext();

        public IEnumerable<Product> DisplayProduct()
        {
            IEnumerable<Product> product = shoppingDbContext.Products.ToList();
            return product;
        }

        //create product method 
        public void AddProduct(Product product)
        {
            shoppingDbContext.Products.Add(product);
            shoppingDbContext.SaveChanges();
        }

        public Product EditProduct(int id)
        {
            Product product = shoppingDbContext.Products.Find(id);
            return product;
        }

        public void EditProduct(Product product)
        {
            shoppingDbContext.Entry(product).State = EntityState.Modified;
            shoppingDbContext.SaveChanges();
        }
        public Product DeleteProduct(int id)
        {
            Product product = shoppingDbContext.Products.Find(id);
            return product;
        }

        public void DeleteConfirmed(int id)
        {
            Product product=shoppingDbContext.Products.Find(id);
            shoppingDbContext.Products.Remove(product);
            shoppingDbContext.SaveChanges();
        }
    }
}
