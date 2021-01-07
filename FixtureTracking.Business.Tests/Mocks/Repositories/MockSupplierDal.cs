using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.Entities.Concrete;
using Moq;
using System.Collections.Generic;

namespace FixtureTracking.Business.Tests.Mocks.Repositories
{
    public class MockSupplierDal : MockEntityRepository<Supplier, ISupplierDal>
    {
        public MockSupplierDal MockGetFixtures(List<Fixture> result)
        {
            Setup(x => x.GetFixtures(It.IsAny<Supplier>()))
                .Returns(result);

            return this;
        }
    }
}
