using FixtureTracking.Business.Abstract;
using FixtureTracking.Business.Constants;
using FixtureTracking.Core.Utilities.Results;
using FixtureTracking.Entities.Concrete;
using Moq;
using System;

namespace FixtureTracking.Business.Tests.Mocks.Services
{
    public class MockFixtureService : Mock<IFixtureService>
    {
        public MockFixtureService MockGetById(IDataResult<Fixture> result)
        {
            Setup(x => x.GetById(It.IsAny<Guid>()))
                .Returns(result);

            return this;
        }

        public MockFixtureService MockUpdatePostiton(IResult result)
        {
            Setup(x => x.UpdatePosition(It.IsAny<Guid>(), It.IsAny<FixturePositions.Position>()))
                .Returns(result);

            return this;
        }
    }
}
