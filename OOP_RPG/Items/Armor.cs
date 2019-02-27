namespace OOP_RPG
{
    public class Armor : IGameItem
    {
        public string Name { get; }
        public int Value { get; }
        public int ResaleValue { get; }
        private int Defense { get; }

        public Armor(string name, int defense, int value)
        {
            Name = name;
            Value = value;
            ResaleValue = Items.CalculatedItemValue(value);
            Defense = defense;
        }

        public int GetAttribute ()
        {
            return Defense;
        }

        public void GetDescription()
        {
            System.Console.WriteLine(Name + " of " + Defense + " Defense");
        }
    }
}