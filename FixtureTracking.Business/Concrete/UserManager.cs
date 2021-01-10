using FixtureTracking.Business.Abstract;
using FixtureTracking.Business.Constants;
using FixtureTracking.Core.Aspects.Autofac.Caching;
using FixtureTracking.Core.Entities.Concrete;
using FixtureTracking.Core.Utilities.Results;
using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FixtureTracking.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal userDal;

        public UserManager(IUserDal userDal)
        {
            this.userDal = userDal;
        }

        [CacheRemoveAspect("IUserService.Get")]
        [CacheRemoveAspect("IDepartmentService.GetUsers")]
        public Guid Add(User user)
        {
            userDal.Add(user);
            return user.Id;
        }

        [CacheRemoveAspect("IUserService.Get")]
        [CacheRemoveAspect("IDepartmentService.GetUsers")]
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

        [CacheAspect()]
        public IDataResult<User> GetByEmail(string email)
        {
            return new SuccessDataResult<User>(userDal.Get(u => u.Email == email));
        }

        [CacheAspect()]
        public IDataResult<User> GetById(Guid userId)
        {
            return new SuccessDataResult<User>(userDal.Get(u => u.Id == userId));
        }

        [CacheAspect()]
        public IDataResult<User> GetByUsername(string username)
        {
            return new SuccessDataResult<User>(userDal.Get(u => u.Username == username));
        }

        [CacheAspect(duration: 2)]
        public string[] GetClaims(User user)
        {
            return userDal.GetClaims(user);
        }

        [CacheAspect(duration: 1)]
        public IDataResult<List<Debit>> GetDebits(Guid userId)
        {
            var user = GetById(userId).Data;
            if (user != null)
                return new SuccessDataResult<List<Debit>>(userDal.GetDebits(user));
            return new ErrorDataResult<List<Debit>>(Messages.UserNotFound);
        }

        [CacheAspect(duration: 2)]
        public IDataResult<List<User>> GetList()
        {
            return new SuccessDataResult<List<User>>(userDal.GetList(u => u.IsEnable == true).ToList());
        }

        [CacheAspect(duration: 2)]
        public List<User> GetListByDepartmentId(int departmentId)
        {
            return userDal.GetList(u => u.DepartmentId == departmentId).ToList();
        }
    }
}
