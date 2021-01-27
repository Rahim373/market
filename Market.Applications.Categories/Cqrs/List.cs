using System.Linq;
using Market.Common;
using Market.Common.ViewModels;
using System.Threading;
using System.Threading.Tasks;
using Market.Applications.Categories.Dtos;
using Market.Domain.Context;
using Market.Domain.Models;

namespace Market.Applications.Categories.Cqrs
{
    public class List
    {
        public class Query : GridFilterViewModel, IRequestWrapper<GridResponseViewModel<CategoryDto>>
        {
        }

        public class Handler : IHandlerWrapper<Query, GridResponseViewModel<CategoryDto>>
        {
            private readonly MarketDbContext _db;

            public Handler(MarketDbContext db)
            {
                _db = db;
            }

            public async Task<ResponseViewModel<GridResponseViewModel<CategoryDto>>> Handle(Query request,
                CancellationToken cancellationToken)
            {
                var response = new ResponseViewModel<GridResponseViewModel<CategoryDto>>();

                var categories = _db.Categories
                    .OrderBy(c => c.DateCreated);

                response.Entity = new PagedListHelper<Category>(categories).ToPagedList<CategoryDto>(request);

                return await Task.FromResult(response);
            }
        }
    }
}