using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;
using System.Linq;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //ProductManager productManager = new ProductManager(new EfProductDal());
            

            //foreach (var product in productManager.GetByUnitPrice(70,120).Data)
            //{

            //    Console.WriteLine(product.ProductName);
            //}

        }
    }
}
