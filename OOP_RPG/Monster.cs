using System;

namespace OOP_RPG
{
    public class Monster
    {
        public string Name { get; private set; }
        public int Strength { get; private set; }
        public int Defense { get; private set; }
        public int OriginalHP { get; private set; }
        public int CurrentHP { get; set; }
        public string Difficulty { get; private set; }
        public DayOfWeek? Day { get; private set; }

        public Monster(string name, int strength, int defense, int originalHP, string difficulty, DayOfWeek day)
        {
            Name = name;
            Strength = strength;
            Defense = defense;
            OriginalHP = originalHP;
            CurrentHP = originalHP;
            Difficulty = difficulty;
            Day = day;
        }
        // This allows for a monster to be generated with a day, just incase I need to for a dungeon or something.
        public Monster(string name, int strength, int defense, int originalHP, string difficulty)
        {
            Name = name;
            Strength = strength;
            Defense = defense;
            OriginalHP = originalHP;
            CurrentHP = originalHP;
            Difficulty = difficulty;
            Day = null;
        }
        public Monster(string name, MonsterPoints monsterPoints, string difficulty)
        {
            Name = name;
            Strength = monsterPoints.Strength;
            Defense = monsterPoints.Defense;
            OriginalHP = monsterPoints.HP;
            CurrentHP = monsterPoints.HP;
            Difficulty = difficulty;
        }
    }
}