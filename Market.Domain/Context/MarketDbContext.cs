using Market.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Domain.Context
{
    public class MarketDbContext : DbContext
    {
        public MarketDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Retailer> Retailers { get; set; }
        public DbSet<RetailerDocument> RetailerDocuments { get; set; }
    }
}
