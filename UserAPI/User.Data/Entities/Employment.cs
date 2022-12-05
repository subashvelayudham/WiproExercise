using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Data.Entities
{
    public class Employment
    {
        public int Id { get; set; }

        [Required]
        public string Company { get; set; }

        [Required]
        public uint? MonthsOfExperience { get; set; }

        [Required]
        public uint? Salary { get; set; }

        [Required]
        public DateTime? StartDate { get; set; } 
        public DateTime? EndDate { get; set; }

    }
}
