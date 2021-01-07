using FixtureTracking.Core.DataAccess.EntityFramework;
using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.DataAccess.Concrete.EntityFramework.Contexts;
using FixtureTracking.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace FixtureTracking.DataAccess.Concrete.EntityFramework
{
    public class EfFixtureDal : EfEntityRepositoryBase<Fixture, FixtureTrackingContext>, IFixtureDal
    {
        public List<Debit> GetDebits(Fixture fixture)
        {
            using var context = new FixtureTrackingContext();
            var result = from debit in context.Debits
                         where debit.FixtureId == fixture.Id
                         select debit;
            return result.ToList();
        }
    }
}
