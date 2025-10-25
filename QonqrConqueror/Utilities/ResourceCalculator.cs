using System;

namespace Qonqr
{
    /// <summary>
    /// Utility class for calculating bot and energy regeneration without polling
    /// </summary>
    public static class ResourceCalculator
    {
        /// <summary>
        /// Calculates current bot count based on last launch time and regeneration rate
        /// </summary>
        public static int CalculateCurrentBots(
            int botsAfterLaunch, 
            double botsPerSecond, 
            DateTime lastLaunchTime,
            int maxCapacity)
        {
            if (lastLaunchTime == DateTime.MinValue)
            {
                return botsAfterLaunch;
            }

            var elapsed = (DateTime.Now - lastLaunchTime).TotalSeconds;
            var regenerated = (int)(botsPerSecond * elapsed);
            return Math.Min(botsAfterLaunch + regenerated, maxCapacity);
        }

        /// <summary>
        /// Calculates current energy count based on last launch time and regeneration rate
        /// </summary>
        public static int CalculateCurrentEnergy(
            int energyAfterLaunch, 
            double energyPerSecond, 
            DateTime lastLaunchTime,
            int maxCapacity)
        {
            if (lastLaunchTime == DateTime.MinValue)
            {
                return energyAfterLaunch;
            }

            var elapsed = (DateTime.Now - lastLaunchTime).TotalSeconds;
            var regenerated = (int)(energyPerSecond * elapsed);
            return Math.Min(energyAfterLaunch + regenerated, maxCapacity);
        }

        /// <summary>
        /// Checks if resources are at full capacity
        /// </summary>
        public static bool AreResourcesFull(
            int currentBots, 
            int botCapacity, 
            int currentEnergy, 
            int energyCapacity)
        {
            return currentBots >= botCapacity && currentEnergy >= energyCapacity;
        }
    }
}
