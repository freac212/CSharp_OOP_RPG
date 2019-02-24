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
        // Note: maybe hide Buy weapon, buy armour, if their inventory is empty.
        // Allow equiping in the store
        // Avoid passing around hero, right?

        public static List<IItems> ShopWeapons = new List<IItems>
        {
            new Weapon("Arm", 3, 20),
            new Weapon("Box", 1, 5),
            new Weapon("Iron Dagger", 2, 15),
            new Weapon("Iron LongSword", 5, 50),
            new Weapon("Iron ShortSword", 3, 19),
            new Weapon("Iron Sword", 4, 30),
            new Weapon("Katana", 7, 150),
            new Weapon("HolyHandGrenade", 5, 100),
            new Weapon("Obsidian Zweihander", 50, 100000),
        };

        public static List<IItems> ShopArmours = new List<IItems>
        {
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
            Console.WriteLine("===== Welcome to the Shop! =====");
            Console.WriteLine("====== Weapons for Sale: =======");

            if (ShopWeapons.Any())
            {
                UI.DisplayItemList(ShopWeapons);
            }
            else
            {
                Console.WriteLine(" > No Weapons in stock...");
            }

            Console.WriteLine("======= Armour for Sale: =======");

            if (ShopArmours.Any())
            {
                UI.DisplayItemList(ShopArmours);
            }
            else
            {
                Console.WriteLine(" > No Armour in stock...");
            }

            Console.WriteLine("======== Hero inventory ========");
            Console.WriteLine($"## {hero.Name}'s gold: ${hero.Gold} ##");
            Console.WriteLine("____________ Weapons ___________");
            UI.DisplayItemList(Items.GetListOfItems(hero.Bag, ItemTypes.Weapon));
            Console.WriteLine("____________ Armour ____________");
            UI.DisplayItemList(Items.GetListOfItems(hero.Bag, ItemTypes.Armour));
            Console.WriteLine("================================");

        }


        private static void ShopInput(Hero hero)
        {
            var shopMainInput = "0";

            while (shopMainInput != "3")
            {
                DrawShop(hero);

                Console.WriteLine("Please choose an option by entering a number.");
                Console.WriteLine("1. Buy a Weapon");
                Console.WriteLine("2. Buy Armour");
                Console.WriteLine("3. Exit Store");

                shopMainInput = Console.ReadLine();

                if (shopMainInput == "1")
                {
                    // Buying a weapon..

                    DrawShop(hero);

                    var shopInput = "";

                    if (ShopWeapons.Any())
                    {
                        Console.WriteLine("Please choose a weapon number from the list above.");
                        Console.WriteLine("Press enter with empty response to exit..");

                        shopInput = Console.ReadLine();
                        if (shopInput == "")
                        {
                            break;
                        }
                        else
                        {
                            // Input needs validation, cannot be 0
                            int.TryParse(shopInput, out int shopInputConverted);
                            if (BuyWeapon(shopInputConverted, hero))
                            {
                                Console.WriteLine("Weapon purchased!");
                            }
                            else
                            {
                                Console.WriteLine("Not enough gold!");
                            }
                            Console.WriteLine("Press any button to return to shop...");
                            Console.ReadLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("There's no Weapons to buy, check again later.");
                        Console.WriteLine("Press any button to return to shop...");
                        Console.ReadLine();
                        break;
                    }

                }
                else if (shopMainInput == "2")
                {
                    // Buying armour
                    var shopInput = "";

                    DrawShop(hero);

                    if (ShopArmours.Any())
                    {
                        Console.WriteLine("Please choose an Armour number from the list above.");
                        Console.WriteLine("Press enter with empty response to exit..");
                        shopInput = Console.ReadLine();

                        if (shopInput == "")
                        {
                            break;
                        }
                        else
                        {
                            // Input needs validation, cannot be 0
                            int.TryParse(shopInput, out int shopInputConverted);
                            if (BuyArmour(shopInputConverted, hero))
                            {
                                Console.WriteLine("Armour purchased!");
                            }
                            else
                            {
                                Console.WriteLine("Not enough gold!");
                            }
                            Console.WriteLine("Press any button to return to shop...");
                            Console.ReadLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("There's no Armour to buy, check again later.");
                        Console.WriteLine("Press any button to return to shop...");
                        Console.ReadLine();
                        break;
                    }

                }
            }
            Console.Clear();
        }

        public static bool BuyWeapon(int weaponID, Hero hero)
        {
            // Get the Weapon they're looking for.
            var weaponIndex = weaponID - 1;
            var getWeapon = (from weapon in ShopWeapons
                             where weaponIndex == ShopWeapons.IndexOf(weapon)
                             select weapon).FirstOrDefault();

            // Check if the hero has enough money
            if (hero.Gold >= getWeapon.Value)
            {
                // Add to Hero's inventory, remove from stores inventory.
                hero.Bag.Add(getWeapon);
                hero.RemoveGold(getWeapon.Value);
                ShopWeapons.RemoveAt(weaponIndex);
                return true;
            }
            else
            {
                // Didn't have enough gold, 
                return false;
            }
        }

        public static bool BuyArmour(int armourID, Hero hero)
        {
            // Get the Armour they're looking for.
            var armourIndex = armourID - 1;
            var getArmour = (from armour in ShopArmours
                             where armourIndex == ShopArmours.IndexOf(armour)
                             select armour).FirstOrDefault();

            // Check if the hero has enough money
            if (hero.Gold >= getArmour.Value)
            {
                // Add to Hero's inventory, remove from stores inventory.
                hero.Bag.Add(getArmour);
                hero.RemoveGold(getArmour.Value);
                ShopArmours.RemoveAt(armourIndex);
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
