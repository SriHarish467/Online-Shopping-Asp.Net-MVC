using System.Data.Entity;

namespace Online_Shopping.DomainModel
{
    public class ShoppingDbContext : DbContext
    {
        public ShoppingDbContext()
            : base("ShoppingDbContext")
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Cart> Carts { get; set; }
    }
}
