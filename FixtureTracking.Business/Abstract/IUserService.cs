using FixtureTracking.Core.Entities.Concrete;
using FixtureTracking.Core.Utilities.Results;
using FixtureTracking.Entities.Dtos.Debit;
using FixtureTracking.Entities.Dtos.User;
using System;
using System.Collections.Generic;

namespace FixtureTracking.Business.Abstract
{
    public interface IUserService
    {
        IDataResult<User> GetById(Guid userId);
        IDataResult<User> GetByUsername(string username);
        IDataResult<User> GetByEmail(string email);
        IDataResult<List<UserForDetailDto>> GetList();
        IDataResult<List<DebitForFixtureDetailDto>> GetDebits(Guid userId);
        List<User> GetListByDepartmentId(int departmentId);
        bool IsAlreadyExistsEmail(string email);
        bool IsAlreadyExistsUsername(string username);
        Guid Add(User user);
        // TODO : Update methods - user
        IResult Delete(Guid userId);


        // For Login Methods - Not Required Authorization
        string[] GetClaimsForLogin(User user);
        User GetUserByEmailForLogin(string email);
    }
}
