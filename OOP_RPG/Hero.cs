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
        public Weapon EquippedWeapon { get; private set; }
        public Armor EquippedArmor { get; private set; }
        public List<Armor> ArmorsBag { get; set; }
        public List<Weapon> WeaponsBag { get; set; }

        /*This is a Constructor.
        When we create a new object from our Hero class, the instance of this class, our hero, has:
        an empty List that has to contain instances of the Armor class,
        an empty List that has to contain instance of the Weapon class,
        stats of the "int" data type, including an intial strength and defense,
        original hitpoints that are going to be the same as the current hitpoints.
        */
        public Hero()
        {
            ArmorsBag = new List<Armor>();
            WeaponsBag = new List<Weapon>();
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
            if (this.EquippedWeapon != null)
            {
                Console.WriteLine($"(+{this.EquippedArmor.Defense})");
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

            foreach (var weapon in this.WeaponsBag)
            {
                Console.WriteLine(weapon.Name + " of " + weapon.Strength + " Strength");
            }

            Console.WriteLine("Armor: ");

            foreach (var armor in this.ArmorsBag)
            {
                Console.WriteLine(armor.Name + " of " + armor.Defense + " Defense");
            }

            Console.WriteLine("Equipped: ");
            if(EquippedWeapon != null)
            {
                Console.WriteLine(EquippedWeapon.Name + " of " + EquippedWeapon.Strength + " Strength");
            } 
            if(EquippedArmor != null)
            {
                Console.WriteLine(EquippedArmor.Name + " of " + EquippedArmor.Defense + " Defense");
            }

            Console.WriteLine($"Gold: {this.Gold}");
        }

        public void EquipWeapon(int weaponIndex)
        {
            if (WeaponsBag.Any())
            {
                this.WeaponsBag[weaponIndex].Equipped = true;
                this.EquippedWeapon = this.WeaponsBag[weaponIndex];
            }
        }

        public void EquipArmor(int armourIndex)
        {
            if (ArmorsBag.Any())
            {
                this.ArmorsBag[armourIndex].Equipped = true;
                this.EquippedArmor = this.ArmorsBag[armourIndex];
            }
        }

        public void UnEquipWeapon()
        {
            if (WeaponsBag.Any())
            {
                this.EquippedWeapon = null;
            }
        }

        public void UnEquipArmor()
        {
            if (WeaponsBag.Any())
            {
                this.EquippedWeapon = null;
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