using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
   public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            
            RuleFor(o => o.CustomerId).NotEmpty();
            RuleFor(o => o.EmployeeId).NotEmpty();
            RuleFor(o => o.ShipCity).NotEmpty();
            RuleFor(o => o.dateTime).NotEmpty();


        }
    }
}
