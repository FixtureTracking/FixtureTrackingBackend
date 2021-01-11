using Castle.DynamicProxy;
using FixtureTracking.Business.Constants;
using FixtureTracking.Core.Extensions;
using FixtureTracking.Core.Utilities.Interceptors.Autofac;
using FixtureTracking.Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FixtureTracking.Business.BusinessAspects.Autofac
{
    public class SecuredOperationAspect : MethodInterception
    {
        private readonly string[] roles;
        private readonly IHttpContextAccessor httpContextAccessor;

        public SecuredOperationAspect(string roles)
        {
            this.roles = roles.Split(",");
            httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in roles)
            {
                if (roleClaims.Contains(role))
                    return;
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}
