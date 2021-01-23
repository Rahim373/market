using MediatR;

namespace Market.Common
{
    public interface IRequestWrapper : IRequest<ResponseViewModel> { }
    public interface IRequestWrapper<T> : IRequest<ResponseViewModel<T>> { }
}
