using FixtureTracking.Core.Entities.Concrete;
using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.Entities.Concrete;
using Moq;
using System.Collections.Generic;

namespace FixtureTracking.Business.Tests.Mocks.Repositories
{
    public class MockDepartmentDal : MockEntityRepository<Department, IDepartmentDal>
    {
        public MockDepartmentDal MockGetUsers(List<User> result)
        {
            Setup(x => x.GetUsers(It.IsAny<Department>()))
                .Returns(result);

            return this;
        }
    }
}
