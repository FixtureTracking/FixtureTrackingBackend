using Castle.DynamicProxy;
using FixtureTracking.Core.Utilities.Interceptors.Autofac;
using FixtureTracking.Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace FixtureTracking.Core.Aspects.Autofac.Performance
{
    public class PerformanceAspect : MethodInterception
    {
        private readonly int interval;
        private readonly Stopwatch stopwatch;

        public PerformanceAspect(int intervalInSeconds)
        {
            interval = intervalInSeconds;
            stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
        }
        protected override void OnBefore(IInvocation invocation)
        {
            stopwatch.Start();
        }

        protected override void OnAfter(IInvocation invocation)
        {
            if (stopwatch.Elapsed.TotalSeconds > interval)
                Debug.WriteLine($"**** Performance : {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name} --> {stopwatch.Elapsed.TotalSeconds} ****");

            stopwatch.Stop();
        }
    }
}
