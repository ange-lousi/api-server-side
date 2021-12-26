using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using assignmentONE.Models;

namespace assignmentONE.Data
{
    public class DBStaffAPIRepo : IStaffAPIRepo
    {
        private readonly WebAPIDBContext _dbContext;

        public DBStaffAPIRepo(WebAPIDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Staff> GetAllStaff()
        {
            IEnumerable<Staff> staffs = _dbContext.Staffs.ToList<Staff>();
            return staffs;
        }

        public Staff GetStaffByID(int id)
        {
            Staff staff = _dbContext.Staffs.FirstOrDefault(e => e.Id == id);
            return staff;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
