using System.Threading;
using System.Threading.Tasks;
using Market.Application.Interfaces;
using Market.Application.Models;
using Market.Application.Products.Commands;
using Market.Domain.Exceptions;

namespace Market.Applications.Products.Categories
{
    public class DeleteCategoryHandler : IHandlerWrapper<DeleteProduct>
    {
        private readonly IApplicationDbContext _db;

        public DeleteCategoryHandler(IApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ResponseViewModel> Handle(DeleteProduct command, CancellationToken cancellationToken)
        {
            var response = new ResponseViewModel();

            var product = await _db.Products.FindAsync(new[] { command.Id }, cancellationToken);

            if (product is not null)
            {
                _db.Products.Remove(product);
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