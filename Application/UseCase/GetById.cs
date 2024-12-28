using Application.Common;
using Application.UseCase.IUseCase;
using Domain.Domain.Interfaces.IRepositorySqlServer;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.UseCase
{
    public class GetById : IGetById
    {
        private readonly IRepository _productRepository;
        private readonly ILogger<GetById> _logger;

        public GetById(IRepository productRepository, ILogger<GetById> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<Result<Product>> GetByIdAsync(int id)
        {
            _logger.LogInformation("Start GetByIdAsync - Resquest: {0}", JsonConvert.SerializeObject(id));

            try
            {
                var product = await _productRepository.GetById(id);
                if(product == null)
                    return Result<Product>.CreateError($"Producto no encontrado.");

                return Result<Product>.CreateSuccess(product, "Producto encontrado.");
            }
            catch (Exception ex)
            {
                return Result<Product>.CreateError($"Error al obtener los productos: {ex.Message}");
            }
        }
    }
}
