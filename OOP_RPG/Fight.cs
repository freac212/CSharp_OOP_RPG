using System;

namespace OOP_RPG
{
    public class Fight
    {
        private Monster CurrentMonster { get; }
        private Hero Hero { get; }

        public Fight(Hero game)
        {
            Hero = game;

            CurrentMonster = MonsterPicker.GetMonster();
        }

        public void Start()
        {
            while (CurrentMonster.CurrentHP > 0 && Hero.CurrentHP > 0)
            {
                Console.WriteLine("You've encountered " + CurrentMonster.Name + "! " + CurrentMonster.Strength + " Strength/" + CurrentMonster.Defense + " Defense/" +
                CurrentMonster.CurrentHP + $" HP. Difficulty: {CurrentMonster.Difficulty}. What will you do?");

                Console.WriteLine("1. Fight");

                var input = Console.ReadLine();

                if (input == "1")
                {
                    HeroTurn();
                }
            }
        }

        private void HeroTurn()
        {
            const double minMultiplier = 0.5;
            const double maxMultiplier = 1.5;
            var randomNum = new Random();

            int compare;
            if (Hero.EquippedWeapon != null)
            {
                // Maybe make this into a method
                var baseDamage = (Hero.Strength + Hero.EquippedWeapon.Strength) - CurrentMonster.Defense;
                int minDamage = (int)(baseDamage * minMultiplier);
                int maxDamage = (int)(baseDamage * maxMultiplier);
                compare = randomNum.Next(minDamage, maxDamage);
            }
            else
            {
                var baseDamage = Hero.Strength - CurrentMonster.Defense;
                int minDamage = (int)(baseDamage * minMultiplier);
                int maxDamage = (int)(baseDamage * maxMultiplier);
                compare = randomNum.Next(minDamage, maxDamage);
            }

            int damage;

            if (compare <= 0)
            {
                damage = 1;
                CurrentMonster.CurrentHP -= damage;
            }
            else
            {
                damage = compare;
                CurrentMonster.CurrentHP -= damage;
            }

            Console.WriteLine("You did " + damage + " damage!");

            if (CurrentMonster.CurrentHP <= 0)
            {
                Win();
            }
            else
            {
                MonsterTurn();
            }
        }

        private void MonsterTurn()
        {
            int damage;
            int compare;
            if (Hero.EquippedArmour != null)
            {
                // Maybe make this into a method
                compare = CurrentMonster.Strength - (Hero.Defense + Hero.EquippedArmour.Defense);
            }
            else
            {
                compare = CurrentMonster.Strength - Hero.Defense;
            }


            if (compare <= 0)
            {
                damage = 1;
                Hero.CurrentHP -= damage;
            }
            else
            {
                damage = compare;
                Hero.CurrentHP -= damage;
            }

            Console.WriteLine(CurrentMonster.Name + " does " + damage + " damage!");

            if (Hero.CurrentHP <= 0)
            {
                Lose();
            }
        }

        private void Win()
        {
            // Fight reward will be called here.
            var GoldWon = FightReward.ApplyRewardToHero(Hero, CurrentMonster.Difficulty);
            Console.WriteLine(CurrentMonster.Name + " has been defeated! You win the battle!");
            Console.WriteLine($"Rewards: Gold: {GoldWon}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }

        private void Lose()
        {
            Console.WriteLine("You've been defeated! :( GAME OVER.");
            Console.WriteLine("Press any key to exit the game");
            Console.ReadKey();
        }
    }
}