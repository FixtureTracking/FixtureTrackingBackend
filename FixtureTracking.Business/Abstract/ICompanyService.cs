using FixtureTracking.Core.Utilities.Results;
using FixtureTracking.Entities.Concrete;
using System.Collections.Generic;

namespace FixtureTracking.Business.Abstract
{
    public interface ICompanyService
    {
        IDataResult<Company> GetById(int companyId);
        IDataResult<List<Company>> GetList();
        IResult Add(Company company);
        IResult Update(Company company);
        IResult Delete(Company company);
    }
}
