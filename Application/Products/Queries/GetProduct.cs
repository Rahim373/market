using Market.Application.Interfaces;
using Market.Application.Models;

namespace Market.Application.Products.Queries
{
    public class GetProduct : IRequestWrapper<ProductDto>
    {
        public string Id { get; }

        public GetProduct(string id)
        {
            Id = id;
        }
    }
}