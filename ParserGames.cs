using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace WindowsFormsApp2
{
    class ParserGames
    {
        public void GetGames()
        {
            DateTime startDate = new DateTime(2021, 10, 17);
            DateTime endDate = DateTime.Now;

            List<Game> games = new List<Game>();

            while (startDate < endDate)
            {
                string gamesOfTheDay = GetJsonString("https://stats.nba.com/stats/scoreboardv3?GameDate=" + startDate.ToString("yyyy-MM-dd") + "&LeagueID=00");
                JArray jsonGamesOfTheDay = GetJsonArray(gamesOfTheDay);
                foreach (JObject game in jsonGamesOfTheDay)
                {
                    JObject jsonAwayTeam = (JObject)game["awayTeam"];
                    JObject jsonHomeTeam = (JObject)game["homeTeam"];
                    string gameID = game["gameId"].ToString();
                    string gameCode = game["gameCode"].ToString();
                    int gameStatus = Convert.ToInt32(game["gameStatus"]);
                    int awayTeamID = Convert.ToInt32(jsonAwayTeam["teamId"]);
                    int homeTeamID = Convert.ToInt32(jsonHomeTeam["teamId"]);

                    string boxScore = GetJsonString("https://stats.nba.com/stats/boxscoretraditionalv3?GameID="+gameID+"&LeagueID=00&endPeriod=4&endRange=28800&rangeType=1&startPeriod=1&startRange=0");
                    JArray jsonBoxScore = JsonConvert.DeserializeObject<JArray>(boxScore);

                    JObject jsonAwayTeamStats = (JObject)jsonBoxScore[0]["boxScoreTraditional"]["awayTeam"]["statistics"];
                    JObject jsonHomeTeamStats = (JObject)jsonBoxScore[0]["boxScoreTraditional"]["homeTeam"]["statistics"];

                    Dictionary<string, int> awayTeamStats = GetTeamStatsInGame(jsonAwayTeamStats);
                    Dictionary<string, int> homeTeamStats = GetTeamStatsInGame(jsonHomeTeamStats);

                    JArray jsonAwayPlayers = (JArray)jsonBoxScore[0]["boxScoreTraditional"]["awayTeam"]["players"];
                    JArray jsonHomePlayers = (JArray)jsonBoxScore[0]["boxScoreTraditional"]["homeTeam"]["players"];

                    List<Dictionary<string, int>> awayPlayersStats = GetPlayersStatsInGame(jsonAwayPlayers);
                    List<Dictionary<string, int>> homePlayersStats = GetPlayersStatsInGame(jsonHomePlayers);

                    games.Add(new Game(gameID, gameStatus, startDate, homeTeamID, awayTeamID, gameCode, awayTeamStats, homeTeamStats, awayPlayersStats, homePlayersStats));
                }
                startDate=startDate.AddDays(1);
                Thread.Sleep(300);
            }
        }
        public Dictionary<string, int> GetTeamStatsInGame(JObject jsonTeam)
        {
            Dictionary<string, int> teamStats = new Dictionary<string, int>();
            int i = 0;

            foreach (JProperty p in jsonTeam.Properties())
            {
                if (i != 0)
                {
                    string key = p.Name;
                    int value = Convert.ToInt32(p.Value);
                    teamStats.Add(key, value);
                }
                i++;
            }
            return teamStats;
        }
        public List<Dictionary<string, int>> GetPlayersStatsInGame(JArray jsonPlayers)
        {
            List<Dictionary<string, int>> playersStats = new List<Dictionary<string, int>>();
            foreach (JObject player in jsonPlayers)
            {
                Dictionary<string, int> playerStats = new Dictionary<string, int>();

                playerStats.Add("PlayerID", Convert.ToInt32(player["personId"]));

                JObject jsonPlayerStats = (JObject)player["statistics"];

                int i = 0;
                foreach (JProperty p in jsonPlayerStats.Properties())
                {
                    if (i != 0)
                    {
                        string key = p.Name;
                        int value = Convert.ToInt32(p.Value);
                        playerStats.Add(key, value);
                    }
                    i++;
                }
                playersStats.Add(playerStats);
            }
            return playersStats;
        }
        //запрос для получение игр по вказанной дате
        //https://stats.nba.com/stats/scoreboardv3?GameDate=2022-02-04&LeagueID=00

        //
        //https://stats.nba.com/stats/boxscoretraditionalv3?GameID=0022100751&LeagueID=00&endPeriod=4&endRange=28800&rangeType=1&startPeriod=1&startRange=0

        public JArray GetJsonArray(string jsonString)
        {
            JArray jArray = JsonConvert.DeserializeObject<JArray>(jsonString);
            string resultJSONstring = jArray[0]["scoreboard"]["games"].ToString();
            JArray resultJSON = JsonConvert.DeserializeObject<JArray>(resultJSONstring);
            return resultJSON;
        }

        string GetJsonString(string url)
        {
            // Создать запрос с использованием URL
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.AutomaticDecompression = DecompressionMethods.GZip;
            //Прописываем заголовки запроса
            request.Headers = new WebHeaderCollection
            {
                { "Accept-Encoding", "gzip, deflate, br" },
                { "Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7" },
                { "Origin", "https://www.nba.com" },
                { "sec-ch-ua", "\"Not A;Brand\";v=\"99\", \"Chromium\";v=\"96\", \"Google Chrome\";v=\"96\""},
                { "sec-ch-ua-platform", "Windows" },
                { "Sec-Fetch-Dest", "empty" },
                { "Sec-Fetch-Mode", "cors" },
                { "Sec-Fetch-Site", "same-site" },
                { "x-nba-stats-origin", "stats" },
                { "x-nba-stats-token", "true" }
            };

            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.45 Safari/537.36";
            request.Accept = "application/json, text/plain, */*";
            request.Host = "stats.nba.com";
            request.Referer = "https://www.nba.com/";
            // Получаем ответ
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // Отображаем статус
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Получаем содержимое потока, возращемый сервером
            Stream dataStream = response.GetResponseStream();
            // Откройте поток, используя StreamReader для легкого доступа.
            StreamReader reader = new StreamReader(dataStream, Encoding.UTF8);
            // Читаем содержимое
            string responseFromServer = reader.ReadToEnd();
            response.Close();
            responseFromServer = responseFromServer.Insert(0, "[");
            responseFromServer = responseFromServer.Insert(responseFromServer.Length, "]");
            return responseFromServer;
        }
    }
}
