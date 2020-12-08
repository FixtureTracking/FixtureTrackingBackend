using FixtureTracking.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace FixtureTracking.Business.Abstract
{
    public interface ICompanyService
    {
        Company GetById(int companyId);
        List<Company> GetList();
        void Add(Company company);
        void Update(Company company);
        void Delete(Company company);
    }
}
