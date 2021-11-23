using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace WindowsFormsApp2
{
    class ParserPlayers
    {
        string GetPlayerStats()
        {
            // Создать запрос с использованием URL
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://stats.nba.com/stats/leaguedashplayerstats?College=&Conference=&Country=&DateFrom=&DateTo=&Division=&DraftPick=&DraftYear=&GameScope=&GameSegment=&Height=&LastNGames=0&LeagueID=00&Location=&MeasureType=Base&Month=0&OpponentTeamID=0&Outcome=&PORound=0&PaceAdjust=N&PerMode=PerGame&Period=0&PlayerExperience=&PlayerPosition=&PlusMinus=N&Rank=N&Season=2021-22&SeasonSegment=&SeasonType=Regular+Season&ShotClockRange=&StarterBench=&TeamID=0&TwoWay=0&VsConference=&VsDivision=&Weight=");
            // Установите свойство метода запроса в GET.
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
            return responseFromServer;//lkj;lk;kl
        }


        
    }
}
