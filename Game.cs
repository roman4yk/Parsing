using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    class Game
    {
        public Game(string gameId, int gameStatus, DateTime dateTime, int homeTeamId, int awayTeamId, string gameCode)
        {
            GameId = gameId;
            GameStatus = gameStatus;
            DateTime = dateTime;
            HomeTeamId = homeTeamId;
            AwayTeamId = awayTeamId;
            GameCode = gameCode;
        }

        public Game(string gameId, int gameStatus, DateTime dateTime, int homeTeamId, int awayTeamId, string gameCode,
            Dictionary<string, int> teamAwayStats, Dictionary<string, int> teamHomeStats,
            List<Dictionary<string, int>> playersAwayStats, List<Dictionary<string, int>> playersHomeStats)
        {
            GameId = gameId;
            GameStatus = gameStatus;
            DateTime = dateTime;
            HomeTeamId = homeTeamId;
            AwayTeamId = awayTeamId;
            GameCode = gameCode;
            TeamAwayStats = teamAwayStats;
            TeamHomeStats = teamHomeStats;
            PlayersAwayStats = playersAwayStats;
            PlayersHomeStats = playersHomeStats;
        }

        string GameId { get; set; }
        int GameStatus { get; set; }
        DateTime DateTime { get; set; }
        int HomeTeamId { get; set; }
        int AwayTeamId { get; set; }
        string GameCode { get; set; }

        Dictionary<string, int> TeamAwayStats { get; set; }
        Dictionary<string, int> TeamHomeStats { get; set; }
        List<Dictionary<string, int>> PlayersAwayStats { get; set; }
        List<Dictionary<string, int>> PlayersHomeStats { get; set; }


    }
}
