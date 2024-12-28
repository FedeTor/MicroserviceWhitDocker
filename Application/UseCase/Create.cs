using Application.Common;
using Application.UseCase.IUseCase;
using Domain.Domain.Interfaces.IRepositorySqlServer;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.UseCase
{
    public class Create : ICreate
    {
        private readonly IRepository _productRepository;
        private readonly ILogger<Create> _logger;

        public Create(IRepository productRepository, ILogger<Create> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<Result<ProductResult>> CreateAsync(ProductDto request)
        {
            _logger.LogInformation("Start CreateAsync - Resquest: {0}", JsonConvert.SerializeObject(request));

            var product = Product.Create(request.Name, request.Price);

            try
            {
                await _productRepository.Add(product);
                var productResult = new ProductResult { Id = product.Id };
                return Result<ProductResult>.CreateSuccess(productResult, "Producto creado exitosamente.");
            }
            catch (Exception ex)
            {
                return Result<ProductResult>.CreateError($"Error al crear el producto: {ex.Message}");
            }
        }
    }
}
