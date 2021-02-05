using System.Threading;
using System.Threading.Tasks;
using Market.Application.Interfaces;
using Market.Application.Products.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Market.Application.Products.Services
{
    public class ProductManager : IProductManager
    {
        private readonly IApplicationDbContext _db;

        public ProductManager(IApplicationDbContext db)
        {
            _db = db;
        }

        private async ValueTask<bool> isSlugUniqueAsync(string id, string slug, CancellationToken cancellationToken)
        {
            bool exists;
            if (string.IsNullOrEmpty(id))
            {
                exists = await _db.Categories.AnyAsync(c => string.Equals(c.Slug.ToLower(), slug.Trim().ToLower()), cancellationToken);
            }
            else
            {
                exists = await _db.Products.AnyAsync(c =>
                    !string.Equals(c.Id.ToLower(), id.ToLower())
                    && string.Equals(c.Slug.ToLower(), slug.Trim().ToLower()), cancellationToken);
            }

            return await new ValueTask<bool>(!exists);
        }

        public async ValueTask<string> GenerateUniqueSlug(string slug, string id, CancellationToken cancellationToken)
        {
            int count = 1;
            var generatedSlug = slug;
            while (!await isSlugUniqueAsync(id, generatedSlug, cancellationToken))
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