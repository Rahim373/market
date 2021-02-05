using Market.Application.Interfaces;
using Market.Application.Models;

namespace Market.Application.Categories.Queries
{
    public class GetCategory : IRequestWrapper<CategoryDto>
    {
        public string Id { get; }

        public GetCategory(string id)
        {
            Id = id;
        }
    }
}