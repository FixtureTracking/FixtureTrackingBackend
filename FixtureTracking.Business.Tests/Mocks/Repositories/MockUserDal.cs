using FixtureTracking.Core.Entities.Concrete;
using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.Entities.Dtos.Debit;
using FixtureTracking.Entities.Dtos.User;
using Moq;
using System.Collections.Generic;

namespace FixtureTracking.Business.Tests.Mocks.Repositories
{
    public class MockUserDal : MockEntityRepository<User, IUserDal>
    {
        public MockUserDal MockGetClaims(string[] result)
        {
            Setup(x => x.GetClaims(It.IsAny<User>()))
                .Returns(result);

            return this;
        }

        public MockUserDal MockGetDebits(List<DebitForFixtureDetailDto> result)
        {
            Setup(x => x.GetDebits(It.IsAny<User>()))
                .Returns(result);

            return this;
        }

        public MockUserDal MockGetDetail(UserForDetailDto result)
        {
            Setup(x => x.GetDetail(It.IsAny<User>()))
                .Returns(result);

            return this;
        }
    }
}
