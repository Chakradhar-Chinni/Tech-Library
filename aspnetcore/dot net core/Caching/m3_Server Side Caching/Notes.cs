(3.1) Cache data on server with sliding/ absolute expirations
(3.2) size-limited cache with absolute & sliding expirations & eviction callbacks, cache priority
(3.3) understanding “weight” function

(3.1) 
Cache data on server with sliding/absolute expirations
- Program.cs
	builder.Services.AddMemoryCache(); added
- ProductController.cs
  IMemoryCache is injected through Constructor
  cache is stored with unique key (never use client input as keys, it causes memory abuse)
  _cache.set stores the data into cache with key, data, cacheTiming


  (3.2)

  Program.cs

  SIZELIMITED MEMORY CACHE CONFIGURATION
   ─────────────────────────────────────
  SizeLimit (1024):
     Maximum total size of all cached items in "units"
     Each cache entry must specify its size via SetSize()
     Units are arbitrary  you define what 1 unit means (could be KB, item count, etc.)
  
  CompactionPercentage (0.25):
     When SizeLimit is reached, remove 25% of cache based on priority and LRU
     Lower priority items are removed first
     Range: 0.0 to 1.0 (0% to 100%)
  
  ExpirationScanFrequency (1 minute):
     How often to scan for and remove expired items
     More frequent = lower memory usage but higher CPU
     Less frequent = higher memory usage but lower CPU
 
ProductController.cs

  CACHE ENTRY OPTIONS:
  ─────────────────────────────────────
  1️) SetSize(int size)  REQUIRED when SizeLimit is set
      Assigns size/weight to cache entry
      Counts toward total SizeLimit
      Use different sizes for different data volumes
  
  
  2) SetPriority(CacheItemPriority)
      CacheItemPriority.Low → Evicted FIRST
      CacheItemPriority.Normal → Default
      CacheItemPriority.High → Evicted LAST
      CacheItemPriority.NeverRemove → Never evicted (use sparingly!)
  
  3) RegisterPostEvictionCallback
      Called when item is removed
      Eviction reasons:
       • Expired  Absolute/sliding expiration reached
       • Capacity  Size limit reached, item compacted
       • Removed  Manually removed via _cache.Remove()
       • Replaced  New value set for same key
       • TokenExpired  CancellationToken triggered
      Useful for: logging, cleanup, refresh triggers  
  
  COMBINING ABSOLUTE + SLIDING:
  ─────────────────────────────────────
  Both can be used together! Item expires when EITHER condition is met:
  
  Example: SetAbsoluteExpiration(10min) + SetSlidingExpiration(2min)
  • Item expires after 10 minutes MAX (absolute)
  • OR expires if not accessed for 2 minutes (sliding)
  • Frequent access keeps it alive, but max 10 minutes total
  
  
  EVICTION SCENARIOS:
  ─────────────────────────────────────
  Scenario 1: Size limit reached
  → CompactionPercentage triggered (25%)
  → Remove Low priority items first (LRU within priority)
  → Then Normal, then High
  → NeverRemove items are never touched
  
  Scenario 2: Absolute expiration
  → Item removed after fixed time, regardless of access
  → Callback fires with reason: Expired
  
  Scenario 3: Sliding expiration
  → Item removed if not accessed within time window
  → Each access resets the timer
  → Callback fires with reason: Expired
  
  Scenario 4: Manual removal
  → _cache.Remove(key)
  → Callback fires with reason: Removed  
  
  BEST PRACTICES:
  ─────────────────────────────────────
  - Always set Size when SizeLimit is configured (throws exception otherwise)
  - Use both absolute + sliding for optimal control
  - Set appropriate priorities (Low for noncritical, High for important)
  - Use eviction callbacks for monitoring and debugging
  - Consider data freshness requirements when setting expirations
  - Use ILogger instead of Console.WriteLine for production
  
  - Don't use NeverRemove unless absolutely necessary (memory leak risk)
  - Don't set SizeLimit too low (excessive compaction overhead)
  - Don't ignore eviction callbacks (valuable for diagnostics)
  - Don't cache sensitive data without encryption
  
  LIMITATIONS:
  ─────────────────────────────────────
  • Inmemory only (lost on restart)
  • Not distributed (single server only)
  • No builtin statistics API
  • Size units are arbitrary (not automatic)
  • Threadsafe but inprocess only
  
  For distributed caching → Use Redis or SQL Server cache
  For detailed statistics → Implement custom tracking or use Redis
  For persistent cache → Use distributed cache providers

  cachekey action in memory 
    - stored in heap memory, so cache is available to all threads
    - if memory is not available, cannot cache - configure memory pressure, limits
  
  TESTING THE DEMO:
  ─────────────────────────────────────
  1. Call /api/Product/premiumProducts repeatedly
     → First call: Cache MISS, subsequent: Cache HIT
     → After 30 sec of no access: Sliding expiration triggers
     → After 5 min total: Absolute expiration triggers
  
  2. Call all endpoints to fill cache
     → Monitor size accumulation (1+2+1+1 = 5 units used)
  
  3. Call /api/Product/clearCache/premiumProducts
     → Manually removes entry, callback fires with "Removed"
  
  4. Check logs for eviction events
     → Look for emoji indicators (- HIT, ⚠️ MISS, 🗑️ Evicted)

- CacheItemPriority.NeverRemove - If cache limit is reached and nothing is available for eviction,  cache silently fails to store the new item because the SizeLimit is reached. On the next request, 	 request will hit the original data source
