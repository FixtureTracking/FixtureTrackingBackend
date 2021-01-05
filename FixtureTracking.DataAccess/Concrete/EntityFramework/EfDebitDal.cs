using FixtureTracking.Core.DataAccess.EntityFramework;
using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.DataAccess.Concrete.EntityFramework.Contexts;
using FixtureTracking.Entities.Concrete;

namespace FixtureTracking.DataAccess.Concrete.EntityFramework
{
    public class EfDebitDal : EfEntityRepositoryBase<Debit, FixtureTrackingContext>, IDebitDal
    {
    }
}
