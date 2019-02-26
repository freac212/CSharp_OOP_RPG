namespace OOP_RPG
{
    internal class Potion : IGameItem
    {
        public string Name { get; }
        public int Value { get; }
        public int ResaleValue { get; }
        private int Health { get; }

        public Potion(string name, int health, int value)
        {
            Name = name;
            Value = value;
            ResaleValue = Items.CalculatedItemValue(value);
            Health = health;
        }

        public int GetAttribute()
        {
            return Health;
        }

        public void GetDescription()
        {
            System.Console.WriteLine(Name + " +" + Health + "HP");
        }
    }
}