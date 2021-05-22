using Business.Constant;
using Core.Utilities;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Logics
{
    public class ProductLogics
    {
        public static IResult CheckPriceOfProduct(IProductDal productDal,Product product)
        {
            var result = productDal.Get(p => p.ProductName == product.ProductName && p.CategoryId == product.CategoryId && p.UnitPrice!=product.UnitPrice);

            if (result == null)
            {
                return new SuccessResult();
            }

            return new ErrorResult(Messages.PriceError);

        }
    }
}
