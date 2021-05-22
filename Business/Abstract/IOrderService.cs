using Core.Utilities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
   public interface IOrderService
    {
        IDataResult<List<Order>> GetAll();
        IDataResult<Order> GetById(int id);
        IResult Add(Order order);
        IResult Delete(Order order);
        IResult Update(Order order);
    }
}
