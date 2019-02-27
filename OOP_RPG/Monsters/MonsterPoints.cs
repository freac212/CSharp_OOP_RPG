namespace OOP_RPG
{
    public class MonsterPoints
    {
        public int Strength { get; private set; }
        public int Defense { get; private set; }
        public int HP { get; private set; }
        public Difficulty Difficulty { get; private set; }

        public MonsterPoints(int strength, int defense, int hp, Difficulty difficulty)
        {
            Strength = strength;
            Defense = defense;
            HP = hp;
            Difficulty = difficulty;
        }
    }
}
