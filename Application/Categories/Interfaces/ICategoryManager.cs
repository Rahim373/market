using System.Threading;
using System.Threading.Tasks;

namespace Market.Application.Categories.Interfaces
{
    public interface ICategoryManager
    {
        ValueTask<string> GenerateUniqueSlugAsync(string slug, string id, CancellationToken cancellationToken);
        ValueTask<bool> IsParentExistsAsync(string id, CancellationToken cancellationToken);
    }
}