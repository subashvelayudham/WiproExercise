using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace User.Data.Models
{
    public class ADDRESS
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string STREET { get; set; }

        [Required]
        public string CITY { get; set; }

        public int? POSTCODE { get; set; }

    }
}
