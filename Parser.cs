using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace WindowsFormsApp2
{
    class Parser
    {
        private WebBrowser wb = new WebBrowser();

        string Address = "https://www.basketball-reference.com/leagues/NBA_2021.html";
        public void GetValutesFromDOM()
        {
            this.wb.ScriptErrorsSuppressed = true;
            this.wb.Navigate(this.Address);
            this.wb.DocumentCompleted += WebBrowser_DocumentCompleted;
        }

        // Метод для обработки загруженной DOM-модели
        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            HtmlDocument hd = wb.Document;
            string html = hd.Body.InnerHtml;
            string regexp1 = "<table class=\"stats_table sortable.*<\\/table>";
            MatchCollection mc1 = Regex.Matches(html, regexp1, RegexOptions.Multiline);
            if (mc1.Count <= 0) return;
            string stringStatistic = mc1[0].Value;
            string regexp2 = "<a href=\"(.*?)\">(.*?)<\\/a>.?<\\/td>" +
                "<td class=\"right \" data-stat=\"g\">(.*?)<\\/td><td class=\"right \" data-stat=\"mp\">(.*?)<\\/td>" +
                "<td class=\"right \" data-stat=\"fg\">(.*?)<\\/td><td class=\"right \" data-stat=\"fga\">(.*?)<\\/td>" +
                "<td class=\"right \" data-stat=\"fg_pct\">(.*?)<\\/td><td class=\"right \" data-stat=\"fg3\">(.*?)<\\/td>" +
                "<td class=\"right \" data-stat=\"fg3a\">(.*?)<\\/td><td class=\"right \" data-stat=\"fg3_pct\">(.*?)<\\/td>" +
                "<td class=\"right \" data-stat=\"fg2\">(.*?)<\\/td><td class=\"right \" data-stat=\"fg2a\">(.*?)<\\/td>" +
                "<td class=\"right \" data-stat=\"fg2_pct\">(.*?)<\\/td><td class=\"right \" data-stat=\"ft\">(.*?)<\\/td>" +
                "<td class=\"right \" data-stat=\"fta\">(.*?)<\\/td><td class=\"right \" data-stat=\"ft_pct\">(.*?)<\\/td>" +
                "<td class=\"right \" data-stat=\"orb\">(.*?)<\\/td><td class=\"right \" data-stat=\"drb\">(.*?)<\\/td>" +
                "<td class=\"right \" data-stat=\"trb\">(.*?)<\\/td><td class=\"right \" data-stat=\"ast\">(.*?)<\\/td>" +
                "<td class=\"right \" data-stat=\"stl\">(.*?)<\\/td><td class=\"right \" data-stat=\"blk\">(.*?)<\\/td>" +
                "<td class=\"right \" data-stat=\"tov\">(.*?)<\\/td><td class=\"right \" data-stat=\"pf\">(.*?)<\\/td>" +
                "<td class=\"right \" data-stat=\"pts\">(.*?)<\\/td><\\/tr>";
            MatchCollection mc2 = Regex.Matches(stringStatistic, regexp2, RegexOptions.Multiline);

            foreach (Match m in mc2)
            {
                string strprofileReference = "https://www.basketball-reference.com" + m.Groups[1].Value;
                string[] splitReference = m.Groups[1].Value.Split('/');
                string abr = splitReference[2];
                string name = m.Groups[2].Value;
                int countGame = Convert.ToInt32(m.Groups[3].Value.Replace('.', ','));
                double fg = Convert.ToDouble(m.Groups[5].Value.Replace('.', ','));
                double fga = Convert.ToDouble(m.Groups[6].Value.Replace('.', ','));
                double fgP = Convert.ToDouble(m.Groups[7].Value.Replace(".", "0,"));
                double threeP = Convert.ToDouble(m.Groups[8].Value.Replace('.', ','));
                double threePA = Convert.ToDouble(m.Groups[9].Value.Replace('.', ','));
                double threePP = Convert.ToDouble(m.Groups[10].Value.Replace(".", "0,"));
                double twoP = Convert.ToDouble(m.Groups[11].Value.Replace('.', ','));
                double twoPA = Convert.ToDouble(m.Groups[12].Value.Replace('.', ','));
                double twoPP = Convert.ToDouble(m.Groups[13].Value.Replace(".", "0,"));
                double ft = Convert.ToDouble(m.Groups[14].Value.Replace('.', ','));
                double fta = Convert.ToDouble(m.Groups[15].Value.Replace('.', ','));
                double ftp = Convert.ToDouble(m.Groups[16].Value.Replace(".", "0,"));
                double oreb = Convert.ToDouble(m.Groups[17].Value.Replace('.', ','));
                double dreb = Convert.ToDouble(m.Groups[18].Value.Replace('.', ','));
                double trb = Convert.ToDouble(m.Groups[19].Value.Replace('.', ','));
                double ast = Convert.ToDouble(m.Groups[20].Value.Replace('.', ','));
                double stl = Convert.ToDouble(m.Groups[21].Value.Replace('.', ','));
                double blk = Convert.ToDouble(m.Groups[22].Value.Replace('.', ','));
                double tov = Convert.ToDouble(m.Groups[23].Value.Replace('.', ','));
                double pf = Convert.ToDouble(m.Groups[24].Value.Replace('.', ','));
                double pts = Convert.ToDouble(m.Groups[25].Value.Replace('.', ','));


                DataModel.ListTeams.Add(new Team(name, abr, countGame, fg, fga, fgP, threeP, threePA, threePP, twoP, twoPA,
                    twoPP, ft, fta, ftp, oreb, dreb, trb, ast, stl, blk, tov, pf, pts, strprofileReference));
            }
            List<Team> teams = DataModel.ListTeams;
            wb.DocumentCompleted -= WebBrowser_DocumentCompleted;
            foreach (Team t in DataModel.ListTeams)
            {
                wb.BeginInvoke(new Action(() =>
                {
                    this.wb.Navigate(t.GetProfileReference());
                    this.wb.DocumentCompleted += FillPlayerProfileReference;
                }));
            }
        }

        private void FillPlayerProfileReference(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            HtmlDocument hd = wb.Document;
            string html = hd.Body.InnerHtml;

        }
    }
}
