using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_RPG
{
    public class Items
    {
        //TODO: Change ItemTypes to class type!
        public static List<IGameItem> GetListOfItems(List<IGameItem> items, Type className)
        {
            return (from item in items
                    where item.GetType() == className
                    select item).ToList();
        }
    }
}
