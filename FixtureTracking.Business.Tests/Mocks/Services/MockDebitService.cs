using FixtureTracking.Business.Abstract;
using FixtureTracking.Core.Utilities.Results;
using FixtureTracking.Entities.Concrete;
using Moq;
using System;
using System.Collections.Generic;

namespace FixtureTracking.Business.Tests.Mocks.Services
{
    public class MockDebitService : Mock<IDebitService>
    {
        public MockDebitService MockGetListByFixtureId(IDataResult<List<Debit>> result)
        {
            Setup(x => x.GetListByFixtureId(It.IsAny<Guid>()))
                .Returns(result);

            return this;
        }
    }
}
