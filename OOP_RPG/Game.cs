using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_RPG
{
    public class Game
    {
        public Hero Hero { get; }

        public Game()
        {
            Hero = new Hero();
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

            while (input != "5")
            {
                Console.Clear();
                UI.DefaultBoxes.DrawInventory(Hero, UI.Grid.GridLeft());
                // Display options
                UI.DefaultBoxes.DrawOptions(new List<string>
                {
                    "Hello " + Hero.Name,
                    "Please choose a number option.",
                    "1. View Stats",
                    "2. View Inventory",
                    "3. Fight Monster",
                    "4. Enter Store",
                    "5. Exit"
                }, UI.Grid.GridCenter());
                // Display output

                input = Console.ReadLine();

                if (input == "1")
                {
                    this.Stats();
                }
                else if (input == "2")
                {
                    this.Inventory();
                }
                else if (input == "3")
                {
                    this.Fight();
                }
                else if (input == "4")
                {
                    Shop.CreateShop(Hero);
                }

                if (Hero.CurrentHP <= 0)
                {
                    return;
                }
            }
        }

        private void Stats()
        {
            Hero.ShowStats();

            Console.WriteLine("Press any key to return to main menu.");
            Console.ReadKey();
        }

        private void Inventory()
        {
            Console.Clear();
            var inventoryInput = "";

            while (inventoryInput != "0")
            {
                Hero.ShowInventory();
                Console.WriteLine("1. Equip Weapon");
                Console.WriteLine("2. Equip Armour");
                Console.WriteLine("3. UnEquip Weapon");
                Console.WriteLine("4. UnEquip Armour");
                Console.WriteLine("5. UnEquip All");
                Console.WriteLine("Press 0 to return to main menu.");
                inventoryInput = Console.ReadLine();

                if (inventoryInput == "1")
                {
                    // Show weapons 
                    //  Equipped
                    //  Un-Equipped
                    // Have options 
                    //  Equip weapon
                    //  Un-Equip Current Weapon

                    var weaponEquip = "";
                    Console.WriteLine("===== Equipped =====");
                    if (Hero.EquippedWeapon != null)
                    {
                        Console.WriteLine($"Current Weapon: {Hero.EquippedWeapon.Name}");
                    }
                    else
                    {
                        Console.WriteLine("No weapon is currently equipped.");
                    }

                    Console.WriteLine("====================");
                    Console.WriteLine("======= Bag ========");
                    UI.DisplayItemList(Items.GetListOfItems(Hero.Bag, typeof(Weapon)));
                    Console.WriteLine("====================");
                    Console.WriteLine("Choose a weapon number to equip.");
                    Console.WriteLine("Or choose 0 to return to Inventory.");

                    weaponEquip = Console.ReadLine();

                    while (!int.TryParse(weaponEquip, out int _x))
                    {
                        Console.WriteLine("That is not a number, please choose again.");
                        weaponEquip = Console.ReadLine();
                    }

                    // If the input can be parsed as a number
                    if (int.TryParse(weaponEquip, out int weaponIndex) && weaponIndex != 0)
                    {
                        // If the number is an index of weapons bag
                        if ((weaponIndex -= 1) <= Items.GetListOfItems(Hero.Bag, typeof(Weapon)).Count - 1)
                        {
                            // Then Equip that weapon
                            Hero.Equip(weaponIndex, typeof(Weapon));
                        }
                        else
                        {
                            Console.WriteLine("There's no Weapon with that number...");
                            Console.WriteLine("Press any key to return to Inventory.");
                            Console.ReadKey();
                        }
                    }
                }
                else if (inventoryInput == "2")
                {
                    var armourEquipInput = "";
                    Console.WriteLine("===== Equipped =====");
                    if (Hero.EquippedArmour != null)
                    {
                        Console.WriteLine($"Current Armour: {Hero.EquippedArmour.Name}");
                    }
                    else
                    {
                        Console.WriteLine("No Armour is currently equipped.");
                    }

                    Console.WriteLine("====================");
                    Console.WriteLine("======= Bag ========");
                    UI.DisplayItemList(Items.GetListOfItems(Hero.Bag, typeof(Armor)));
                    Console.WriteLine("====================");
                    Console.WriteLine("Choose an Armour number to equip.");
                    Console.WriteLine("Or choose 0 to return to Inventory.");

                    armourEquipInput = Console.ReadLine();

                    while (!int.TryParse(armourEquipInput, out int _x))
                    {
                        Console.WriteLine("That is not a number, please choose again.");
                        armourEquipInput = Console.ReadLine();
                    }

                    // If the input can be parsed as a number
                    if (int.TryParse(armourEquipInput, out int armourIndex) && armourIndex != 0)
                    {
                        // If the number is an index of armour bag
                        if ((armourIndex -= 1) <= Items.GetListOfItems(Hero.Bag, typeof(Armor)).Count - 1)
                        {
                            // Then Equip that Armour
                            Hero.Equip(armourIndex, typeof(Armor));
                        }
                        else
                        {
                            Console.WriteLine("There's no Armour with that number...");
                            Console.WriteLine("Press any key to return to Inventory.");
                            Console.ReadKey();
                        }
                    }
                }
                else if (inventoryInput == "3")
                {
                    if (Hero.UpEquip(typeof(Weapon)))
                    {
                        Console.WriteLine("Weapon UnEquiped.");
                    } else
                    {
                        Console.WriteLine("There's no weapon to UnEquip.");

                    }
                    Console.WriteLine("Press any key to return to Inventory.");
                    Console.ReadKey();
                }
                else if (inventoryInput == "4")
                {
                    if (Hero.UpEquip(typeof(Armor)))
                    {
                        Console.WriteLine("Armour UnEquiped.");
                    }
                    else
                    {
                        Console.WriteLine("There's no Armour to UnEquip.");

                    }
                    Console.WriteLine("Press any key to return to Inventory.");
                    Console.ReadKey();
                }
                else if (inventoryInput == "5")
                {
                    Hero.UpEquip();
                    Console.WriteLine("All Items UnEquiped");
                    Console.WriteLine("Press any key to return to Inventory.");
                    Console.ReadKey();
                }
            }
        }

        private void Fight()
        {
            var fight = new Fight(Hero);

            fight.Start();
        }
    }
}