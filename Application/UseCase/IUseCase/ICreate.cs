using Application.Common;
using Domain.Dtos;
using Domain.Entities;

namespace Application.UseCase.IUseCase
{
    public interface ICreate
    {
        Task<Result<ProductResult>> CreateAsync(ProductDto request);
    }
}
