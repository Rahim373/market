using Market.Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Market.Catalog.Domain.Context
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions options) : base(options)
        {

        }

        public IDbConnection Connection => Database.GetDbConnection();

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
