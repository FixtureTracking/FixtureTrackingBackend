using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.Entities.Concrete;
using Moq;
using System.Collections.Generic;

namespace FixtureTracking.Business.Tests.Mocks.Repositories
{
    public class MockCategoryDal : MockEntityRepository<Category, ICategoryDal>
    {
        public MockCategoryDal MockGetFixtures(List<Fixture> result)
        {
            Setup(x => x.GetFixtures(It.IsAny<Category>()))
                .Returns(result);

            return this;
        }
    }
}
