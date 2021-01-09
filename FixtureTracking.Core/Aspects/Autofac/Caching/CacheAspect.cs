using Castle.DynamicProxy;
using FixtureTracking.Core.CrossCuttingConcerns.Caching;
using FixtureTracking.Core.Utilities.Interceptors.Autofac;
using FixtureTracking.Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace FixtureTracking.Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private readonly int duration;
        private readonly ICacheManager cacheManager;

        public CacheAspect(int duration = 10)
        {
            this.duration = duration;
            cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = $"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}"; // FixtureTracking.Business.Abstract.ICategoryService.GetById
            var arguments = invocation.Arguments.ToList();
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})"; // FixtureTracking.Business.Abstract.ICategoryService.GetById(1) - .MethodOne(param1, param2) - .MethodTwo()

            if (cacheManager.IsAdded(key))
            {
                invocation.ReturnValue = cacheManager.Get(key);
                return;
            }

            invocation.Proceed();
            cacheManager.Add(key, invocation.ReturnValue, duration);
        }
    }
}
