using FixtureTracking.Core.DataAccess.EntityFramework;
using FixtureTracking.Core.Entities.Concrete;
using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.DataAccess.Concrete.EntityFramework.Contexts;
using FixtureTracking.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace FixtureTracking.DataAccess.Concrete.EntityFramework
{
    public class EfDepartmentDal : EfEntityRepositoryBase<Department, FixtureTrackingContext>, IDepartmentDal
    {
        public List<User> GetUsers(Department department)
        {
            using var context = new FixtureTrackingContext();
            var result = from user in context.Users
                         where user.DepartmentId == department.Id
                         select user;
            return result.ToList();
        }
    }
}
