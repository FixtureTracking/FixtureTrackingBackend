﻿using FixtureTracking.Business.Abstract;
using FixtureTracking.Business.BusinessAspects.Autofac;
using FixtureTracking.Business.Constants;
using FixtureTracking.Business.ValidationRules.FluentValidation.CategoryValidations;
using FixtureTracking.Core.Aspects.Autofac.Caching;
using FixtureTracking.Core.Aspects.Autofac.Performance;
using FixtureTracking.Core.Aspects.Autofac.Validation;
using FixtureTracking.Core.CrossCuttingConcerns.Logging.NLog.Loggers;
using FixtureTracking.Core.Utilities.CustomExceptions;
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

        [SecuredOperationAspect("Category.Add")]
        [ValidationAspect(typeof(CategoryForAddValidator))]
        [CacheRemoveAspect("ICategoryService.Get")]
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

        [SecuredOperationAspect("Category.Delete")]
        [CacheRemoveAspect("ICategoryService.Get")]
        public IResult Delete(short categoryId)
        {
            var category = GetById(categoryId).Data;
            category.IsEnable = false;
            category.UpdatedAt = DateTime.Now;

            categoryDal.Update(category);
            return new SuccessResult(Messages.CategoryDeleted);
        }

        [SecuredOperationAspect("Category.Get")]
        [CacheAspect()]
        public IDataResult<Category> GetById(short categoryId)
        {
            var category = categoryDal.Get(c => c.Id == categoryId);
            if (category != null)
                return new SuccessDataResult<Category>(category);
            throw new ObjectNotFoundException(Messages.CategoryNotFound);
        }

        [PerformanceLogAspect(1, typeof(FileLogger))]
        [SecuredOperationAspect("Category.GetFixtures")]
        [CacheAspect(duration: 2)]
        public IDataResult<List<Fixture>> GetFixtures(short categoryId)
        {
            var category = GetById(categoryId).Data;
            return new SuccessDataResult<List<Fixture>>(categoryDal.GetFixtures(category));
        }

        [PerformanceLogAspect(1, typeof(FileLogger))]
        [SecuredOperationAspect("Category.List")]
        [CacheAspect(duration: 2)]
        public IDataResult<List<Category>> GetList()
        {
            return new SuccessDataResult<List<Category>>(categoryDal.GetList(c => c.IsEnable == true).ToList());
        }

        [SecuredOperationAspect("Category.Update")]
        [ValidationAspect(typeof(CategoryForUpdateValidator))]
        [CacheRemoveAspect("ICategoryService.Get")]
        public IResult Update(CategoryForUpdateDto categoryForUpdateDto)
        {
            var category = GetById(categoryForUpdateDto.Id).Data;
            category.Description = categoryForUpdateDto.Description;
            category.Name = categoryForUpdateDto.Name;
            category.UpdatedAt = DateTime.Now;

            categoryDal.Update(category);
            return new SuccessResult(Messages.CategoryUpdated);
        }
    }
}
