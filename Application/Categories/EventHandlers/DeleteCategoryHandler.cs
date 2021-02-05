using System.Threading;
using System.Threading.Tasks;
using Market.Application.Categories.Commands;
using Market.Application.Interfaces;
using Market.Application.Models;
using Market.Domain.Exceptions;

namespace Market.Applications.Catalog.Cqrs.Categories
{
    public class DeleteCategoryHandler : IHandlerWrapper<DeleteCategory>
    {
        private readonly IApplicationDbContext _db;

        public DeleteCategoryHandler(IApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ResponseViewModel> Handle(DeleteCategory command, CancellationToken cancellationToken)
        {
            var response = new ResponseViewModel();

            var category = await _db.Categories.FindAsync(new[] {command.Id}, cancellationToken);

            if (category is not null)
            {
                _db.Categories.Remove(category);
                await _db.SaveChangesAsync(cancellationToken);
                response.Succeed();
                response.AddMessage("Successfully deleted.", MessageType.Success);
            }
            else
            {
                throw new NotFoundException();
            }

            return await Task.FromResult(response);
        }
    }
}