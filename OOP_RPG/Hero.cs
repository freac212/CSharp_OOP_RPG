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
        public IGameItem EquippedWeapon { get; private set; }
        public IGameItem EquippedArmour { get; private set; }
        public IGameItem EquippedShield { get; private set; }
        public int MonsterKills { get; private set; }
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
            Strength = 1200;
            Defense = 12;
            OriginalHP = 30;
            CurrentHP = 30;
            Gold = 10;
        }

        //These are the Methods of our Class.
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
                else if (itemType == typeof(Shield))
                {
                    this.EquippedShield = itemBag[weaponIndex];
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
            }
            else if (type == typeof(Shield))
            {
                if (this.EquippedShield == null)
                {
                    return false;
                }
                else
                {
                    var ShieldBag = Items.GetListOfItems(this.Bag, typeof(Shield));
                    this.EquippedShield = null;
                    return true;
                }
            }
            else
            {
                this.EquippedWeapon = null;
                this.EquippedArmour = null;
            }
            return false;
        }

        public void AddGold(int gold)
        {
            if(gold >= 0)
            {
                Gold += gold;
            } else
            {
                throw new Exception("Passing a negative value");
            }
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

        public void UseHealthPotion(IGameItem potion)
        {
            // If the potion is going to over heal the player, deny it, and just set the
            // player HP to their HP cap.
            if (CurrentHP + potion.GetAttribute() >= OriginalHP)
            {
                CurrentHP = OriginalHP;
            }
            else
            {
                // Else, heal up the value of the potion.
                CurrentHP += potion.GetAttribute();
            }
            // Remove potion from inventory.
            this.Bag.Remove(potion);
        }

        public void UseHealthPotion(int potionIndex)
        {
            // If the potion is going to over heal the player, deny it, and just set the
            // player HP to their HP cap.
            var potions = Items.GetListOfItems(this.Bag, typeof(Potion));
            var potion = potions[potionIndex];

            if (CurrentHP + potion.GetAttribute() >= OriginalHP)
            {
                CurrentHP = OriginalHP;
            }
            else
            {
                // Else, heal up the value of the potion.
                CurrentHP += potion.GetAttribute();
            }
            // Remove potion from inventory.
            this.Bag.Remove(potion);
        }

        public void RemoveItemFromHero(IGameItem item)
        {
            // Remove Item from bag
            this.Bag.Remove(item);

            // Remove Item from equipment slots if they're equipped
            if (item == this.EquippedWeapon)
            {
                this.UpEquip(typeof(Weapon));
            }
            else if (item == this.EquippedArmour)
            {
                this.UpEquip(typeof(Armor));
            }
            else if (item == this.EquippedShield)
            {
                this.UpEquip(typeof(Shield));
            }
        }

        public void IncrementMonsterKills()
        {
            MonsterKills++;
        }
    }
}