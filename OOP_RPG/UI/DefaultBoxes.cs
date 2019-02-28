using System.Collections.Generic;
using System.Linq;

namespace OOP_RPG.UI
{
    public class DefaultBoxes
    {
        /* The Default Boxes contains methods that are bassically organized boxes
         * that contain methods from the Draw class to draw, well, boxes with purpose.
         * You can design a "Box" here by calling certain draw method in order, to draw
         * whatever you want. In my case, I have things like inventory, options, store, etc
         * all somewhat neatly placed here to call in the Game page whenever I need them.
         */
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
                // Break is in here for asthetic reasons. (i.e. it hides when there's no Armour)
                inventory.BoxInventoryMiddleBreak();
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
                var bag = (from item in hero.Bag
                           where item != hero.EquippedWeapon && item != hero.EquippedArmour && item != hero.EquippedShield
                           select item).ToList();

                var heroesWeapons = Items.GetListOfItems(bag, typeof(Weapon));
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
                var heroesArmour = Items.GetListOfItems(bag, typeof(Armor));
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
                var heroesPotions = Items.GetListOfItems(bag, typeof(Potion));
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
                var heroesShield = Items.GetListOfItems(bag, typeof(Shield));
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
            optionsBox.BoxCursorBar();
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

        internal static void DrawAchievements(AchievementList achievements, Grid grid)
        {
            int BlockMax = grid.GridMax;
            int BlockMin = grid.GridMin;

            var achieveBox = new Draw(BlockMin, BlockMax);
            // TITLE
            achieveBox.BoxTop();
            achieveBox.BoxTitle("Achievements");
            achieveBox.BoxMiddleBreak();
            // ACHIEVEMENTS
            achieveBox.BoxInventorySubHeader("Completed: ");
            foreach (var achieve in achievements.CompletedAchievements)
            {
                achieveBox.BoxItemGenericAchievement(achieve.Achievement.AchievementName);
                achieveBox.BoxItemGenericAchievementAttribute("Points", achieve.Achievement.PointValue);
                achieveBox.BoxItemGenericAchievementAttribute(achieve.TimeCompleted);
            }
            achieveBox.BoxMiddleBreak();
            achieveBox.BoxTitle($"Total Points: {achievements.GetTotalPoints()}");
            achieveBox.BoxMiddleBreak();
            if (achievements.DefeatedMonsters.Any())
            {
                achieveBox.BoxInventorySubHeader("Monsters Killed: ");
                foreach (var monster in achievements.DefeatedMonsters)
                {
                    achieveBox.BoxItemGenericMonster(monster.Name, "Difficulty", monster.Difficulty);
                }
            }
            achieveBox.BoxEnd();
        }

        internal static void DrawManageInventory(Hero hero, Grid grid, List<IGameItem> itemsToList)
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
                // Break is in here for asthetic reasons. (i.e. it hides when there's no Armour)
                inventory.BoxInventoryMiddleBreak();
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

            for (int i = 0; i < itemsToList.Count; i++)
            {
                if (hero.Bag[i] is Weapon)
                {
                    inventory.BoxInventoryWeapon(itemsToList[i], i + 1);
                }
                else if (hero.Bag[i] is Armor)
                {
                    inventory.BoxInventoryArmour(itemsToList[i], i + 1);
                }
                else if (hero.Bag[i] is Potion)
                {
                    inventory.BoxInventoryPotion(itemsToList[i], i + 1);
                }
                else if (hero.Bag[i] is Shield)
                {
                    inventory.BoxInventoryShield(itemsToList[i], i + 1);
                }
            }

            inventory.BoxEnd();
            // OUTPUT
            inventory.Output();
        }

        internal static void DrawUsables(AchievementList achievements, Grid grid)
        {
            int BlockMax = grid.GridMax;
            int BlockMin = grid.GridMin;

            var achieveBox = new Draw(BlockMin, BlockMax);
            // TITLE
            achieveBox.BoxTop();
            achieveBox.BoxTitle("Achievements");
            achieveBox.BoxMiddleBreak();
            // ACHIEVEMENTS
            achieveBox.BoxInventorySubHeader("Completed: ");
            foreach (var achieve in achievements.CompletedAchievements)
            {
                achieveBox.BoxItemGenericAchievement(achieve.Achievement.AchievementName);
                achieveBox.BoxItemGenericAchievementAttribute("Points", achieve.Achievement.PointValue);
                achieveBox.BoxItemGenericAchievementAttribute(achieve.TimeCompleted);
            }
            achieveBox.BoxMiddleBreak();
            achieveBox.BoxTitle($"Total Points: {achievements.GetTotalPoints()}");
            achieveBox.BoxMiddleBreak();
            if (achievements.DefeatedMonsters.Any())
            {
                achieveBox.BoxInventorySubHeader("Monsters Killed: ");
                foreach (var monster in achievements.DefeatedMonsters)
                {
                    achieveBox.BoxItemGenericMonster(monster.Name, "Difficulty", monster.Difficulty);
                }
            }
            achieveBox.BoxEnd();
        }
    }
}
