using FixtureTracking.Business.Abstract;
using FixtureTracking.Business.Constants;
using FixtureTracking.Core.Utilities.Results;
using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.Entities.Concrete;
using FixtureTracking.Entities.Dtos.Company;
using System;
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

        public IResult Add(CompanyForAddDto companyForAddDto)
        {
            var company = new Company()
            {
                Name = companyForAddDto.Name,
                CreatedAt = companyForAddDto.CreatedAt,
                IsEnable = true,
                UpdatedAt = DateTime.Now
            };
            _companyDal.Add(company);
            return new SuccessResult(Messages.CompanyAdded);
        }

        public IResult Delete(short companyId)
        {
            var company = GetById(companyId).Data;
            if (company != null)
            {
                _companyDal.Delete(company);
                return new SuccessResult(Messages.CompanyDeleted);
            }
            return new ErrorResult(Messages.CompanyNotFound);
        }

        public IDataResult<Company> GetById(short companyId)
        {
            return new SuccessDataResult<Company>(_companyDal.Get(c => c.Id == companyId));
        }

        public IDataResult<List<Company>> GetList()
        {
            return new SuccessDataResult<List<Company>>(_companyDal.GetList().ToList());
        }

        public IResult Update(CompanyForUpdateDto companyForUpdateDto)
        {
            var company = new Company()
            {
                Id = companyForUpdateDto.Id,
                Expense = companyForUpdateDto.Expense,
                Income = companyForUpdateDto.Income,
                Name = companyForUpdateDto.Name,
                IsEnable = true,
                CreatedAt = companyForUpdateDto.CreatedAt,
                UpdatedAt = DateTime.Now
            };
            _companyDal.Update(company);
            return new SuccessResult(Messages.CompanyUpdated);
        }
    }
}
