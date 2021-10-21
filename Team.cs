using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    public class Team
    {
        string Name { get; set; }
        string Abriviation { get; set; }
        int CountGame { get; set; }
        double FieldGoals { get; set; }
        double FielGoalsAttempts { get; set; }
        double FielGoalPersentage { get; set; }
        double ThreePointGoals { get; set; }
        double ThreePointAttempts { get; set; }
        double ThreePointPersentage { get; set; }
        double TwoPointGoals { get; set; }
        double TwoPointAttempts { get; set; }
        double TwoPointPersentage { get; set; }
        double FreeThrow { get; set; }
        double FreeThrowAttemps { get; set; }
        double FreeThrowPercentage { get; set; }
        double OffReb { get; set; }
        double DeffReb { get; set; }
        double TotalReb { get; set; }
        double Assists { get; set; }
        double Steals { get; set; }
        double Blocks { get; set; }
        double Turnovers { get; set; }
        double Fouls { get; set; }
        double Points { get; set; }
        string TeamProfileRefernce { get; set; }

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
