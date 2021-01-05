using FixtureTracking.Core.DataAccess.EntityFramework;
using FixtureTracking.Core.Entities.Concrete;
using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.DataAccess.Concrete.EntityFramework.Contexts;

namespace FixtureTracking.DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, FixtureTrackingContext>, IUserDal
    {
    }
}
