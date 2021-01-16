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

namespace FixtureTracking.Core.Aspects.Autofac.Logging
{
    public class LogAspect : MethodInterception
    {
        private readonly LoggerServiceBase loggerServiceBase;
        private readonly IHttpContextAccessor httpContextAccessor;

        public LogAspect(Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase))
                throw new Exception(AspectMessages.WrongLoggerType);

            loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);
            httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            loggerServiceBase.Info(GetLogDetail(invocation));
        }

        private LogDetail GetLogDetail(IInvocation invocation)
        {
            var logPrameters = new List<LogParameter>();

            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logPrameters.Add(new LogParameter()
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name
                });
            }

            var claimNameIdendifier = httpContextAccessor.HttpContext.User.NameIdentifier();

            var logDetail = new LogDetail()
            {
                MethodName = $"{invocation.Method.ReflectedType.Name}.{invocation.Method.Name}",
                ClaimId = claimNameIdendifier,
                LogParameters = logPrameters
            };

            return logDetail;
        }
    }
}
