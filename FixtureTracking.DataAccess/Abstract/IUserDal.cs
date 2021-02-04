using FixtureTracking.Core.DataAccess;
using FixtureTracking.Core.Entities.Concrete;
using FixtureTracking.Entities.Dtos.Debit;
using FixtureTracking.Entities.Dtos.User;
using System.Collections.Generic;

namespace FixtureTracking.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        string[] GetClaims(User user);
        UserForDetailDto GetDetail(User user);
        List<DebitForFixtureDetailDto> GetDebits(User user);
    }
}
