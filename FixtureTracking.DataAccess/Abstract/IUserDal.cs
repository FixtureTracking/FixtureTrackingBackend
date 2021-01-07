using FixtureTracking.Core.DataAccess;
using FixtureTracking.Core.Entities.Concrete;
using FixtureTracking.Entities.Concrete;
using System.Collections.Generic;

namespace FixtureTracking.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        string[] GetClaims(User user);
        List<Debit> GetDebits(User user);
    }
}
