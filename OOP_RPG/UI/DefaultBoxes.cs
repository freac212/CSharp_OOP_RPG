using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_RPG.UI
{
    public class DefaultBoxes
    {
        public static void DrawInventory(Hero hero, Grid grid, bool drawItemsWithIndexs = false)
        {
            int BlockMax = grid.GridMax;
            int BlockMin = grid.GridMin;

            var inventory = new Draw(BlockMin, BlockMax);
            //TITLE
            inventory.BoxTop();
            inventory.BoxTitle(hero.Name);
            inventory.BoxMiddleBreak();
            // STATS
            inventory.BoxCharacterAttributeStrength(hero);
            inventory.BoxCharacterAttributeDefense(hero);
            inventory.BoxCharacterAttributeHP(hero);
            inventory.BoxCharacterAttribute("Gold", hero.Gold);
            inventory.BoxMiddleBreak();

            // EQUIPPED
            inventory.BoxInventoryHeader("Equipped");

            //  Equipped Weapon
            if (hero.EquippedWeapon != null)
            {
                inventory.BoxInventorySubHeader("Weapon: ");
                inventory.BoxInventoryWeapon(hero.EquippedWeapon);
                // Break is in here for asthetic reasons. (i.e. it hides when there's no weapons)
                inventory.BoxInventoryMiddleBreak();
            }

            // Equipped Armour
            if (hero.EquippedArmour != null)
            {
                inventory.BoxInventorySubHeader("Armour: ");
                inventory.BoxInventoryArmour(hero.EquippedArmour);
            }

            // Equipped Shield
            if (hero.EquippedShield != null)
            {
                inventory.BoxInventorySubHeader("Shield: ");
                inventory.BoxInventoryShield(hero.EquippedShield);
            }

            inventory.BoxMiddleBreak();

            // INVENTORY
            inventory.BoxInventoryHeader("INVENTORY/BAG");

            // WEAPONS
            if (drawItemsWithIndexs)
            {
                for (int i = 0; i < hero.Bag.Count; i++)
                {
                    if (hero.Bag[i] is Weapon)
                    {
                        inventory.BoxInventoryWeapon(hero.Bag[i], i + 1);
                    }
                    else if (hero.Bag[i] is Armor)
                    {
                        inventory.BoxInventoryArmour(hero.Bag[i], i + 1);
                    }
                    else if (hero.Bag[i] is Potion)
                    {
                        inventory.BoxInventoryPotion(hero.Bag[i], i + 1);
                    }
                    else if (hero.Bag[i] is Shield)
                    {
                        inventory.BoxInventoryShield(hero.Bag[i], i + 1);
                    }
                }
            }
            else
            {
                var heroesWeapons = Items.GetListOfItems(hero.Bag, typeof(Weapon));
                if (heroesWeapons.Any())
                {
                    inventory.BoxInventorySubHeader("Weapons: ");

                    foreach (var weapon in heroesWeapons)
                    {
                        inventory.BoxInventoryWeapon(weapon);
                    }
                    inventory.BoxInventoryMiddleBreak();
                }
                // ARMOUR 
                var heroesArmour = Items.GetListOfItems(hero.Bag, typeof(Armor));
                if (heroesArmour.Any())
                {
                    inventory.BoxInventorySubHeader("Armour: ");

                    foreach (var armour in heroesArmour)
                    {
                        inventory.BoxInventoryArmour(armour);
                    }
                    inventory.BoxInventoryMiddleBreak();
                }
                // POTIONS
                var heroesPotions = Items.GetListOfItems(hero.Bag, typeof(Potion));
                if (heroesPotions.Any())
                {
                    inventory.BoxInventorySubHeader("Potions: ");

                    foreach (var potion in heroesPotions)
                    {
                        inventory.BoxInventoryPotion(potion);
                    }
                    inventory.BoxInventoryMiddleBreak();
                }
                // ARMOUR 
                var heroesShield = Items.GetListOfItems(hero.Bag, typeof(Shield));
                if (heroesShield.Any())
                {
                    inventory.BoxInventorySubHeader("Shields: ");

                    foreach (var shield in heroesShield)
                    {
                        inventory.BoxInventoryShield(shield);
                    }
                }
            }
            inventory.BoxEnd();
            // OUTPUT
            inventory.Output();
        }

        internal static void DrawOptions(List<string> options, Grid grid)
        {
            int BlockMax = grid.GridMax;
            int BlockMin = grid.GridMin;

            var optionsBox = new Draw(BlockMin, BlockMax);
            //TITLE
            optionsBox.BoxTop();
            optionsBox.BoxTitle("Options");
            optionsBox.BoxMiddleBreak();
            // STATS
            for (int i = 0; i < options.Count; i++)
            {
                optionsBox.BoxListOption(options[i], i + 1);
            }
            optionsBox.BoxMiddleBreak();
            optionsBox.BoxEnd();
        }

        internal static void DrawStore(List<IGameItem> items, Grid grid)
        {
            int BlockMax = grid.GridMax;
            int BlockMin = grid.GridMin;

            var storeBox = new Draw(BlockMin, BlockMax);
            //TITLE
            storeBox.BoxTop();
            storeBox.BoxTitle("Store");
            storeBox.BoxMiddleBreak();
            // STATS
            storeBox.BoxInventorySubHeader("Items: ");
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] is Weapon)
                {
                    storeBox.BoxInventoryWeapon(items[i], i + 1, false);
                }
                else if (items[i] is Armor)
                {
                    storeBox.BoxInventoryArmour(items[i], i + 1, false);
                }
                else if (items[i] is Potion)
                {
                    storeBox.BoxInventoryPotion(items[i], i + 1, false);
                }
                else if (items[i] is Shield)
                {
                    storeBox.BoxInventoryShield(items[i], i + 1, false);
                }
            }
            storeBox.BoxMiddleBreak();
            storeBox.BoxEnd();
        }
    }
}
