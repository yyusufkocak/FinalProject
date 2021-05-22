using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constant;
using Business.Logics;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Validation;
using Core.Aspects.Autofac.Caching;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities;
using Core.Utilities.Business;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }


        [ValidationAspect(typeof(ProductValidator))]
        [SecuredOperation("product.add,admin")]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            var result = BusinessRules.Run(CommonLogics.SystemMaintenanceTime(),ProductLogics.CheckPriceOfProduct(_productDal,product));
            if (!result.Success)
            {
                return new ErrorResult(result.Message);
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Delete(Product product)
        {
            var result = BusinessRules.Run(CommonLogics.SystemMaintenanceTime());
            if (!result.Success)
            {
                return new ErrorResult(result.Message);
            }
            return new SuccessResult(Messages.ProductDeleted);
        }
        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
            var result = BusinessRules.Run(CommonLogics.SystemMaintenanceTime());
            if (!result.Success)
            {
                return new ErrorDataResult<List<Product>>(result.Message);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll().ToList(), Messages.successTransaction);
        }
        [SecuredOperation("product.list,admin")]
        [CacheAspect(duration: 10)]
        public IDataResult<List<Product>> GetByCategoryId(int id)
        {
            var result = BusinessRules.Run(CommonLogics.SystemMaintenanceTime());
            if (!result.Success)
            {
                return new ErrorDataResult<List<Product>>(result.Message);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id), Messages.successTransaction);

        }
        [CacheAspect(10)]
        public IDataResult<Product> GetById(int id)
        {
            var result = BusinessRules.Run(CommonLogics.SystemMaintenanceTime());
            if (!result.Success)
            {
                return new ErrorDataResult<Product>(result.Message);
            }
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == id), Messages.successTransaction);
        }
        [CacheAspect]
        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            var result = BusinessRules.Run(CommonLogics.SystemMaintenanceTime());
            if (!result.Success)
            {
                return new ErrorDataResult<List<Product>>(result.Message);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice > min && p.UnitPrice < max), Messages.successTransaction);

        }
        [CacheAspect]
        public IDataResult<List<ProductDetailDto>> GetProductdetails()
        {
            var result = BusinessRules.Run(CommonLogics.SystemMaintenanceTime());
            if (!result.Success)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(result.Message);
            }

            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails(), Messages.successTransaction);
        }
        [ValidationAspect(typeof(ProductValidator))]
        [SecuredOperation("admin")]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            var result = BusinessRules.Run(CommonLogics.SystemMaintenanceTime());
            if (!result.Success)
            {
                return new ErrorResult(result.Message);
            }
            return new SuccessResult(Messages.ProductUpdated);
        }


    }
}
