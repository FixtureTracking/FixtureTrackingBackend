using Castle.DynamicProxy;
using FixtureTracking.Core.Utilities.Interceptors.Autofac;
using System.Transactions;

namespace FixtureTracking.Core.Aspects.Autofac.Transaction
{
    public class TransactionScopeAspect : MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using var transactionScope = new TransactionScope();

            try
            {
                invocation.Proceed();
                transactionScope.Complete();
            }
            catch (System.Exception e)
            {
                transactionScope.Dispose();
                throw e;
            }
        }
    }
}
