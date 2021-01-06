using FixtureTracking.Business.Abstract;
using FixtureTracking.Entities.Concrete;
using Moq;
using System.Collections.Generic;

namespace FixtureTracking.Business.Tests.Mocks.Services
{
    public class MockFixtureService : Mock<IFixtureService>
    {
        public MockFixtureService MockGetListByCategoryId(List<Fixture> result)
        {
            Setup(x => x.GetListByCategoryId(It.IsAny<short>()))
                .Returns(result);

            return this;
        }

        public MockFixtureService MockGetListBySupplierId(List<Fixture> result)
        {
            Setup(x => x.GetListBySupplierId(It.IsAny<int>()))
                .Returns(result);

            return this;
        }
    }
}
