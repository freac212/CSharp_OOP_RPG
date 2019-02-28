using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_RPG
{
    static class Shop
    {
        public static List<IGameItem> ShopItems = new List<IGameItem>
        {
            new Weapon("Arm", 3, 50),
            new Weapon("Box", 1, 5),
            new Weapon("Iron Dagger", 2, 25),
            new Weapon("Iron LongSword", 5, 75),
            new Weapon("Iron ShortSword", 3, 45),
            new Weapon("Iron Sword", 4, 100),
            new Weapon("Katana", 7, 400),
            new Weapon("Small Axe", 3, 50),
            new Weapon("Axe", 5, 120),
            new Weapon("HolyHandGrenade", 5, 120),
            new Weapon("Weapon Of Doom", 8, 1000),
            new Weapon("Obsidian Zweihander", 50, 100000),

            new Armor("Iron Chestplate", 3, 20),
            new Armor("Leather Chestplate", 1, 5),
            new Armor("Chainmail", 2, 15),
            new Armor("Dragon Armour", 10, 10000),

            new Potion("Small Health Potion", 5, 10),
            new Potion("Health Potion", 15, 30),
            new Potion("Large Health Potion", 30, 60),

            new Shield("Round Wooden Shield", 3, 40),
            new Shield("Iron Box Shield", 4, 80),
            new Shield("Steel Box Shield", 5, 100),
            new Shield("Obsidian Shield", 6, 1000)
        };

        public static void CreateShop(Hero hero)
        {
            DrawShop(hero);
            ShopInput(hero);
        }

        public static void DrawShop(Hero hero)
        {
            Console.Clear();
            UI.DefaultBoxes.DrawInventory(hero, UI.Grid.Left);
            UI.DefaultBoxes.DrawStore(ShopItems, UI.Grid.Right);
            UI.DefaultBoxes.DrawOptions(new List<string>
                {
                    "Please choose an option.",
                    "1. Buy an item",
                    "2. Sell an item",
                    "3. Exit Store"
                }, UI.Grid.Center);
        }

        private static void ShopInput(Hero hero)
        {
            var shopMainInput = "0";

            while (shopMainInput != "3")
            {
                DrawShop(hero);

                shopMainInput = Console.ReadLine();

                if (shopMainInput == "1")
                {
                    // Buying an item..
                    var shopInput = "";

                    if (ShopItems.Any())
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
                            continue;
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
                                    UI.Draw.PrintToOutput(new List<string>
                                        {
                                            "Item purchased!",
                                            "Press any button to return to shop..."
                                        });
                                }
                                else
                                {
                                    UI.Draw.PrintToOutput(new List<string>
                                        {
                                            "Not enough gold!",
                                            "Press any button to return to shop..."
                                        });
                                }
                            }
                            else
                            {
                                UI.Draw.PrintToOutput(new List<string>
                                    {
                                        "Input value was either 0 or less",
                                        "Press any button to return to shop..."
                                    });
                            }
                            Console.ReadLine();
                        }
                    }
                    else
                    {
                        UI.Draw.PrintToOutput(new List<string> { "There's no Items to buy, check again later.", "Press any button to return to shop..." });
                        Console.ReadLine();
                        break;
                    }

                }
                else if (shopMainInput == "2")
                {
                    // Buying an item..
                    var sellInput = "";

                    if (hero.Bag.Any())
                    {
                        Console.Clear();
                        UI.DefaultBoxes.DrawInventory(hero, UI.Grid.Left, true);
                        UI.DefaultBoxes.DrawStore(ShopItems, UI.Grid.Right);
                        UI.DefaultBoxes.DrawOptions(new List<string>
                        {
                            "Please choose an item #",
                            "Press enter to exit..",
                        }, UI.Grid.Center);

                        sellInput = Console.ReadLine();
                        if (sellInput == "")
                        {
                            continue;
                        }
                        else
                        {

                            int.TryParse(sellInput, out int sellInputIndex);
                            // Input validation, cannot be 0
                            if (sellInputIndex > 0)
                            {
                                PlayerSellItem(sellInputIndex, hero);
                                UI.Draw.PrintToOutput(new List<string>
                                {
                                    "Item sold!",
                                    "Press any button to return to shop..."
                                });
                            }
                            else
                            {
                                UI.Draw.PrintToOutput(new List<string>
                                {
                                    "Input value was either 0 or less",
                                    "Press any button to return to shop..."
                                });
                            }
                            Console.ReadLine();
                        }
                    }
                    else
                    {
                        UI.Draw.PrintToOutput(new List<string> { "You have no Items to sell, earn gold to buy some!", "Press any button to return to shop..." });
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
                // If item is not a potion, remove from store.
                if (!IsPotion(getItem.GetType()))
                {
                    ShopItems.RemoveAt(itemIndex);
                }
               
                return true;
            }
            else
            {
                // Didn't have enough gold, 
                return false;
            }
        }

        public static int PlayerSellItem(int itemID, Hero hero)
        {
            var itemIndex = itemID - 1;
            var getItem = (from item in hero.Bag
                           where itemIndex == hero.Bag.IndexOf(item)
                           select item).FirstOrDefault();

            // Add to Shop's inventory, remove from Hero's inventory.
            // If the item is not a potion, add it to the shops inventory
            if (!IsPotion(getItem.GetType()))
            {
                ShopItems.Add(getItem);
            }

            hero.RemoveItemFromHero(getItem);
            hero.AddGold(getItem.ResaleValue);
            return getItem.ResaleValue;
        }

        public static bool IsPotion(Type type)
        {
            return type == typeof(Potion) ? true : false;
        }
    }
}
