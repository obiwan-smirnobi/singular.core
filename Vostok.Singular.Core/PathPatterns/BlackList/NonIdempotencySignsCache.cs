﻿using System.Collections.Generic;
using System.Linq;
using Vostok.Commons.Collections;
using Vostok.Singular.Core.PathPatterns.BlackList.Settings;

namespace Vostok.Singular.Core.PathPatterns.BlackList
{
    internal class NonIdempotencySignsCache : ISettingsCache<NonIdempotencySign>
    {
        private readonly CachingTransform<NonIdempotencySignsSettings, List<NonIdempotencySign>> cache;

        public NonIdempotencySignsCache(INonIdempotencySignsSettingsProvider nonIdempotencySignsSettingsProvider)
        {
            cache = new CachingTransform<NonIdempotencySignsSettings, List<NonIdempotencySign>>(
                PreprocessSigns,
                nonIdempotencySignsSettingsProvider.Get);
        }

        public List<NonIdempotencySign> Get()
        {
            return cache.Get();
        }

        private static List<NonIdempotencySign> PreprocessSigns(NonIdempotencySignsSettings nonIdempotencySignsSettings)
        {
            var signs = nonIdempotencySignsSettings.Signs;
            var processedSigns = new List<NonIdempotencySign>(signs.Count);

            processedSigns.AddRange(
                signs.Select(
                    sign => new NonIdempotencySign(
                        sign.Method,
                        sign.PathPattern
                    )
                ));

            return processedSigns;
        }
    }
}