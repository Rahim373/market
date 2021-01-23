using System;
using System.Threading.Tasks;
using Market.Catalog.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace Market.Catalog.Applications.Categories.Manager
{
    public interface ICategoryManager
    {
        ValueTask<string> GenerateUniqueSlug(string slug, string id = default);
    }

    public class CategoryManager : ICategoryManager
    {
        private readonly CatalogDbContext _db;

        public CategoryManager(CatalogDbContext db)
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
            while (! await isSlugUniqueAsync(id, generatedSlug) )
            {
                generatedSlug = $"{slug}-{count++}";
            }

            return await new ValueTask<string>(generatedSlug);
        }
    }
}