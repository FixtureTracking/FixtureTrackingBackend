using FixtureTracking.Core.Entities.Concrete;
using FixtureTracking.Core.Utilities.Results;
using System;
using System.Collections.Generic;

namespace FixtureTracking.Business.Abstract
{
    public interface IUserService
    {
        IDataResult<User> GetById(Guid userId);
        IDataResult<User> GetByUsername(string username);
        IDataResult<User> GetByEmail(string email);
        IDataResult<string[]> GetClaims(User user);
        IDataResult<List<User>> GetList();
        IDataResult<List<User>> GetListByDepartmentId(int departmentId);
        Guid Add(User user);
        // TODO : Update methods - user
        IResult Delete(Guid userId);
    }
}
