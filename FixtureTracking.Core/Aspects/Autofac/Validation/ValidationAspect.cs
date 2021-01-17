using Castle.DynamicProxy;
using FixtureTracking.Core.CrossCuttingConcerns.Validation.FluentValidation;
using FixtureTracking.Core.Utilities.Interceptors.Autofac;
using FixtureTracking.Core.Utilities.Messages;
using FluentValidation;
using System;
using System.Linq;

namespace FixtureTracking.Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private readonly Type validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
                throw new System.Exception(AspectMessages.WrongValidationType);

            this.validatorType = validatorType;
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(validatorType);
            var entityType = validatorType.BaseType.GetGenericArguments()[0];
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
