using Basket.API.Repositories.Interfaces;
using Basket.API.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            this.redisCache = redisCache;
        }

        public async Task<ShoppingCart?> GetBasket(string userName)
        {
            string? basketInString = await redisCache.GetStringAsync(userName);
            if (string.IsNullOrEmpty(basketInString))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<ShoppingCart>(basketInString);
        }

        public async Task<ShoppingCart?> UpdateBasket(ShoppingCart basket)
        {
            await redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            return await GetBasket(basket.UserName);
        }

        public async Task DeleteBasket(string userName)
        {
            await redisCache.RemoveAsync(userName);
        }
    }
}
