using UsersMicroservice.Core.Common;

namespace UsersMicroservice.Core.Application
{
    public interface IApplicationService<T, R>
    {
        Task<Result<R>> Execute(T data);
    }
}
