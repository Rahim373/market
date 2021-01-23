using System.Threading;
using System.Threading.Tasks;
using Market.Catalog.Domain.Context;
using Market.Common;
using Market.Common.Exceptions;

namespace Market.Catalog.Applications.Categories.Cqrs
{
    public class Delete
    {
        public class Command : IRequestWrapper
        {
            public string Id { get; }

            public Command(string id)
            {
                Id = id;
            }
        }

        public class Handler : IHandlerWrapper<Command>
        {
            private readonly CatalogDbContext _db;

            public Handler(CatalogDbContext db)
            {
                _db = db;
            }

            public async Task<ResponseViewModel> Handle(Command command, CancellationToken cancellationToken)
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
}