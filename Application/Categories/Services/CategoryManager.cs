using System.Threading;
using System.Threading.Tasks;
using Market.Application.Categories.Interfaces;
using Market.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Market.Application.Categories.Services
{
    public class CategoryManager : ICategoryManager
    {
        private readonly IApplicationDbContext _db;

        public CategoryManager(IApplicationDbContext db)
        {
            _db = db;
        }

        private async ValueTask<bool> IsSlugUniqueAsync(string id, string slug, CancellationToken cancellationToken)
        {
            bool exists;
            if (string.IsNullOrEmpty(id))
            {
                exists = await _db.Categories.AnyAsync(c => string.Equals(c.Slug.ToLower(), slug.Trim().ToLower()), cancellationToken);
            }
            else
            {
                exists = await _db.Categories.AnyAsync(c =>
                    !string.Equals(c.Id.ToLower(), id.ToLower())
                    && string.Equals(c.Slug.ToLower(), slug.Trim().ToLower()), cancellationToken);
            }
            return await new ValueTask<bool>(!exists);
        }

        public async ValueTask<string> GenerateUniqueSlugAsync(string slug, string id, CancellationToken cancellationToken)
        {
            int count = 1;
            var generatedSlug = slug;
            while (!await IsSlugUniqueAsync(id, generatedSlug, cancellationToken))
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