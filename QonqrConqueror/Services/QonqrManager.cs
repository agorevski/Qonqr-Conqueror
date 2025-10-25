using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qonqr
{
    public class QonqrManager
    {

        #region Variables

        private ApiCall _apiCall;
        private LoginApiCall _loginApiCall;
        private Stats _statistics;
        private List<Zoneski> _zonesList;
        private Harvester _harvest;
        private List<Zoneski> _fortsList;
        private Launcher _launch; 
        private List<Player> _accounts;


        #endregion

        #region Constructor

        public QonqrManager()
        {
            _apiCall = new ApiCall();
            _statistics = new Stats();
            _zonesList = new List<Zoneski>();
            _harvest = new Harvester();
            _fortsList = new List<Zoneski>();
            _launch = new Launcher();

            ConfigFile configFile = new ConfigFile();
            _accounts = configFile.GetAccounts();

            ResetCoordinates();
        }

        #endregion

        #region Properties

        public string Lattitude { get; set; }

        public string Longitude { get; set; }

        public Stats Statistics 
        { 
            get { return _statistics; } 
            private set { _statistics = value; }
        }

        public List<Zoneski> Zones
        {
            get { return _zonesList; }
            private set { _zonesList = value; }
        }

        public Harvester Harvest
        {
            get { return _harvest; }
            private set { _harvest = value; }
        }

        public List<Zoneski> Forts
        {
            get { return _fortsList; }
            private set { _fortsList = value; }
        }

        public Launcher Launch
        {
            get { return _launch; }
            private set { _launch = value; }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// A helper method that logs into all of your accounts and sets some
        /// additional metadata on them based on the login information
        /// </summary>
        /// <returns>true if all logins were successful</returns>
        public bool LoginAllAccounts()
        {
            bool successful = true;
            foreach (Player account in _accounts)
            {
                // Perform the actual Login Call
                _loginApiCall = _apiCall.Login(account.Username, account.Password, account.DeviceId);

                // Set some more properties on the account for future usage
                account.Level = _loginApiCall.PlayerProfile.Level;
                account.Faction = _loginApiCall.PlayerProfile.FactionId;

                if (_loginApiCall != null)
                {
                    successful = false;
                    break;
                }
            }

            return successful;
        }

        /// <summary>
        /// A helper method that logs into all of your accounts and sets some
        /// additional metadata on them based on the login information
        /// </summary>
        /// <returns>true if all logins were successful</returns>
        public bool GetAllForts()
        {
            bool successful = true;
            foreach (Player account in _accounts)
            {
                FortsApiCall forts = _apiCall.Forts(account.Latitude, account.Longitude);
                account.Forts = forts.PlayerForts.Forts;

                if (forts != null)
                {
                    successful = false;
                    break;
                }
            }
            return successful;
        }

        /// <summary>
        /// A helper method that Harvests all of your accounts and sets some additional
        /// metadata on them based on the harvest information
        /// </summary>
        /// <returns></returns>
        public bool PerformHarvestAll()
        {
            bool successful = true;
            foreach (Player account in _accounts)
            {
                HarvestAll harvestAll = _apiCall.HarvestAll(account.Latitude, account.Longitude);
                account.SessionHarvestTotal += harvestAll.QreditsEarned;

                if (harvestAll != null)
                {
                    successful = false;
                    break;
                }
            }
            return successful;
        }

        // Deprecated method that Nick was using, that hard-codes the DeviceID. Bad!
        //public bool Login(string username, string password)
        //{
        //    string deviceId = "masdkAhsmGPs" + username + "=";

        //    _loginApiCall = _apiCall.Login(username, password, deviceId);

        //    bool successful = _loginApiCall != null;

        //    if (successful)
        //        LoadStats();

        //    return successful;
        //}

        // Deprecated function Nick was using
        //public bool GetForts()
        //{
        //    FortsApiCall fortsApiCall = _apiCall.Forts(Lattitude, Longitude);

        //    LoadForts(fortsApiCall);

        //    return fortsApiCall != null; // this is null if the call failed
        //}

        //Deprecated function Nick was using
        //public bool PerformHarvest()
        //{

        //    HarvestAll harvestAll = _apiCall.HarvestAll(Lattitude, Longitude);

        //    LoadHarvest(harvestAll);

        //    return harvestAll != null; // this is null if the call failed
        //}


        private void LoadForts(FortsApiCall fortsApiCall)
        {
            if (fortsApiCall != null && fortsApiCall.PlayerForts != null && fortsApiCall.PlayerForts.Forts != null)
            {
                _fortsList = new List<Zoneski>();

                foreach (Forts fort in fortsApiCall.PlayerForts.Forts)
                {
                    Zoneski zoneski = new Zoneski();

                    zoneski.ControlState = ZoneControlStateConverter(fort.ZoneControlState);
                    zoneski.ZoneName = fort.ZoneName;
                    zoneski.ZoneId = fort.ZoneId.ToString();
                    zoneski.Latitude = fort.Latitude.ToString();
                    zoneski.Longitude = fort.Longitude.ToString();
                    zoneski.CurrentGasInTank = fort.CurrentGasInTank;

                    _fortsList.Add(zoneski);
                }
            }
        }

        private void LoadHarvest(HarvestAll harvestAll)
        {
            _harvest.CreditsEarned = harvestAll.QreditsEarned;

            _harvest.TotalCreditsEarned += harvestAll.QreditsEarned;
        }

        public void ResetCoordinates()
        {
            Lattitude = "47.6469383239746";
            Longitude = "-122.133738517761";
        }

        public void LoadStats()
        {
            bool successful = _loginApiCall != null;

            if (!successful)
            {
                _statistics.BotCapacity = "";
                _statistics.EnergyCapacity = "";
                _statistics.CurrentExperience = "";
                _statistics.Level = "";
                _statistics.ExperienceToLevel = "";
                _statistics.Zones = "";
                _statistics.Credits = "";
                _statistics.CodeName = "";
            }
            else
            {
                _statistics.BotCapacity = _loginApiCall.HUD.BotCapacity.ToString();
                _statistics.EnergyCapacity = _loginApiCall.HUD.EnergyCapacity.ToString();
                _statistics.CurrentExperience = _loginApiCall.HUD.Experience.ToString();
                _statistics.Level = _loginApiCall.PlayerProfile.Level.ToString();
                _statistics.ExperienceToLevel = (_loginApiCall.HUD.LevelXpUpperBound - _loginApiCall.HUD.Experience).ToString();
                _statistics.Zones = _loginApiCall.PlayerProfile.TotalZonesCaptured.ToString() + "/" + _loginApiCall.PlayerProfile.ZonesCurrentlyLeading.ToString();
                _statistics.Credits = _loginApiCall.HUD.Qredits.ToString();
                _statistics.CodeName = _loginApiCall.PlayerProfile.Codename;
            }
        }

        public bool ScanZones()
        {
            ZonesPinsApiCall zonesPins = _apiCall.ZonesPins(Lattitude, Longitude);

            LoadZones(zonesPins);

            return zonesPins != null;
        }

        private void LoadZones(ZonesPinsApiCall zonesPins)
        {
            if (zonesPins != null && zonesPins.Zones != null)
            {
                _zonesList = new List<Zoneski>();

                foreach (Zone zone in zonesPins.Zones)
                {
                    Zoneski zoneski = new Zoneski();
                    
                    zoneski.ControlState = ZoneControlStateConverter(zone.ControlState);
                    zoneski.ZoneName = zone.ZoneName;
                    zoneski.ZoneId = zone.ZoneId.ToString();
                    zoneski.Latitude = zone.Latitude.ToString();
                    zoneski.Longitude = zone.Longitude.ToString();

                    _zonesList.Add(zoneski);
                }
            }
        }

        private string ZoneControlStateConverter(int zoneControlState)
        {
            switch (zoneControlState)
            {
                case 0:
                    return "*U*";
                case 1:
                    return "*L*";
                case 2:
                    return "*S*";
                case 3:
                    return "*F*";
                default:
                    return string.Empty;
            };

        }

        public bool LaunchBots(Zoneski zone, int myLevel)
        {
            string attackFormation = "0";
            if (myLevel >= 42)
            {
                attackFormation = "1024"; // Shockwave 4
            }
            else if (myLevel >= 29)
            {
                attackFormation = "1023"; // Shockwave 3
            }
            else if (myLevel >= 13)
            {
                attackFormation = "1022"; // Shockwave 2
            }
            else if (myLevel >= 3)
            {
                attackFormation = "1021"; // Shockwave 1
            }
            else
            {
                attackFormation = "1011"; // Zone Assault 1
            }

            LaunchApiCall launchData = _apiCall.Launch(zone.Latitude, zone.Longitude, zone.ZoneId, attackFormation);

            if (launchData != null) // gather launch data only if the call was successful
            {
                _launch.BotsAfterLaunch = launchData.HUD.BotCountAfterLastDeployment;
                _launch.BotsPerSecond = launchData.HUD.BotsPerSecond;
                _launch.BotsLaunched = launchData.Summary.Breakdown.BotsLaunched;
                _launch.EnergyAfterLaunch = launchData.HUD.EnergyCountAfterLastDeployment;
                _launch.EnergyPerSecond = launchData.HUD.EnergyPerSecond;
                _launch.PlayerCapturedZone = launchData.Summary.Rewards.PlayerCapturedZone;
            }

            return launchData != null; // this is null if the call failed
        }

        #endregion

        #region Structs

        public struct Stats
        {
            public string BotCapacity { get; set; }

            public string EnergyCapacity { get; set; }

            public string CurrentExperience { get; set; }

            public string Level { get; set; }

            public string ExperienceToLevel { get; set; }

            public string Zones { get; set; }

            public string Credits { get; set; }

            public string CodeName { get; set; }
        }

        public struct Zoneski
        {
            public string ZoneName { get; set; }

            public string ZoneId { get; set; }

            public string Latitude { get; set; }

            public string Longitude { get; set; }

            public string ControlState { get; set; }

            public int CurrentGasInTank { get; set; }
        }

        public struct Harvester
        {
            public int CreditsEarned { get; set; }

            public int TotalCreditsEarned { get; set; }
        }

        public struct Launcher
        {
            public int BotsAfterLaunch { get; set; }

            public double BotsPerSecond { get; set; }

            public int BotsLaunched { get; set; }

            public bool PlayerCapturedZone { get; set; }

            public int EnergyAfterLaunch { get; set; }

            public double EnergyPerSecond { get; set; }

        }

        #endregion

    }
}

