using Aggregator.Services;
using Aggregator.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aggregator.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly RedisCartRepository repository;

        public CartController(RedisCartRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetBasket(string userId)
        {
           try
            {
                return Ok(await repository.GetCartAsync(userId));
            } catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("items/add/{cartId}")]
        public async Task<IActionResult> AddToCart(string cartId, CartItem item)
        {
            try
            {
                return Ok(await repository.AddToCartAsync(cartId, item));
            } catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost("items/remove/{cartId}")]
        public async Task<IActionResult> RemoveItemFromBasketAsync(string cartId, CartItem request)
        {
            try
            {
                return Ok(await repository.RemoveItemFromCartAsync(cartId, request));
            } catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteBasketAsync(string id)
        {
            try
            {
                return Ok(await repository.DeleteCartAsync(id));
            } catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("items/get/{id}")]
        public async Task<IActionResult> GetBasketProductsAsync(string id)
        {
            try
            {
                return Ok(await repository.GetCartProductsAsync(id));
            } catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
