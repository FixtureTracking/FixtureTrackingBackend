using FixtureTracking.Core.Utilities.Results;
using FixtureTracking.Entities.Concrete;
using FixtureTracking.Entities.Dtos.Company;
using System.Collections.Generic;

namespace FixtureTracking.Business.Abstract
{
    public interface ICompanyService
    {
        IDataResult<Company> GetById(short companyId);
        IDataResult<List<Company>> GetList();
        IResult Add(CompanyForAddDto companyForAddDto);
        IResult Update(CompanyForUpdateDto companyForUpdateDto);
        IResult Delete(short companyId);
    }
}
