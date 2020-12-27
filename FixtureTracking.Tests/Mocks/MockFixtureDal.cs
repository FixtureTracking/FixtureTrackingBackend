using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.Entities.Concrete;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FixtureTracking.Tests.Mocks
{
    public class MockFixtureDal : Mock<IFixtureDal>
    {
        public MockFixtureDal MockGet(Fixture result)
        {
            Setup(x => x.Get(It.IsAny<Expression<Func<Fixture, bool>>>()))
               .Returns(result);

            return this;
        }

        public MockFixtureDal MockGetList(List<Fixture> results)
        {
            Setup(x => x.GetList(It.IsAny<Expression<Func<Fixture, bool>>>()))
               .Returns(results);

            return this;
        }

        public MockFixtureDal MockAdd(Fixture result)
        {
            Setup(x => x.Add(It.IsAny<Fixture>()))
               .Returns(result);

            return this;
        }

        public MockFixtureDal MockUpdate()
        {
            Setup(x => x.Update(It.IsAny<Fixture>()));

            return this;
        }

        public MockFixtureDal VerifyUpdate(Times times)
        {
            Verify(x => x.Update(It.IsAny<Fixture>()), times);

            return this;
        }

        public MockFixtureDal MockDelete()
        {
            Setup(x => x.Delete(It.IsAny<Fixture>()));

            return this;
        }

        public MockFixtureDal VerifyDelete(Times times)
        {
            Verify(x => x.Delete(It.IsAny<Fixture>()), times);

            return this;
        }
    }
}
