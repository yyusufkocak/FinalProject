using Business.Abstract;
using Business.Constant;
using Core.Utilities;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess;
using Core.Aspect.Autofac.Validation;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Business;
using Business.Logics;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;;
        }
        [ValidationAspect(typeof(OrderValidator))]
        [SecuredOperation("order.add")]
        [CacheRemoveAspect("IOrderService.Get")]
        public IResult Add(Order order)
        {
            var result = BusinessRules.Run(CommonLogics.SystemMaintenanceTime(),OrderLogics.CheckOrderLimit(_orderDal,order));
            if (!result.Success)
            {
                return new ErrorResult(result.Message);
            }

            _orderDal.Add(order);
            return new SuccessResult(Messages.OrderAdded);
        }

        [SecuredOperation("order.delete")]
        [CacheRemoveAspect("IOrderService.Get")]
        public IResult Delete(Order order)
        {
            var result = BusinessRules.Run(CommonLogics.SystemMaintenanceTime());
            if (!result.Success)
            {
                return new ErrorResult(result.Message);
            }

            _orderDal.Delete(order);

            return new SuccessResult(Messages.OrderDeleted);
        }
        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<Order>> GetAll()
        {
            var result = BusinessRules.Run(CommonLogics.SystemMaintenanceTime());
            if (!result.Success)
            {
                return new ErrorDataResult<List<Order>>(result.Message);
            }

            return new SuccessDataResult<List<Order>>(_orderDal.GetAll(), Messages.successTransaction);
        }

        [CacheAspect(5)]
        [PerformanceAspect(5)]
        public IDataResult<Order> GetById(int id)
        {
            var result = BusinessRules.Run(CommonLogics.SystemMaintenanceTime());
            if (!result.Success)
            {
                return new ErrorDataResult<Order>(result.Message);
            }

            return new SuccessDataResult<Order>(_orderDal.Get(o => o.OrderId == id), Messages.successTransaction);

        }

        [ValidationAspect(typeof(OrderValidator))]
        [SecuredOperation("order.update")]
        [CacheRemoveAspect("IOrderService.Get")]
        public IResult Update(Order order)
        {
            var result = BusinessRules.Run(CommonLogics.SystemMaintenanceTime());
            if (!result.Success)
            {
                return new ErrorResult(result.Message);
            }

            _orderDal.Update(order);
            return new SuccessResult(Messages.OrderUpdated);
        }
    }
}
