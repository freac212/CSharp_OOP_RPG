using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_RPG
{
    public class Items
    {
        public static List<IGameItem> GetListOfItems(List<IGameItem> items, Type className)
        {
            return (from item in items
                    where item.GetType() == className
                    select item).ToList();
        }

        public static int CalculatedItemValue(int value)
        {
            var random = new Random();
            return random.Next((int)(Settings.ValueDepreciationMin * value), (int)(Settings.ValueDepreciationMax * value));
        }
    }
}
