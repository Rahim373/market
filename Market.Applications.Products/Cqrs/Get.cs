using System.Threading;
using System.Threading.Tasks;
using Mapster;
using Market.Applications.Products.Dtos;
using Market.Common;
using Market.Common.Exceptions;
using Market.Domain.Context;

namespace Market.Applications.Products.Cqrs
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
            private readonly MarketDbContext _db;

            public Handler(MarketDbContext db)
            {
                _db = db;
            }

            public async Task<ResponseViewModel<ProductDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var response = new ResponseViewModel<ProductDto>();
                var product = await _db.Products.FindAsync(new[] {request.Id}, cancellationToken);
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