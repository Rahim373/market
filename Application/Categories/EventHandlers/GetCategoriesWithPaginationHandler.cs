using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Market.Application.Categories.Queries;
using Market.Application.Helpers;
using Market.Application.Interfaces;
using Market.Application.Models;
using Market.Domain.Entities;

namespace Market.Application.Categories.EventHandlers
{
    public class GetCategoriesWithPaginationHandler: IHandlerWrapper<GetCategoriesWithPagination, GridResponseViewModel<CategoryDto>>
    {
        private readonly IApplicationDbContext _db;

        public GetCategoriesWithPaginationHandler(IApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ResponseViewModel<GridResponseViewModel<CategoryDto>>> Handle(GetCategoriesWithPagination request,
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