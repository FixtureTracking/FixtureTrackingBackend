﻿using FixtureTracking.Business.Abstract;
using FixtureTracking.Business.Constants;
using FixtureTracking.Core.Utilities.Results;
using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.Entities.Concrete;
using FixtureTracking.Entities.Dtos.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FixtureTracking.Business.Concrete
{
    public class SupplierManager : ISupplierService
    {
        private readonly ISupplierDal supplierDal;
        private readonly IFixtureService fixtureService;

        public SupplierManager(ISupplierDal supplierDal, IFixtureService fixtureService)
        {
            this.supplierDal = supplierDal;
            this.fixtureService = fixtureService;
        }

        public IDataResult<int> Add(SupplierForAddDto supplierForAddDto)
        {
            var supplier = new Supplier()
            {
                CreatedAt = DateTime.Now,
                Description = supplierForAddDto.Description,
                IsEnable = true,
                Name = supplierForAddDto.Name,
                UpdatedAt = DateTime.Now
            };
            supplierDal.Add(supplier);
            return new SuccessDataResult<int>(supplier.Id, Messages.SupplierAdded);
        }

        public IResult Delete(int supplierId)
        {
            var supplier = GetById(supplierId).Data;
            if (supplier != null)
            {
                supplier.IsEnable = false;
                supplier.UpdatedAt = DateTime.Now;

                supplierDal.Update(supplier);
                return new SuccessResult(Messages.SupplierDeleted);
            }
            return new ErrorResult(Messages.SupplierNotFound);
        }

        public IDataResult<Supplier> GetById(int supplierId)
        {
            return new SuccessDataResult<Supplier>(supplierDal.Get(s => s.Id == supplierId));
        }

        public IDataResult<List<Fixture>> GetFixtures(int supplierId)
        {
            var supplier = GetById(supplierId).Data;
            if (supplier != null)
                return new ErrorDataResult<List<Fixture>>(fixtureService.GetListBySupplierId(supplierId));
            return new ErrorDataResult<List<Fixture>>(Messages.SupplierNotFound);
        }

        public IDataResult<List<Supplier>> GetList()
        {
            return new SuccessDataResult<List<Supplier>>(supplierDal.GetList(s => s.IsEnable == true).ToList());
        }

        public IResult Update(SupplierForUpdateDto supplierForUpdateDto)
        {
            var supplier = GetById(supplierForUpdateDto.Id).Data;
            if (supplier != null)
            {
                supplier.Description = supplierForUpdateDto.Description;
                supplier.Name = supplierForUpdateDto.Name;
                supplier.UpdatedAt = DateTime.Now;

                supplierDal.Update(supplier);
                return new SuccessResult(Messages.SupplierUpdated);
            }
            return new ErrorResult(Messages.SupplierNotFound);
        }
    }
}
