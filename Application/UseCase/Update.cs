using Application.Common;
using Application.UseCase.IUseCase;
using Domain.Domain.Interfaces.IRepositorySqlServer;
using Domain.Dtos;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.UseCase
{
    public class Update : IUpdate
    {
        private readonly IRepository _productRepository;
        private readonly ILogger<Update> _logger;

        public Update(IRepository productRepository, ILogger<Update> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<Result<string>> UpdateAsync(ProductDto request)
        {
            _logger.LogInformation("Start UpdateAsync - Resquest: {0}", JsonConvert.SerializeObject(request));

            try
            {
                var product = await _productRepository.GetById(request.Id);

                if (product == null)
                    return Result<string>.CreateError("Producto no encontrado.");

                product.Update(request.Name, request.Price);

                await _productRepository.Update(product);

                return Result<string>.CreateSuccess("Producto actualizado exitosamente.");
            }
            catch (Exception ex)
            {
                return Result<string>.CreateError($"Error al actualizar el producto: {ex.Message}");
            }
        }
    }
}
