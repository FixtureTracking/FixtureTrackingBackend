using FixtureTracking.Core.DataAccess;
using FixtureTracking.Entities.Concrete;
using System.Collections.Generic;

namespace FixtureTracking.DataAccess.Abstract
{
    public interface ISupplierDal : IEntityRepository<Supplier>
    {
        List<Fixture> GetFixtures(Supplier supplier);
    }
}
