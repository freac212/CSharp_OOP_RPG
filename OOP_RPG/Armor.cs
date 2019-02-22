namespace OOP_RPG
{
    public class Armor
    {
        public string Name { get; }
        public int Defense { get; }
        public int Value { get; }
        public bool Equipped { get; set; }

        public Armor(string name, int defense, int value)
        {
            Name = name;
            Defense = defense;
            Value = value;
            Equipped = false;
        }
    }
}