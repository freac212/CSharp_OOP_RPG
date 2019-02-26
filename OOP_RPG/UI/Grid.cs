using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_RPG.UI
{
    public class Grid
    {
        public int GridMin { get; set; }
        public int GridMax { get; set; }

        public Grid(int min, int max)
        {
            GridMin = min;
            GridMax = max;
        }

        public static Grid Left = new Grid(0, Console.BufferWidth / 3);
        public static Grid Center = new Grid(Console.BufferWidth - ((Console.BufferWidth / 3) * 2), Console.BufferWidth - (Console.BufferWidth / 3));
        public static Grid Right = new Grid(Console.BufferWidth - (Console.BufferWidth / 3), Console.BufferWidth);
    }
}
