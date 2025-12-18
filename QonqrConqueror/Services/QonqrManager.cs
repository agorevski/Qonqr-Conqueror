using Qonqr.Exceptions;
using Qonqr.Models;

namespace Qonqr;

/// <summary>
/// Main service class that coordinates QONQR operations across multiple accounts.
/// Handles login, zone scanning, harvesting, and bot launching.
/// </summary>
public class QonqrManager
{
    #region Private Fields

    private readonly ApiCall _apiCall;
    private LoginApiCall _loginApiCall;
    private readonly Stats _statistics;
    private List<ZoneInfo> _zonesList;
    private readonly HarvestResult _harvestResult;
    private List<ZoneInfo> _fortsList;
    private readonly LaunchResult _launchResult;
    private readonly List<Player> _accounts;

    #endregion

    #region Constructor

    public QonqrManager()
    {
        _apiCall = new ApiCall();
        _statistics = new Stats();
        _zonesList = new List<ZoneInfo>();
        _harvestResult = new HarvestResult();
        _fortsList = new List<ZoneInfo>();
        _launchResult = new LaunchResult();

        ConfigFile configFile = new ConfigFile();
        _accounts = configFile.GetAccounts();

        ResetCoordinates();
    }

    #endregion

    #region Properties

    public string Latitude { get; set; }

    public string Longitude { get; set; }

    public Stats Statistics => _statistics;

    public List<ZoneInfo> Zones => _zonesList;

    public HarvestResult Harvest => _harvestResult;

    public List<ZoneInfo> Forts => _fortsList;

    public LaunchResult Launch => _launchResult;

    #endregion

    #region Public Methods

    /// <summary>
    /// Logs into all configured accounts and sets metadata from login responses
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>true if all logins were successful</returns>
    public async Task<bool> LoginAllAccountsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            foreach (Player account in _accounts)
            {
                _loginApiCall = await _apiCall.LoginAsync(
                    account.Username,
                    account.Password,
                    account.DeviceId,
                    cancellationToken);

                account.Level = _loginApiCall.PlayerProfile.Level;
                account.Faction = _loginApiCall.PlayerProfile.FactionId;
            }

            LoadStats();
            return true;
        }
        catch (LoginFailedException ex)
        {
            Logger.LogError("LoginAllAccountsAsync", ex, ex.Username);
            return false;
        }
        catch (QonqrApiException ex)
        {
            Logger.LogError("LoginAllAccountsAsync", ex, "system");
            return false;
        }
    }

    /// <summary>
    /// Retrieves forts for all configured accounts
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>true if all fort requests were successful</returns>
    public async Task<bool> GetAllFortsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _fortsList.Clear();

            foreach (Player account in _accounts)
            {
                FortsApiCall forts = await _apiCall.FortsAsync(
                    account.Latitude,
                    account.Longitude,
                    cancellationToken);

                account.Forts = forts.PlayerForts.Forts;
                LoadForts(forts);
            }

            return true;
        }
        catch (QonqrApiException ex)
        {
            Logger.LogError("GetAllFortsAsync", ex, "system");
            return false;
        }
    }

    /// <summary>
    /// Harvests resources from all accounts
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>true if all harvests were successful</returns>
    public async Task<bool> PerformHarvestAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _harvestResult.ResetCurrentEarnings();

            foreach (Player account in _accounts)
            {
                HarvestAll harvestAll = await _apiCall.HarvestAllAsync(
                    account.Latitude,
                    account.Longitude,
                    cancellationToken);

                account.SessionHarvestTotal += harvestAll.QreditsEarned;
                _harvestResult.RecordHarvest(harvestAll.QreditsEarned);
            }

            return true;
        }
        catch (QonqrApiException ex)
        {
            Logger.LogError("PerformHarvestAllAsync", ex, "system");
            return false;
        }
    }

    /// <summary>
    /// Scans for zones at the current coordinates
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>true if scan was successful</returns>
    public async Task<bool> ScanZonesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            ZonesPinsApiCall zonesPins = await _apiCall.ZonesPinsAsync(
                Latitude,
                Longitude,
                cancellationToken);

            LoadZones(zonesPins);
            return true;
        }
        catch (QonqrApiException ex)
        {
            Logger.LogError("ScanZonesAsync", ex, "system");
            return false;
        }
    }

    /// <summary>
    /// Launches bots at the specified zone
    /// </summary>
    /// <param name="zone">Target zone information</param>
    /// <param name="playerLevel">Current player level (determines attack formation)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>true if launch was successful</returns>
    public async Task<bool> LaunchBotsAsync(ZoneInfo zone, int playerLevel, CancellationToken cancellationToken = default)
    {
        try
        {
            string attackFormation = DetermineAttackFormation(playerLevel);

            LaunchApiCall launchData = await _apiCall.LaunchAsync(
                zone.Latitude,
                zone.Longitude,
                zone.ZoneId,
                attackFormation,
                cancellationToken);

            _launchResult.UpdateFromApiResponse(launchData);
            return true;
        }
        catch (QonqrApiException ex)
        {
            Logger.LogError("LaunchBotsAsync", ex, "system");
            return false;
        }
    }

    /// <summary>
    /// Resets coordinates to default values
    /// </summary>
    public void ResetCoordinates()
    {
        Latitude = Constants.DefaultCoordinates.Latitude;
        Longitude = Constants.DefaultCoordinates.Longitude;
    }

    /// <summary>
    /// Loads statistics from the most recent login response
    /// </summary>
    public void LoadStats()
    {
        if (_loginApiCall == null)
        {
            ClearStats();
            return;
        }

        _statistics.BotCapacity = _loginApiCall.HUD.BotCapacity.ToString();
        _statistics.EnergyCapacity = _loginApiCall.HUD.EnergyCapacity.ToString();
        _statistics.CurrentExperience = _loginApiCall.HUD.Experience.ToString();
        _statistics.Level = _loginApiCall.PlayerProfile.Level.ToString();
        _statistics.ExperienceToLevel = (_loginApiCall.HUD.LevelXpUpperBound - _loginApiCall.HUD.Experience).ToString();
        _statistics.Zones = $"{_loginApiCall.PlayerProfile.TotalZonesCaptured}/{_loginApiCall.PlayerProfile.ZonesCurrentlyLeading}";
        _statistics.Credits = _loginApiCall.HUD.Qredits.ToString();
        _statistics.CodeName = _loginApiCall.PlayerProfile.Codename;
    }

    #endregion

    #region Private Methods

    private void ClearStats()
    {
        _statistics.BotCapacity = string.Empty;
        _statistics.EnergyCapacity = string.Empty;
        _statistics.CurrentExperience = string.Empty;
        _statistics.Level = string.Empty;
        _statistics.ExperienceToLevel = string.Empty;
        _statistics.Zones = string.Empty;
        _statistics.Credits = string.Empty;
        _statistics.CodeName = string.Empty;
    }

    private void LoadForts(FortsApiCall fortsApiCall)
    {
        if (fortsApiCall?.PlayerForts?.Forts == null)
        {
            return;
        }

        _fortsList = new List<ZoneInfo>();

        foreach (Forts fort in fortsApiCall.PlayerForts.Forts)
        {
            _fortsList.Add(ZoneInfo.FromApiFort(fort, ZoneControlStateConverter));
        }
    }

    private void LoadZones(ZonesPinsApiCall zonesPins)
    {
        if (zonesPins?.Zones == null)
        {
            return;
        }

        _zonesList = new List<ZoneInfo>();

        foreach (Zone zone in zonesPins.Zones)
        {
            _zonesList.Add(ZoneInfo.FromApiZone(zone, ZoneControlStateConverter));
        }
    }

    private static string ZoneControlStateConverter(int zoneControlState)
    {
        return zoneControlState switch
        {
            Constants.ZoneControlStates.UncapturedValue => Constants.ZoneControlStates.Uncaptured,
            Constants.ZoneControlStates.LegionValue => Constants.ZoneControlStates.Legion,
            Constants.ZoneControlStates.SwarmValue => Constants.ZoneControlStates.Swarm,
            Constants.ZoneControlStates.FacelessValue => Constants.ZoneControlStates.Faceless,
            _ => string.Empty
        };
    }

    private static string DetermineAttackFormation(int playerLevel)
    {
        if (playerLevel >= Constants.AttackFormations.Shockwave4MinLevel)
        {
            return Constants.AttackFormations.Shockwave4;
        }
        if (playerLevel >= Constants.AttackFormations.Shockwave3MinLevel)
        {
            return Constants.AttackFormations.Shockwave3;
        }
        if (playerLevel >= Constants.AttackFormations.Shockwave2MinLevel)
        {
            return Constants.AttackFormations.Shockwave2;
        }
        if (playerLevel >= Constants.AttackFormations.Shockwave1MinLevel)
        {
            return Constants.AttackFormations.Shockwave1;
        }

        return Constants.AttackFormations.ZoneAssault1;
    }

    #endregion
}
