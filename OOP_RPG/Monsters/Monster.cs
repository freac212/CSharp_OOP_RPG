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
        public Difficulty Difficulty { get; private set; }
        public DayOfWeek? Day { get; private set; }
        
        // For regular Monster creation.
        public Monster(string name, int strength, int defense, int originalHP, Difficulty difficulty, DayOfWeek day)
        {
            Name = name;
            Strength = strength;
            Defense = defense;
            OriginalHP = originalHP;
            CurrentHP = originalHP;
            Difficulty = difficulty;
            Day = day;
        }
        // This allows for a monster to be generated without a day, just incase I need to for a dungeon or something.
        public Monster(string name, int strength, int defense, int originalHP, Difficulty difficulty)
        {
            Name = name;
            Strength = strength;
            Defense = defense;
            OriginalHP = originalHP;
            CurrentHP = originalHP;
            Difficulty = difficulty;
            Day = null;
        }
        // This is mainly for auto generated monsters
        public Monster(string name, MonsterPoints monsterPoints, DayOfWeek date)
        {
            Name = name;
            Strength = monsterPoints.Strength;
            Defense = monsterPoints.Defense;
            OriginalHP = monsterPoints.HP;
            CurrentHP = monsterPoints.HP;
            Difficulty = monsterPoints.Difficulty;
            Day = date;
        }
    }
}