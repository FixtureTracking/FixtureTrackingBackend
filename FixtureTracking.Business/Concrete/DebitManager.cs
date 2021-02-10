using FixtureTracking.Business.Abstract;
using FixtureTracking.Business.BusinessAspects.Autofac;
using FixtureTracking.Business.Constants;
using FixtureTracking.Business.ValidationRules.FluentValidation.DebitValidations;
using FixtureTracking.Core.Aspects.Autofac.Caching;
using FixtureTracking.Core.Aspects.Autofac.Performance;
using FixtureTracking.Core.Aspects.Autofac.Validation;
using FixtureTracking.Core.CrossCuttingConcerns.Logging.NLog.Loggers;
using FixtureTracking.Core.Utilities.CustomExceptions;
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
        private readonly IFixtureService fixtureService;

        public DebitManager(IDebitDal debitDal, IFixtureService fixtureService)
        {
            this.debitDal = debitDal;
            this.fixtureService = fixtureService;
        }

        [SecuredOperationAspect("Debit.Add")]
        [ValidationAspect(typeof(DebitForAddValidator))]
        [CacheRemoveAspect("IFixtureService.GetDebits")]
        [CacheRemoveAspect("IUserService.GetDebits")]
        public IDataResult<Guid> Add(DebitForAddDto debitForAddDto)
        {
            var fixture = fixtureService.GetById(debitForAddDto.FixtureId).Data;
            if (fixture.FixturePositionId != (short)FixturePositions.Position.Available)
                throw new LogicException(Messages.DebitFixtureIsNotAvailable);

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

            fixtureService.UpdatePosition(debitForAddDto.FixtureId, FixturePositions.Position.Debit);
            return new SuccessDataResult<Guid>(debit.Id, Messages.DebitAdded);
        }

        [SecuredOperationAspect("Debit.Delete")]
        [CacheRemoveAspect("IFixtureService.GetDebits")]
        [CacheRemoveAspect("IUserService.GetDebits")]
        public IResult Delete(Guid debitId)
        {
            var debit = GetById(debitId).Data;
            debit.IsReturn = true;
            debit.DateReturn = DateTime.Now;
            debit.UpdatedAt = DateTime.Now;

            debitDal.Update(debit);
            return new SuccessResult(Messages.DebitDeleted);
        }

        [SecuredOperationAspect("Debit.Get")]
        [CacheAspect()]
        public IDataResult<Debit> GetById(Guid debitId)
        {
            var debit = debitDal.Get(d => d.Id == debitId);
            if (debit != null)
                return new SuccessDataResult<Debit>(debit);
            throw new ObjectNotFoundException(Messages.DebitNotFound);
        }

        [PerformanceLogAspect(1, typeof(FileLogger))]
        [SecuredOperationAspect("Debit.List")]
        [CacheAspect(duration: 2)]
        public IDataResult<List<Debit>> GetList()
        {
            return new SuccessDataResult<List<Debit>>(debitDal.GetList().ToList());
        }

        [PerformanceLogAspect(1, typeof(FileLogger))]
        [SecuredOperationAspect("Debit.List")]
        [CacheAspect(duration: 2)]
        public List<Debit> GetListByFixtureId(Guid fixtureId)
        {
            return debitDal.GetList(d => d.FixtureId == fixtureId).ToList();
        }

        [PerformanceLogAspect(1, typeof(FileLogger))]
        [SecuredOperationAspect("Debit.List")]
        [CacheAspect(duration: 2)]
        public List<Debit> GetListByUserId(Guid userId)
        {
            return debitDal.GetList(d => d.UserId == userId).ToList();
        }

        [SecuredOperationAspect("Debit.Update")]
        [ValidationAspect(typeof(DebitForUpdateValidator))]
        [CacheRemoveAspect("IFixtureService.GetDebits")]
        [CacheRemoveAspect("IUserService.GetDebits")]
        public IResult Update(DebitForUpdateDto debitForUpdateDto)
        {
            var debit = GetById(debitForUpdateDto.Id).Data;
            debit.DateDebit = debitForUpdateDto.DateDebit;
            debit.Description = debitForUpdateDto.Description;
            debit.FixtureId = debitForUpdateDto.FixtureId;
            debit.UserId = debitForUpdateDto.UserId;
            debit.UpdatedAt = DateTime.Now;

            debitDal.Update(debit);
            return new SuccessResult(Messages.DebitUpdated);
        }
    }
}
