using Application.Common;
using Application.UseCase.IUseCase;
using Domain.Domain.Interfaces.IRepositorySqlServer;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.UseCase
{
    public class GetAll : IGetAll
    {
        private readonly IRepository _productRepository;
        private readonly ILogger<GetAll> _logger;

        public GetAll(IRepository productRepository, ILogger<GetAll> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<Product>>> GetAllAsync()
        {
            _logger.LogInformation("Start GetAllAsync");

            var products = await _productRepository.GetAll();

            return products.Any()
                ? Result<IEnumerable<Product>>.CreateSuccess(products, "Productos encontrados.")
                : Result<IEnumerable<Product>>.CreateSuccess(products, "No se encontraron productos cargados.");
        }

    }
}
