using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.Entities.Concrete;

namespace FixtureTracking.Business.Tests.Mocks
{
    public class MockCategoryDal : MockEntityRepository<Category, ICategoryDal>
    {
    }
}
