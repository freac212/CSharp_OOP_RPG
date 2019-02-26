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
        // FIX
        public IGameItem EquippedWeapon { get; private set; }
        public IGameItem EquippedArmour { get; private set; }
        public List<IGameItem> Bag { get; set; }

        /*This is a Constructor.
        When we create a new object from our Hero class, the instance of this class, our hero, has:
        an empty List that has to contain instances of the Armor class,
        an empty List that has to contain instance of the Weapon class,
        stats of the "int" data type, including an intial strength and defense,
        original hitpoints that are going to be the same as the current hitpoints.
        */
        public Hero()
        {
            Bag = new List<IGameItem>();
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
                Console.WriteLine($"(+{this.EquippedWeapon.GetAttribute()})");
            }
            else
            {
                Console.WriteLine();
            }

            Console.Write("Defense: " + this.Defense);
            if (this.EquippedArmour != null)
            {
                Console.WriteLine($"(+{this.EquippedArmour.GetAttribute()})");
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

            foreach (var weapon in Items.GetListOfItems(this.Bag, typeof(Weapon)))
            {
                weapon.GetDescription();
            }

            Console.WriteLine("Armor: ");

            foreach (var armor in Items.GetListOfItems(this.Bag, typeof(Armor)))
            {
                armor.GetDescription();
            }

            Console.WriteLine("Equipped: ");
            if (EquippedWeapon != null)
            {
                EquippedWeapon.GetDescription();
            }
            if (EquippedArmour != null)
            {
                EquippedArmour.GetDescription();
            }

            Console.WriteLine($"Gold: {this.Gold}");
        }

        public void Equip(int weaponIndex, Type itemType)
        {
            var itemBag = Items.GetListOfItems(this.Bag, itemType);
            if (itemBag.Any())
            {
                if (itemType == typeof(Weapon))
                {
                    this.EquippedWeapon = itemBag[weaponIndex];
                }
                else if (itemType == typeof(Armor))
                {
                    this.EquippedArmour = itemBag[weaponIndex];
                }
            }
        }

        public bool UpEquip(Type type = null)
        {
            if (type == typeof(Weapon))
            {
                if (this.EquippedWeapon == null)
                {
                    return false;
                }
                else
                {
                    var WeaponsBag = Items.GetListOfItems(this.Bag, typeof(Weapon));
                    this.EquippedWeapon = null;
                    return true;
                }
            }
            else if (type == typeof(Armor))
            {
                if (this.EquippedArmour == null)
                {
                    return false;
                }
                else
                {
                    var ArmourBag = Items.GetListOfItems(this.Bag, typeof(Armor));
                    this.EquippedArmour = null;
                    return true;
                }
            } else
            {
                this.EquippedWeapon = null;
                this.EquippedArmour = null;
            }
            return false;
        }

    public void AddGold(int gold)
    {
        //TODO: Add gold coins check, ensure the value is a positive int.
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