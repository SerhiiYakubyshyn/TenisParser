using GameParser.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameParser.Logic
{
    public class TenisParser
    {
        private ChromeDriver _driver;
        private List<Game> _games;
        public TenisParser()
        {
            _driver = new ChromeDriver(Directory.GetCurrentDirectory());
            _games = new List<Game>();
        }
        public void CreateNewListGame()
        {
            _games = null;
            _games = new List<Game>();
        }
        public void SetURL(string path)
        {
            _driver.Manage().Window.Maximize();
            _driver.Url = path;
        }
        public void LoginUser(string login, string password)
        {
            _driver.FindElement(By.ClassName("loginDropTop")).Click();
            _driver.FindElementById("userLogin").SendKeys(login);
            _driver.FindElementById("userPassword").SendKeys(password);
            _driver.FindElement(By.ClassName("orangeBut")).Click();
        }
        public void FillGame()
        {
            FillingGames();
        }
        public List<Game> GetGamesList()
        {
            return _games;
        }
        private void FillingGames()
        {
            try
            {
                CreateNewListGame();
                var elementsList = _driver.FindElementById("live_bets_on_main").FindElements(By.ClassName("wIc"));
                foreach (var element in elementsList)
                {
                    var gemeList = element.FindElements(By.ClassName("kofsTableLine"));
                    foreach (var gameElement in gemeList)
                    {
                        var game = new Game();
                        game.Sportsman1 = SportsmanParser(gameElement.FindElement(By.XPath("div[1]/div[1]/a/span[1]/span[1]")).GetAttribute("innerText"));
                        game.Sportsman2 = SportsmanParser(gameElement.FindElement(By.XPath("div[1]/div[1]/a/span[1]/span[2]")).GetAttribute("innerText"));
                        var allScoreElements = gameElement.FindElement(By.XPath("div[1]/div[1]/div[2]/div[2]/div/span[1]")).GetAttribute("innerText");
                        game.TotalScore = GameTotalScoreParser(allScoreElements);
                        game.GamesScores = GamesScoresParser(allScoreElements);
                        _games.Add(game);
                    }
                }
            }
            catch (Exception ex)
            {
                Thread.Sleep(2000);
                FillingGames();
            }
        }
        private Sportsman SportsmanParser(string sportsmanElement)
        {
            var sportsman = new Sportsman();
            if (sportsmanElement != null)
            {
                string[] sportsmanArr = sportsmanElement.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                sportsman.Name = sportsmanArr[0];
                sportsman.LastName = sportsmanArr[1];
                sportsman.National = sportsmanArr[2];
            }
            return sportsman;
        }
        private GameScore GameTotalScoreParser(string scoreElement)
        {
            var scoreGame = new GameScore();
            char[] separators = new char[] { ' ', '-', '[', ']', ',' };
            if (scoreElement != null)
            {
                scoreGame.ScoreSportsmen1 = int.Parse(scoreElement.Split(separators, StringSplitOptions.RemoveEmptyEntries)[0]);
                scoreGame.ScoreSportsmen2 = int.Parse(scoreElement.Split(separators, StringSplitOptions.RemoveEmptyEntries)[1]);
            }
            return scoreGame;
        }
        private List<GameScore> GamesScoresParser(string scoreElement)
        {
            var scoresGames = new List<GameScore>();
            if (scoreElement != null)
            {
                scoreElement = scoreElement.Remove(0, 3);
                if (scoreElement != null && scoreElement.Length >= 2)
                {
                    char[] separators = new char[] { ' ', '-', '[', ']', ',' };
                    string[] scoresGamesArr = scoreElement.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < scoresGamesArr.Length; i += 2)
                    {
                        var scoreGame = new GameScore();
                        scoreGame.ScoreSportsmen1 = int.Parse(scoresGamesArr[i]);
                        scoreGame.ScoreSportsmen2 = int.Parse(scoresGamesArr[i + 1]);
                        scoresGames.Add(scoreGame);
                    }
                }
            }
            return scoresGames;
        }
    }
}
