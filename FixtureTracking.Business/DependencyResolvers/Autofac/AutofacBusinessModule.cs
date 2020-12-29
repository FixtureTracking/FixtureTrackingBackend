using Autofac;
using FixtureTracking.Business.Abstract;
using FixtureTracking.Business.Concrete;
using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.DataAccess.Concrete.EntityFramework;

namespace FixtureTracking.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CompanyManager>().As<ICompanyService>();
            builder.RegisterType<EfCompanyDal>().As<ICompanyDal>();

            builder.RegisterType<FixtureManager>().As<IFixtureService>();
            builder.RegisterType<EfFixtureDal>().As<IFixtureDal>();
        }
    }
}
