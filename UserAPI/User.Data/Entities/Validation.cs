using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Data.Entities
{
    public class Validation
    {
        public bool IsValid { 
            get
            {
                return !Errorlist.Any();
            }
            set {;}
        }
        public List<string> Errorlist { get; set; }


        public Validation()
        {
            Errorlist = new List<string>();
        }
    }
}
