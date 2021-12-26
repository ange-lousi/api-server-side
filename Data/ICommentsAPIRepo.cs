using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using assignmentONE.Models;

namespace assignmentONE.Data
{
    public interface ICommentsAPIRepo
    {
        IEnumerable<Comments> GetAllComment(); 
        Comments AddComment(Comments comment);
        void SaveChanges();
    }
}
