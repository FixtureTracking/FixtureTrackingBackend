using Castle.DynamicProxy;
using FixtureTracking.Core.Aspects.Autofac.Exception;
using FixtureTracking.Core.CrossCuttingConcerns.Logging.NLog.Loggers;
using System;
using System.Linq;
using System.Reflection;

namespace FixtureTracking.Core.Utilities.Interceptors.Autofac
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            var methodAttributes = type.GetMethod(method.Name).GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);
            classAttributes.Add(new ExceptionLogAspect(typeof(DatabaseLogger)));

            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
