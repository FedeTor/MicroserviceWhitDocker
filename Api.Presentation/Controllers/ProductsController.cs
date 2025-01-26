using Application.UseCase.IUseCase;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly ICreate _create;
        private readonly IGetAll _all;
        private readonly IGetById _get;
        private readonly IUpdate _update;
        private readonly IDelete _delete;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ICreate create, IGetAll all, IGetById get, IUpdate update, IDelete delete, ILogger<ProductsController> logger)
        {
            _create = create;
            _all = all;
            _get = get;
            _update = update;
            _delete = delete;
            _logger = logger;
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create(ProductDto product)
        {
            _logger.LogInformation("Start Create - Request: {0}", JsonConvert.SerializeObject(product));

            try
            {
                var result = await _create.CreateAsync(product);

                if (result.Success)
                {
                    return Ok(new
                    {
                        success = true,
                        data = result.Data,
                        message = result.Message
                    });
                }
                else
                {
                    _logger.LogWarning("Error al crear el producto: {0}", result.Message);
                    return BadRequest(new
                    {
                        success = false,
                        message = result.Message
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al crear el producto.");
                return StatusCode(500, new
                {
                    success = false,
                    message = "Ocurrió un error inesperado al crear el producto."
                });
            }
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            _logger.LogInformation("Start GetAll");

            try
            {
                var result = await _all.GetAllAsync();

                if (result.Success)
                {
                    return Ok(new
                    {
                        success = true,
                        data = result.Data,
                        message = result.Message
                    });
                }
                else
                {
                    _logger.LogWarning("Error al obtener productos: {0}", result.Message);
                    return BadRequest(new
                    {
                        success = false,
                        message = result.Message
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener los productos.");
                return StatusCode(500, new
                {
                    success = false,
                    message = "Ocurrió un error inesperado en el servidor."
                });
            }
        }

        [HttpGet("get")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            _logger.LogInformation("Start GetById - Request: {0}", id);

            try
            {
                var result = await _get.GetByIdAsync(id);

                if (result.Success)
                {
                    return Ok(new
                    {
                        success = true,
                        data = result.Data,
                        message = result.Message
                    });
                }
                else
                {
                    _logger.LogWarning("Producto no encontrado: {0}", result.Message);
                    return NotFound(new
                    {
                        success = false,
                        message = result.Message
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al buscar un producto.");
                return StatusCode(500, new
                {
                    success = false,
                    message = "Ocurrió un error inesperado al buscar el producto."
                });
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(ProductDto request)
        {
            _logger.LogInformation("Start Update - Request: {0}", JsonConvert.SerializeObject(request));

            try
            {
                var result = await _update.UpdateAsync(request);

                if (result.Success)
                {
                    return Ok(new
                    {
                        success = true,
                        data = result.Data,
                        message = result.Message
                    });
                }
                else
                {
                    _logger.LogWarning("Error al actualizar el producto: {0}", result.Message);
                    return BadRequest(new
                    {
                        success = false,
                        message = result.Message
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al actualizar el producto.");
                return StatusCode(500, new
                {
                    success = false,
                    message = "Ocurrió un error inesperado al actualizar el producto."
                });
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Start Delete - Request: {0}", id);

            try
            {
                var result = await _delete.DeleteAsync(id);

                if (result.Success)
                {
                    return Ok(new
                    {
                        success = true,
                        message = result.Message
                    });
                }
                else
                {
                    _logger.LogWarning("Error al eliminar un producto: {0}", result.Message);
                    return NotFound(new
                    {
                        success = false,
                        message = result.Message
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al eliminar un producto.");
                return StatusCode(500, new
                {
                    success = false,
                    message = "Ocurrió un error inesperado al eliminar el producto."
                });
            }
        }
    }
}