using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;
        public InMemoryProductDal()
        {
            _products = new List<Product> {
            new Product{ProductId=1,CategoryId=4,UnitPrice=13,ProductName="Bardak",UnitsInStock=50},
            new Product{ProductId=2,CategoryId=4,UnitPrice=10,ProductName="Çay Bardağı",UnitsInStock=50},
            new Product{ProductId=3,CategoryId=3,UnitPrice=12,ProductName="Kalem",UnitsInStock=50},
            new Product{ProductId=4,CategoryId=2,UnitPrice=15,ProductName="Sabun",UnitsInStock=50},
            new Product{ProductId=5,CategoryId=1,UnitPrice=9,ProductName="Çorap",UnitsInStock=50}
            };
        }

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            var productToDelete = _products.SingleOrDefault(p=>p.ProductId==product.ProductId);
            _products.Remove(productToDelete);
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            return _products.Where(p=>p.CategoryId==categoryId).ToList();
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            var productToUpdate = _products.SingleOrDefault(p=>p.ProductId==product.ProductId);
            productToUpdate.ProductId = product.ProductId;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
        }
    }
}
