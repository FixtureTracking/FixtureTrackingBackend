﻿using FixtureTracking.Business.Abstract;
using FixtureTracking.Business.Constants;
using FixtureTracking.Core.Entities.Concrete;
using FixtureTracking.Core.Utilities.Results;
using FixtureTracking.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FixtureTracking.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal userDal;
        private readonly IDepartmentService departmentService;

        public UserManager(IUserDal userDal, IDepartmentService departmentService)
        {
            this.userDal = userDal;
            this.departmentService = departmentService;
        }

        public Guid Add(User user)
        {
            userDal.Add(user);
            return user.Id;
        }

        public IResult Delete(Guid userId)
        {
            var user = GetById(userId).Data;
            if (user != null)
            {
                user.IsEnable = false;
                user.UpdatedAt = DateTime.Now;

                userDal.Update(user);
                return new SuccessResult(Messages.UserDeleted);
            }
            return new ErrorResult(Messages.UserNotFound);
        }

        public IDataResult<User> GetByEmail(string email)
        {
            return new SuccessDataResult<User>(userDal.Get(u => u.Email == email));
        }

        public IDataResult<User> GetById(Guid userId)
        {
            return new SuccessDataResult<User>(userDal.Get(u => u.Id == userId));
        }

        public IDataResult<User> GetByUsername(string username)
        {
            return new SuccessDataResult<User>(userDal.Get(u => u.Username == username));
        }

        public IDataResult<string[]> GetClaims(User user)
        {
            var claimResult = departmentService.GetOperationClaimNames(user.DepartmentId);
            if (claimResult.Success)
                return new SuccessDataResult<string[]>(claimResult.Data);
            return new ErrorDataResult<string[]>(Messages.DepartmentNotFound);
        }

        public IDataResult<List<User>> GetList()
        {
            return new SuccessDataResult<List<User>>(userDal.GetList(u => u.IsEnable == true).ToList());
        }

        public IDataResult<List<User>> GetListByDepartmentId(int departmentId)
        {
            return new SuccessDataResult<List<User>>(userDal.GetList(u => u.DepartmentId == departmentId).ToList());
        }
    }
}
