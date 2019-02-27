using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_RPG.UI
{
    public class Draw
    {
        // TODO: ADD AN OUTPUT LOG LIST, DISPLAYS LAST 4 MESSAGES!!!! Will contain a clear method aswell..?
        // So Output class
        //      > AddToOutput
        //      > ClearOutput
        // Two important properties that have to be at the top of all methods, this is because DisplayInventory sets it
        // While drawing/ updating output uses it.

        // Working code chart:
        // \u2588 █
        // \u2593 ▓
        // \u2580 ▀
        // \u2550 ═
        // \u2554 ╔
        // \u2557 ╗
        // \u2551 ║
        // \u255A ╚
        // \u255D ╝
        // \u2560 ╠
        // \u2563 ╣
        // \u2500 ─
        // \u255A ╚
        // \u255D ╝
        //|                                     | <- Console width..

        public int BlockMax { get; set; }
        private int BlockMin { get; set; }
        private int CursorPos { get; set; }
        private const int BorderWidth = 2;

        private static int consoleOutputTop;
        private const int outputHeight = 4;

        public static void PrintToOutput(List<string> outputs)
        {
            // get current cursor,
            var lastCursorTop = Console.CursorTop;
            var lastCursorLeft = Console.CursorLeft;

            // set new cursor TOP: 26 LEFT:0
            Console.SetCursorPosition(0, consoleOutputTop);

            // print incoming text
            for (int i = 0; i < outputs.Count; i++)
            {
                Console.WriteLine(outputs[i]);
            }

            // place cursor back.
            Console.SetCursorPosition(lastCursorLeft, lastCursorTop);
        }
        public static void PrintToOutput(string output)
        {
            // get current cursor,
            var lastCursorTop = Console.CursorTop;
            var lastCursorLeft = Console.CursorLeft;

            // set new cursor TOP: 26 LEFT:0
            Console.SetCursorPosition(0, consoleOutputTop);
            // print incoming text
            Console.WriteLine(output);

            // place cursor back.
            Console.SetCursorPosition(lastCursorLeft, lastCursorTop);
        }

        public static void PrintToOptions(List<string> options)
        {
            for (int i = 0; i < options.Count; i++)
            {
                Console.SetCursorPosition(Console.BufferWidth / 3 + 2, i);
                Console.Write(options[i]);
            }
            Console.SetCursorPosition(Console.BufferWidth / 3 + 2, Console.CursorTop + 1);

        }
        public static void PrintToOptions(string option)
        {
            Console.SetCursorPosition(Console.BufferWidth / 3 + 2, 0);
            Console.Write(option);
            Console.SetCursorPosition(Console.BufferWidth / 3 + 2, Console.CursorTop + 1);

        }

        public static void ClearLine(int lineY = 0)
        {
            Console.SetCursorPosition(lineY, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(lineY, Console.CursorTop - 1);
        }
        public static void ClearLine(int width, int lineY = 0)
        {
            Console.SetCursorPosition(lineY, Console.CursorTop - 1);
            Console.Write(new string(' ', width));
            Console.SetCursorPosition(lineY, Console.CursorTop - 1);
        }

        public Draw(int blockMin, int blockMax)
        {
            // Not to Scale*
            // Min                              Max                                             Console.Width
            // |                                |                                               :
            // 0                                20                                              60
            BlockMax = blockMax;
            BlockMin = blockMin;
            CursorPos = 0;
        }

        public void BoxTop()
        {
            Console.SetCursorPosition(BlockMin, CursorPos++);
            Console.Write("\u2554");
            Console.Write(new string('\u2550', (BlockMax - BlockMin) - BorderWidth));
            Console.Write("\u2557");
            Console.SetCursorPosition(BlockMin, CursorPos++);
        }

        public void BoxTitle(string title)
        {
            Console.Write("\u2551");
            Console.Write(new string(' ', ((BlockMax - BlockMin) / 2 - title.Length / 2) - BorderWidth));
            Console.Write(title);
            Console.SetCursorPosition(BlockMax - 1, Console.CursorTop);
            Console.Write("\u2551");
            Console.SetCursorPosition(BlockMin, CursorPos++);
        }

        public void BoxMiddleBreak()
        {
            Console.Write("\u2560");
            Console.Write(new string('\u2550', (BlockMax - BlockMin) - BorderWidth));
            Console.Write("\u2563");
            Console.SetCursorPosition(BlockMin, CursorPos++);
        }

        public void BoxCharacterAttribute(string attributeTitle, int attribute)
        {
            Console.Write("\u2551" + attributeTitle);
            Console.SetCursorPosition((BlockMin + ((BlockMax - BlockMin) / 2)) - 1, Console.CursorTop);
            Console.Write(":");
            Console.SetCursorPosition((BlockMin + ((BlockMax - BlockMin) / 2) + (BlockMax - BlockMin) / 4) - 1, Console.CursorTop);
            Console.Write(attribute);
            Console.SetCursorPosition(BlockMax - 1, Console.CursorTop);
            Console.Write("\u2551");
            Console.SetCursorPosition(BlockMin, CursorPos++);
        }

        public void BoxCharacterAttributeStrength(Hero hero)
        {
            Console.Write("\u2551" + "Strength");
            Console.SetCursorPosition((BlockMin + ((BlockMax - BlockMin) / 2)) - 1, Console.CursorTop);
            Console.Write(":");
            Console.SetCursorPosition((BlockMin + ((BlockMax - BlockMin) / 2) + (BlockMax - BlockMin) / 4) - 1, Console.CursorTop);
            Console.Write(hero.Strength);

            // Check for combined strength from the charaters strength stat + weapons strength
            if (hero.EquippedWeapon != null)
            {
                Console.Write($"(+{hero.EquippedWeapon.GetAttribute()})");
            }
            Console.SetCursorPosition(BlockMax - 1, Console.CursorTop);
            Console.Write("\u2551");
            Console.SetCursorPosition(BlockMin, CursorPos++);
        }

        public void BoxCharacterAttributeDefense(Hero hero)
        {
            Console.Write("\u2551" + "Defense");
            Console.SetCursorPosition((BlockMin + ((BlockMax - BlockMin) / 2)) - 1, Console.CursorTop);
            Console.Write(":");
            Console.SetCursorPosition((BlockMin + ((BlockMax - BlockMin) / 2) + (BlockMax - BlockMin) / 4) - 1, Console.CursorTop);
            Console.Write(hero.Defense);

            // Check for combined defense from the charaters defense stat + armours defense
            if (hero.EquippedArmour != null && hero.EquippedShield != null)
            {
                Console.Write($"(+{hero.EquippedArmour.GetAttribute() + hero.EquippedShield.GetAttribute()})");
            } else if (hero.EquippedShield != null)
            {
                Console.Write($"(+{hero.EquippedShield.GetAttribute()})");
            }
            else if (hero.EquippedArmour != null)
            {
                Console.Write($"(+{hero.EquippedArmour.GetAttribute()})");
            }
            Console.SetCursorPosition(BlockMax - 1, Console.CursorTop);
            Console.Write("\u2551");
            Console.SetCursorPosition(BlockMin, CursorPos++);
        }

        public void BoxCharacterAttributeHP(Hero hero)
        {
            Console.Write("\u2551" + "Hitpoints");
            Console.SetCursorPosition((BlockMin + ((BlockMax - BlockMin) / 2)) - 1, Console.CursorTop);
            Console.Write(":");
            Console.SetCursorPosition((BlockMin + ((BlockMax - BlockMin) / 2) + (BlockMax - BlockMin) / 4) - 1, Console.CursorTop);
            Console.Write(hero.CurrentHP + "/" + hero.OriginalHP);
            Console.SetCursorPosition(BlockMax - 1, Console.CursorTop);
            Console.Write("\u2551");
            Console.SetCursorPosition(BlockMin, CursorPos++);
        }

        public void BoxInventoryHeader(string title)
        {
            Console.Write("\u2551");
            Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
            // For new string cases, it should be Max - min
            Console.Write(new string('\u2500', ((BlockMax - BlockMin) - 4)));
            Console.SetCursorPosition((BlockMin + ((BlockMax - BlockMin) / 2) - title.Length / 2 - 1), Console.CursorTop);
            Console.Write(title);
            Console.SetCursorPosition(BlockMax - 1, Console.CursorTop);
            Console.Write("\u2551");
            Console.SetCursorPosition(BlockMin, CursorPos++);
        }

        public void BoxInventorySubHeader(string title)
        {
            Console.Write("\u2551");
            Console.SetCursorPosition(BlockMin + 4, Console.CursorTop);
            Console.Write(title);
            Console.SetCursorPosition(BlockMax - 1, Console.CursorTop);
            Console.Write("\u2551");
            Console.SetCursorPosition(BlockMin, CursorPos++);
        }

        public void BoxInventoryWeapon(IGameItem eqpdWeapon, int? index = null, bool DisplayResaleValue = true)
        {
            Console.Write("\u2551");
            Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
            if(index is null)
            {
                Console.Write($"> {eqpdWeapon.Name}"); // hero.Name
            }
            else
            {
                Console.Write($"{index} > {eqpdWeapon.Name}"); // hero.Name
            }
            Console.SetCursorPosition(BlockMax - 1, Console.CursorTop);
            Console.Write("\u2551");
            Console.SetCursorPosition(BlockMin, CursorPos++);
            Console.Write("\u2551");
            Console.SetCursorPosition(Console.CursorLeft + 3, Console.CursorTop);
            Console.Write("<");
            Console.Write($"STR: +{eqpdWeapon.GetAttribute()}"); // Strength
            Console.Write(" | ");
            if (DisplayResaleValue)
            {
                Console.Write($"Value: {eqpdWeapon.ResaleValue}"); // 
            }
            else
            {
                // For the store
                Console.Write($"Value: {eqpdWeapon.Value}"); // Value
            }
            Console.Write(">");
            Console.SetCursorPosition(BlockMax - 1, Console.CursorTop);
            Console.Write("\u2551");
            Console.SetCursorPosition(BlockMin, CursorPos++);
        }

        public void BoxInventoryArmour(IGameItem eqpdArmour, int? index = null, bool DisplayResaleValue = true)
        {
            Console.Write("\u2551");
            Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
            if (index is null)
            {
                Console.Write($"> {eqpdArmour.Name}"); // hero.Name
            }
            else
            {
                // For the store
                Console.Write($"{index} > {eqpdArmour.Name}"); // hero.Name
            }
            Console.SetCursorPosition(BlockMax - 1, Console.CursorTop);
            Console.Write("\u2551");
            Console.SetCursorPosition(BlockMin, CursorPos++);
            Console.Write("\u2551");
            Console.SetCursorPosition(Console.CursorLeft + 3, Console.CursorTop);
            Console.Write("<");
            Console.Write($"DEF: +{eqpdArmour.GetAttribute()}"); // Defense
            Console.Write(" | ");
            if (DisplayResaleValue)
            {
                Console.Write($"Value: {eqpdArmour.ResaleValue}"); // Value
            }
            else
            {
                // For the store
                Console.Write($"Value: {eqpdArmour.Value}"); // Value
            }
            Console.Write(">");
            Console.SetCursorPosition(BlockMax - 1, Console.CursorTop);
            Console.Write("\u2551");
            Console.SetCursorPosition(BlockMin, CursorPos++);
        }

        internal void BoxInventoryPotion(IGameItem potion, int? index = null, bool displayResaleValue = true)
        {
            Console.Write("\u2551");
            Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
            if (index is null)
            {
                Console.Write($"> {potion.Name}"); // hero.Name
            }
            else
            {
                Console.Write($"{index} > {potion.Name}"); // hero.Name
            }
            Console.SetCursorPosition(BlockMax - 1, Console.CursorTop);
            Console.Write("\u2551");
            Console.SetCursorPosition(BlockMin, CursorPos++);
            Console.Write("\u2551");
            Console.SetCursorPosition(Console.CursorLeft + 3, Console.CursorTop);
            Console.Write("<");
            Console.Write($"Heals: +{potion.GetAttribute()}"); // Healing Value.
            Console.Write(" | ");
            if (displayResaleValue)
            {
                Console.Write($"Value: {potion.ResaleValue}"); // Value
            }
            else
            {
                // For the store
                Console.Write($"Value: {potion.Value}"); // Value
            }
            Console.Write(">");
            Console.SetCursorPosition(BlockMax - 1, Console.CursorTop);
            Console.Write("\u2551");
            Console.SetCursorPosition(BlockMin, CursorPos++);
        }

        internal void BoxInventoryShield(IGameItem shield, int? index = null, bool displayResaleValue = true)
        {
            Console.Write("\u2551");
            Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
            if (index is null)
            {
                Console.Write($"> {shield.Name}"); // Shield name
            }
            else
            {
                Console.Write($"{index} > {shield.Name}"); // Shield name with index
            }
            Console.SetCursorPosition(BlockMax - 1, Console.CursorTop);
            Console.Write("\u2551");
            Console.SetCursorPosition(BlockMin, CursorPos++);
            Console.Write("\u2551");
            Console.SetCursorPosition(Console.CursorLeft + 3, Console.CursorTop);
            Console.Write("<");
            Console.Write($"DEF: +{shield.GetAttribute()}"); // Defense Value.
            Console.Write(" | ");
            if (displayResaleValue)
            {
                Console.Write($"Value: {shield.ResaleValue}"); // Value
            }
            else
            {
                // For the store
                Console.Write($"Value: {shield.Value}"); // Value
            }
            Console.Write(">");
            Console.SetCursorPosition(BlockMax - 1, Console.CursorTop);
            Console.Write("\u2551");
            Console.SetCursorPosition(BlockMin, CursorPos++);
        }

        public void BoxInventoryMiddleBreak()
        {
            Console.Write("\u2551");
            Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
            Console.Write(new string('\u2500', ((BlockMax - BlockMin) - 4)));
            Console.SetCursorPosition(BlockMax - 1, Console.CursorTop);
            Console.Write("\u2551");
            Console.SetCursorPosition(BlockMin, CursorPos++);
        }

        public void BoxEnd()
        {
            Console.Write("\u255A");
            Console.Write(new string('\u2550', ((BlockMax - BlockMin) - 2)));
            Console.Write("\u255D");
            Console.SetCursorPosition(BlockMin, CursorPos++);
        }

        public void Output()
        {
            Console.SetCursorPosition(0, Console.CursorTop + 1);

            Console.Write("\u2554");
            Console.Write(new string('\u2550', (Console.BufferWidth - 2)));
            Console.Write("\u2557");
            var output = "Output";
            Console.SetCursorPosition(output.Length, Console.CursorTop - 1);
            Console.Write(output);
            Console.SetCursorPosition(0, Console.CursorTop + 1);

            // This is where we get the value to add things to the output
            consoleOutputTop = Console.CursorTop;

            Console.SetCursorPosition(0, Console.CursorTop + outputHeight - 1);
            Console.Write("\u255A");
            Console.Write(new string('\u2550', (Console.BufferWidth - 2)));
            Console.Write("\u255D");
            Console.SetCursorPosition(BlockMin, CursorPos++);
        }

        public void BoxListOption(string option, int index)
        {
            Console.Write("\u2551");
            Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
            Console.Write($"{option}");
            Console.SetCursorPosition(BlockMax - 1, Console.CursorTop);
            Console.Write("\u2551");
            Console.SetCursorPosition(BlockMin, CursorPos++);
        }

        public static void ItemList(List<IGameItem> list)
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

                    if (item is Weapon)
                    {
                        Console.WriteLine($"- DF: {item.GetAttribute()}");
                    }
                    else if (item is Armor)
                    {
                        Console.WriteLine($"- STR: {item.GetAttribute()}");
                    }
                }
            }
        }
    }
}
