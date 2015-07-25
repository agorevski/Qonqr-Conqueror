namespace Qonqr
{
    public class LoginInfo
    {
        public string BotCapacity { get; set; }
        public string BotCountAfterLastDeployment { get; set; }
        public string BotsPerSecond { get; set; }
        public string Codename { get; set; }
        public string EnergyCapacity { get; set; }
        public string EnergyCountAfterLastDeployment { get; set; }
        public string EnergyPerSecond { get; set; }
        public string Experience { get; set; }
        public string FactionId { get; set; }
        public string LastBotDeployTimeUTC { get; set; }
        public string LastEnergyDeployTimeUTC { get; set; }
        public string Level { get; set; }
        public string LevelXpUpperBound { get; set; }
        public string PlayerId { get; set; }
        public string Qredits { get; set; }
        public string ServerTimeUTC { get; set; }

        public string AllowPublicMessages { get; set; }
        public string ImageUrl { get; set; }
        public string TotalZonesCaptured { get; set; }
        public string ZonesCurrentlyLeading { get; set; }

        public LoginInfo()
        { }
    }
}

