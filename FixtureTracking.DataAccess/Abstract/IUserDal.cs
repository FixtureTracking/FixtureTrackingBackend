using FixtureTracking.Core.DataAccess;
using FixtureTracking.Core.Entities.Concrete;

namespace FixtureTracking.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        string[] GetClaims(User user);
    }
}
