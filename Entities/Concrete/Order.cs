using Core.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Order:IEntity
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; } //veritabanı dışardan çekildiği için bu alan string olmak zorunda oldu.
        public int EmployeeId { get; set; }
        public DateTime dateTime { get; set; }
        public string ShipCity { get; set; }
    }
}
