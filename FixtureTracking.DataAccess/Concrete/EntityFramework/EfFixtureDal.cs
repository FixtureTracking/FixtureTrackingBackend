using FixtureTracking.Core.DataAccess.EntityFramework;
using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.DataAccess.Concrete.EntityFramework.Contexts;
using FixtureTracking.Entities.Concrete;
using FixtureTracking.Entities.Dtos.Debit;
using System.Collections.Generic;
using System.Linq;

namespace FixtureTracking.DataAccess.Concrete.EntityFramework
{
    public class EfFixtureDal : EfEntityRepositoryBase<Fixture, FixtureTrackingContext>, IFixtureDal
    {
        public List<DebitForUserDetailDto> GetDebits(Fixture fixture)
        {
            using var context = new FixtureTrackingContext();
            var result = from debit in context.Debits
                         join user in context.Users
                         on debit.UserId equals user.Id
                         join department in context.Departments
                         on user.DepartmentId equals department.Id
                         where debit.FixtureId == fixture.Id
                         select new DebitForUserDetailDto()
                         {
                             Debit = debit,
                             DepartmentName = department.Name,
                             UserFullName = $"{user.FirstName} {user.LastName}"

                         };
            return result.ToList();
        }
    }
}
