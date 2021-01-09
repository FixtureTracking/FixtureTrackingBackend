﻿using FixtureTracking.Core.CrossCuttingConcerns.Caching;
using FixtureTracking.Core.CrossCuttingConcerns.Caching.Microsoft;
using FixtureTracking.Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace FixtureTracking.Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
        }
    }
}