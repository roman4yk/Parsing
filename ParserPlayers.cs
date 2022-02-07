using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;

namespace WindowsFormsApp2
{
    class ParserPlayers
    {
        public ParserPlayers()
        { 
            PlayerStatsString = GetPlayerStats();
        }

        private string PlayerStatsString { get;}


        string GetPlayerStats()
        {
            // Создать запрос с использованием URL
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://stats.nba.com/stats/leaguedashplayerstats?College=&Conference=&Country=&DateFrom=&DateTo=&Division=&DraftPick=&DraftYear=&GameScope=&GameSegment=&Height=&LastNGames=0&LeagueID=00&Location=&MeasureType=Base&Month=0&OpponentTeamID=0&Outcome=&PORound=0&PaceAdjust=N&PerMode=PerGame&Period=0&PlayerExperience=&PlayerPosition=&PlusMinus=N&Rank=N&Season=2021-22&SeasonSegment=&SeasonType=Regular+Season&ShotClockRange=&StarterBench=&TeamID=0&TwoWay=0&VsConference=&VsDivision=&Weight=");
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
            return responseFromServer;
        }

        public List<Player> GetPlayers()
        {
            List<Player> listPlayers = new List<Player>();

            string regExpPlayer = "\\[(\\d*),\"(.*?)\",\"(.*?)\",(\\d*),\"(\\w*)\",(\\d*).\\d*,(\\d*),(\\d*),(\\d*),(\\d*.\\d*)," +
                "(\\d*.\\d*),(\\d*.\\d*),(\\d*.\\d*),(\\d*.\\d*),(\\d*.\\d*),(\\d*.\\d*),(\\d*.\\d*),(\\d*.\\d*),(\\d*.\\d*)," +
                "(\\d*.\\d*),(\\d*.\\d*),(\\d*.\\d*),(\\d*.\\d*),(\\d*.\\d*),(\\d*.\\d*),(\\d*.\\d*),(\\d*.\\d*),(\\d*.\\d*)," +
                "(\\d*.\\d*),(\\d*.\\d*),(\\d*.\\d*),(\\-\\d*.\\d*|.\\d*.\\d*),(\\d*.\\d*),(\\d*),(\\d*),(\\d*),(\\d*),(\\d*)," +
                "(\\d*),(\\d*),(\\d*),(\\d*),(\\d*),(\\d*),(\\d*),(\\d*),(\\d*),(\\d*),(\\d*),(\\d*),(\\d*),(\\d*),(\\d*),(\\d*)," +
                "(\\d*),(\\d*),(\\d*),(\\d*),(\\d*),(\\d*),(\\d*),(\\d*),(\\d*),(\\d*),(\\d*),\"(\\d*\\S\\d*)";

            Regex regex = new Regex(regExpPlayer);
            MatchCollection playersMathes = regex.Matches(PlayerStatsString);

            foreach (Match player in playersMathes)
            {
                string playerinfo = GetPlayerInfo(Convert.ToInt32(player.Groups[1].Value));
                string regExpInfo = "\\[\\d*,\"(.*?)\",\"(.*?)\",\".*?\",\".*?,.*?\",\".*?\",\".*?\",\"(\\d*-\\d*-\\d*).*?\",\".*?\",\"(\\w*|\\w*\\s\\w*|\\w*\\s\\w*\\s\\w*|\\w*\\s\\w*\\s\\w*\\s\\w*)\",\".*?\",\"(\\d-\\d*)\",\"(\\d*)\",(\\d*),\".*?\",\"(\\w*|\\w*\\S\\w*)\".*,\"(\\w)\"\\]";

                Regex regexinfo = new Regex(regExpInfo);
                MatchCollection infoPlayerMatches = regexinfo.Matches(playerinfo);

                if (infoPlayerMatches.Count > 0)
                {
                    int id = Convert.ToInt32(player.Groups[1].Value);
                    string name = infoPlayerMatches[0].Groups[1].Value;
                    string surname = infoPlayerMatches[0].Groups[2].Value;
                    DateTime birthday = Convert.ToDateTime(infoPlayerMatches[0].Groups[3].Value);
                    string country = infoPlayerMatches[0].Groups[4].Value;
                    double height = Convert.ToDouble(infoPlayerMatches[0].Groups[5].Value.Replace('-', ','));
                    double weight = Convert.ToDouble(infoPlayerMatches[0].Groups[6].Value);
                    int age = Convert.ToInt32(player.Groups[6].Value);
                    string gp = infoPlayerMatches[0].Groups[8].Value;
                    int seasonExp = Convert.ToInt32(infoPlayerMatches[0].Groups[7].Value);
                    bool greatest75 = ConvertToBool(infoPlayerMatches[0].Groups[9].Value);

                    double[] statistic = new double[player.Groups.Count - 6];
                    int j = 0;
                    for (int i = 6; i < player.Groups.Count; i++)
                    {
                        statistic[j] = Convert.ToDouble(player.Groups[i].Value.Replace('.', ','));
                        j++;
                    }

                    Player p = new Player(id, name, surname, birthday, country, height, weight, age, gp, seasonExp, 0, 0, greatest75, statistic);
                    listPlayers.Add(p);
                    Thread.Sleep(600);
                }
               
            }
            return listPlayers;
            
        }
        bool ConvertToBool(string boolname)
        {
            if (boolname == "Y") return true;
            else return false;
        }

        string GetPlayerInfo(int idPlayer)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://stats.nba.com/stats/commonplayerinfo?LeagueID=&PlayerID=" + idPlayer.ToString());
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
            // Создать запрос с использованием URL

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
            return responseFromServer;
        }





    }
}
