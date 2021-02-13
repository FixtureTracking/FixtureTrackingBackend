using FixtureTracking.Core.DataAccess.EntityFramework;
using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.DataAccess.Concrete.EntityFramework.Contexts;
using FixtureTracking.Entities.Concrete;
using FixtureTracking.Entities.Dtos.Debit;
using FixtureTracking.Entities.Dtos.Fixture;
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

        public List<FixtureForDetailDto> GetDetailList()
        {
            var context = new FixtureTrackingContext();
            var result = from fixture in context.Fixtures
                         join category in context.Categories
                         on fixture.CategoryId equals category.Id
                         join supplier in context.Suppliers
                         on fixture.SupplierId equals supplier.Id
                         join fixturePosition in context.FixturePositions
                         on fixture.FixturePositionId equals fixturePosition.Id
                         where fixture.FixturePositionId != 0
                         select new FixtureForDetailDto()
                         {
                             Fixture = fixture,
                             CategoryName = category.Name,
                             FixturePosName = fixturePosition.Name,
                             SupplierName = supplier.Name
                         };
            return result.ToList();
        }
    }
}
