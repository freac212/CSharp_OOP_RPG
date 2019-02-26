using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_RPG
{
    static class Shop
    {
        // Sort by options later on?
        // TODO: maybe hide Buy weapon, buy armour, if their inventory is empty.
        // TODO: Allow equiping in the store
        // TODO: Avoid passing around hero, right?

        // TODO: Can also use a single list of both ShopWeapons and ShopArmours, calling it Shop inventory, this is 
        // just remnants of pre Items interface update.
        // TODO: When displaying items, sort the list by type, then display, maybe allow the user to sort the list by item??
        public static List<IGameItem> ShopItems = new List<IGameItem>
        {
            new Weapon("Arm", 3, 20),
            new Weapon("Box", 1, 5),
            new Weapon("Iron Dagger", 2, 15),
            new Weapon("Iron LongSword", 5, 50),
            new Weapon("Iron ShortSword", 3, 19),
            new Weapon("Iron Sword", 4, 30),
            new Weapon("Katana", 7, 150),
            new Weapon("HolyHandGrenade", 5, 100),
            new Weapon("Weapon Of Doom", 9, 400),
            new Weapon("Obsidian Zweihander", 50, 100000),
            new Armor("Iron Chestplate", 3, 20),
            new Armor("Leather Chestplate", 1, 5),
            new Armor("Dragon Armour Set", 25, 10000),
        };

        public static void CreateShop(Hero hero)
        {
            DrawShop(hero);
            ShopInput(hero);
        }

        public static void DrawShop(Hero hero)
        {
            Console.Clear();
            //Console.WriteLine("===== Welcome to the Shop! =====");
            //Console.WriteLine("====== Items for Sale: =======");

            //if (ShopItems.Any())
            //{
            //    UI.DisplayItemList(ShopItems);
            //}

            //Console.WriteLine("======== Hero inventory ========");
            //Console.WriteLine($"## {hero.Name}'s gold: ${hero.Gold} ##");
            //Console.WriteLine("____________ Weapons ___________");
            //// TODO: Send weapon class type into these two methods
            //UI.DisplayItemList(Items.GetListOfItems(hero.Bag, typeof(Weapon)));
            //Console.WriteLine("____________ Armour ____________");
            //UI.DisplayItemList(Items.GetListOfItems(hero.Bag, typeof(Armor)));
            //Console.WriteLine("================================");


        }


        private static void ShopInput(Hero hero)
        {
            var shopMainInput = "0";

            while (shopMainInput != "3")
            {
                //DrawShop(hero);
                Console.Clear();
                UI.DefaultBoxes.DrawInventory(hero, UI.Grid.Left);
                UI.DefaultBoxes.DrawStore(ShopItems, UI.Grid.Right);
                UI.DefaultBoxes.DrawOptions(new List<string>
                {
                    "Please choose an option.",
                    "1. Buy a Weapon",
                    "2. Buy Armour",
                    "3. Exit Store"
                }, UI.Grid.Center);

                shopMainInput = Console.ReadLine();

                if (shopMainInput == "1")
                {
                    // Buying a weapon..

                    DrawShop(hero);

                    var shopInput = "";

                    if (Items.GetListOfItems(ShopItems, typeof(Weapon)).Any())
                    {
                        Console.Clear();
                        UI.DefaultBoxes.DrawInventory(hero, UI.Grid.Left);
                        UI.DefaultBoxes.DrawStore(ShopItems, UI.Grid.Right);
                        UI.DefaultBoxes.DrawOptions(new List<string>
                        {
                            "Please choose an item #",
                            "Press enter to exit..",
                        }, UI.Grid.Center);

                        shopInput = Console.ReadLine();
                        if (shopInput == "")
                        {
                            break;
                        }
                        else
                        {

                            int.TryParse(shopInput, out int shopInputConverted);
                            // Input validation, cannot be 0
                            if (shopInputConverted > 0)
                            {
                                // If the item is purchased, return true.
                                if (BuyItem(shopInputConverted, hero))
                                {
                                    UI.Draw.PrintToOutput(new List<string>{
                                        "Weapon purchased!",
                                        "Press any button to return to shop..."
                                    });
                                }
                                else
                                {
                                    UI.Draw.PrintToOutput(new List<string> {
                                        "Not enough gold!",
                                        "Press any button to return to shop..."
                                    });
                                }
                            }
                            else
                            {
                                UI.Draw.PrintToOutput(new List<string> {
                                    "Input value was either 0 or less",
                                    "Press any button to return to shop..."
                                });
                            }
                            Console.ReadLine();
                        }
                    }
                    else
                    {
                        UI.Draw.PrintToOutput(new List<string> { "There's no Weapons to buy, check again later.", "Press any button to return to shop..." });
                        Console.ReadLine();
                        break;
                    }

                }
                else if (shopMainInput == "2")
                {
                    // Buying armour
                    var shopInput = "";

                    DrawShop(hero);

                    if (Items.GetListOfItems(ShopItems, typeof(Armor)).Any())
                    {
                        Console.Clear();
                        UI.DefaultBoxes.DrawInventory(hero, UI.Grid.Left);
                        UI.DefaultBoxes.DrawStore(ShopItems, UI.Grid.Right);
                        UI.DefaultBoxes.DrawOptions(new List<string>
                        {
                            "Please choose an item #",
                            "Press enter to exit..",
                        }, UI.Grid.Center);
                        shopInput = Console.ReadLine();

                        if (shopInput == "")
                        {
                            break;
                        }
                        else
                        {

                            int.TryParse(shopInput, out int shopInputConverted);
                            // Input validation, cannot be 0 or less
                            if (shopInputConverted > 0)
                            {
                                // If the item is purchased, return true.
                                if (BuyItem(shopInputConverted, hero))
                                {
                                    UI.Draw.PrintToOutput("Armour purchased!");
                                }
                                else
                                {
                                    UI.Draw.PrintToOutput("Not enough gold!");
                                }
                            }
                            else
                            {
                                UI.Draw.PrintToOutput("Input value was either 0 or less");
                            }

                            UI.Draw.PrintToOutput("Press any button to return to shop...");
                            Console.ReadLine();
                        }
                    }
                    else
                    {
                        UI.Draw.PrintToOutput(new List<string> { "There's no Armour to buy, check again later.", "Press any button to return to shop..." });
                        Console.ReadLine();
                        break;
                    }

                }
            }
            Console.Clear();
        }

        public static bool BuyItem(int itemID, Hero hero)
        {
            var itemIndex = itemID - 1;
            var getItem = (from item in ShopItems
                           where itemIndex == ShopItems.IndexOf(item)
                           select item).FirstOrDefault();

            // Check if the hero has enough money
            if (hero.Gold >= getItem.Value)
            {
                // Add to Hero's inventory, remove from stores inventory.
                hero.Bag.Add(getItem);
                hero.RemoveGold(getItem.Value);
                ShopItems.RemoveAt(itemIndex);
                return true;
            }
            else
            {
                // Didn't have enough gold, 
                return false;
            }
        }
    }
}
