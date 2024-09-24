using Basket.API.Models;
using Basket.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket(string userName)
        {
            try
            {
                var basket = await basketRepository.GetBasket(userName);
                if (basket == null)
                {
                    return NotFound("User not found.");
                }
                return Ok(basket);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBasket([FromBody] ShoppingCart shoppingCart)
        {
            try
            {
                var basket = await basketRepository.UpdateBasket(shoppingCart);
                if (basket == null)
                {
                    return NotFound("User not found.");
                }
                return Ok(basket);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket (string userName)
        {
            try
            {
                var basket = await basketRepository.GetBasket(userName);
                if (basket == null)
                {
                    return NotFound("User not found.");
                }
                await basketRepository.DeleteBasket(userName);
                return Ok("Basket has been deleted.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
