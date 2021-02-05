using Market.Application.Models;
using MediatR;

namespace Market.Application.Interfaces
{
    public interface IHandlerWrapper<TRequest> : IRequestHandler<TRequest, ResponseViewModel> where TRequest : IRequestWrapper { }
    public interface IHandlerWrapper<TRequest, TResponse> : IRequestHandler<TRequest, ResponseViewModel<TResponse>> where TRequest : IRequestWrapper<TResponse> { }
}
