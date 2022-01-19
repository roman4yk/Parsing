using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    enum GamePosition
    {
        PointGuard=1,
        ShootingGuard=2,
        SmallForward=3,
        PowerForward=4,
        Center=5
    }


    public class Player
    {
        public Player(int iD, string name, string surname, DateTime birthdate,
            string country, double height, double weight, int age,
            string position, int fromYear, int draftRound, int draftNumber,
            bool greatest75, double[] statistic)
        {
            ID = iD;
            Name = name;
            Surname = surname;
            Birthdate = birthdate;
            Country = country;
            this.height = height;
            this.weight = weight;
            Age = age;
            Position = position;
            SeasonExp = fromYear;
            DraftRound = draftRound;
            DraftNumber = draftNumber;
            Greatest75 = greatest75;
            Statistic = statistic;
        }

        int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthdate { get; set; }
        public string Country { get; set; }
        public double height { get; set; }
        public double weight { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }
        public int SeasonExp { get; set; }
        public int DraftRound { get; set; }
        public int DraftNumber { get; set; }
        public bool Greatest75 { get; set; }

        //"headers":["PLAYER_ID","PLAYER_NAME","NICKNAME","TEAM_ID","TEAM_ABBREVIATION","AGE","GP","W","L","W_PCT","MIN","FGM",
        //"FGA","FG_PCT","FG3M","FG3A","FG3_PCT","FTM","FTA","FT_PCT","OREB","DREB","REB","AST","TOV","STL","BLK","BLKA","PF",
        //"PFD","PTS","PLUS_MINUS","NBA_FANTASY_PTS","DD2","TD3","GP_RANK","W_RANK","L_RANK","W_PCT_RANK","MIN_RANK","FGM_RANK",
        //"FGA_RANK","FG_PCT_RANK","FG3M_RANK","FG3A_RANK","FG3_PCT_RANK","FTM_RANK","FTA_RANK","FT_PCT_RANK","OREB_RANK",
        //"DREB_RANK","REB_RANK","AST_RANK","TOV_RANK","STL_RANK","BLK_RANK","BLKA_RANK","PF_RANK","PFD_RANK","PTS_RANK",
        //"PLUS_MINUS_RANK","NBA_FANTASY_PTS_RANK","DD2_RANK","TD3_RANK","CFID","CFPARAMS"]
        public double[] Statistic { get; set; }

    }
}
    
