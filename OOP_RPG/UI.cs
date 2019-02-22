using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_RPG
{
    static class UI
    {
        public static void DisplayWeaponList(List<Weapon> list)
        {
            if (list.Any())
            {
                var largestValueLengthArmour = list.Max(armour => armour.Value.ToString().Count());
                var largestNameLengthArmour = list.Max(armour => armour.Name.ToString().Count());

                for (int armourIndex = 0; armourIndex < list.Count; armourIndex++)
                {
                    var armour = list[armourIndex];
                    Console.Write($"{armourIndex + 1} > ${armour.Value} ");
                    Console.SetCursorPosition(6 + largestValueLengthArmour, Console.CursorTop);
                    Console.Write($":: {armour.Name}");
                    Console.SetCursorPosition(10 + largestValueLengthArmour + largestNameLengthArmour, Console.CursorTop);
                    Console.WriteLine($"- STR: {armour.Strength}");
                }
            }
        }
        public static void DisplayArmourList(List<Armor> list)
        {
            if (list.Any())
            {
                var largestValueLengthArmour = list.Max(armour => armour.Value.ToString().Count());
                var largestNameLengthArmour = list.Max(armour => armour.Name.ToString().Count());

                for (int armourIndex = 0; armourIndex < list.Count; armourIndex++)
                {
                    var armour = list[armourIndex];
                    Console.Write($"{armourIndex + 1} > ${armour.Value} ");
                    Console.SetCursorPosition(6 + largestValueLengthArmour, Console.CursorTop);
                    Console.Write($":: {armour.Name}");
                    Console.SetCursorPosition(10 + largestValueLengthArmour + largestNameLengthArmour, Console.CursorTop);
                    Console.WriteLine($"- DF: {armour.Defense}");
                }
            }
        }
    }
}
