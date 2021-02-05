using System.Threading;
using System.Threading.Tasks;

namespace Market.Application.Products.Interfaces
{
    public interface IProductManager
    {
        ValueTask<string> GenerateUniqueSlug(string slug, string id, CancellationToken cancellationToken);
        ValueTask<bool> IsCategoryExistsAsync(string categoryId, CancellationToken cancellationToken);
    }
}