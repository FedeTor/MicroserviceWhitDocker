using Application.Common;
using Domain.Entities;

namespace Application.UseCase.IUseCase
{
    public interface IGetAll
    {
        Task<Result<IEnumerable<Product>>> GetAllAsync();
    }
}
