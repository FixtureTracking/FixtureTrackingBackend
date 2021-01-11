using FixtureTracking.Core.DataAccess;
using FixtureTracking.Core.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FixtureTracking.Business.Tests.Mocks.Repositories
{
    public class MockEntityRepository<TEntity, TRepository> : Mock<TRepository>
        where TEntity : class, IEntity, new()
        where TRepository : class, IEntityRepository<TEntity>
    {
        public MockEntityRepository<TEntity, TRepository> MockGet(TEntity result)
        {
            Setup(x => x.Get(It.IsAny<Expression<Func<TEntity, bool>>>()))
               .Returns(result);

            return this;
        }

        public MockEntityRepository<TEntity, TRepository> MockGetList(List<TEntity> results)
        {
            Setup(x => x.GetList(It.IsAny<Expression<Func<TEntity, bool>>>()))
               .Returns(results);

            return this;
        }

        public MockEntityRepository<TEntity, TRepository> MockAny(bool result)
        {
            Setup(x => x.Any(It.IsAny<Expression<Func<TEntity, bool>>>()))
               .Returns(result);

            return this;
        }

        public MockEntityRepository<TEntity, TRepository> MockAdd(TEntity result)
        {
            Setup(x => x.Add(It.IsAny<TEntity>()))
               .Returns(result);

            return this;
        }

        public MockEntityRepository<TEntity, TRepository> MockUpdate()
        {
            Setup(x => x.Update(It.IsAny<TEntity>()));

            return this;
        }

        public MockEntityRepository<TEntity, TRepository> VerifyUpdate(Times times)
        {
            Verify(x => x.Update(It.IsAny<TEntity>()), times);

            return this;
        }

        public MockEntityRepository<TEntity, TRepository> MockDelete()
        {
            Setup(x => x.Delete(It.IsAny<TEntity>()));

            return this;
        }

        public MockEntityRepository<TEntity, TRepository> VerifyDelete(Times times)
        {
            Verify(x => x.Delete(It.IsAny<TEntity>()), times);

            return this;
        }
    }
}
