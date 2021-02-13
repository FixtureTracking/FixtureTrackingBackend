using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.Entities.Concrete;
using FixtureTracking.Entities.Dtos.Debit;
using FixtureTracking.Entities.Dtos.Fixture;
using Moq;
using System.Collections.Generic;

namespace FixtureTracking.Business.Tests.Mocks.Repositories
{
    public class MockFixtureDal : MockEntityRepository<Fixture, IFixtureDal>
    {
        public MockFixtureDal MockGetDetailList(List<FixtureForDetailDto> result)
        {
            Setup(x => x.GetDetailList())
                .Returns(result);

            return this;
        }

        public MockFixtureDal MockGetDebits(List<DebitForUserDetailDto> result)
        {
            Setup(x => x.GetDebits(It.IsAny<Fixture>()))
                .Returns(result);

            return this;
        }
    }
}
