using FixtureTracking.Business.Abstract;
using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace FixtureTracking.Business.Concrete
{
    public class CompanyManager : ICompanyService
    {
        private readonly ICompanyDal _companyDal;

        public CompanyManager(ICompanyDal companyDal)
        {
            _companyDal = companyDal;
        }

        public void Add(Company company)
        {
            _companyDal.Add(company);
        }

        public void Delete(Company company)
        {
            _companyDal.Delete(company);
        }

        public Company GetById(int companyId)
        {
            return _companyDal.Get(c => c.Id == companyId);
        }

        public List<Company> GetList()
        {
            return _companyDal.GetList().ToList();
        }

        public void Update(Company company)
        {
            _companyDal.Update(company);
        }
    }
}
