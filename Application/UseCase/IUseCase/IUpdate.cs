using Application.Common;
using Domain.Dtos;  

namespace Application.UseCase.IUseCase
{
    public interface IUpdate
    {
        Task<Result<string>> UpdateAsync(ProductDto request);
    }
}
