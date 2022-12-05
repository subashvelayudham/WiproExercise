using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace User.Data.Models
{
    public class USER_EMPLOYMENT
    {
        [Key]
        public int ID { get; set; }
        public int USER_ID { get; set; }
        public int EMPLOYMENT_ID { get; set; }
    }
}
