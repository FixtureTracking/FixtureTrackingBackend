using Castle.DynamicProxy;
using FixtureTracking.Core.CrossCuttingConcerns.Logging;
using FixtureTracking.Core.CrossCuttingConcerns.Logging.NLog;
using FixtureTracking.Core.Extensions;
using FixtureTracking.Core.Utilities.Interceptors.Autofac;
using FixtureTracking.Core.Utilities.IoC;
using FixtureTracking.Core.Utilities.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FixtureTracking.Core.Aspects.Autofac.Performance
{
    public class PerformanceLogAspect : MethodInterception
    {
        private readonly int interval;
        private readonly Stopwatch stopwatch;
        private readonly LoggerServiceBase loggerServiceBase;
        private readonly IHttpContextAccessor httpContextAccessor;

        public PerformanceLogAspect(int intervalInSeconds, Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase))
                throw new System.Exception(AspectMessages.WrongLoggerType);

            interval = intervalInSeconds;
            stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
            loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);
            httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }
        protected override void OnBefore(IInvocation invocation)
        {
            stopwatch.Restart();
        }

        protected override void OnAfter(IInvocation invocation)
        {
            if (stopwatch.Elapsed.TotalSeconds > interval)
            {
                var logDetail = GetLogDetail(invocation);
                logDetail.ExceptionMessage = $"Performance | Expected: {interval} seconds *** Actual: {stopwatch.Elapsed.TotalSeconds:0.##} seconds";
                loggerServiceBase.Warn(logDetail);
            }

            stopwatch.Stop();
        }

        private LogDetailWithException GetLogDetail(IInvocation invocation)
        {
            var logPrameters = new List<LogParameter>();

            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logPrameters.Add(new LogParameter()
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = "*", // the content of 'value' is confidential because it may contain important information
                    Type = invocation.Arguments[i].GetType().Name
                });
            }

            var claimNameIdendifier = httpContextAccessor.HttpContext.User.NameIdentifier();

            var logDetailWithException = new LogDetailWithException()
            {
                MethodName = $"{invocation.Method.ReflectedType.Name}.{invocation.Method.Name}",
                ClaimId = claimNameIdendifier,
                LogParameters = logPrameters
            };

            return logDetailWithException;
        }
    }
}
