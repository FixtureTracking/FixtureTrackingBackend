using FixtureTracking.Core.Entities.Concrete;
using FixtureTracking.Core.Utilities.Results;
using FixtureTracking.Entities.Concrete;
using System;
using System.Collections.Generic;

namespace FixtureTracking.Business.Abstract
{
    public interface IUserService
    {
        IDataResult<User> GetById(Guid userId);
        IDataResult<User> GetByUsername(string username);
        IDataResult<User> GetByEmail(string email);
        IDataResult<List<User>> GetList();
        IDataResult<List<Debit>> GetDebits(Guid userId);
        string[] GetClaims(User user);
        List<User> GetListByDepartmentId(int departmentId);
        Guid Add(User user);
        // TODO : Update methods - user
        IResult Delete(Guid userId);
    }
}
