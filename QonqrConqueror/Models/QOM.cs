using System.Text.Json.Serialization;

namespace Qonqr;

/// <summary>
/// Base class for QONQR Object Model types
/// </summary>
public class QOM
{
}

/// <summary>
/// Response from the Launch API endpoint
/// </summary>
[Serializable]
public class LaunchApiCall
{
    public HUD HUD { get; set; }
    public PlayerProfile PlayerProfile { get; set; }
    public Summary Summary { get; set; }
    public Zone Zone { get; set; }
}

/// <summary>
/// Response from the HarvestAll API endpoint
/// </summary>
[Serializable]
public class HarvestAll
{
    public HUD HUD { get; set; }
    public PlayerForts PlayerForts { get; set; }
    public int QreditsEarned { get; set; }
}

/// <summary>
/// Response from the Login API endpoint
/// </summary>
[Serializable]
public class LoginApiCall
{
    public Content Content { get; set; }
    public HUD HUD { get; set; }
    public PlayerAccount PlayerAccount { get; set; }
    public PlayerInventory PlayerInventory { get; set; }
    public PlayerProfile PlayerProfile { get; set; }
    public PlayerUpgrades PlayerUpgrades { get; set; }
    public string User { get; set; }
}

/// <summary>
/// Response from the Forts API endpoint
/// </summary>
[Serializable]
public class FortsApiCall
{
    public PlayerForts PlayerForts { get; set; }
}

/// <summary>
/// Player forts information
/// </summary>
[Serializable]
public class PlayerForts
{
    public List<Forts> Forts { get; set; }
    public int TotalFortsEstablished { get; set; }
    public int TotalFortsUnused { get; set; }
}

/// <summary>
/// Individual fort information
/// </summary>
[Serializable]
public class Forts
{
    public string CountryName { get; set; }
    public DateTime CreateDateUTC { get; set; }
    public int CurrentGasInTank { get; set; }
    public int FortId { get; set; }
    public DateTime LastFiredDateUTC { get; set; }
    public DateTime LastHarvestDateUTC { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string RegionName { get; set; }
    public int TankCapacity { get; set; }
    public int ZoneControlState { get; set; }
    public int ZoneId { get; set; }
    public string ZoneName { get; set; }
    public string ZoneOperatingAtPercent { get; set; }
}

/// <summary>
/// Response from the ZonesPins API endpoint
/// </summary>
[Serializable]
public class ZonesPinsApiCall
{
    public List<Zone> Zones { get; set; }
    public int Count { get; set; }
}

/// <summary>
/// Player upgrades information
/// </summary>
[Serializable]
public class PlayerUpgrades
{
    public List<Entries> Entries { get; set; }
}

/// <summary>
/// Player inventory information
/// </summary>
[Serializable]
public class PlayerInventory
{
    public List<Entries> Entries { get; set; }
    public int MissileRangeBonusPercentage { get; set; }
    public int RangeBonusPercentage { get; set; }
}

/// <summary>
/// Entry in player inventory or upgrades
/// </summary>
[Serializable]
public class Entries
{
    public int ConsumableItemId { get; set; }
    public int Quantity { get; set; }
    public int UpgradeFamilyId { get; set; }
    public int UpgradeId { get; set; }
}

/// <summary>
/// Player account information
/// </summary>
[Serializable]
public class PlayerAccount
{
    public DateTime BirthDate { get; set; }
    public DateTime CreatedOnUTC { get; set; }
    public string Email { get; set; }
    public string FacebookAuthorizationCode { get; set; }
    public int FacebookUserId { get; set; }
    public string Gender { get; set; }
    public int HomeZoneId { get; set; }
    public bool IsAdmin { get; set; }
    public int PlayerId { get; set; }
}

/// <summary>
/// Content data from login response
/// </summary>
[Serializable]
public class Content
{
    public List<ConsumableItemsContentResponses> ConsumableItemsContentResponses { get; set; }
    public DateTime ConsumableItemsRefreshDateUTC { get; set; }
    public double ConsumableItemsVersionNumber { get; set; }
    public ExchangeRateContentResponses ExchangeRateContentResponses { get; set; }
    public List<FormationsContentResponses> FormationsContentResponses { get; set; }
    public DateTime FormationsRefreshDateUTC { get; set; }
    public double FormationsVersionNumber { get; set; }
    public List<MedalsContentResponses> MedalsContentResponses { get; set; }
    public DateTime MedalsRefreshDateUTC { get; set; }
    public double MedalsVersionNumber { get; set; }
    public List<RanksContentResponses> RanksContentResponses { get; set; }
    public DateTime RanksRefreshDateUTC { get; set; }
    public double RanksVersionNumber { get; set; }
    public List<ScopeUpgradesContentResponses> ScopeUpgradesContentResponses { get; set; }
    public DateTime ScopeUpgradesRefreshDateUTC { get; set; }
    public double ScopeUpgradesVersionNumber { get; set; }
}

/// <summary>
/// Scope upgrades content data
/// </summary>
[Serializable]
public class ScopeUpgradesContentResponses
{
    public string CategoryImageUrl { get; set; }
    public string CategoryName { get; set; }
    public int CubePrice { get; set; }
    public bool FacelessUnlocked { get; set; }
    public string FullDescription { get; set; }
    public string ImageUrl { get; set; }
    public bool LegionUnlocked { get; set; }
    public int LevelRequired { get; set; }
    public string Name { get; set; }
    public int NextUpgradeId { get; set; }
    public bool PurchaseableForCubes { get; set; }
    public bool PurchaseableForQredits { get; set; }
    public int QreditPrice { get; set; }
    public int RankRequired { get; set; }
    public string ShortDescription { get; set; }
    public string ShortName { get; set; }
    public int SortOrderInCategory { get; set; }
    public bool SwarmUnlocked { get; set; }
    public int UpgradeFamilyId { get; set; }
    public int UpgradeId { get; set; }
    public int UpgradePrereq { get; set; }
}

/// <summary>
/// Medals content data
/// </summary>
[Serializable]
public class MedalsContentResponses
{
    public string FullDescription { get; set; }
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    public double MedalCompletionState { get; set; }
    public string Name { get; set; }
    public string RequirementStateDescription { get; set; }
    public string ShortDescription { get; set; }
    public string ShortName { get; set; }
}

/// <summary>
/// Ranks content data
/// </summary>
[Serializable]
public class RanksContentResponses
{
    public string FullDescription { get; set; }
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    public string Name { get; set; }
    public string ShortDescription { get; set; }
    public string ShortName { get; set; }
}

/// <summary>
/// Exchange rate content data
/// </summary>
[Serializable]
public class ExchangeRateContentResponses
{
    public int CubeToQreditRate { get; set; }
}

/// <summary>
/// Formations content data
/// </summary>
[Serializable]
public class FormationsContentResponses
{
    public int Aggression { get; set; }
    public int Aoe { get; set; }
    public int BotCost { get; set; }
    public int ConsumableRequired { get; set; }
    public int Damage { get; set; }
    public int EnergyCost { get; set; }
    public bool FacelessUnlocked { get; set; }
    public int Family { get; set; }
    public string FullDescription { get; set; }
    public int HitPoints { get; set; }
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    public bool LegionUnlocked { get; set; }
    public int LevelRequired { get; set; }
    public int Maneuver { get; set; }
    public string Name { get; set; }
    public int Range { get; set; }
    public int RankRequired { get; set; }
    public int Shield { get; set; }
    public string ShortDescription { get; set; }
    public string ShortName { get; set; }
    public int SortOrderInManuever { get; set; }
    public int Survivability { get; set; }
    public bool SwarmUnlocked { get; set; }
    public int Threat { get; set; }
    public int UpgradeFamRequired { get; set; }
}

/// <summary>
/// Consumable items content data
/// </summary>
[Serializable]
public class ConsumableItemsContentResponses
{
    public string CategoryImageUrl { get; set; }
    public string CategoryName { get; set; }
    public int ConsumableItemId { get; set; }
    public int CubePrice { get; set; }
    public bool FacelessUnlocked { get; set; }
    public string FullDescription { get; set; }
    public string ImageUrl { get; set; }
    public bool LegionUnlocked { get; set; }
    public int LevelRequired { get; set; }
    public int MaxQuantity { get; set; }
    public string Name { get; set; }
    public int PrereqUpgradeFamilyId { get; set; }
    public bool PurchaseableForCubes { get; set; }
    public bool PurchaseableForQredits { get; set; }
    public int QreditPrice { get; set; }
    public int RankRequired { get; set; }
    public string ShortDescription { get; set; }
    public string ShortName { get; set; }
    public int SortOrderInCategory { get; set; }
    public bool SwarmUnlocked { get; set; }
}

/// <summary>
/// HUD (Heads-Up Display) data containing player resources and stats
/// </summary>
[Serializable]
public class HUD
{
    public int BotCapacity { get; set; }
    public int BotCountAfterLastDeployment { get; set; }
    public double BotsPerSecond { get; set; }
    public string Codename { get; set; }
    public int Cubes { get; set; }
    public int EnergyCapacity { get; set; }
    public int EnergyCountAfterLastDeployment { get; set; }
    public double EnergyPerSecond { get; set; }
    public int Experience { get; set; }
    public int FactionId { get; set; }
    public DateTime LastBotDeployTimeUTC { get; set; }
    public DateTime LastEnergyDeployTimeUTC { get; set; }
    public int Level { get; set; }
    public int LevelXpLowerBound { get; set; }
    public int LevelXpUpperBound { get; set; }
    public int PlayerId { get; set; }
    public int Qredits { get; set; }
    public int RankId { get; set; }
    public DateTime ServerTimeUTC { get; set; }
}

/// <summary>
/// Player profile information
/// </summary>
[Serializable]
public class PlayerProfile
{
    public bool AllowPublicMessages { get; set; }
    public string Codename { get; set; }
    public int FactionId { get; set; }
    public string ImageUrl { get; set; }
    public int Level { get; set; }
    public int PlayerId { get; set; }
    public int RankId { get; set; }
    public int TotalZonesCaptured { get; set; }
    public int ZonesCurrentlyLeading { get; set; }
}

/// <summary>
/// Zone information
/// </summary>
[Serializable]
public class Zone
{
    public string CapturedByCodename { get; set; }
    public string CapturedByPlayerId { get; set; }
    public int ControlState { get; set; }
    public string CountryCode { get; set; }
    public int CountryId { get; set; }
    public string CountryName { get; set; }
    public DateTime DateCapturedUTC { get; set; }
    public int FacelessCount { get; set; }
    public double? Latitude { get; set; }
    public string LeaderCodename { get; set; }
    public int LeaderPlayerId { get; set; }
    public DateTime LeaderSinceDateUTC { get; set; }
    public int LegionCount { get; set; }
    public double? Longitude { get; set; }
    public string RegionCode { get; set; }
    public int RegionId { get; set; }
    public string RegionName { get; set; }
    public int SwarmCount { get; set; }
    public int ZoneId { get; set; }
    public string ZoneName { get; set; }
}

/// <summary>
/// Summary of a launch operation
/// </summary>
[Serializable]
public class Summary
{
    public Breakdown Breakdown { get; set; }
    public int PlayerFaction { get; set; }
    public Rewards Rewards { get; set; }
    public List<Rivals> Rivals { get; set; }
}

/// <summary>
/// Breakdown of a launch operation
/// </summary>
[Serializable]
public class Breakdown
{
    public int BattleHistoryId { get; set; }
    public int BotsKilledInAction { get; set; }
    public int BotsLaunched { get; set; }
    public int EnemyBotsDestroyed { get; set; }
    public int EnemyBotsFaced { get; set; }
    public int FinalBotsInZone { get; set; }
    public int InitialPlayerBotsInZone { get; set; }
    public string KillRatio { get; set; }
    public int LaunchedFormationId { get; set; }
    public string SurvivalRatio { get; set; }
}

/// <summary>
/// Rewards from a launch operation
/// </summary>
[Serializable]
public class Rewards
{
    public bool LeveledUp { get; set; }
    public string LeveldUpMessage { get; set; }
    public int NewRankId { get; set; }
    public bool PlayerCapturedZone { get; set; }
    public bool PlayerTookLeadInZone { get; set; }
    public bool RankedUp { get; set; }
    public int XpGained { get; set; }
}

/// <summary>
/// Rival player information from a battle
/// </summary>
[Serializable]
public class Rivals
{
    public int BotsKilled { get; set; }
    public string Codename { get; set; }
    public int Faction { get; set; }
    public string ImageUrl { get; set; }
    public int InitialBots { get; set; }
    public bool KnockedOut { get; set; }
    public int PlayerId { get; set; }
}
