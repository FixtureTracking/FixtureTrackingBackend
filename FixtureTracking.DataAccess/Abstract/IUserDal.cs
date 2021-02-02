using FixtureTracking.Core.DataAccess;
using FixtureTracking.Core.Entities.Concrete;
using FixtureTracking.Entities.Dtos.Debit;
using System.Collections.Generic;

namespace FixtureTracking.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        string[] GetClaims(User user);
        List<DebitForFixtureDetailDto> GetDebits(User user);
    }
}
