﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IResponseCache
    {
        Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive);

        Task<string> GetCacheResponse(string cacheKey);
    }
}
