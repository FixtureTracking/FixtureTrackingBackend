using FixtureTracking.Core.DataAccess.EntityFramework;
using FixtureTracking.Core.Entities.Concrete;
using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.DataAccess.Concrete.EntityFramework.Contexts;
using FixtureTracking.Entities.Dtos.Debit;
using FixtureTracking.Entities.Dtos.User;
using System.Collections.Generic;
using System.Linq;

namespace FixtureTracking.DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, FixtureTrackingContext>, IUserDal
    {
        public string[] GetClaims(User user)
        {
            using var context = new FixtureTrackingContext();
            var result = from department in context.Departments
                         where department.Id == user.DepartmentId
                         select department.OperationClaimNames;
            return result.FirstOrDefault();
        }

        public List<DebitForFixtureDetailDto> GetDebits(User user)
        {
            using var context = new FixtureTrackingContext();
            var result = from debit in context.Debits
                         join fixture in context.Fixtures
                         on debit.FixtureId equals fixture.Id
                         where debit.UserId == user.Id
                         select new DebitForFixtureDetailDto()
                         {
                             Debit = debit,
                             FixtureDescription = fixture.Description,
                             FixtureName = fixture.Name,
                             FixturePictureUrl = fixture.PictureUrl
                         };
            return result.ToList();
        }

        public UserForDetailDto GetDetail(User user)
        {
            using var context = new FixtureTrackingContext();
            var result = from department in context.Departments
                         where department.Id == user.DepartmentId
                         select new UserForDetailDto()
                         {
                             User = user,
                             DepartmentName = department.Name,
                             FullName = $"{user.FirstName} {user.LastName}"
                         };
            return result.FirstOrDefault();
        }

        public List<UserForDetailDto> GetDetailList()
        {
            using var context = new FixtureTrackingContext();
            var result = from user in context.Users
                         join department in context.Departments
                         on user.DepartmentId equals department.Id
                         where user.IsEnable == true
                         select new UserForDetailDto()
                         {
                             User = user,
                             DepartmentName = department.Name,
                             FullName = $"{user.FirstName} {user.LastName}"
                         };
            return result.ToList();
        }
    }
}
