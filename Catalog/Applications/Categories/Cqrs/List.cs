using System.Linq;
using Market.Catalog.Domain.Context;
using Market.Catalog.Domain.Models;
using Market.Common;
using Market.Common.ViewModels;
using System.Threading;
using System.Threading.Tasks;
using Market.Catalog.Applications.Categories.Dtos;

namespace Market.Catalog.Applications.Categories.Cqrs
{
    public class List
    {
        public class Query : GridFilterViewModel, IRequestWrapper<GridResponseViewModel<CategoryDto>>
        {
        }

        public class Handler : IHandlerWrapper<Query, GridResponseViewModel<CategoryDto>>
        {
            private readonly CatalogDbContext _db;

            public Handler(CatalogDbContext db)
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