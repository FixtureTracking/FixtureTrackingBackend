using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.Entities.Concrete;
using FixtureTracking.Tests.Mocks;

namespace FixtureTracking.Business.Tests.Mocks
{
    public class MockDebitDal : MockEntityRepository<Debit, IDebitDal>
    {
    }
}
