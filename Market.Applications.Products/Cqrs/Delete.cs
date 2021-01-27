using System.Threading;
using System.Threading.Tasks;
using Market.Common;
using Market.Common.Exceptions;
using Market.Domain.Context;

namespace Market.Applications.Products.Cqrs
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
            private readonly MarketDbContext _db;

            public Handler(MarketDbContext db)
            {
                _db = db;
            }

            public async Task<ResponseViewModel> Handle(Command command, CancellationToken cancellationToken)
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
}