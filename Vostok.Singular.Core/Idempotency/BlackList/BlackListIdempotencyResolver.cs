using System;
using Vostok.Singular.Core.Idempotency.BlackList.Settings;

namespace Vostok.Singular.Core.Idempotency.BlackList
{
    internal class BlackListIdempotencyResolver
    {
        private readonly INonIdempotencySignsCache nonIdempotencySignsCache;

        public BlackListIdempotencyResolver(INonIdempotencySignsCache nonIdempotencySignsCache)
        {
            this.nonIdempotencySignsCache = nonIdempotencySignsCache;
        }

        public bool IsIdempotent(string method, string path)
        {
            var signs = nonIdempotencySignsCache.Get();

            foreach (var sign in signs)
            {
                if (sign.Method == null || sign.PathPattern == null)
                    continue;

                if (!string.Equals(sign.Method, method, StringComparison.OrdinalIgnoreCase) || path == null)
                    continue;

                if (sign.PathPattern.IsMatch(path))
                    return false;
            }

            return true;
        }
    }
}