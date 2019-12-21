using System.Data.Entity;

namespace ProductCategory
{
    public class Context : DbContext
    {
        public Context() : base("DbConnection") { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }

}