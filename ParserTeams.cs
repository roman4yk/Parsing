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
    class ParserTeams
    {

        List<Dictionary<string,object>> TeamsInfo = new List<Dictionary<string, object>>();

        //TeamStatsKeys ["GP","W","L","W_PCT","MIN","FGM","FGA","FG_PCT","FG3M","FG3A","FG3_PCT","FTM","FTA","FT_PCT","OREB","DREB","REB","AST","TOV","STL","BLK","BLKA","PF","PFD","PTS","PLUS_MINUS"]
        List<Dictionary<string,float>> TeamsStats = new List<Dictionary<string, float>>();
        
        public ParserTeams()
        {
            string s = GetJsonString("https://stats.nba.com/stats/boxscoretraditionalv3?GameID=0022100751&LeagueID=00&endPeriod=4&endRange=28800&rangeType=1&startPeriod=1&startRange=0");
            string jsonInfoTeamsString = GetJsonString("https://stats.nba.com/stats/leaguestandingsv3?LeagueID=00&Season=2021-22&SeasonType=Regular%20Season");
            TeamsInfo = GetTeamsInfo(jsonInfoTeamsString);
            TeamsStats = GetTeamsStats(TeamsInfo);
        }

        public JArray GetJsonArray(string jsonString)
        {
            JArray jArray = JsonConvert.DeserializeObject<JArray>(jsonString);  
            string resultJSONstring = jArray[0]["resultSets"][0].ToString();
            resultJSONstring = resultJSONstring.Insert(0, "[");
            resultJSONstring = resultJSONstring.Insert(resultJSONstring.Length,"]");
            JArray resultJSON  = JsonConvert.DeserializeObject<JArray>(resultJSONstring); 
            return resultJSON;
        }
        
        public List<Dictionary<string,float>> GetTeamsStats(List<Dictionary<string,object>> teamsInfo)
        {
            List<Dictionary<string,float>> result = new List<Dictionary<string, float>>();

            int i = 0;
            foreach(Dictionary<string,object> teamInfo in teamsInfo)
            {
                
                int idTeam = Convert.ToInt32(teamInfo["TeamID"]);
                string jsonStringStatsTeam = GetJsonString("https://stats.nba.com/stats/teamdashboardbygeneralsplits?DateFrom=&DateTo=&GameSegment=&LastNGames=0&LeagueID=00&Location=&MeasureType=Base&Month=0&OpponentTeamID=0&Outcome=&PORound=0&PaceAdjust=N&PerMode=PerGame&Period=0&PlusMinus=N&Rank=N&Season=2021-22&SeasonSegment=&SeasonType=Regular+Season&ShotClockRange=&Split=general&TeamID="
                    +idTeam.ToString()+"&VsConference=&VsDivision=");
                JArray jsonTeamStats = GetJsonArray(jsonStringStatsTeam);
                result.Add(new Dictionary<string, float>());
                for (int j =3;j<28;j++)
                {
                    string key  = jsonTeamStats[0]["headers"][j].ToString();
                    float value = Convert.ToSingle(jsonTeamStats[0]["rowSet"][0][j]);
                    result[i].Add(key, value); 
                }
                i++;
                Thread.Sleep(600);

            }
            return result;
        }

        public List<Dictionary<string,object>> GetTeamsInfo(string jsonString)
        {
            List<Dictionary<string,object>> result= new List<Dictionary<string,object>>();
            JArray resultJSON  = GetJsonArray(jsonString); 

            for(int i =0;i<resultJSON[0]["rowSet"].Count();i++)
            {
                result.Add(new Dictionary<string, object>());
                for (int j =0;j<resultJSON[0]["headers"].Count();j++)
                {
                    string key  = resultJSON[0]["headers"][j].ToString();
                    string value = resultJSON[0]["rowSet"][i][j].ToString();
                     result[i].Add(key, value); 
                }
            }
            return result;
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
            responseFromServer =responseFromServer.Insert(responseFromServer.Length,"]");
            return responseFromServer;
        }
    }
}
