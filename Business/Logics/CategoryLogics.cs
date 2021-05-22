using Business.Constant;
using Core.Utilities;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Logics
{
    public class CategoryLogics
    {
        public static IResult CheckIfCategoryAlreadyExist(ICategoryDal categoryDal, Category category)
        {
            var result = categoryDal.Get(c => c.CategoryName==category.CategoryName);

            if (result == null)
            {
                return new SuccessResult();
            }

            return new ErrorResult(Messages.CategoryAlreadyExists);

        }
    }
}
