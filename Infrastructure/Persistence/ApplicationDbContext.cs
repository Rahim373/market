using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Market.Application.Interfaces;
using Market.Domain.Common;
using Market.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Market.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private readonly IDomainEventService _domainEventService;

        public ApplicationDbContext(ICurrentUserService currentUserService, IDateTime dateTime, IDomainEventService domainEventService)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
            _domainEventService = domainEventService;
        }
        
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Retailer> Retailers { get; set; }
        public DbSet<RetailerProduct> RetailerProducts { get; set; }
        public DbSet<RetailerDocument> RetailerDocuments { get; set; }
        public DbSet<ContactDetail> ContactDetails { get; set; }
        public DbSet<RetailerContactDetail> RetailerContactDetails { get; set; }
        public DbSet<Referral> Referrals { get; set; }
        public DbSet<PointHistory> PointHistories { get; set; }
        public DbSet<Country> Countries { get; set; }
        
        public DatabaseFacade DB => Database;
        
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<BaseEntity> entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.DateCreated = _dateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = _currentUserService.UserId;
                        entry.Entity.DateUpdated = _dateTime.Now;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            await DispatchEvents();

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Referral>()
                .HasOne(r => r.User)
                .WithOne(u => u.Referral)
                .HasForeignKey<Referral>(r => r.UserId)
                .IsRequired(true);

            base.OnModelCreating(builder);
        }
        private async Task DispatchEvents()
        {
            while (true)
            {
                var domainEventEntity = ChangeTracker
                    .Entries<IHasDomainEvent>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x)
                    .FirstOrDefault(domainEvent => !domainEvent.IsPublished);
                if (domainEventEntity == null) break;

                domainEventEntity.IsPublished = true;
                await _domainEventService.Publish(domainEventEntity);
            }
        }
    }
}