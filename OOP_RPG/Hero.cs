using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_RPG
{
    public class Hero
    {
        // These are the Properties of our Class.
        public string Name { get; set; }
        public int Strength { get; }
        public int Defense { get; }
        public int OriginalHP { get; set; }
        public int CurrentHP { get; set; }
        public int Gold { get; private set; }
        public IItems EquippedWeapon { get; private set; }
        public IItems EquippedArmour { get; private set; }
        public List<IItems> Bag { get; set; }

        /*This is a Constructor.
        When we create a new object from our Hero class, the instance of this class, our hero, has:
        an empty List that has to contain instances of the Armor class,
        an empty List that has to contain instance of the Weapon class,
        stats of the "int" data type, including an intial strength and defense,
        original hitpoints that are going to be the same as the current hitpoints.
        */
        public Hero()
        {
            Bag = new List<IItems>();
            Strength = 10;
            Defense = 10;
            OriginalHP = 30;
            CurrentHP = 30;
            Gold = 400;
        }

        //These are the Methods of our Class.
        public void ShowStats()
        {
            Console.Clear();
            Console.WriteLine("*****" + this.Name + "*****");

            Console.Write("Strength: " + this.Strength);
            if (this.EquippedWeapon != null)
            {
                Console.WriteLine($"(+{this.EquippedWeapon.Strength})");
            }
            else
            {
                Console.WriteLine();
            }

            Console.Write("Defense: " + this.Defense);
            if (this.EquippedArmour != null)
            {
                Console.WriteLine($"(+{this.EquippedArmour.Defense})");
            }
            else
            {
                Console.WriteLine();
            }
            Console.WriteLine("Hitpoints: " + this.CurrentHP + "/" + this.OriginalHP);
        }

        public void ShowInventory()
        {
            Console.Clear();
            Console.WriteLine("*****  INVENTORY ******");
            Console.WriteLine("Weapons: ");

            foreach (var weapon in Items.GetListOfItems(this.Bag, ItemTypes.Weapon))
            {
                Console.WriteLine(weapon.Name + " of " + weapon.Strength + " Strength");
            }

            Console.WriteLine("Armor: ");

            foreach (var armor in Items.GetListOfItems(this.Bag, ItemTypes.Armour))
            {
                Console.WriteLine(armor.Name + " of " + armor.Defense + " Defense");
            }

            Console.WriteLine("Equipped: ");
            if (EquippedWeapon != null)
            {
                Console.WriteLine(EquippedWeapon.Name + " of " + EquippedWeapon.Strength + " Strength");
            }
            if (EquippedArmour != null)
            {
                Console.WriteLine(EquippedArmour.Name + " of " + EquippedArmour.Defense + " Defense");
            }

            Console.WriteLine($"Gold: {this.Gold}");
        }

        public void EquipWeapon(int weaponIndex)
        {
            var WeaponsBag = Items.GetListOfItems(this.Bag, ItemTypes.Weapon);
            if (WeaponsBag.Any())
            {
                WeaponsBag[weaponIndex].Equipped = true;
                this.EquippedWeapon = WeaponsBag[weaponIndex];
            }
        }

        public void EquipArmor(int armourIndex)
        {
            var ArmoursBag = Items.GetListOfItems(this.Bag, ItemTypes.Armour);
            if (ArmoursBag.Any())
            {
                ArmoursBag[armourIndex].Equipped = true;
                this.EquippedArmour = ArmoursBag[armourIndex];
            }
        }

        public void AddGold(int gold)
        {
            Gold += gold;
        }

        public void RemoveGold(int gold)
        {
            if (Gold - gold >= 0)
            {
                Gold -= gold;
            }
            else
            {
                throw new Exception("Negative gold value.");
            }
            // Else you broke dood.
            // Nah this is more of a safe check, this actual check will happen in
            // this store class, and the purchase will be denied if they don't have
            // Enough funds.
        }
    }
}