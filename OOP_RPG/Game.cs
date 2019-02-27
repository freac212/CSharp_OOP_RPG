using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_RPG
{
    public class Game
    {
        public Hero Hero { get; }
        public AchievementList achievements { get; }

        public Game()
        {
            Hero = new Hero();
            achievements = new AchievementList();
        }

        public void Start()
        {

            Console.WriteLine("Welcome hero!");
            Console.WriteLine("Please enter your name:");

            Hero.Name = Console.ReadLine();

            Main();
        }

        private void Main()
        {
            var input = "0";

            while (input != "4")
            {
                Console.Clear();
                UI.DefaultBoxes.DrawInventory(Hero, UI.Grid.Left);
                UI.DefaultBoxes.DrawOptions(new List<string>
                {
                    "Hello " + Hero.Name,
                    "Please choose a number option.",
                    "1. Manage Inventory",
                    "2. Fight Monster",
                    "3. Enter Store",
                    "4. Exit"
                }, UI.Grid.Center);
                UI.DefaultBoxes.DrawAchievements(achievements, UI.Grid.Right);
                UI.Draw.UpdateInputCursor();

                input = Console.ReadLine();

                if (input == "1")
                {
                    this.Inventory();
                }
                else if (input == "2")
                {
                    this.Fight();
                }
                else if (input == "3")
                {
                    Shop.CreateShop(Hero);
                }

                if (Hero.CurrentHP <= 0)
                {
                    return;
                }
            }
        }

        private void Inventory()
        {
            Console.Clear();
            var inventoryInput = "";

            while (inventoryInput != "0")
            {
                Console.Clear();
                UI.DefaultBoxes.DrawInventory(Hero, UI.Grid.Left);
                UI.DefaultBoxes.DrawOptions(new List<string>
                {
                    "1. Equip Weapon",
                    "2. Equip Armour",
                    "3. Equip Shield",
                    "4. UnEquip Weapon",
                    "5. UnEquip Armour",
                    "6. UnEquip Shield",
                    "7. UnEquip All",
                    "8. Use a HealthPotion",
                    "Press 0 to return to main menu."
                }, UI.Grid.Center);
                UI.Draw.UpdateInputCursor();

                inventoryInput = Console.ReadLine();

                if (inventoryInput == "1")
                {
                    Equip(typeof(Weapon));
                }
                else if (inventoryInput == "2")
                {
                    Equip(typeof(Armor));
                }
                else if (inventoryInput == "3")
                {
                    Equip(typeof(Shield));
                }
                else if (inventoryInput == "4")
                {
                    UnEquip(typeof(Weapon));
                }
                else if (inventoryInput == "5")
                {
                    UnEquip(typeof(Armor));
                }
                else if (inventoryInput == "6")
                {
                    UnEquip(typeof(Shield));
                }
                else if (inventoryInput == "7")
                {
                    UnEquipAll();
                }
                else if (inventoryInput == "8")
                {
                    UseHealthPotion();
                }
            }
        }

        private void UseHealthPotion()
        {
            Console.Clear();
            UI.DefaultBoxes.DrawManageInventory(Hero, UI.Grid.Left, Items.GetListOfItems(Hero.Bag, typeof(Potion)));
            var potionUseInput = "";
            if (Items.GetListOfItems(Hero.Bag, typeof(Potion)).Any())
            {
                UI.DefaultBoxes.DrawOptions(new List<string>
                {
                    "Choose an item to use number to use!",
                    "Press 0 to return to main menu."
                }, UI.Grid.Center);
                UI.Draw.UpdateInputCursor();

                potionUseInput = Console.ReadLine();

                while (!int.TryParse(potionUseInput, out int _x))
                {
                    Console.WriteLine("That is not a number, please choose again.");
                    potionUseInput = Console.ReadLine();
                }

                // If the input can be parsed as a number
                if (int.TryParse(potionUseInput, out int potionIndex) && potionIndex != 0)
                {
                    // If the number is an index of health potion bag
                    if ((potionIndex -= 1) <= Items.GetListOfItems(Hero.Bag, typeof(Potion)).Count - 1)
                    {
                        // Then use that potion
                        Hero.UseHealthPotion(potionIndex);
                        UI.Draw.PrintToOutput(new List<string> {
                            "Item used!",
                            $"Your HP is now {Hero.CurrentHP}/{Hero.OriginalHP}"
                        });
                    }
                    else
                    {
                        UI.Draw.PrintToOutput(new List<string> {
                            "There's no potion with that number..."
                        });
                    }
                    UI.DefaultBoxes.DrawOptions(new List<string> { "Press any key to return to Inventory." }, UI.Grid.Center);
                    UI.Draw.UpdateInputCursor();
                    Console.ReadKey();
                }
            }
            else
            {
                UI.DefaultBoxes.DrawOptions(new List<string> { "Press any key to return to Inventory." }, UI.Grid.Center);
                UI.Draw.PrintToOutput(new List<string> {
                    "You don't have any health potions... ",
                    "Go buy some from the store!"
                });
                UI.Draw.UpdateInputCursor();
                Console.ReadKey();
            }
        }

        private void UnEquipAll()
        {
            Hero.UpEquip();
            Console.Clear();
            UI.DefaultBoxes.DrawInventory(Hero, UI.Grid.Left);
            UI.Draw.PrintToOutput("All Items UnEquiped");
            UI.DefaultBoxes.DrawOptions(new List<string>
                {
                    "Press any key to return to main menu."
                }, UI.Grid.Center);
            UI.Draw.UpdateInputCursor();
            Console.ReadKey();
        }

        private void UnEquip(Type type)
        {
            Console.Clear();
            UI.DefaultBoxes.DrawInventory(Hero, UI.Grid.Left);
            if (Hero.UpEquip(type))
            {
                UI.Draw.PrintToOutput("Item UnEquiped.");
            }
            else
            {
                UI.Draw.PrintToOutput("There was no item to UnEquip..");
            }
            UI.DefaultBoxes.DrawOptions(new List<string> { "Press any key to return to Inventory." }, UI.Grid.Center);
            UI.Draw.UpdateInputCursor();
            Console.ReadKey();
        }

        private void Equip(Type itemType)
        {
            var equipInput = "";

            Console.Clear();
            if (itemType == typeof(Weapon) && Items.GetListOfItems(Hero.Bag, typeof(Weapon)).Any())
            {
                UI.DefaultBoxes.DrawManageInventory(Hero, UI.Grid.Left, Items.GetListOfItems(Hero.Bag, typeof(Weapon)));
                UI.DefaultBoxes.DrawOptions(new List<string>
                {
                    "Choose an item number to equip..",
                    "Press 0 to return to main menu."
                }, UI.Grid.Center);
                TakeInput();
            }
            else if (itemType == typeof(Armor) && Items.GetListOfItems(Hero.Bag, typeof(Armor)).Any())
            {
                UI.DefaultBoxes.DrawManageInventory(Hero, UI.Grid.Left, Items.GetListOfItems(Hero.Bag, typeof(Armor)));
                UI.DefaultBoxes.DrawOptions(new List<string>
                {
                    "Choose an item number to equip..",
                    "Press 0 to return to main menu."
                }, UI.Grid.Center);
                TakeInput();
            }
            else if (itemType == typeof(Shield) && Items.GetListOfItems(Hero.Bag, typeof(Shield)).Any())
            {
                UI.DefaultBoxes.DrawManageInventory(Hero, UI.Grid.Left, Items.GetListOfItems(Hero.Bag, typeof(Shield)));
                UI.DefaultBoxes.DrawOptions(new List<string>
                {
                    "Choose an item number to equip..",
                    "Press 0 to return to main menu."
                }, UI.Grid.Center);
                TakeInput();
            }
            else
            {
                UI.DefaultBoxes.DrawInventory(Hero, UI.Grid.Left);
                UI.Draw.PrintToOutput("There's no item of that type in your bag.");
                UI.DefaultBoxes.DrawOptions(new List<string>
                {
                    "Press any key to return to main menu."
                }, UI.Grid.Center);
            }



            void TakeInput()
            {
                UI.Draw.UpdateInputCursor();
                equipInput = Console.ReadLine();
                
                while (!int.TryParse(equipInput, out int _x))
                {
                    UI.Draw.PrintToOutput("That is not a number, please choose again.");
                    equipInput = Console.ReadLine();
                }

                // If the input can be parsed as a number
                if (int.TryParse(equipInput, out int itemIndex) && itemIndex != 0)
                {
                    Console.Clear();
                    UI.DefaultBoxes.DrawInventory(Hero, UI.Grid.Left);
                    // If the number is an index of armour bag
                    if ((itemIndex -= 1) <= Items.GetListOfItems(Hero.Bag, itemType).Count - 1)
                    {
                        // Then Equip that Item
                        Hero.Equip(itemIndex, itemType);
                        UI.Draw.PrintToOutput("Item equipped!");
                    }
                    else
                    {
                        UI.Draw.PrintToOutput("There's no item with that number...");
                    }
                    UI.DefaultBoxes.DrawOptions(new List<string> { "Press any key to return to Inventory." }, UI.Grid.Center);
                    UI.Draw.UpdateInputCursor();
                    Console.ReadKey();
                }
            }
        }
        private void Fight()
        {
            var fight = new Fight(Hero, achievements);

            fight.Start();
        }
    }
}