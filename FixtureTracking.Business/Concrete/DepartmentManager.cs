using FixtureTracking.Business.Abstract;
using FixtureTracking.Business.Constants;
using FixtureTracking.Core.Entities.Concrete;
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
        private readonly IUserService userService;

        public DepartmentManager(IDepartmentDal departmentDal, IUserService userService)
        {
            this.departmentDal = departmentDal;
            this.userService = userService;
        }

        public IDataResult<int> Add(DepartmentForAddDto departmentForAddDto)
        {
            var department = new Department()
            {
                CreatedAt = DateTime.Now,
                Description = departmentForAddDto.Description,
                IsEnable = true,
                Name = departmentForAddDto.Name,
                OperationClaimNames = new string[] { },
                UpdatedAt = DateTime.Now
            };
            departmentDal.Add(department);
            return new SuccessDataResult<int>(department.Id, Messages.DepartmentAdded);
        }

        public IResult Delete(int departmentId)
        {
            var department = GetById(departmentId).Data;
            if (department != null)
            {
                department.IsEnable = false;
                department.UpdatedAt = DateTime.Now;

                departmentDal.Update(department);
                return new SuccessResult(Messages.DepartmentDeleted);
            }
            return new ErrorResult(Messages.DepartmentNotFound);
        }

        public IDataResult<Department> GetById(int departmentId)
        {
            return new SuccessDataResult<Department>(departmentDal.Get(d => d.Id == departmentId));
        }

        public IDataResult<List<Department>> GetList()
        {
            return new SuccessDataResult<List<Department>>(departmentDal.GetList(d => d.IsEnable == true).ToList());
        }

        public IDataResult<string[]> GetOperationClaimNames(int departmentId)
        {
            var department = GetById(departmentId).Data;
            if (department != null)
                return new SuccessDataResult<string[]>(department.OperationClaimNames);
            return new ErrorDataResult<string[]>(Messages.DepartmentNotFound);
        }

        public IDataResult<List<User>> GetUsers(int departmentId)
        {
            var department = GetById(departmentId).Data;
            if (department != null)
                return new SuccessDataResult<List<User>>(userService.GetListByDepartmentId(departmentId));
            return new ErrorDataResult<List<User>>(Messages.DepartmentNotFound);
        }

        public IResult Update(DepartmentForUpdateDto departmentForUpdateDto)
        {
            var department = GetById(departmentForUpdateDto.Id).Data;
            if (department != null)
            {
                department.Description = departmentForUpdateDto.Description;
                department.Name = departmentForUpdateDto.Name;
                department.UpdatedAt = DateTime.Now;

                departmentDal.Update(department);
                return new SuccessResult(Messages.DepartmentUpdated);
            }
            return new ErrorResult(Messages.DepartmentNotFound);
        }

        public IResult UpdateOperationClaim(DepartmentForUpdateClaimDto departmentForUpdateClaimDto)
        {
            var department = GetById(departmentForUpdateClaimDto.Id).Data;
            if (department != null)
            {
                department.OperationClaimNames = departmentForUpdateClaimDto.OperationClaimNames;
                department.UpdatedAt = DateTime.Now;

                departmentDal.Update(department);
                return new SuccessResult(Messages.DepartmentClaimUpdated);
            }
            return new ErrorResult(Messages.DepartmentNotFound);
        }
    }
}
