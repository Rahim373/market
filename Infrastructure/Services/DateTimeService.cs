using Market.Application.Interfaces;

namespace Market.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public System.DateTime Now => System.DateTime.UtcNow;
    }
}