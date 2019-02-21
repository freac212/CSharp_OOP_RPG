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
            var compare = Hero.Strength - CurrentMonster.Defense;
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
            var compare = CurrentMonster.Strength - Hero.Defense;

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
        }

        private void Lose()
        {
            Console.WriteLine("You've been defeated! :( GAME OVER.");
            Console.WriteLine("Press any key to exit the game");
            Console.ReadKey();
        }
    }
}