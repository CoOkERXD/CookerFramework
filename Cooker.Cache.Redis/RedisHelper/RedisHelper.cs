using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Cooker.Cache.Redis.RedisConsts;

namespace Cooker.Cache.Redis.RedisHelper
{
    public class RedisHelper : IRedisHelper
    {
        private readonly IDistributedCache _cache;

        public RedisHelper(IDistributedCache cache)
        {
            this._cache = cache;
        }
        public async Task<(string, bool)> HasValue(string cacheKey, CancellationToken ct)
        {
            var json = await _cache.GetStringAsync(cacheKey, ct);
            if (json == RedisConst.NOT_FOUND)
            {
                return (RedisConst.NOT_FOUND, false);
            }
            if (string.IsNullOrWhiteSpace(json))
            {
                return (RedisConst.NOT_EXIST, false);
            }
            return (json, true);
        }
        public async Task<T> GetOrTryCreateStringAsync<T>(string cacheKey, Func<Task<T>> dealMethod, DistributedCacheEntryOptions options, CancellationToken ct)
        {
            var value = await _cache.GetStringAsync(cacheKey, ct);
            if (value == RedisConst.NOT_FOUND)
            {
                return default(T);
            }
            if (!string.IsNullOrWhiteSpace(value))
            {
                return JsonSerializer.Deserialize<T>(value);
            }
            var entity = await dealMethod();
            if (entity == null)
            {
                await _cache.SetStringAsync(cacheKey, RedisConst.NOT_FOUND, new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1) }, ct);
                return entity;
            }
            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(entity), options, ct);
            return entity;
        }
}
}