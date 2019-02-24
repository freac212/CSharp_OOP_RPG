namespace OOP_RPG
{
    public class Weapon : IItems
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public bool Equipped { get; set; }
        public ItemTypes Type { get; set; }

        public int Strength { get; set; }
        public int Defense { get; set; }

        public Weapon(string name, int strength, int value)
        {
            Name = name;
            Value = value;
            Equipped = false;
            Type = ItemTypes.Weapon;

            Strength = strength;
            Defense = 0;
        }
    }
}