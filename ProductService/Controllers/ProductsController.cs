using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.MongoDbConfig;

namespace ProductService.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MongoDbService databaseService;

        public ProductsController(MongoDbService databaseService)
        {
            this.databaseService = databaseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            return Ok(await databaseService.GetAllAsync());
        }

        [HttpPost]
        public async Task<List<Product>> GetProductsByNameAync([FromBody] string query)
        {
            var searchResult = await databaseService.GetByNameAsync(query);
            return searchResult;
        }



    }
}
