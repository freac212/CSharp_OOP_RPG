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
            Gold = 0;
        }

        //These are the Methods of our Class.
        public void ShowStats()
        {
            Console.WriteLine("*****" + this.Name + "*****");
            Console.WriteLine("Strength: " + this.Strength);
            Console.WriteLine("Defense: " + this.Defense);
            Console.WriteLine("Hitpoints: " + this.CurrentHP + "/" + this.OriginalHP);
        }

        public void ShowInventory()
        {
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

            Console.WriteLine($"Gold: {this.Gold}");
        }

        public void EquipWeapon()
        {
            if (WeaponsBag.Any())
            {
                this.EquippedWeapon = this.WeaponsBag[0];
            }
        }

        public void EquipArmor()
        {
            if (ArmorsBag.Any())
            {
                this.EquippedArmor = this.ArmorsBag[0];
            }
        }

        public void AddGold(int gold)
        {
            Gold += gold;
        }

        public void RemoveGold(int gold)
        {
            if(Gold - gold >= 0)
            {
                Gold -= gold;
            } else
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