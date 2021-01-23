using Mapster;
using Market.Catalog.Applications.Categories.Dtos;
using Market.Catalog.Domain.Context;
using Market.Common;
using System.Threading;
using System.Threading.Tasks;
using Market.Common.Exceptions;

namespace Market.Catalog.Applications.Categories.Cqrs
{
    public class Get
    {
        public class Query : IRequestWrapper<CategoryDto>
        {
            public string Id { get; }

            public Query(string id)
            {
                Id = id;
            }
        }

        public class Handler : IHandlerWrapper<Query, CategoryDto>
        {
            private readonly CatalogDbContext _db;

            public Handler(CatalogDbContext db)
            {
                _db = db;
            }

            public async Task<ResponseViewModel<CategoryDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var response = new ResponseViewModel<CategoryDto>();
                var category = await _db.Categories.FindAsync(new[] {request.Id}, cancellationToken);
                if (category is not null)
                {
                    response.Entity = category.Adapt<CategoryDto>();
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