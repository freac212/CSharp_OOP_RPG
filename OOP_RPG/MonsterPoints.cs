namespace OOP_RPG
{
    public class MonsterPoints
    {
        public int Strength { get; private set; }
        public int Defense { get; private set; }
        public int HP { get; private set; }
        public string Difficulty { get; private set; }

        public MonsterPoints(int strength, int defense, int hp, string difficulty)
        {
            Strength = strength;
            Defense = defense;
            HP = hp;
            Difficulty = difficulty;
        }
    }
}
