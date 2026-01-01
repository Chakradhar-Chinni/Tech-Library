using Microsoft.AspNetCore.Mvc;
using REST_API_Caching.Controllers.Models;

namespace REST_API_Caching.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        [HttpGet("premiumProducts")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client, NoStore = false)]
        public IActionResult GetPremiumProducts()
        {
            
            Console.WriteLine(HttpContext.GetEndpoint());
            Console.WriteLine(HttpContext.Request);
            var premiumProductsList = new List<ProductInfo>();

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

            return Ok(premiumProductsList);
        }
    }
}
