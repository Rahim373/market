using Market.Application.Interfaces;

namespace Market.Application.Products.Commands
{
    public class DeleteProduct : IRequestWrapper
    {
        public string Id { get; }

        public DeleteProduct(string id)
        {
            Id = id;
        }
    }
}