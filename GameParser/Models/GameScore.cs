using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameParser.Models
{
    public class GameScore
    {
        public int ScoreSportsmen1 { get; set; }
        public int ScoreSportsmen2 { get; set; }

        public override bool Equals(object? obj)
        {
            return obj.GetHashCode() == this.GetHashCode();
        }
        public override int GetHashCode()
        {
            return ScoreSportsmen1 + ScoreSportsmen2;
        }
    }
}
