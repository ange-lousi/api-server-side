using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using assignmentONE.Models;


namespace assignmentONE.Data
{
    public class DBProductAPIRepo : IProductAPIRepo
    {
        private readonly WebAPIDBContext _dbContext;

        public DBProductAPIRepo(WebAPIDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Product> GetAllProduct()
        {
            IEnumerable<Product> products = _dbContext.Products.ToList<Product>();
            return products;
        }

        public IEnumerable<Product> GetProductByName(string n)
        {
            IEnumerable<Product> products = _dbContext.Products.Where(e => e.Name.ToLower().Contains(n.ToLower())).ToList<Product>();
            return products;
        }


        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

    }
}
