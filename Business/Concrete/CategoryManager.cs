using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constant;
using Business.Logics;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Validation;
using Core.Aspects.Autofac.Caching;
using Core.Utilities;
using Core.Utilities.Business;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        [ValidationAspect(typeof(CategoryValidator))]
        [SecuredOperation("user")]
        [CacheRemoveAspect("ICategoryService.Get")]
        public IResult Add(Category category)
        {
            var result = BusinessRules.Run(CommonLogics.SystemMaintenanceTime()
                ,CategoryLogics.CheckIfCategoryAlreadyExist(_categoryDal,category));
            if (!result.Success)
            {
                return new ErrorResult();
            }
            return new SuccessResult(Messages.CategoryAdded);
        }
        [SecuredOperation("user")]
        [CacheRemoveAspect("ICategoryService.Get")]
        public IResult Delete(Category category)
        {
            var result = BusinessRules.Run(CommonLogics.SystemMaintenanceTime());
            if (!result.Success)
            {
                return new ErrorResult(result.Message);
            }

            return new SuccessResult(Messages.CategoryDeleted);
        }
        [CacheAspect]
        public IDataResult<List<Category>> GetAll()
        {
            var result = BusinessRules.Run(CommonLogics.SystemMaintenanceTime());
            if (!result.Success)
            {
                return new ErrorDataResult<List<Category>>(result.Message);
            }
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll(), Messages.successTransaction);
        }
        [CacheAspect(10)]
        public IDataResult<Category> GetById(int id)
        {
            var result = BusinessRules.Run(CommonLogics.SystemMaintenanceTime());
            if (!result.Success)
            {
                return new ErrorDataResult<Category>(result.Message);
            }

            return new SuccessDataResult<Category>(_categoryDal.Get(c => c.CategoryId == id), Messages.successTransaction);

        }


        [ValidationAspect(typeof(CategoryValidator))]
        [SecuredOperation("user")]
        [CacheRemoveAspect("ICategoryService.Get")]
        public IResult Update(Category category)
        {
            var result = BusinessRules.Run(CommonLogics.SystemMaintenanceTime());
            if (!result.Success)
            {
                return new ErrorResult(result.Message);
            }

            return new SuccessResult(Messages.CategoryUpdated);
        }

       
    }
}
