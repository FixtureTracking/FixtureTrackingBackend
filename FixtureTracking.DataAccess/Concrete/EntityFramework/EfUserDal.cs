using FixtureTracking.Core.DataAccess.EntityFramework;
using FixtureTracking.Core.Entities.Concrete;
using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.DataAccess.Concrete.EntityFramework.Contexts;
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
    }
}
