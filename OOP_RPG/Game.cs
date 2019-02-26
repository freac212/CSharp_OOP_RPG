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
                Hero.ShowInventory();
                Console.WriteLine("1. Equip Weapon");
                Console.WriteLine("2. Equip Armour");
                Console.WriteLine("3. UnEquip Weapon");
                Console.WriteLine("4. UnEquip Armour");
                Console.WriteLine("5. UnEquip All");
                Console.WriteLine("6. Use a HealthPotion");
                Console.WriteLine("Press 0 to return to main menu.");
                inventoryInput = Console.ReadLine();

                if (inventoryInput == "1")
                {
                    EquipWeapon();
                }
                else if (inventoryInput == "2")
                {
                    EquipArmour();
                }
                else if (inventoryInput == "3")
                {
                    UnEquipWeapon();
                }
                else if (inventoryInput == "4")
                {
                    UnEquipArmour();
                }
                else if (inventoryInput == "5")
                {
                    UnEquipAll();
                }
                else if (inventoryInput == "6")
                {
                    UseHealthPotion();
                }
            }
        }

        private void UseHealthPotion()
        {
            var potionUseInput = "";
            Console.WriteLine("===== Potions =====");
            if (Items.GetListOfItems(Hero.Bag, typeof(Potion)).Any())
            {
                Console.WriteLine("======= Bag ========");
                UI.Draw.ItemList(Items.GetListOfItems(Hero.Bag, typeof(Potion)));
                Console.WriteLine("====================");
                Console.WriteLine("Choose a potion to use.");
                Console.WriteLine("Or choose 0 to return to Inventory.");

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
                        Console.WriteLine("Potion used!");
                        Console.WriteLine($"Your HP is now {Hero.CurrentHP}/{Hero.OriginalHP}");

                    }
                    else
                    {
                        Console.WriteLine("There's no potion with that number...");
                        Console.WriteLine("Press any key to return to Inventory.");
                        Console.ReadKey();
                    }
                }
            }
            else
            {
                Console.WriteLine("You don't have any health potions... ");
                Console.WriteLine("Go buy some from the store!");
            }
            Console.WriteLine("Press any key to return to Inventory.");
            Console.ReadKey();
        }

        private void UnEquipAll()
        {
            Hero.UpEquip();
            Console.WriteLine("All Items UnEquiped");
            Console.WriteLine("Press any key to return to Inventory.");
            Console.ReadKey();
        }

        private void UnEquipArmour()
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

        private void UnEquipWeapon()
        {
            if (Hero.UpEquip(typeof(Weapon)))
            {
                Console.WriteLine("Weapon UnEquiped.");
            }
            else
            {
                Console.WriteLine("There's no weapon to UnEquip.");

            }
            Console.WriteLine("Press any key to return to Inventory.");
            Console.ReadKey();
        }

        private void EquipArmour()
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

            Console.WriteLine("======= Bag ========");
            UI.Draw.ItemList(Items.GetListOfItems(Hero.Bag, typeof(Armor)));
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

        private void EquipWeapon()
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

            Console.WriteLine("======= Bag ========");
            UI.Draw.ItemList(Items.GetListOfItems(Hero.Bag, typeof(Weapon)));
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

        private void Fight()
        {
            var fight = new Fight(Hero);

            fight.Start();
        }
    }
}