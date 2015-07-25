namespace Qonqr
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Player
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string DeviceId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public int Faction { get; set; }
        public int Level { get; set; }
        public List<Forts> Forts { get; set; }
        public int SessionHarvestTotal { get; set; }

        public PlayerAccount PlayerAccount { get; set; }
        public HUD HUD { get; set; }

        public double CurBots { get; set; }
        public double BotsPerSec { get; set; }
        public int BotsLaunched { get; set; }
        public DateTime LastBotDeploymentTimeUTC { get; set; }

        public Player()
        { }

        /// <summary>
        /// A very simple check that will tell you if this player has
        /// reached the maximum capacity of bots
        /// </summary>
        /// <returns>True if the account has the maximum capacity of bots</returns>
        public bool HasFullBots()
        {
            return CurBots == HUD.BotCapacity;
        }

        /// <summary>
        /// A very simple check that will tell you if you have the minimum
        /// number of bots required to perform an attack
        /// </summary>
        /// <param name="botsNeeded">The number of bots needed for the specific launch</param>
        /// <returns>True if there are enough bots</returns>
        public bool HasEnoughBotsForLaunch(int botsNeeded)
        {
            return CurBots > botsNeeded;
        }

        /// <summary>
        /// A function designed to be called once every second to increment 
        /// the number of bots that this player currently has
        /// </summary>
        public void IncrementBots()
        {
            if (CurBots < HUD.BotCapacity)
            {
                CurBots += HUD.BotsPerSecond;
            }

            if (CurBots > HUD.BotCapacity)
            {
                CurBots = HUD.BotCapacity;
            }
        }

    }
}
