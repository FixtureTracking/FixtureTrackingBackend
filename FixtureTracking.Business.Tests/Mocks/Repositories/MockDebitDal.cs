using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.Entities.Concrete;
using FixtureTracking.Entities.Dtos.Debit;
using System.Collections.Generic;

namespace FixtureTracking.Business.Tests.Mocks.Repositories
{
    public class MockDebitDal : MockEntityRepository<Debit, IDebitDal>
    {
        public MockDebitDal MockGetDetailList(List<DebitForDetailDto> result)
        {
            Setup(x => x.GetDetailList())
                .Returns(result);

            return this;
        }
    }
}
