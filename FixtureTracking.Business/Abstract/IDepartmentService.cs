using FixtureTracking.Core.Utilities.Results;
using FixtureTracking.Entities.Concrete;
using FixtureTracking.Entities.Dtos.Department;
using System.Collections.Generic;

namespace FixtureTracking.Business.Abstract
{
    public interface IDepartmentService
    {
        IDataResult<Department> GetById(int departmentId);
        IDataResult<string[]> GetOperationClaimNames(int departmentId);
        IDataResult<List<Department>> GetList();
        IDataResult<int> Add(DepartmentForAddDto departmentForAddDto);
        IResult Update(DepartmentForUpdateDto departmentForUpdateDto);
        IResult UpdateOperationClaim(DepartmentForUpdateClaimDto departmentForUpdateClaimDto);
        IResult Delete(int departmentId);
    }
}
