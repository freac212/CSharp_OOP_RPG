using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_RPG
{
    public class Settings
    {
        // Min and Max of hero damage, it provides variance in hero damage.
        internal const double HeroDamageMinMultiplier = 0.5;
        internal const double HeroDamageMaxMultiplier = 1.5;

        // Run away from fight chances of success based on difficulty
        internal const int BaseRoll = 100;
        // if roll is > 50% Success (50% of winning)
        internal const int EasyRoll = (BaseRoll - 50) + 1;
        // if roll is > 75% Success (25% of winning)
        internal const int MediumRoll = (BaseRoll - 25) + 1;
        // if roll is > 95% Success (5% of winning)
        internal const int HardRoll = (BaseRoll - 5) + 1;

        // Points available to monsters
        internal const int EasyMonsterPoints = 30;
        internal const int MediumMonsterPoints = 40;
        internal const int HardMonsterPoints = 50;

        // Constants, percentage based values for Easy, Medium, and Hard Monsters. These are percentages of the points above.
        // I.E. .25 of 30 points is 7ish, that is now an Easy Monsters strength.
        internal const double MonsterStrengthPercentageMin = .25;
        internal const double MonsterStrengthPercentageMax = .35;
        internal const double MonsterDefensePercentageMin = .09;
        internal const double MonsterDefensePercentageMax = .18;
        // HP is calculated with the remaining points.

        internal const int EnemiesCreatedPerDay = 10;

        // A min max value for item depreciation, if you purchase an item from the store, the resale value is calculated from these
        // two numbers.
        internal const double ValueDepreciationMin = 0.6;
        internal const double ValueDepreciationMax = 0.8;
    }
}
