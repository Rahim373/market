using System.Threading;
using System.Threading.Tasks;
using Market.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace Market.Applications.Categories.Manager
{
    public interface ICategoryManager
    {
        ValueTask<string> GenerateUniqueSlug(string slug, string id = default);
        ValueTask<bool> IsParentExistsAsync(string parentCategoryId, CancellationToken cancellationToken);
    }

    public class CategoryManager : ICategoryManager
    {
        private readonly MarketDbContext _db;

        public CategoryManager(MarketDbContext db)
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
                exists = await _db.Categories.AnyAsync(c =>
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

        public async ValueTask<bool> IsParentExistsAsync(string id, CancellationToken cancellationToken)
        {
            var exists = await _db.Categories.AnyAsync(c => c.Id.ToLower() == id.ToLower(), cancellationToken: cancellationToken);
            return await new ValueTask<bool>(exists);
        }
    }
}