namespace OOP_RPG
{
    public class Weapon : IGameItem
    {
        public string Name { get; }
        public int Value { get; }

        private int Strength { get; }

        public Weapon(string name, int strength, int value)
        {
            Name = name;
            Value = value;

            Strength = strength;
        }

        public int GetAttribute()
        {
            return Strength;
        }

        public void GetDescription()
        {
            System.Console.WriteLine(Name + " of " + Strength + " Strength");
        }
    }
}