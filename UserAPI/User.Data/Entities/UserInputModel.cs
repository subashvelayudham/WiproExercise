using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Data.Entities
{
    public class UserInputModel
    {       
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; } 

        public Address Address { get; set; }
        public List<Employment> Employments { get; set; } 
        
        public UserInputModel()
        {
            Employments = new List<Employment>();
        }

    }
}
