using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_RPG
{
    public class Achievement
    {
        public string AchievementName { get; private set; }
        public Difficulty AchievementType { get; private set; }
        public int AchievementKillAmount { get; private set; }
        public int PointValue { get; private set; }

        public Achievement(string name, int killAmount, int points, Difficulty difficulty = Difficulty.none)
        {
            AchievementName = name;
            AchievementType = difficulty;
            AchievementKillAmount = killAmount;
            PointValue = points;
        }
    }
}
