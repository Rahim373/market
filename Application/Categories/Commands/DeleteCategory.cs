using Market.Application.Interfaces;

namespace Market.Application.Categories.Commands
{
    public class DeleteCategory : IRequestWrapper
    {
        public string Id { get; }

        public DeleteCategory(string id)
        {
            Id = id;
        }
    }
}