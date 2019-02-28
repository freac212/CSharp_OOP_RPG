using System;
using System.Collections.Generic;
using System.Linq;

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
                    $"Strength/{CurrentMonster.Strength} Defense/{CurrentMonster.Defense} HP/{CurrentMonster.CurrentHP} Difficulty: {CurrentMonster.Difficulty}. What will you do?"
                    });
                UI.DefaultBoxes.DrawOptions(new List<string>
                {
                    "1. Fight",
                    "2. Use a Potion",
                    "3. Run"
                }, UI.Grid.Center);
                UI.Draw.UpdateInputCursor();

                var input = Console.ReadLine();

                if (input == "1")
                {
                    HeroTurn();
                }
                else if (input == "2")
                {
                    UseAPotion();
                }
                else if (input == "3")
                {
                    RunFromFight();
                }
            }
        }

        private void UseAPotion()
        {
            Game.UseHealthPotion(Hero);
        }

        private void HeroTurn()
        {
            var randomNum = new Random();

            int compare;
            if (Hero.EquippedWeapon != null)
            {
                // Maybe make this into a method
                var baseDamage = (Hero.Strength + Hero.EquippedWeapon.GetAttribute()) - CurrentMonster.Defense;
                int minDamage = (int)(baseDamage * Settings.HeroDamageMinMultiplier);
                int maxDamage = (int)(baseDamage * Settings.HeroDamageMaxMultiplier);
                compare = randomNum.Next(minDamage, maxDamage);
            }
            else
            {
                var baseDamage = Hero.Strength - CurrentMonster.Defense;
                int minDamage = (int)(baseDamage * Settings.HeroDamageMinMultiplier);
                int maxDamage = (int)(baseDamage * Settings.HeroDamageMaxMultiplier);
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
            Hero.IncrementMonsterKills();

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

        private void EvadeSucceeded()
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
            int roll = 100 + 1;
            int chanceToEvade;

            roll = randomNum.Next(0, roll);

            switch (CurrentMonster.Difficulty)
            {
                case Difficulty.Easy:
                    chanceToEvade = Settings.EasyRoll;
                    break;

                case Difficulty.Medium:
                    chanceToEvade = Settings.MediumRoll;
                    break;

                case Difficulty.Hard:
                    chanceToEvade = Settings.HardRoll;
                    break;

                default:
                    chanceToEvade = Settings.EasyRoll;
                    break;
            };

            if (roll >= chanceToEvade)
            {
                EvadeSucceeded();
            }
            else
            {
                EvadeFailed();
            }
        }
    }
}