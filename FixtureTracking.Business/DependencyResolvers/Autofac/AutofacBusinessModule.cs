using Autofac;
using FixtureTracking.Business.Abstract;
using FixtureTracking.Business.Concrete;
using FixtureTracking.Core.Utilities.Security.Tokens;
using FixtureTracking.Core.Utilities.Security.Tokens.Jwt;
using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.DataAccess.Concrete.EntityFramework;

namespace FixtureTracking.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();

            builder.RegisterType<DebitManager>().As<IDebitService>();
            builder.RegisterType<EfDebitDal>().As<IDebitDal>();

            builder.RegisterType<DepartmentManager>().As<IDepartmentService>();
            builder.RegisterType<EfDepartmentDal>().As<IDepartmentDal>();

            builder.RegisterType<FixtureManager>().As<IFixtureService>();
            builder.RegisterType<EfFixtureDal>().As<IFixtureDal>();

            builder.RegisterType<SupplierManager>().As<ISupplierService>();
            builder.RegisterType<EfSupplierDal>().As<ISupplierDal>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();
        }
    }
}
