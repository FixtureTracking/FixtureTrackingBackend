using FixtureTracking.Core.Utilities.Results;
using FixtureTracking.Entities.Concrete;
using FixtureTracking.Entities.Dtos.Category;
using System.Collections.Generic;

namespace FixtureTracking.Business.Abstract
{
    public interface ICategoryService
    {
        IDataResult<Category> GetById(short categoryId);
        IDataResult<List<Category>> GetList();
        IDataResult<short> Add(CategoryForAddDto categoryForAddDto);
        IResult Update(Category category);
        IResult Delete(short categoryId);
    }
}
