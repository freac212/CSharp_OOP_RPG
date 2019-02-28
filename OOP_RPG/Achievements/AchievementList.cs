using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_RPG
{
    public class AchievementList
    {
        // The Achievements are solely based on kills.
        public List<Monster> DefeatedMonsters = new List<Monster>();
        public List<Achievement> BaseAchievements = new List<Achievement>
        {
            new Achievement("Kill 5 Monsters", 5, 1),
            new Achievement("Kill 25 Monsters", 25, 10),
            new Achievement("Kill 5 Easy Monsters", 5, 2, Difficulty.Easy),
            new Achievement("Kill 5 Medium Monsters",  5, 3, Difficulty.Medium),
            new Achievement("Kill 5 Hard Monsters", 5, 10, Difficulty.Hard),
        };
        public List<CompletedAchievement> CompletedAchievements = new List<CompletedAchievement>();

        public int CountDefeatedMonsters(Difficulty? difficulty = null)
        {
            if (DefeatedMonsters.Any())
            {
                switch (difficulty)
                {
                    case Difficulty.Easy:
                        //Return count of easy monsters.
                        var easyCount = (from EasyMonster in DefeatedMonsters
                                         where EasyMonster.Difficulty == Difficulty.Easy
                                         select EasyMonster).Count();
                        return easyCount;

                    case Difficulty.Medium:
                        //Return count of medium monsters.
                        var mediumCount = (from mediumMonster in DefeatedMonsters
                                           where mediumMonster.Difficulty == Difficulty.Medium
                                           select mediumMonster).Count();
                        return mediumCount;

                    case Difficulty.Hard:
                        //Return count of hard monsters.
                        var hardCount = (from hardMonster in DefeatedMonsters
                                         where hardMonster.Difficulty == Difficulty.Hard
                                         select hardMonster).Count();
                        return hardCount;

                    default:
                        // Else, no difficulty chosen, so return whole count.
                        return DefeatedMonsters.Count;
                }
            }
            else
            {
                return 0;
            }
        }

        public void CheckForAchievement()
        {
            // Count total kills
            var totalKills = CountDefeatedMonsters();
            // Count Easy kills
            var easyKills = CountDefeatedMonsters(Difficulty.Easy);
            // Count Medium kills
            var mediumKills = CountDefeatedMonsters(Difficulty.Medium);
            // Count Hard kills
            var hardKills = CountDefeatedMonsters(Difficulty.Hard);

            // Sort out already completed Achievements
            var sortedBaseAchievements = (from baseAchieve in BaseAchievements
                                           //from completeAchieve in CompletedAchievements
                                           where !CompletedAchievements.Any(p => p.Achievement.AchievementName == baseAchieve.AchievementName)
                                           select baseAchieve).ToList();

            // Apply Achievements accordingly
            foreach (var achievement in sortedBaseAchievements)
            {
                if (achievement.AchievementType == Difficulty.Easy)
                {
                    if(easyKills >= achievement.AchievementKillAmount)
                    {
                        var completedAchievement = new CompletedAchievement(achievement, DateTime.Now);
                        CompletedAchievements.Add(completedAchievement);
                    }
                } else if (achievement.AchievementType == Difficulty.Medium)
                {
                    if (mediumKills >= achievement.AchievementKillAmount)
                    {
                        var completedAchievement = new CompletedAchievement(achievement, DateTime.Now);
                        CompletedAchievements.Add(completedAchievement);
                    }
                } else if (achievement.AchievementType == Difficulty.Hard)
                {
                    if (hardKills >= achievement.AchievementKillAmount)
                    {
                        var completedAchievement = new CompletedAchievement(achievement, DateTime.Now);
                        CompletedAchievements.Add(completedAchievement);
                    }
                } else if (achievement.AchievementType == Difficulty.none)
                {
                    if (totalKills >= achievement.AchievementKillAmount)
                    {
                        var completedAchievement = new CompletedAchievement(achievement, DateTime.Now);
                        CompletedAchievements.Add(completedAchievement);
                    }
                }
            }
        }

        public void AddToDefeatedMonsters(Monster monster)
        {
            DefeatedMonsters.Add(monster);
            CheckForAchievement();
        }

        public int GetTotalPoints()
        {
            int pointCount = 0;
            foreach (var achieve in CompletedAchievements)
            {
                pointCount += achieve.Achievement.PointValue;
            }
            return pointCount;
        }
    }
}
