using Microsoft.AspNetCore.Http;
using System;

namespace ReactAdminNetCoreServerAPI.Common.Definitions
{
    public enum CacheInterval
    {
        NoCache = 0,
        FiveSeconds = 5,
        ThirtySeconds = 30,
        OneMinute = 60,
        FiveMinutes = 300,
        ThirtyMinutes = 1800,
        OneHour = 3600,
        OneDay = 86400,
        Forever = 10000000
    }

    public static class CacheIntervalExtensions
    {
        public static HttpContext AddCacheValidUntilHeader(this HttpContext httpContext, CacheInterval cacheInterval)
        {
            if (cacheInterval != CacheInterval.NoCache )
            {
                // TODO: add Cache-Control, Expires, ETag, and Last-Modified headers per https://tools.ietf.org/html/rfc7234
                DateTime expires = DateTime.UtcNow.AddSeconds((double)cacheInterval);
                httpContext.Response.Headers.Add("validUntil", $"new Date('{expires.ToString("yyyy-mm-ddThh:MM:SS")}')");
            }

            return httpContext;
        }
    }
}
