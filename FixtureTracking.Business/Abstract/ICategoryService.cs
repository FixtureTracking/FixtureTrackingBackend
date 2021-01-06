using FixtureTracking.Core.Utilities.Results;
using FixtureTracking.Entities.Concrete;
using FixtureTracking.Entities.Dtos.Category;
using System;
using System.Collections.Generic;

namespace FixtureTracking.Business.Abstract
{
    public interface ICategoryService
    {
        IDataResult<Category> GetById(short categoryId);
        IDataResult<List<Category>> GetList();
        IDataResult<List<Fixture>> GetFixtures(short categoryId);
        IDataResult<short> Add(CategoryForAddDto categoryForAddDto);
        IResult Update(CategoryForUpdateDto categoryForUpdateDto);
        IResult Delete(short categoryId);
    }
}
