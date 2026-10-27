using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cooker.Domain.IocTag;
using Microsoft.Extensions.Caching.Distributed;

namespace Cooker.Cache.Redis.RedisHelper
{
    public interface IRedisHelper : ISingleton
    {
        Task<(string, bool)> HasValue(string cacheKey, CancellationToken ct);
        Task<T> GetOrTryCreateStringAsync<T>(string valueStatus, Func<Task<T>> dealMethod,DistributedCacheEntryOptions options, CancellationToken ct);
    }
}