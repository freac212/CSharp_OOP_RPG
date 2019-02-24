using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_RPG
{
    public class Items
    {
        public static List<IItems> GetListOfItems(List<IItems> items, ItemTypes type)
        {
            return (from item in items
                    where item.Type == type
                    select item).ToList();
        }
    }

    // This seems rather un-ogranized. Like they're all part of Items, but should they have their own file?
    public interface IItems
    {
        string Name { get; set; }
        int Value { get; set; }
        bool Equipped { get; set; }
        ItemTypes Type { get; set; }

        // I'd prefer not to include both, and have each item implement both, but in order for all the methods
        // that work with Items to work, both strength and defense have to be there. So I made them nullable types
        // To allow this to work. I'd prefer something better, but I can't think of anything to replace it.
        int Strength { get; set; }
        int Defense { get; set; }

    }

    public enum ItemTypes
    {
        Weapon,
        Armour
    }
}
