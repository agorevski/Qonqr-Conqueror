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
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>true if all logins were successful</returns>
        public async Task<bool> LoginAllAccountsAsync(CancellationToken cancellationToken = default)
        {
            bool successful = true;
            foreach (Player account in _accounts)
            {
                // Perform the actual Login Call
                _loginApiCall = await _apiCall.LoginAsync(account.Username, account.Password, account.DeviceId, cancellationToken);

                if (_loginApiCall == null)
                {
                    successful = false;
                    break;
                }

                // Set some more properties on the account for future usage
                account.Level = _loginApiCall.PlayerProfile.Level;
                account.Faction = _loginApiCall.PlayerProfile.FactionId;
            }

            if (successful && _loginApiCall != null)
            {
                LoadStats();
            }

            return successful;
        }

        /// <summary>
        /// A helper method that gets forts for all of your accounts
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>true if all fort requests were successful</returns>
        public async Task<bool> GetAllFortsAsync(CancellationToken cancellationToken = default)
        {
            bool successful = true;
            _fortsList.Clear();
            
            foreach (Player account in _accounts)
            {
                FortsApiCall forts = await _apiCall.FortsAsync(account.Latitude, account.Longitude, cancellationToken);
                
                if (forts == null)
                {
                    successful = false;
                    break;
                }

                account.Forts = forts.PlayerForts.Forts;
                
                // Add to combined forts list
                LoadForts(forts);
            }
            
            return successful;
        }

        /// <summary>
        /// A helper method that Harvests all of your accounts and sets some additional
        /// metadata on them based on the harvest information
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>true if all harvests were successful</returns>
        public async Task<bool> PerformHarvestAllAsync(CancellationToken cancellationToken = default)
        {
            bool successful = true;
            _harvest.CreditsEarned = 0;
            
            foreach (Player account in _accounts)
            {
                HarvestAll harvestAll = await _apiCall.HarvestAllAsync(account.Latitude, account.Longitude, cancellationToken);
                
                if (harvestAll == null)
                {
                    successful = false;
                    break;
                }

                account.SessionHarvestTotal += harvestAll.QreditsEarned;
                LoadHarvest(harvestAll);
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
            Lattitude = Constants.DefaultCoordinates.Latitude;
            Longitude = Constants.DefaultCoordinates.Longitude;
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

        public async Task<bool> ScanZonesAsync(CancellationToken cancellationToken = default)
        {
            ZonesPinsApiCall zonesPins = await _apiCall.ZonesPinsAsync(Lattitude, Longitude, cancellationToken);

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
                case Constants.ZoneControlStates.UncapturedValue:
                    return Constants.ZoneControlStates.Uncaptured;
                case Constants.ZoneControlStates.LegionValue:
                    return Constants.ZoneControlStates.Legion;
                case Constants.ZoneControlStates.SwarmValue:
                    return Constants.ZoneControlStates.Swarm;
                case Constants.ZoneControlStates.FacelessValue:
                    return Constants.ZoneControlStates.Faceless;
                default:
                    return string.Empty;
            };

        }

        public async Task<bool> LaunchBotsAsync(Zoneski zone, int myLevel, CancellationToken cancellationToken = default)
        {
            string attackFormation = Constants.AttackFormations.ZoneAssault1;
            
            if (myLevel >= Constants.AttackFormations.Shockwave4MinLevel)
            {
                attackFormation = Constants.AttackFormations.Shockwave4;
            }
            else if (myLevel >= Constants.AttackFormations.Shockwave3MinLevel)
            {
                attackFormation = Constants.AttackFormations.Shockwave3;
            }
            else if (myLevel >= Constants.AttackFormations.Shockwave2MinLevel)
            {
                attackFormation = Constants.AttackFormations.Shockwave2;
            }
            else if (myLevel >= Constants.AttackFormations.Shockwave1MinLevel)
            {
                attackFormation = Constants.AttackFormations.Shockwave1;
            }

            LaunchApiCall launchData = await _apiCall.LaunchAsync(zone.Latitude, zone.Longitude, zone.ZoneId, attackFormation, cancellationToken);

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
