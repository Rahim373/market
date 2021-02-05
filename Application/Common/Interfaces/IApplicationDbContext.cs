using System.Threading;
using System.Threading.Tasks;
using Market.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Market.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Retailer> Retailers { get; set; }
        public DbSet<RetailerProduct> RetailerProducts { get; set; }
        public DbSet<RetailerDocument> RetailerDocuments { get; set; }
        public DbSet<ContactDetail> ContactDetails { get; set; }
        public DbSet<RetailerContactDetail> RetailerContactDetails { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Referral> Referrals { get; set; }
        public DbSet<PointHistory> PointHistories { get; set; }
        public DbSet<Country> Countries { get; set; }
        
        public DatabaseFacade DB { get; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}