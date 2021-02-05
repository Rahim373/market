using Market.Application.Models;
using MediatR;

namespace Market.Application.Interfaces
{
    public interface IRequestWrapper : IRequest<ResponseViewModel> { }
    public interface IRequestWrapper<T> : IRequest<ResponseViewModel<T>> { }
}
