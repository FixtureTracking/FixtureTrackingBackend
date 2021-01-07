using FixtureTracking.Core.DataAccess.EntityFramework;
using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.DataAccess.Concrete.EntityFramework.Contexts;
using FixtureTracking.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace FixtureTracking.DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal : EfEntityRepositoryBase<Category, FixtureTrackingContext>, ICategoryDal
    {
        public List<Fixture> GetFixtures(Category category)
        {
            using var context = new FixtureTrackingContext();
            var result = from fixture in context.Fixtures
                         where fixture.CategoryId == category.Id
                         select fixture;
            return result.ToList();
        }
    }
}
