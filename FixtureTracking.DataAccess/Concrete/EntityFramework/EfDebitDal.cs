using FixtureTracking.Core.DataAccess.EntityFramework;
using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.DataAccess.Concrete.EntityFramework.Contexts;
using FixtureTracking.Entities.Concrete;
using FixtureTracking.Entities.Dtos.Debit;
using System.Collections.Generic;
using System.Linq;

namespace FixtureTracking.DataAccess.Concrete.EntityFramework
{
    public class EfDebitDal : EfEntityRepositoryBase<Debit, FixtureTrackingContext>, IDebitDal
    {
        public List<DebitForDetailDto> GetDetailList()
        {
            using var context = new FixtureTrackingContext();
            var result = from debit in context.Debits
                         join fixture in context.Fixtures
                         on debit.FixtureId equals fixture.Id
                         join user in context.Users
                         on debit.UserId equals user.Id
                         join department in context.Departments
                         on user.DepartmentId equals department.Id
                         select new DebitForDetailDto()
                         {
                             Debit = debit,
                             DepartmentName = department.Name,
                             FixtureDescription = fixture.Description,
                             FixtureName = fixture.Name,
                             FixturePictureUrl = fixture.PictureUrl,
                             UserFullName = $"{user.FirstName} {user.LastName}",
                         };
            return result.ToList();
        }
    }
}
