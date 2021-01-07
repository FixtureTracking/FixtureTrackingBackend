using FixtureTracking.Core.DataAccess.EntityFramework;
using FixtureTracking.Core.Entities.Concrete;
using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.DataAccess.Concrete.EntityFramework.Contexts;
using FixtureTracking.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace FixtureTracking.DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, FixtureTrackingContext>, IUserDal
    {
        public string[] GetClaims(User user)
        {
            using var context = new FixtureTrackingContext();
            var result = from department in context.Departments
                         where department.Id == user.DepartmentId
                         select department.OperationClaimNames;
            return result.FirstOrDefault();
        }

        public List<Debit> GetDebits(User user)
        {
            using var context = new FixtureTrackingContext();
            var result = from debit in context.Debits
                         where debit.UserId == user.Id
                         select debit;
            return result.ToList();
        }
    }
}
