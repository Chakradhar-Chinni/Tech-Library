using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using REST_API_Caching.Controllers.Models;

namespace REST_API_Caching.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IMemoryCache cache, ILogger<ProductController> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        /// <summary>
        /// Demo: Size=1, Absolute=5min, Sliding=30sec, Priority=Normal
        /// </summary>
        [HttpGet("premiumProducts")]
        public IActionResult GetPremiumProducts()
        {
            const string cacheKey = "premiumProducts";
            
            // Try to get cached data
            if (!_cache.TryGetValue(cacheKey, out List<ProductInfo> premiumProductsList))
            {
                _logger.LogInformation("Cache MISS for '{CacheKey}' - Generating data", cacheKey);
                
                // Data not in cache, create it
                premiumProductsList = new List<ProductInfo>
                {
                    new ProductInfo
                    {
                        ProductId = 1,
                        ProductName = "M1 Chip",
                        ProductDescription = "M1 chip is advanced processor",
                        ProductCategory = "Apple Hardware",
                    },
                    new ProductInfo
                    {
                        ProductId = 2,
                        ProductName = "M2 Chip",
                        ProductDescription = "M2 chip is advanced processor",
                        ProductCategory = "Apple Hardware",
                    }
                };

                // Set comprehensive cache options
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Size: Required when SizeLimit is configured in Program.cs
                    .SetSize(1) // This entry costs 1 unit toward the 1024 total units
                    
                    // Absolute Expiration: Item removed after this time, regardless of access
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                    
                    // Sliding Expiration: Item removed if not accessed within this time
                    // Resets on each access (good for frequently accessed data)
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                    
                    // Priority: Determines eviction order during compaction
                    .SetPriority(CacheItemPriority.Normal)
                    
                    // Eviction Callback: Called when item is removed
                    .RegisterPostEvictionCallback((key, value, reason, state) =>
                    {
                        _logger.LogWarning(
                            " Cache evicted - Key: '{Key}', Reason: {Reason}, Time: {Time}",
                            key, reason, DateTime.UtcNow.ToString("HH:mm:ss"));
                    });

                // Save data in cache
                _cache.Set(cacheKey, premiumProductsList, cacheEntryOptions);
                _logger.LogInformation("Data cached with key '{CacheKey}'", cacheKey);
            }
            else
            {
                _logger.LogInformation("Cache HIT for '{CacheKey}' - Returning cached data", cacheKey);
            }

            return Ok(new
            {
                source = _cache.TryGetValue(cacheKey, out _) ? "cache" : "fresh",
                products = premiumProductsList,
                timestamp = DateTime.UtcNow
            });
        }

        /// <summary>
        /// Demo: Size=2, Absolute=2min, Sliding=20sec, Priority=Low (evicted first)
        /// </summary>
        [HttpGet("standardProducts")]
        public IActionResult GetStandardProducts()
        {
            const string cacheKey = "standardProducts";
            
            if (!_cache.TryGetValue(cacheKey, out List<ProductInfo> standardProductsList))
            {
                _logger.LogInformation("Cache MISS for '{CacheKey}'", cacheKey);
                
                standardProductsList = new List<ProductInfo>
                {
                    new ProductInfo
                    {
                        ProductId = 3,
                        ProductName = "iPhone 15",
                        ProductDescription = "Latest iPhone model",
                        ProductCategory = "Apple Mobile",
                    },
                    new ProductInfo
                    {
                        ProductId = 4,
                        ProductName = "iPad Pro",
                        ProductDescription = "Professional tablet",
                        ProductCategory = "Apple Tablet",
                    }
                };

                // LOW Priority: First to be evicted during compaction
                // Larger Size: Costs 2 units (simulating larger data)
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSize(2) // Costs 2 units (larger entry)
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(2))
                    .SetSlidingExpiration(TimeSpan.FromSeconds(20))
                    .SetPriority(CacheItemPriority.Low) // ⬇️ First to be evicted
                    .RegisterPostEvictionCallback((key, value, reason, state) =>
                    {
                        _logger.LogWarning(
                            "LOW-priority item evicted - Key: '{Key}', Reason: {Reason}",
                            key, reason);
                    });

                _cache.Set(cacheKey, standardProductsList, cacheEntryOptions);
                _logger.LogInformation("💾 Low-priority data cached: '{CacheKey}'", cacheKey);
            }
            else
            {
                _logger.LogInformation("✅ Cache HIT for '{CacheKey}'", cacheKey);
            }

            return Ok(new
            {
                source = "cache",
                priority = "Low",
                products = standardProductsList,
                timestamp = DateTime.UtcNow
            });
        }

        /// <summary>
        /// Demo: Size=1, Absolute=10min, Sliding=1min, Priority=High (evicted last)
        /// </summary>
        [HttpGet("criticalProducts")]
        public IActionResult GetCriticalProducts()
        {
            const string cacheKey = "criticalProducts";
            
            if (!_cache.TryGetValue(cacheKey, out List<ProductInfo> criticalProductsList))
            {
                _logger.LogInformation("⚠️ Cache MISS for '{CacheKey}'", cacheKey);
                
                criticalProductsList = new List<ProductInfo>
                {
                    new ProductInfo
                    {
                        ProductId = 5,
                        ProductName = "MacBook Pro M3",
                        ProductDescription = "Professional laptop with M3 chip",
                        ProductCategory = "Apple Laptop",
                    }
                };

                // HIGH Priority: Last to be evicted during compaction
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSize(1)
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1))
                    .SetPriority(CacheItemPriority.High) // ⬆️ Last to be evicted
                    .RegisterPostEvictionCallback((key, value, reason, state) =>
                    {
                        _logger.LogError(
                            "HIGH-priority item evicted! Key: '{Key}', Reason: {Reason}",
                            key, reason);
                    });

                _cache.Set(cacheKey, criticalProductsList, cacheEntryOptions);
                _logger.LogInformation("High-priority data cached: '{CacheKey}'", cacheKey);
            }
            else
            {
                _logger.LogInformation("Cache HIT for '{CacheKey}'", cacheKey);
            }

            return Ok(new
            {
                source = "cache",
                priority = "High",
                products = criticalProductsList,
                timestamp = DateTime.UtcNow
            });
        }

        /// <summary>
        /// Demo: NeverRemove priority - use sparingly!
        /// </summary>
        [HttpGet("vipProducts")]
        public IActionResult GetVipProducts()
        {
            const string cacheKey = "vipProducts";
            
            if (!_cache.TryGetValue(cacheKey, out List<ProductInfo> vipProductsList))
            {
                _logger.LogInformation("Cache MISS for '{CacheKey}'", cacheKey);
                
                vipProductsList = new List<ProductInfo>
                {
                    new ProductInfo
                    {
                        ProductId = 6,
                        ProductName = "Apple Vision Pro",
                        ProductDescription = "Revolutionary spatial computer",
                        ProductCategory = "Apple AR/VR",
                    }
                };

                // NEVER REMOVE: Won't be evicted during compaction (use very sparingly!)
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSize(1)
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                    .SetPriority(CacheItemPriority.NeverRemove) // ⚠️ Won't be evicted!
                    .RegisterPostEvictionCallback((key, value, reason, state) =>
                    {
                        _logger.LogCritical(
                            "NEVER-REMOVE item evicted! Key: '{Key}', Reason: {Reason}",
                            key, reason);
                    });

                _cache.Set(cacheKey, vipProductsList, cacheEntryOptions);
                _logger.LogInformation("Never-remove data cached: '{CacheKey}'", cacheKey);
            }
            else
            {
                _logger.LogInformation("Cache HIT for '{CacheKey}'", cacheKey);
            }

            return Ok(new
            {
                source = "cache",
                priority = "NeverRemove",
                products = vipProductsList,
                timestamp = DateTime.UtcNow
            });
        }

        /// <summary>
        /// Manually remove a cache entry
        /// </summary>
        [HttpDelete("clearCache/{cacheKey}")]
        public IActionResult ClearCache(string cacheKey)
        {
            _cache.Remove(cacheKey);
            _logger.LogInformation("Cache manually cleared for key: '{CacheKey}'", cacheKey);
            return Ok(new { message = $"Cache cleared for key: {cacheKey}", timestamp = DateTime.UtcNow });
        }

        /// <summary>
        /// Get cache configuration info
        /// </summary>
        [HttpGet("cacheInfo")]
        public IActionResult GetCacheInfo()
        {
            return Ok(new
            {
                configuration = new
                {
                    sizeLimit = 1024,
                    compactionPercentage = "25%",
                    expirationScanFrequency = "1 minute"
                },
                endpoints = new
                {
                    premiumProducts = new { size = 1, priority = "Normal", absoluteExp = "5min", slidingExp = "30sec" },
                    standardProducts = new { size = 2, priority = "Low", absoluteExp = "2min", slidingExp = "20sec" },
                    criticalProducts = new { size = 1, priority = "High", absoluteExp = "10min", slidingExp = "1min" },
                    vipProducts = new { size = 1, priority = "NeverRemove", absoluteExp = "1hour", slidingExp = "5min" }
                },
                evictionPriorityOrder = new[] { "Low", "Normal", "High", "NeverRemove" },
                notes = new
                {
                    sizeLimit = "Total cache size limited to 1024 units",
                    absoluteExpiration = "Item expires after fixed time, regardless of access",
                    slidingExpiration = "Item expires if not accessed within time window (timer resets on access)",
                    compaction = "When size limit reached, 25% of cache evicted starting with Low priority",
                    evictionReasons = new[] { "Expired", "Capacity", "Removed", "Replaced", "TokenExpired" }
                }
            });
        }
    }
}
