namespace OOP_RPG
{
    public class Weapon
    {
        public string Name { get; private set; }
        public int Strength { get; private set; }
        public int Value { get; private set; }
        public bool Equipped { get; set; }

        public Weapon(string name, int strength, int value)
        {
            Name = name;
            Strength = strength;
            Value = value;
            Equipped = false;
        }
    }
}