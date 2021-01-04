﻿using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.Entities.Concrete;

namespace FixtureTracking.Business.Tests.Mocks.Repositories
{
    public class MockDepartmentDal : MockEntityRepository<Department, IDepartmentDal>
    {
    }
}