namespace OOP_RPG
{
    internal class Shield : IGameItem
    {
        public string Name { get; }
        public int Value { get; }
        public int ResaleValue { get; }
        private int Defense { get; }

        public Shield(string name, int defense, int value)
        {
            Name = name;
            Value = value;
            ResaleValue = Items.CalculatedItemValue(value);
            Defense = defense;
        }

        public int GetAttribute()
        {
            return Defense;
        }

        public void GetDescription()
        {
            System.Console.WriteLine(Name + " of " + Defense + " Defense");
        }
    }
}