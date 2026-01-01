using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using REST_API_Caching.Controllers.Models;

namespace REST_API_Caching.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IMemoryCache _cache;

        public ProductController(IMemoryCache cache)
        {
            _cache = cache;
        }

        [HttpGet("premiumProducts")]
        public IActionResult GetPremiumProducts()
        {
            const string cacheKey = "premiumProducts";
            
            // Try to get cached data
            if (!_cache.TryGetValue(cacheKey, out List<ProductInfo> premiumProductsList))
            {
                Console.WriteLine("Cache MISS - Generating data");
                
                // Data not in cache, create it
                premiumProductsList = new List<ProductInfo>();

                premiumProductsList.Add(new ProductInfo
                {
                    ProductId = 1,
                    ProductName = "M1 Chip",
                    ProductDescription = "M1 chip is advanced processew",
                    ProductCategory = "Apple Hardware",
                });

                premiumProductsList.Add(new ProductInfo
                {
                    ProductId = 2,
                    ProductName = "M2 Chip",
                    ProductDescription = "M2 chip is advanced processer",
                    ProductCategory = "Apple Hardware",
                });

                // Set cache options
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(120));
                    //.SetSlidingExpiration(TimeSpan.FromSeconds(30));


                // Save data in cache
                _cache.Set(cacheKey, premiumProductsList, cacheEntryOptions);
            }
            else
            {
                Console.WriteLine("Cache HIT - Returning cached data");
            }

            return Ok(premiumProductsList);
        }
    }
}
