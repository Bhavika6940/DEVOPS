using System;
using System.Collections.Generic;
using System.Linq;
using ProductService.Models;
namespace ProductService.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product GetById(Guid id);
        Product Add(Product product);
        Product Update(Product product);
        bool Delete(Guid id);
    }
    public class ProductService : IProductService
    {
        private readonly List<Product> _products;
        public ProductService()
        {
            _products = new List<Product>();
        }
        public IEnumerable<Product> GetAll()
        {
            return _products;
        }
        public Product GetById(Guid id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }
        public Product Add(Product product)
        {
            if (product.Id == Guid.Empty)
            {
                product.Id = Guid.NewGuid();
            }
            _products.Add(product);
            return product;
        }
        public Product Update(Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id ==
            product.Id);
            if (existingProduct == null)
            {
                return null;
            }
            _products.Remove(existingProduct);
            _products.Add(product);
            return product;
        }
        public bool Delete(Guid id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return false;
            }
            return _products.Remove(product);
        }
    }
}