using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.Entities.Concrete;

namespace FixtureTracking.Business.Tests.Mocks
{
    public class MockSupplierDal : MockEntityRepository<Supplier, ISupplierDal>
    {
    }
}
