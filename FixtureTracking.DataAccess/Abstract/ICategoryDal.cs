using FixtureTracking.Core.DataAccess;
using FixtureTracking.Entities.Concrete;
using System.Collections.Generic;

namespace FixtureTracking.DataAccess.Abstract
{
    public interface ICategoryDal : IEntityRepository<Category>
    {
        List<Fixture> GetFixtures(Category category);
    }
}
