using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace User.Data.Models
{
    public class EMPLOYMENT
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string COMPANY_NAME { get; set; }

        [Required]
        public int MONTHS_OF_EXPERIENCE { get; set; }

        [Required]
        public int SALARY { get; set; }

        [Required]
        public DateTime STARTDATE { get; set; }
        public DateTime? ENDDATE { get; set; }

    }
}
