using FixtureTracking.Business.Abstract;
using FixtureTracking.Business.Constants;
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

        public IResult Delete(Guid debitId)
        {
            var debit = GetById(debitId).Data;
            if (debit != null)
            {
                debitDal.Delete(debit);
                return new SuccessResult(Messages.DebitDeleted);
            }
            return new ErrorResult(Messages.DebitNotFound);
        }

        public IDataResult<Debit> GetById(Guid debitId)
        {
            return new SuccessDataResult<Debit>(debitDal.Get(d => d.Id == debitId));
        }

        public IDataResult<List<Debit>> GetList()
        {
            return new SuccessDataResult<List<Debit>>(debitDal.GetList().ToList());
        }

        public IDataResult<List<Debit>> GetListByFixtureId(Guid fixtureId)
        {
            return new SuccessDataResult<List<Debit>>(debitDal.GetList(d => d.FixtureId == fixtureId).ToList());
        }

        public IDataResult<List<Debit>> GetListByUserId(Guid userId)
        {
            return new SuccessDataResult<List<Debit>>(debitDal.GetList(d => d.UserId == userId).ToList());
        }

        public IResult Update(Debit debit)
        {
            if (GetById(debit.Id).Data != null)
            {
                debitDal.Update(debit);
                return new SuccessResult(Messages.DebitUpdated);
            }
            return new ErrorResult(Messages.DebitNotFound);
        }
    }
}
