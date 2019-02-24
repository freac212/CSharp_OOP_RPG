using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_RPG
{
    static class UI
    {
        public static void DisplayItemList(List<IItems> list)
        {
            if (list.Any())
            {
                var largestValueLength = list.Max(item => item.Value.ToString().Count());
                var largestNameLength = list.Max(item => item.Name.ToString().Count());

                for (int itemIndex = 0; itemIndex < list.Count; itemIndex++)
                {
                    var item = list[itemIndex];
                    Console.Write($"{itemIndex + 1} > ${item.Value} ");
                    Console.SetCursorPosition(6 + largestValueLength, Console.CursorTop);
                    Console.Write($":: {item.Name}");
                    Console.SetCursorPosition(10 + largestValueLength + largestNameLength, Console.CursorTop);

                    if (item.Type == ItemTypes.Armour)
                    {
                        Console.WriteLine($"- DF: {item.Defense}");
                    }
                    else if (item.Type == ItemTypes.Weapon)
                    {
                        Console.WriteLine($"- STR: {item.Strength}");
                    }
                }
            }
        }
    }
}
