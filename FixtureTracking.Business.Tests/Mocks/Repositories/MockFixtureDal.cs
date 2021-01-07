using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.Entities.Concrete;
using Moq;
using System.Collections.Generic;

namespace FixtureTracking.Business.Tests.Mocks.Repositories
{
    public class MockFixtureDal : MockEntityRepository<Fixture, IFixtureDal>
    {
        public MockFixtureDal MockGetDebits(List<Debit> result)
        {
            Setup(x => x.GetDebits(It.IsAny<Fixture>()))
                .Returns(result);

            return this;
        }
    }
}
