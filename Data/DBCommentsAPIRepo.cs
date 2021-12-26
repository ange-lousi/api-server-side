using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using assignmentONE.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace assignmentONE.Data
{
    public class DBCommentsAPIRepo : ICommentsAPIRepo
    {
        private readonly WebAPIDBContext _dbContext;

        public DBCommentsAPIRepo(WebAPIDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Comments AddComment(Comments comment)
        {
            EntityEntry<Comments> e = _dbContext.theComments.Add(comment);
            Comments c = e.Entity;
            _dbContext.SaveChanges();
            return c;
        }
        public IEnumerable<Comments> GetAllComment()
        {
            IEnumerable<Comments> comment = _dbContext.theComments.ToList<Comments>();
            return comment;
        }
       
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
