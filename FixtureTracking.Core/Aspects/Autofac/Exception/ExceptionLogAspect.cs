using Castle.DynamicProxy;
using FixtureTracking.Core.CrossCuttingConcerns.Logging;
using FixtureTracking.Core.CrossCuttingConcerns.Logging.NLog;
using FixtureTracking.Core.Extensions;
using FixtureTracking.Core.Utilities.CustomExceptions;
using FixtureTracking.Core.Utilities.Interceptors.Autofac;
using FixtureTracking.Core.Utilities.IoC;
using FixtureTracking.Core.Utilities.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace FixtureTracking.Core.Aspects.Autofac.Exception
{
    public class ExceptionLogAspect : MethodInterception
    {
        private readonly LoggerServiceBase loggerServiceBase;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ExceptionLogAspect(Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase))
                throw new System.Exception(AspectMessages.WrongLoggerType);

            loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);
            httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnException(IInvocation invocation, System.Exception e)
        {
            if (!(e is LogicException))
            {
                var logDetailWithException = GetLogDetail(invocation);
                logDetailWithException.ExceptionMessage = e.Message;

                loggerServiceBase.Error(logDetailWithException);
            }
        }

        private LogDetailWithException GetLogDetail(IInvocation invocation)
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
