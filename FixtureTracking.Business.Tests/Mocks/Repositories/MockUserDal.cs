using FixtureTracking.Core.Entities.Concrete;
using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.Entities.Concrete;
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

        public MockUserDal MockGetDebits(List<Debit> result)
        {
            Setup(x => x.GetDebits(It.IsAny<User>()))
                .Returns(result);

            return this;
        }
    }
}
