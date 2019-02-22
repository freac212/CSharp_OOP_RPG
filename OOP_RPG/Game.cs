using System;

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
                Console.WriteLine("Hello " + Hero.Name);
                Console.WriteLine("Please choose an option by entering a number.");
                Console.WriteLine("1. View Stats");
                Console.WriteLine("2. View Inventory");
                Console.WriteLine("3. Fight Monster");
                Console.WriteLine("4. Enter Store");
                Console.WriteLine("5. Exit");

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
                    UI.DisplayWeaponList(Hero.WeaponsBag);
                    Console.WriteLine("====================");
                    Console.WriteLine("Choose a weapon number to equip.");
                    Console.WriteLine("Or choose 0 to return to Inventory.");

                    weaponEquip = Console.ReadLine();

                    while(!int.TryParse(weaponEquip, out int _x))
                    {
                        Console.WriteLine("That is not a number, please choose again.");
                        weaponEquip = Console.ReadLine();
                    }
                    // If the input can be parsed as a number
                    if (int.TryParse(weaponEquip, out int weaponIndex) && weaponIndex != 0)
                    {
                        // If the number is an index of weapons bag
                        if ((weaponIndex -= 1) <= Hero.WeaponsBag.Count-1)
                        {
                            // Then Equip that weapon
                            Hero.EquipWeapon(weaponIndex);
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
                    var armourEquipInput= "";
                    Console.WriteLine("===== Equipped =====");
                    if (Hero.EquippedArmor != null)
                    {
                        Console.WriteLine($"Current Armour: {Hero.EquippedArmor.Name}");
                    }
                    else
                    {
                        Console.WriteLine("No Armour is currently equipped.");
                    }

                    Console.WriteLine("====================");
                    Console.WriteLine("======= Bag ========");
                    UI.DisplayArmourList(Hero.ArmorsBag);
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
                        if ((armourIndex -= 1) <= Hero.ArmorsBag.Count-1)
                        {
                            // Then Equip that Armour
                            Hero.EquipArmor(armourIndex);
                        }
                        else
                        {
                            Console.WriteLine("There's no Armour with that number...");
                            Console.WriteLine("Press any key to return to Inventory.");
                            Console.ReadKey();
                        }
                    }
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