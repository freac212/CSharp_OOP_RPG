using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_RPG
{
    public class CompletedAchievement
    {
        public Achievement Achievement { get; private set; }
        public DateTime TimeCompleted { get; private set; }

        public CompletedAchievement(Achievement achievement, DateTime date)
        {
            Achievement = achievement;
            TimeCompleted = date;
        }
    }
}
