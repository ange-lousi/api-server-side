using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace assignmentONE.Models
{
    public class Comments
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string IP { get; set; }

        public DateTime Time { get; set; }

        public string Comment { get; set; }
        public string Name { get; set; }
        

        
    }
}
