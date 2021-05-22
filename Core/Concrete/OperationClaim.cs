using Core.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Concrete
{
    public class OperationClaim : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
