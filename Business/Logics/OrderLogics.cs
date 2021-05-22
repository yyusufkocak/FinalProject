using Business.Constant;
using Core.Utilities;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Logics
{
    public class OrderLogics
    {
        public static IResult CheckOrderLimit(IOrderDal orderDal, Order order)
        {
            var result = orderDal.GetAll(o => o.CustomerId == order.CustomerId && o.dateTime == order.dateTime).Count;



            if (result! > 10)
            {
                return new SuccessResult();
            }

            return new ErrorResult(Messages.OrderLimit);

        }
    }
}
