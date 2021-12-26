using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using assignmentONE.Models;


namespace assignmentONE.Data
{
    public interface IProductAPIRepo
    {
        IEnumerable<Product> GetAllProduct();
        IEnumerable<Product> GetProductByName(string n);
 
        void SaveChanges();
       
    }
}
