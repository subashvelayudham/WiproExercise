using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace User.Data.Models
{
    public class USER
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string FIRST_NAME { get; set; }

        [Required]
        public string LAST_NAME { get; set; }

        [Required]
        public string EMAIL { get; set; }
        public int ADDRESS_ID { get; set; }

    }
}
