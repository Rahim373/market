using MediatR;

namespace Market.Common
{
    public interface IHandlerWrapper<TRequest> : IRequestHandler<TRequest, ResponseViewModel> where TRequest : IRequestWrapper { }
    public interface IHandlerWrapper<TRequest, TResponse> : IRequestHandler<TRequest, ResponseViewModel<TResponse>> where TRequest : IRequestWrapper<TResponse> { }
}
