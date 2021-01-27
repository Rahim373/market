using Mapster;
using Market.Catalog.Applications.Products.Dtos;
using Market.Catalog.Domain.Context;
using Market.Common;
using Market.Common.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace Market.Catalog.Applications.Products.Cqrs
{
    public class Get
    {
        public class Query : IRequestWrapper<ProductDto>
        {
            public string Id { get; }

            public Query(string id)
            {
                Id = id;
            }
        }

        public class Handler : IHandlerWrapper<Query, ProductDto>
        {
            private readonly CatalogDbContext _db;

            public Handler(CatalogDbContext db)
            {
                _db = db;
            }

            public async Task<ResponseViewModel<ProductDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var response = new ResponseViewModel<ProductDto>();
                var product = await _db.Products.FindAsync(new[] { request.Id }, cancellationToken);
                if (product is not null)
                {
                    response.Entity = product.Adapt<ProductDto>();
                    response.Succeed();
                }
                else
                {
                    throw new NotFoundException();
                }

                return await Task.FromResult(response);
            }
        }
    }
}