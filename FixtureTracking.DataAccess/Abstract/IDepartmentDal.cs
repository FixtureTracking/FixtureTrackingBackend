using FixtureTracking.Core.DataAccess;
using FixtureTracking.Core.Entities.Concrete;
using FixtureTracking.Entities.Concrete;
using System.Collections.Generic;

namespace FixtureTracking.DataAccess.Abstract
{
    public interface IDepartmentDal : IEntityRepository<Department>
    {
        List<User> GetUsers(Department department);
    }
}
