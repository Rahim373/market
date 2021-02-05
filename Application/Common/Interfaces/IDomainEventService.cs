using System.Threading.Tasks;
using Market.Domain.Common;

namespace Market.Application.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}