namespace OOP_RPG
{
    public class Armor : IItems
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public bool Equipped { get; set; }
        public ItemTypes Type { get; set; }

        public int Strength { get; set; }
        public int Defense { get; set; }

        public Armor(string name, int defense, int value)
        {
            Name = name;
            Value = value;
            Equipped = false;
            Type = ItemTypes.Armour;

            Defense = defense;
            Strength = 0;
        }
    }
}