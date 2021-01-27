using Market.Common;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using Market.Applications.Categories.Dtos;
using Market.Common.Exceptions;
using Market.Domain.Context;

namespace Market.Applications.Categories.Cqrs
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
            private readonly MarketDbContext _db;

            public Handler(MarketDbContext db)
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