using FixtureTracking.Core.DataAccess.EntityFramework;
using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.DataAccess.Concrete.EntityFramework.Contexts;
using FixtureTracking.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace FixtureTracking.DataAccess.Concrete.EntityFramework
{
    public class EfSupplierDal : EfEntityRepositoryBase<Supplier, FixtureTrackingContext>, ISupplierDal
    {
        public List<Fixture> GetFixtures(Supplier supplier)
        {
            using var context = new FixtureTrackingContext();
            var result = from fixture in context.Fixtures
                         where fixture.SupplierId == supplier.Id
                         select fixture;
            return result.ToList();
        }
    }
}
