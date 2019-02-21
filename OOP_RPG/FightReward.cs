using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_RPG
{
    static class FightReward
    {
        public static int ApplyRewardToHero(Hero hero, Difficulty difficulty)
        {
            // Gold reward is calculated based on difficulty
            //  BaseGold: 30
            //          Min:    Max:
            //  Easy:   1 %     30 %
            //  Medium: 30 %    70 %
            //  Hard:   70 %    100 %
            const double EasyMin = 0.01;
            const double EasyMax = 0.30;

            const double MediumMin = 0.30;
            const double MediumMax = 0.70;

            const double HardMin = 0.70;
            const double HardMax = 1;


            int baseRewardGold = 30;
            var randomNum = new Random();

            switch (difficulty)
            {
                case Difficulty.Easy:
                    baseRewardGold = randomNum.Next((int)(baseRewardGold * EasyMin), (int)(baseRewardGold * EasyMax));
                    break;

                case Difficulty.Medium:
                    baseRewardGold = randomNum.Next((int)(baseRewardGold * MediumMin), (int)(baseRewardGold * MediumMax));
                    break;

                case Difficulty.Hard:
                    baseRewardGold = randomNum.Next((int)(baseRewardGold * HardMin), (int)(baseRewardGold * HardMax));
                    break;

                default:
                    break;
            }
            hero.AddGold(baseRewardGold);
            return baseRewardGold;
        }
    }
}
