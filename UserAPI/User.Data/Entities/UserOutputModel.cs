using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Data.Entities
{
    public class UserOutputModel
    {
        public bool IsValid { get; set; }
        public string Output { get; set; }

        public List<string> Validations { get; set; }
    }
}
