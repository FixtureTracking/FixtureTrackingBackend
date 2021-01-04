using FixtureTracking.Business.Abstract;
using FixtureTracking.Core.Utilities.Results;
using Moq;

namespace FixtureTracking.Business.Tests.Mocks.Services
{
    public class MockDepartmentService : Mock<IDepartmentService>
    {
        public MockDepartmentService MockGetOperationClaimNames(IDataResult<string[]> result)
        {
            Setup(x => x.GetOperationClaimNames(It.IsAny<int>()))
                .Returns(result);

            return this;
        }
    }
}
