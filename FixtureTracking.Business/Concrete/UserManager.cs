using FixtureTracking.Business.Abstract;
using FixtureTracking.Business.BusinessAspects.Autofac;
using FixtureTracking.Business.Constants;
using FixtureTracking.Core.Aspects.Autofac.Caching;
using FixtureTracking.Core.Aspects.Autofac.Performance;
using FixtureTracking.Core.CrossCuttingConcerns.Logging.NLog.Loggers;
using FixtureTracking.Core.Entities.Concrete;
using FixtureTracking.Core.Utilities.CustomExceptions;
using FixtureTracking.Core.Utilities.Results;
using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.Entities.Dtos.Debit;
using FixtureTracking.Entities.Dtos.User;
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

        [SecuredOperationAspect("User.Add,Auth.Register")]
        [CacheRemoveAspect("IUserService.Get")]
        [CacheRemoveAspect("IDepartmentService.GetUsers")]
        public Guid Add(User user)
        {
            userDal.Add(user);
            return user.Id;
        }

        [SecuredOperationAspect("User.Delete")]
        [CacheRemoveAspect("IUserService.Get")]
        [CacheRemoveAspect("IDepartmentService.GetUsers")]
        public IResult Delete(Guid userId)
        {
            var user = GetById(userId).Data;
            user.IsEnable = false;
            user.UpdatedAt = DateTime.Now;

            userDal.Update(user);
            return new SuccessResult(Messages.UserDeleted);
        }

        [SecuredOperationAspect("User.Get")]
        [CacheAspect()]
        public IDataResult<User> GetByEmail(string email)
        {
            var user = userDal.Get(u => u.Email == email);
            if (user != null)
                return new SuccessDataResult<User>(user);
            throw new ObjectNotFoundException(Messages.UserNotFound);
        }

        [SecuredOperationAspect("User.Get")]
        [CacheAspect()]
        public IDataResult<User> GetById(Guid userId)
        {
            var user = userDal.Get(u => u.Id == userId);
            if (user != null)
                return new SuccessDataResult<User>(user);
            throw new ObjectNotFoundException(Messages.UserNotFound);
        }

        [SecuredOperationAspect("User.Get")]
        [CacheAspect()]
        public IDataResult<User> GetByUsername(string username)
        {
            var user = userDal.Get(u => u.Username == username);
            if (user != null)
                return new SuccessDataResult<User>(user);
            throw new ObjectNotFoundException(Messages.UserNotFound);
        }

        public string[] GetClaimsForLogin(User user)
        {
            return userDal.GetClaims(user);
        }

        [PerformanceLogAspect(1, typeof(FileLogger))]
        [SecuredOperationAspect("User.GetDebits,User.Me")]
        [CacheAspect(duration: 2)]
        public IDataResult<List<DebitForFixtureDetailDto>> GetDebits(Guid userId)
        {
            var user = GetById(userId).Data;
            return new SuccessDataResult<List<DebitForFixtureDetailDto>>(userDal.GetDebits(user));
        }

        [SecuredOperationAspect("User.Get,User.Me")]
        [CacheAspect()]
        public IDataResult<UserForDetailDto> GetDetail(Guid userId)
        {
            var user = GetById(userId).Data;
            return new SuccessDataResult<UserForDetailDto>(userDal.GetDetail(user));
        }

        [PerformanceLogAspect(1, typeof(FileLogger))]
        [SecuredOperationAspect("User.List")]
        [CacheAspect(duration: 2)]
        public IDataResult<List<UserForDetailDto>> GetList()
        {
            return new SuccessDataResult<List<UserForDetailDto>>(userDal.GetDetailList());
        }

        [PerformanceLogAspect(1, typeof(FileLogger))]
        [SecuredOperationAspect("User.List")]
        [CacheAspect(duration: 2)]
        public List<User> GetListByDepartmentId(int departmentId)
        {
            return userDal.GetList(u => u.DepartmentId == departmentId).ToList();
        }

        public User GetUserByEmailForLogin(string email)
        {
            return userDal.Get(u => u.Email == email && u.IsEnable == true);
        }

        [SecuredOperationAspect("User.Any,Auth.Register")]
        public bool IsAlreadyExistsEmail(string email)
        {
            return userDal.Any(u => u.Email == email);
        }

        [SecuredOperationAspect("User.Any,Auth.Register")]
        public bool IsAlreadyExistsUsername(string username)
        {
            return userDal.Any(u => u.Username == username);
        }
    }
}
