using Application.Common;
using Application.UseCase.IUseCase;
using Domain.Domain.Interfaces.IRepositorySqlServer;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.UseCase
{
    public class Delete : IDelete
    {
        private readonly IRepository _productRepository;
        private readonly ILogger<Delete> _logger;

        public Delete(IRepository productRepository, ILogger<Delete> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<Result<ProductResult>> DeleteAsync(int id)
        {
            _logger.LogInformation("Start DeleteAsync - Request: {0}", JsonConvert.SerializeObject(id));

            try
            {
                var product = await _productRepository.GetById(id);

                if (product == null)
                    return Result<ProductResult>.CreateError("Producto no encontrado.");

                await _productRepository.Delete(product);

                return Result<ProductResult>.CreateSuccess(new ProductResult { Id = id }, "Producto eliminado exitosamente.");
            }
            catch (Exception ex)
            {
                return Result<ProductResult>.CreateError($"Error al eliminar el producto: {ex.Message}");
            }
        }
    }
}
