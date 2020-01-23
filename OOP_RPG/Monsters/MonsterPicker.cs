using System;
using System.Collections.Generic;
using System.Linq;

namespace OOP_RPG
{
    public static class MonsterPicker
    {
        // It was suggested I place readonly before the IReadOnlyList... So why not eh?
        private static readonly IReadOnlyList<Monster> MonsterList = new List<Monster>
        {
            // ========= HARD CODED MONSTERS ==========
            // TOTAL MONSTERS: 35
            // 2/5 = Easy   -> 14 Easy Monsters
            // 2/5 = Medium -> 14 Medium Monsters
            // 1/5 = Hard   -> 7 Hard Monsters

            // EASY 20 points            Stngth DEF HP
            new Monster("Slime",            10, 3, 5, Difficulty.Easy, DayOfWeek.Sunday),
            new Monster("Flame Slime",      15, 3, 5, Difficulty.Easy, DayOfWeek.Monday),
            new Monster("Armoured Slime",   10, 8, 4, Difficulty.Easy, DayOfWeek.Thursday),

            new Monster("Bat",              7, 3, 10, Difficulty.Easy, DayOfWeek.Friday),
            new Monster("Lizard",           15, 7, 8, Difficulty.Easy, DayOfWeek.Saturday),
            new Monster("Snake",            15, 3, 10, Difficulty.Easy, DayOfWeek.Tuesday),
            new Monster("Spider",           13, 3, 12, Difficulty.Easy, DayOfWeek.Wednesday),

            new Monster("Ghoul",            10, 4, 15, Difficulty.Easy, DayOfWeek.Thursday),
            new Monster("Zombie",           10, 5, 15, Difficulty.Easy, DayOfWeek.Sunday),
            new Monster("Zombie Archer",    20, 3, 15, Difficulty.Easy, DayOfWeek.Saturday),
            new Monster("Meaty Monster",    10, 6, 15, Difficulty.Easy, DayOfWeek.Wednesday),
            new Monster("Skeleton",         10, 5, 10, Difficulty.Easy, DayOfWeek.Monday),

            new Monster("Thief",            15, 5, 20, Difficulty.Easy, DayOfWeek.Tuesday),
            new Monster("Goblin",           15, 5, 20, Difficulty.Easy, DayOfWeek.Friday),

            // MEDIUM 30 points
            new Monster("Stone Golem",      10, 10, 10, Difficulty.Medium, DayOfWeek.Monday),
            new Monster("Ghost",            23, 3, 5, Difficulty.Medium, DayOfWeek.Sunday),
            new Monster("Large Bat",        12, 3, 15, Difficulty.Medium, DayOfWeek.Friday),
            new Monster("Large Spider",     15, 5, 10, Difficulty.Medium, DayOfWeek.Tuesday),
            new Monster("Large Slime",      3, 2, 25, Difficulty.Medium, DayOfWeek.Thursday),

            new Monster("Skeleton Knight",  12, 5, 13, Difficulty.Medium, DayOfWeek.Wednesday),
            new Monster("Zombie Knight",    13, 4, 13, Difficulty.Medium, DayOfWeek.Saturday),
            new Monster("Goblin Fighter",   20, 1, 9, Difficulty.Medium, DayOfWeek.Friday),
            new Monster("Large Goblin",     10, 7, 13, Difficulty.Medium, DayOfWeek.Saturday),
            new Monster("Orc",              7, 5, 18, Difficulty.Medium, DayOfWeek.Thursday),

            new Monster("Mimic",            28, 1, 1, Difficulty.Medium, DayOfWeek.Sunday),
            new Monster("That Bob guy",     15, 10, 5, Difficulty.Medium, DayOfWeek.Monday),

            new Monster("Grandius",         20, 8, 2, Difficulty.Medium, DayOfWeek.Wednesday),
            new Monster("Steel Warrior",    5, 15, 10, Difficulty.Medium, DayOfWeek.Tuesday),
                
            // HARD 40 points
            new Monster("Dark Fighter",                     15, 10, 15, Difficulty.Hard, DayOfWeek.Monday),
            new Monster("Elemental Mage",                   25, 1, 14, Difficulty.Hard, DayOfWeek.Tuesday),
            new Monster("Spider Queen",                     15, 15, 10, Difficulty.Hard, DayOfWeek.Sunday),
            new Monster("Mirror Of the Damned",             38, 1, 1, Difficulty.Hard, DayOfWeek.Wednesday),
            new Monster("Xelephian the Dragon",             20, 15, 5, Difficulty.Hard, DayOfWeek.Saturday),
            new Monster("George the Reaper",                30, 5, 5, Difficulty.Hard, DayOfWeek.Friday),
            new Monster("The keeper of the Dark Realm",     10000, 10000, 10000, Difficulty.Hard)
            // ========= END OF HARD CODED MONSTERS ==========
        };

        public static Monster GetMonster(Difficulty? difficulty = null, bool isHarcodedList = false)
        {
            // Choose a list type
            // > Generate a list
            List<Monster> Monsters;
            // > Use hard-coded list
            //List<Monster> Monsters = MonsterList;

            if (isHarcodedList)
                Monsters = MonsterList.ToList();
            else 
                Monsters = MonsterGenerator.GenerateMonsters(); // Note, this generates an entire list of monsters everytime a monster is needed.

            List<Monster> SortedMonsters;
            DateTime currentDate = DateTime.Today;

            if (difficulty == Difficulty.Easy)
            {
                SortedMonsters = (from monster in Monsters
                                  where monster.Difficulty == Difficulty.Easy &&
                                  monster.Day == currentDate.DayOfWeek
                                  select monster).ToList();
            }
            else if (difficulty == Difficulty.Medium)
            {
                SortedMonsters = (from monster in Monsters
                                  where monster.Difficulty == Difficulty.Medium &&
                                  monster.Day == currentDate.DayOfWeek
                                  select monster).ToList();
            }
            else if (difficulty == Difficulty.Hard)
            {
                SortedMonsters = (from monster in Monsters
                                  where monster.Difficulty == Difficulty.Hard &&
                                  monster.Day == currentDate.DayOfWeek
                                  select monster).ToList();
            }
            else
            {
                // Else, difficulty not selected, pick based on day.
                SortedMonsters = (from monster in Monsters
                                  where monster.Day == currentDate.DayOfWeek
                                  select monster).ToList();
            }

            return SortedMonsters[new Random().Next(0, SortedMonsters.Count - 1)];
        }
    }
}
