using System.Threading;
using System.Threading.Tasks;
using Market.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace Market.Applications.Products.Manager
{
    public interface IProductManager
    {
        ValueTask<string> GenerateUniqueSlug(string slug, string id = default);
        ValueTask<bool> IsCategoryExistsAsync(string categoryId, CancellationToken cancellationToken);
    }

    public class ProductManager : IProductManager
    {
        private readonly MarketDbContext _db;

        public ProductManager(MarketDbContext db)
        {
            _db = db;
        }

        private async ValueTask<bool> isSlugUniqueAsync(string id, string slug)
        {
            bool exists;
            if (string.IsNullOrEmpty(id))
            {
                exists = await _db.Categories.AnyAsync(c => string.Equals(c.Slug.ToLower(), slug.Trim().ToLower()));
            }
            else
            {
                exists = await _db.Products.AnyAsync(c =>
                    !string.Equals(c.Id.ToLower(), id.ToLower())
                    && string.Equals(c.Slug.ToLower(), slug.Trim().ToLower()));
            }

            return await new ValueTask<bool>(!exists);
        }

        public async ValueTask<string> GenerateUniqueSlug(string slug, string id = default)
        {
            int count = 1;
            var generatedSlug = slug;
            while (!await isSlugUniqueAsync(id, generatedSlug))
            {
                generatedSlug = $"{slug}-{count++}";
            }

            return await new ValueTask<string>(generatedSlug);
        }

        public async ValueTask<bool> IsCategoryExistsAsync(string categoryId, CancellationToken cancellationToken)
        {
            var exists = await _db.Categories.AnyAsync(c => c.Id.ToLower() == categoryId.ToLower(),
                cancellationToken: cancellationToken);
            return await new ValueTask<bool>(exists);
        }
    }
}