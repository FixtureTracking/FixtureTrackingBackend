﻿using FixtureTracking.Business.Abstract;
using FixtureTracking.Business.Constants;
using FixtureTracking.Core.Utilities.Results;
using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.Entities.Concrete;
using FixtureTracking.Entities.Dtos.Category;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FixtureTracking.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            this.categoryDal = categoryDal;
        }

        public IDataResult<short> Add(CategoryForAddDto categoryForAddDto)
        {
            var category = new Category()
            {
                CreatedAt = DateTime.Now,
                Description = categoryForAddDto.Description,
                IsEnable = true,
                Name = categoryForAddDto.Name,
                UpdatedAt = DateTime.Now
            };
            categoryDal.Add(category);
            return new SuccessDataResult<short>(category.Id, Messages.CategoryAdded);
        }

        public IResult Delete(short categoryId)
        {
            var category = GetById(categoryId).Data;
            if (category != null)
            {
                category.IsEnable = false;
                category.UpdatedAt = DateTime.Now;

                categoryDal.Update(category);
                return new SuccessResult(Messages.CategoryDeleted);
            }
            return new ErrorResult(Messages.CategoryNotFound);
        }

        public IDataResult<Category> GetById(short categoryId)
        {
            return new SuccessDataResult<Category>(categoryDal.Get(c => c.Id == categoryId));
        }

        public IDataResult<List<Category>> GetList()
        {
            return new SuccessDataResult<List<Category>>(categoryDal.GetList(c => c.IsEnable == true).ToList());
        }

        public IResult Update(CategoryForUpdateDto categoryForUpdateDto)
        {
            var category = GetById(categoryForUpdateDto.Id).Data;
            if (category != null)
            {
                category.Description = categoryForUpdateDto.Description;
                category.Name = categoryForUpdateDto.Name;
                category.UpdatedAt = DateTime.Now;

                categoryDal.Update(category);
                return new SuccessResult(Messages.CategoryUpdated);
            }
            return new ErrorResult(Messages.CategoryNotFound);
        }
    }
}