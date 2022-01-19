using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    public class Team
    {
        public string Name { get; set; }
        public string Abriviation { get; set; }
        public int CountGame { get; set; }
        public double FieldGoals { get; set; }
        public double FielGoalsAttempts { get; set; }
        public double FielGoalPersentage { get; set; }
        public double ThreePointGoals { get; set; }
        public double ThreePointAttempts { get; set; }
        public double ThreePointPersentage { get; set; }
        public double TwoPointGoals { get; set; }
        public double TwoPointAttempts { get; set; }
        public double TwoPointPersentage { get; set; }
        public double FreeThrow { get; set; }
        public double FreeThrowAttemps { get; set; }
        public double FreeThrowPercentage { get; set; }
        public double OffReb { get; set; }
        public double DeffReb { get; set; }
        public double TotalReb { get; set; }
        public double Assists { get; set; }
        public double Steals { get; set; }
        public double Blocks { get; set; }
        public double Turnovers { get; set; }
        public double Fouls { get; set; }
        public double Points { get; }
        public string TeamProfileRefernce { get; set; }

        List<Player> ListPlayer { get; set; }
        public Team(string name, string abriviation, int countGame, double fieldGoals,
            double fielGoalsAttempts, double fielGoalPersentage, double threePointGoals,
            double threePointAttempts, double threePointPersentage, double twoPointGoals,
            double twoPointAttempts, double twoPointPersentage, double freeThrow, double freeThrowAttemps,
            double freeThrowPercentage, double offReb, double deffReb, double totalReb, double assists,
            double steals, double blocks, double turnovers, double fouls, double points, string teamProfileReference)
        {
            Name = name;
            Abriviation = abriviation;
            CountGame = countGame;
            FieldGoals = fieldGoals;
            FielGoalsAttempts = fielGoalsAttempts;
            FielGoalPersentage = fielGoalPersentage;
            ThreePointGoals = threePointGoals;
            ThreePointAttempts = threePointAttempts;
            ThreePointPersentage = threePointPersentage;
            TwoPointGoals = twoPointGoals;
            TwoPointAttempts = twoPointAttempts;
            TwoPointPersentage = twoPointPersentage;
            FreeThrow = freeThrow;
            FreeThrowAttemps = freeThrowAttemps;
            FreeThrowPercentage = freeThrowPercentage;
            OffReb = offReb;
            DeffReb = deffReb;
            TotalReb = totalReb;
            Assists = assists;
            Steals = steals;
            Blocks = blocks;
            Turnovers = turnovers;
            Fouls = fouls;
            Points = points;
            TeamProfileRefernce = teamProfileReference;
            ListPlayer = new List<Player>();
        }
        public string GetProfileReference()
        {
            return this.TeamProfileRefernce;
        }
    }
}
