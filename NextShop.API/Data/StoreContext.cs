using Microsoft.EntityFrameworkCore;

using NextShop.API.Entities;

namespace NextShop.API.Data
{
    public class StoreContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
    }
}
