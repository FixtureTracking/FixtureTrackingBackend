using FixtureTracking.Business.Abstract;
using FixtureTracking.Business.BusinessAspects.Autofac;
using FixtureTracking.Business.Constants;
using FixtureTracking.Business.ValidationRules.FluentValidation.DepartmentValidations;
using FixtureTracking.Core.Aspects.Autofac.Caching;
using FixtureTracking.Core.Aspects.Autofac.Performance;
using FixtureTracking.Core.Aspects.Autofac.Validation;
using FixtureTracking.Core.CrossCuttingConcerns.Logging.NLog.Loggers;
using FixtureTracking.Core.Entities.Concrete;
using FixtureTracking.Core.Utilities.CustomExceptions;
using FixtureTracking.Core.Utilities.Results;
using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.Entities.Concrete;
using FixtureTracking.Entities.Dtos.Department;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FixtureTracking.Business.Concrete
{
    public class DepartmentManager : IDepartmentService
    {
        private readonly IDepartmentDal departmentDal;

        public DepartmentManager(IDepartmentDal departmentDal)
        {
            this.departmentDal = departmentDal;
        }

        [SecuredOperationAspect("Department.Add")]
        [ValidationAspect(typeof(DepartmentForAddValidator))]
        [CacheRemoveAspect("IDepartmentService.Get")]
        public IDataResult<int> Add(DepartmentForAddDto departmentForAddDto)
        {
            var department = new Department()
            {
                CreatedAt = DateTime.Now,
                Description = departmentForAddDto.Description,
                IsEnable = true,
                Name = departmentForAddDto.Name,
                OperationClaimNames = new string[] { "User.Me" },
                UpdatedAt = DateTime.Now
            };
            departmentDal.Add(department);
            return new SuccessDataResult<int>(department.Id, Messages.DepartmentAdded);
        }

        [SecuredOperationAspect("Department.Delete")]
        [CacheRemoveAspect("IDepartmentService.Get")]
        public IResult Delete(int departmentId)
        {
            var department = GetById(departmentId).Data;
            department.IsEnable = false;
            department.UpdatedAt = DateTime.Now;

            departmentDal.Update(department);
            return new SuccessResult(Messages.DepartmentDeleted);
        }

        [SecuredOperationAspect("Department.Get")]
        [CacheAspect()]
        public IDataResult<Department> GetById(int departmentId)
        {
            var department = departmentDal.Get(d => d.Id == departmentId);
            if (department != null)
                return new SuccessDataResult<Department>(department);
            throw new ObjectNotFoundException(Messages.DepartmentNotFound);
        }

        [PerformanceLogAspect(1, typeof(FileLogger))]
        [SecuredOperationAspect("Department.List")]
        [CacheAspect(duration: 2)]
        public IDataResult<List<Department>> GetList()
        {
            return new SuccessDataResult<List<Department>>(departmentDal.GetList(d => d.IsEnable == true).ToList());
        }

        [SecuredOperationAspect("Department.GetOperationClaimNames")]
        [CacheAspect()]
        public IDataResult<string[]> GetOperationClaimNames(int departmentId)
        {
            var department = GetById(departmentId).Data;
            return new SuccessDataResult<string[]>(department.OperationClaimNames);
        }

        [PerformanceLogAspect(1, typeof(FileLogger))]
        [SecuredOperationAspect("Department.GetUsers")]
        [CacheAspect(duration: 2)]
        public IDataResult<List<User>> GetUsers(int departmentId)
        {
            var department = GetById(departmentId).Data;
            return new SuccessDataResult<List<User>>(departmentDal.GetUsers(department));
        }

        [SecuredOperationAspect("Department.Update")]
        [ValidationAspect(typeof(DepartmentForUpdateValidator))]
        [CacheRemoveAspect("IDepartmentService.Get")]
        public IResult Update(DepartmentForUpdateDto departmentForUpdateDto)
        {
            var department = GetById(departmentForUpdateDto.Id).Data;
            department.Description = departmentForUpdateDto.Description;
            department.Name = departmentForUpdateDto.Name;
            department.UpdatedAt = DateTime.Now;

            departmentDal.Update(department);
            return new SuccessResult(Messages.DepartmentUpdated);
        }

        [SecuredOperationAspect("Department.UpdateClaim")]
        [ValidationAspect(typeof(DepartmentForUpdateClaimsValidator))]
        [CacheRemoveAspect("IDepartmentService.Get")]
        public IResult UpdateOperationClaim(DepartmentForUpdateClaimDto departmentForUpdateClaimDto)
        {
            var department = GetById(departmentForUpdateClaimDto.Id).Data;
            departmentForUpdateClaimDto.OperationClaimNames.Append("User.Me");
            department.OperationClaimNames = departmentForUpdateClaimDto.OperationClaimNames;
            department.UpdatedAt = DateTime.Now;

            departmentDal.Update(department);
            return new SuccessResult(Messages.DepartmentClaimUpdated);
        }
    }
}
