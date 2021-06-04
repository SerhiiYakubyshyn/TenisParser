using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameParser.Models
{
    public class Game
    {
        public Sportsman Sportsman1 { get; set; }
        public Sportsman Sportsman2 { get; set; }
        public GameScore TotalScore { get; set; }
        public List<GameScore> GamesScores { get; set; }
 
        public override int GetHashCode()
        {
            var code = 0;
            if (GamesScores == null)
                return Sportsman1.GetHashCode() + Sportsman2.GetHashCode() + TotalScore.GetHashCode() + code;
            code += GamesScores.Sum(gameScore => gameScore.GetHashCode());

            return Sportsman1.GetHashCode() + Sportsman2.GetHashCode() + TotalScore.GetHashCode() + code;
        }
    }
}
