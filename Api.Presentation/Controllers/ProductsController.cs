using Application.UseCase.IUseCase;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Cors;
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
            _logger.LogInformation("Start Create - Resquest: {0}", JsonConvert.SerializeObject(product));
            var result = await _create.CreateAsync(product);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            _logger.LogInformation("Start GetAll");
            var products = await _all.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("get")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            _logger.LogInformation("Start GetById - Resquest: {0}", JsonConvert.SerializeObject(id));
            var product = await _get.GetByIdAsync(id);
            return Ok(product);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(ProductDto request)
        {
            _logger.LogInformation("Start Update - Resquest: {0}", JsonConvert.SerializeObject(request));
            var product = await _update.UpdateAsync(request);
            return Ok(product);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Start Delete - Resquest: {0}", JsonConvert.SerializeObject(id));
            var product = await _delete.DeleteAsync(id);
            return Ok(product);
        }
    }
}
