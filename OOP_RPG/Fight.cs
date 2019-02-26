using System;
using System.Collections.Generic;

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
                Console.Clear();
                UI.DefaultBoxes.DrawInventory(Hero, UI.Grid.Left);
                UI.Draw.PrintToOutput("You've encountered " + CurrentMonster.Name + "! " + CurrentMonster.Strength + " Strength/" + CurrentMonster.Defense + " Defense/" +
                CurrentMonster.CurrentHP + $" HP. Difficulty: {CurrentMonster.Difficulty}. What will you do?");

                UI.DefaultBoxes.DrawOptions(new List<string>
                {
                    "1. Fight"
                }, UI.Grid.Center);

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
                var baseDamage = (Hero.Strength + Hero.EquippedWeapon.GetAttribute()) - CurrentMonster.Defense;
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

            UI.Draw.PrintToOutput("You did " + damage + " damage!");

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
                compare = CurrentMonster.Strength - (Hero.Defense + Hero.EquippedArmour.GetAttribute());
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

            UI.Draw.PrintToOutput(CurrentMonster.Name + " does " + damage + " damage!");

            if (Hero.CurrentHP <= 0)
            {
                Lose();
            }
        }

        private void Win()
        {
            // Fight reward will be called here.
            var GoldWon = Loot.LootGenerator(Hero, CurrentMonster.Difficulty);
            Console.Clear();
            UI.DefaultBoxes.DrawInventory(Hero, UI.Grid.Left);
            UI.Draw.PrintToOutput(new List<string>
            {
                    CurrentMonster.Name + " has been defeated! You win the battle!",
                    $"Rewards: Gold: {GoldWon}"
            });
            UI.DefaultBoxes.DrawOptions(new List<string>
                {
                    "Press any key to continue..."
                }, UI.Grid.Center);
            Console.ReadLine();
        }

        private void Lose()
        {
            Console.Clear();
            UI.DefaultBoxes.DrawInventory(Hero, UI.Grid.Left);
            UI.DefaultBoxes.DrawOptions(new List<string>
                {
                    "You've been defeated! :( GAME OVER."
                }, UI.Grid.Center);
            UI.Draw.PrintToOptions("Press any key to exit the game");
            Console.ReadKey();
        }
    }
}