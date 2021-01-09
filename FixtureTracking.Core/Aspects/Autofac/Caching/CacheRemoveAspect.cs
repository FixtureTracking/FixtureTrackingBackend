using Castle.DynamicProxy;
using FixtureTracking.Core.CrossCuttingConcerns.Caching;
using FixtureTracking.Core.Utilities.Interceptors.Autofac;
using FixtureTracking.Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace FixtureTracking.Core.Aspects.Autofac.Caching
{
    public class CacheRemoveAspect : MethodInterception
    {
        private readonly string pattern;
        private readonly ICacheManager cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            this.pattern = pattern;
            cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            cacheManager.RemovePattern(pattern);
        }

    }
}
