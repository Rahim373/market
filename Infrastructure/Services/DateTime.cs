using Market.Application.Interfaces;

namespace Market.Infrastructure.Services
{
    public class DateTime : IDateTime
    {
        public System.DateTime Now => System.DateTime.UtcNow;
    }
}