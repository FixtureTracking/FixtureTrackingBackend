using FixtureTracking.Business.Abstract;
using FixtureTracking.Entities.Concrete;
using Moq;
using System;
using System.Collections.Generic;

namespace FixtureTracking.Business.Tests.Mocks.Services
{
    public class MockDebitService : Mock<IDebitService>
    {
        public MockDebitService MockGetListByUserId(List<Debit> result)
        {
            Setup(x => x.GetListByUserId(It.IsAny<Guid>()))
                .Returns(result);

            return this;
        }
    }
}
