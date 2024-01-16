using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_RPG
{
    static class Loot
    {
        public static int LootGenerator(Hero hero, Difficulty difficulty)
        {
            // Gold reward is calculated based on difficulty
            //  BaseGold: 31
            //          Min:    Max:
            //  Easy:   1 %     30 %
            //  Medium: 30 %    70 %
            //  Hard:   70 %    100 %
            int rewardGold = 0;
            var randomNum = new Random();

            switch (difficulty)
            {
                case Difficulty.Easy:
                    rewardGold = randomNum.Next((int)(Settings.BaseRewardGold * Settings.GoldEarnedEasyMin), (int)(Settings.BaseRewardGold * Settings.GoldEarnedEasyMax));
                    break;

                case Difficulty.Medium:
                    rewardGold = randomNum.Next((int)(Settings.BaseRewardGold * Settings.GoldEarnedMediumMin), (int)(Settings.BaseRewardGold * Settings.GoldEarnedMediumMax));
                    break;

                case Difficulty.Hard:
                    rewardGold = randomNum.Next((int)(Settings.BaseRewardGold * Settings.GoldEarnedHardMin), (int)(Settings.BaseRewardGold * Settings.GoldEarnedHardMax));
                    break;

                default:
                    break;
            }

            return rewardGold;
        }
    }
}
