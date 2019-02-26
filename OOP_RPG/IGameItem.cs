using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_RPG
{
    public interface IGameItem
    {
        string Name { get; }
        int Value { get; }
        int ResaleValue { get; }

        int GetAttribute();
        void GetDescription();
    }
}
