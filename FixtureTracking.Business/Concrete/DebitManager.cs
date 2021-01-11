using FixtureTracking.Business.Abstract;
using FixtureTracking.Business.BusinessAspects.Autofac;
using FixtureTracking.Business.Constants;
using FixtureTracking.Business.ValidationRules.FluentValidation.DebitValidations;
using FixtureTracking.Core.Aspects.Autofac.Caching;
using FixtureTracking.Core.Aspects.Autofac.Validation;
using FixtureTracking.Core.Utilities.Results;
using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.Entities.Concrete;
using FixtureTracking.Entities.Dtos.Debit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FixtureTracking.Business.Concrete
{
    public class DebitManager : IDebitService
    {
        private readonly IDebitDal debitDal;

        public DebitManager(IDebitDal debitDal)
        {
            this.debitDal = debitDal;
        }

        [SecuredOperationAspect("Debit.Add", Priority = 1)]
        [ValidationAspect(typeof(DebitForAddValidator))]
        [CacheRemoveAspect("IFixtureService.GetDebits")]
        [CacheRemoveAspect("IUserService.GetDebits")]
        public IDataResult<Guid> Add(DebitForAddDto debitForAddDto)
        {
            var debit = new Debit()
            {
                CreatedAt = DateTime.Now,
                DateDebit = debitForAddDto.DateDebit,
                DateReturn = DateTime.MaxValue,
                Description = debitForAddDto.Description,
                FixtureId = debitForAddDto.FixtureId,
                IsReturn = false,
                UpdatedAt = DateTime.Now,
                UserId = debitForAddDto.UserId
            };
            debitDal.Add(debit);
            return new SuccessDataResult<Guid>(debit.Id, Messages.DebitAdded);
        }

        [SecuredOperationAspect("Debit.Delete", Priority = 1)]
        [CacheRemoveAspect("IFixtureService.GetDebits")]
        [CacheRemoveAspect("IUserService.GetDebits")]
        public IResult Delete(Guid debitId)
        {
            var debit = GetById(debitId).Data;
            if (debit != null)
            {
                debit.IsReturn = true;
                debit.DateReturn = DateTime.Now;
                debit.UpdatedAt = DateTime.Now;

                debitDal.Update(debit);
                return new SuccessResult(Messages.DebitDeleted);
            }
            return new ErrorResult(Messages.DebitNotFound);
        }

        [SecuredOperationAspect("Debit.Get", Priority = 1)]
        [CacheAspect()]
        public IDataResult<Debit> GetById(Guid debitId)
        {
            return new SuccessDataResult<Debit>(debitDal.Get(d => d.Id == debitId));
        }

        [SecuredOperationAspect("Debit.List", Priority = 1)]
        [CacheAspect(duration: 2)]
        public IDataResult<List<Debit>> GetList()
        {
            return new SuccessDataResult<List<Debit>>(debitDal.GetList().ToList());
        }

        [SecuredOperationAspect("Debit.List", Priority = 1)]
        [CacheAspect(duration: 2)]
        public List<Debit> GetListByFixtureId(Guid fixtureId)
        {
            return debitDal.GetList(d => d.FixtureId == fixtureId).ToList();
        }

        [SecuredOperationAspect("Debit.List", Priority = 1)]
        [CacheAspect(duration: 2)]
        public List<Debit> GetListByUserId(Guid userId)
        {
            return debitDal.GetList(d => d.UserId == userId).ToList();
        }

        [SecuredOperationAspect("Debit.Update", Priority = 1)]
        [ValidationAspect(typeof(DebitForUpdateValidator))]
        [CacheRemoveAspect("IFixtureService.GetDebits")]
        [CacheRemoveAspect("IUserService.GetDebits")]
        public IResult Update(DebitForUpdateDto debitForUpdateDto)
        {
            var debit = GetById(debitForUpdateDto.Id).Data;
            if (debit != null)
            {
                debit.DateDebit = debitForUpdateDto.DateDebit;
                debit.Description = debitForUpdateDto.Description;
                debit.FixtureId = debitForUpdateDto.FixtureId;
                debit.UserId = debitForUpdateDto.UserId;
                debit.UpdatedAt = DateTime.Now;

                debitDal.Update(debit);
                return new SuccessResult(Messages.DebitUpdated);
            }
            return new ErrorResult(Messages.DebitNotFound);
        }
    }
}
