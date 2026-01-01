caching & its Types
client side: Response Caching
server side: InMemory Caching, Distributed Caching
Conditional Requests - mix of Response + InMemory

--
Choosing the Right Approach

Static-ish, public content: Response caching ([ResponseCache] + UseResponseCaching).
Expensive computations on a single instance: IMemoryCache.
Scaled out / multiple instances: IDistributedCache (Redis/Azure Cache for Redis).
Bandwidth optimization: ETag/conditional requests (can be layered on top).

------------------------------------------

Common Pitfalls & Tips
Cache invalidation: Provide explicit refresh endpoints or bust keys after write operations.
Key design: Include version suffixes (_v1) to rotate caches safely after schema changes.
Size/TTL: Use sensible TTLs; add SlidingExpiration + AbsoluteExpiration.
Don’t cache user-specific PII in shared caches without scoping keys by user/tenant.
Avoid caching errors (e.g., 500s) and very large payloads unless compressed.
Thread safety: For IMemoryCache, consider a GetOrCreateAsync pattern to avoid stampede.
