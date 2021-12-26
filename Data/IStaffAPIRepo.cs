using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using assignmentONE.Models;


namespace assignmentONE.Data
{
    public interface IStaffAPIRepo
    {
        IEnumerable<Staff> GetAllStaff();
        Staff GetStaffByID(int id);
        void SaveChanges();
    }
}
