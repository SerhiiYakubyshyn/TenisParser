using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameParser.Models
{
    public class Sportsman
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string National { get; set; }

        public override bool Equals(object? obj)
        {
            return obj.GetHashCode() == this.GetHashCode();
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode() + LastName.GetHashCode() + National.GetHashCode();
        }
    }
}
