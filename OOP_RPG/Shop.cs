﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_RPG
{
    static class Shop
    {
        // Sort by options later on?
        // TODO: Allow equiping in the store
        // TODO: Avoid passing around hero, right?
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
            new Potion("Small Health Potion", 5, 10),
            new Potion("Health Potion", 15, 30),
            new Potion("Large Health Potion", 30, 60),
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
                    "2. Exit Store"
                }, UI.Grid.Center);
        }

        private static void ShopInput(Hero hero)
        {
            var shopMainInput = "0";

            while (shopMainInput != "2")
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
                                    UI.Draw.PrintToOutput(new List<string>{
                                        "Item purchased!",
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
