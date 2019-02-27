using System;
using System.Collections.Generic;

namespace OOP_RPG
{
    public class Fight
    {
        private Monster CurrentMonster { get; }
        private Hero Hero { get; }
        private AchievementList Achievements { get; }
        private bool LostFight = false;

        public Fight(Hero game, AchievementList achieve)
        {
            Hero = game;
            Achievements = achieve;
            CurrentMonster = MonsterPicker.GetMonster();
        }

        public void Start()
        {
            while (CurrentMonster.CurrentHP > 0 && Hero.CurrentHP > 0 && LostFight != true)
            {
                Console.Clear();
                UI.DefaultBoxes.DrawInventory(Hero, UI.Grid.Left);
                UI.Draw.PrintToOutput(new List<string> {
                    "You've encountered " + CurrentMonster.Name + "! ",
                    $"Strength/{CurrentMonster.Strength} Defense/{CurrentMonster.Defense}" + CurrentMonster.CurrentHP + $" HP. Difficulty: {CurrentMonster.Difficulty}. What will you do?"
                    });
                UI.DefaultBoxes.DrawOptions(new List<string>
                {
                    "1. Fight",
                    "2. Run"
                }, UI.Grid.Center);
                UI.Draw.UpdateInputCursor();

                var input = Console.ReadLine();

                if (input == "1")
                {
                    HeroTurn();
                }
                else if (input == "2")
                {
                    RunFromFight();
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
            // Check/Add-to Achievements
            Achievements.AddToDefeatedMonsters(CurrentMonster);

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
            UI.Draw.UpdateInputCursor();
            Console.ReadLine();
        }

        private void Lose()
        {
            Console.Clear();
            LostFight = true; // Cuz they died..
            UI.DefaultBoxes.DrawInventory(Hero, UI.Grid.Left);
            UI.Draw.PrintToOutput(new List<string>
                {
                    "You've been defeated! :( GAME OVER.",
                });
            UI.DefaultBoxes.DrawOptions(new List<string>
                {
                    "Press any key to exit the game."
                }, UI.Grid.Center);
            UI.Draw.UpdateInputCursor();
            Console.ReadKey();
        }

        private void EvadeFight()
        {
            Console.Clear();
            LostFight = true; // Lost the fight, but escaped with only a few scratches
            UI.DefaultBoxes.DrawInventory(Hero, UI.Grid.Left);
            UI.DefaultBoxes.DrawOptions(new List<string>
                {
                    "Press any key to continue..."
                }, UI.Grid.Center);
            UI.Draw.PrintToOutput(new List<string>
                {
                    "Cowardice only gets you so far..",
                });
            UI.Draw.UpdateInputCursor();
            Console.ReadKey();
        }

        private void EvadeFailed()
        {
            Console.Clear();
            UI.DefaultBoxes.DrawInventory(Hero, UI.Grid.Left);
            UI.DefaultBoxes.DrawOptions(new List<string>
                    {
                        "Press any key to continue..."
                    }, UI.Grid.Center);
            UI.Draw.PrintToOutput(new List<string>
                        {
                            "You failed to run from the fight...",
                        });
            UI.Draw.UpdateInputCursor();
            Console.ReadLine();
            MonsterTurn();
        }

        private void RunFromFight()
        {
            // roll succeeds; Evade;
            // roll fails, enemy attacks, you stay and have to fight.
            var randomNum = new Random();
            const int baseRoll = 100;
            // if roll is > 50% Success
            const int easyRoll = (baseRoll - 50) + 1;
            // if roll is > 25% Success
            const int mediumRoll = (baseRoll - 25) + 1;
            // if roll is > 5% Success
            const int hardRoll = (baseRoll - 5) + 1;
            int roll = 100 + 1;
            int chanceToEvade;

            roll = randomNum.Next(0, roll);

            switch (CurrentMonster.Difficulty)
            {
                case Difficulty.Easy:
                    chanceToEvade = easyRoll;
                    break;

                case Difficulty.Medium:
                    chanceToEvade = mediumRoll;
                    break;

                case Difficulty.Hard:
                    chanceToEvade = hardRoll;
                    break;

                default:
                    chanceToEvade = easyRoll;
                    break;
            };

            if (roll >= chanceToEvade)
            {
                EvadeFight();
            }
            else
            {
                EvadeFailed();
            }
        }
    }
}