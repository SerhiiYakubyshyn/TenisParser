using GameParser.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameParser.Logic
{
    public class GamesService
    {
        private List<Game> _games;
        public GamesService()
        {
            _games = new List<Game>();
        }
        public void SetGemesList(List<Game> games)
        {
            SearchGame(games);
            _games = games;
        }
        public List<Game> GetGames()
        {
            return _games;
        }
        private void SearchGame(List<Game> games)
        {
            foreach (var _game in _games)
            {
                if (IsGameOver(_game, games))
                    WriteGame(_game);
            }
        }
        private bool IsGameOver(Game game, List<Game> games)
        {
            foreach (var gameNew in games)
            {
                if (game.Sportsman1.Name == gameNew.Sportsman1.Name && game.Sportsman1.LastName == gameNew.Sportsman1.LastName && game.Sportsman2.Name == gameNew.Sportsman2.Name && game.Sportsman2.LastName == gameNew.Sportsman2.LastName)
                    return false;
            }
            return true;
        }
        private void WriteGame(Game game)
        {
            var strJson = JsonConvert.SerializeObject(game);
            File.AppendAllText(Directory.GetCurrentDirectory() + "myGames.txt", strJson);
        }
        public void ShowGameConsole()
        {
            Console.Clear();
            foreach (var game in _games)
            {
                Console.WriteLine(game.Sportsman1.Name + " " + game.Sportsman1.LastName + " " + game.Sportsman1.National);
                Console.WriteLine(game.Sportsman2.Name + " " + game.Sportsman2.LastName + " " + game.Sportsman2.National);
                Console.Write(game.TotalScore.ScoreSportsmen1 + ":" + game.TotalScore.ScoreSportsmen2 + " [");
                foreach (var score in game.GamesScores)
                {
                    Console.Write(' ' + score.ScoreSportsmen1.ToString() + "|" + score.ScoreSportsmen2.ToString() + ' ');
                }
                Console.Write("]");
                Console.WriteLine();
            }
        }
    }
}
