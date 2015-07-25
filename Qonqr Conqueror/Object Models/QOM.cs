using System;
using System.Collections.Generic;
namespace Qonqr
{
    public class QOM
    {

    }

    [Serializable]
    public class LaunchApiCall
    {
        public HUD HUD;
        public PlayerProfile PlayerProfile;
        public Summary Summary;
        public Zone Zone;
    }

    [Serializable]
    public class HarvestAll
    {
        public HUD HUD;
        public PlayerForts PlayerForts;
        public int QreditsEarned;
    }

    [Serializable]
    public class LoginApiCall
    {
        public Content Content;
        public HUD HUD;
        public PlayerAccount PlayerAccount;
        public PlayerInventory PlayerInventory;
        public PlayerProfile PlayerProfile;
        public PlayerUpgrades PlayerUpgrades;
        public string User;
    }

    [Serializable]
    public class FortsApiCall
    {
        public PlayerForts PlayerForts;
    }

    [Serializable]
    public class PlayerForts
    {
        public List<Forts> Forts;
        public int TotalFortsEstablished;
        public int TotalFortsUnused;
    }

    [Serializable]
    public class Forts
    {
        public string CountryName;
        public DateTime CreateDateUTC;
        public int CurrentGasInTank;
        public int FortId;
        public DateTime LastFiredDateUTC;
        public DateTime LastHarvestDateUTC;
        public double Latitude;
        public double Longitude;
        public string RegionName;
        public int TankCapacity;
        public int ZoneControlState;
        public int ZoneId;
        public string ZoneName;
        public string ZoneOperatingAtPercent;
    }

    [Serializable]
    public class ZonesPinsApiCall
    {
        public List<Zone> Zones;
        public int Count;
    }

    [Serializable]
    public class PlayerUpgrades
    {
        public List<Entries> Entries;

    }

    [Serializable]
    public class PlayerInventory
    {
        public List<Entries> Entries;
        public int MissileRangeBonusPercentage;
        public int RangeBonusPercentage;
    }

    [Serializable]
    public class Entries
    {
        public int ConsumableItemId;
        public int Quantity;
        public int UpgradeFamilyId;
        public int UpgradeId;
    }

    [Serializable]
    public class PlayerAccount
    {
        public DateTime BirthDate;
        public DateTime CreatedOnUTC;
        public string Email;
        public string FacebookAuthorizationCode;
        public int FacebookUserId;
        public string Gender;
        public int HomeZoneId;
        public bool IsAdmin;
        public int PlayerId;
    }

    [Serializable]
    public class Content
    {
        public List<ConsumableItemsContentResponses> ConsumableItemsContentResponses;
        public DateTime ConsumableItemsRefreshDateUTC;
        public double ConsumableItemsVersionNumber;
        public ExchangeRateContentResponses ExchangeRateContentResponses;
        public List<FormationsContentResponses> FormationsContentResponses;
        public DateTime FormationsRefreshDateUTC;
        public double FormationsVersionNumber;
        public List<MedalsContentResponses> MedalsContentResponses;
        public DateTime MedalsRefreshDateUTC;
        public double MedalsVersionNumber;
        public List<RanksContentResponses> RanksContentResponses;
        public DateTime RanksRefreshDateUTC;
        public double RanksVersionNumber;
        public List<ScopeUpgradesContentResponses> ScopeUpgradesContentResponses;
        public DateTime ScopeUpgradesRefreshDateUTC;
        public double ScopeUpgradesVersionNumber;
    }

    [Serializable]
    public class ScopeUpgradesContentResponses
    {
        public string CategoryImageUrl;
        public string CategoryName;
        public int CubePrice;
        public bool FacelessUnlocked;
        public string FullDescription;
        public string ImageUrl;
        public bool LegionUnlocked;
        public int LevelRequired;
        public string Name;
        public int NextUpgradeId;
        public bool PurchaseableForCubes;
        public bool PurchaseableForQredits;
        public int QreditPrice;
        public int RankRequired;
        public string ShortDescription;
        public string ShortName;
        public int SortOrderInCategory;
        public bool SwarmUnlocked;
        public int UpgradeFamilyId;
        public int UpgradeId;
        public int UpgradePrereq;
    }

    [Serializable]
    public class MedalsContentResponses
    {
        public string FullDescription;
        public int Id;
        public string ImageUrl;
        public double MedalCompletionState;
        public string Name;
        public string RequirementStateDescription;
        public string ShortDescription;
        public string ShortName;
    }

    [Serializable]
    public class RanksContentResponses
    {
        public string FullDescription;
        public int Id;
        public string ImageUrl;
        public string Name;
        public string ShortDescription;
        public string ShortName;
    }

    [Serializable]
    public class ExchangeRateContentResponses
    {
        public int CubeToQreditRate;
    }

    [Serializable]
    public class FormationsContentResponses
    {
        public int Aggression;
        public int Aoe;
        public int BotCost;
        public int ConsumableRequired;
        public int Damage;
        public int EnergyCost;
        public bool FacelessUnlocked;
        public int Family;
        public string FullDescription;
        public int HitPoints;
        public int Id;
        public string ImageUrl;
        public bool LegionUnlocked;
        public int LevelRequired;
        public int Maneuver;
        public string Name;
        public int Range;
        public int RankRequired;
        public int Shield;
        public string ShortDescription;
        public string ShortName;
        public int SortOrderInManuever;
        public int Survivability;
        public bool SwarmUnlocked;
        public int Threat;
        public int UpgradeFamRequired;
    }

    [Serializable]
    public class ConsumableItemsContentResponses
    {
        public string CategoryImageUrl;
        public string CategoryName;
        public int ConsumableItemId;
        public int CubePrice;
        public bool FacelessUnlocked;
        public string FullDescription;
        public string ImageUrl;
        public bool LegionUnlocked;
        public int LevelRequired;
        public int MaxQuantity;
        public string Name;
        public int PrereqUpgradeFamilyId;
        public bool PurchaseableForCubes;
        public bool PurchaseableForQredits;
        public int QreditPrice;
        public int RankRequired;
        public string ShortDescription;
        public string ShortName;
        public int SortOrderInCategory;
        public bool SwarmUnlocked;
    }

    [Serializable]
    public class HUD
    {
        public int BotCapacity;
        public int BotCountAfterLastDeployment;
        public double BotsPerSecond;
        public string Codename;
        public int Cubes;
        public int EnergyCapacity;
        public int EnergyCountAfterLastDeployment;
        public double EnergyPerSecond;
        public int Experience;
        public int FactionId;
        public DateTime LastBotDeployTimeUTC;
        public DateTime LastEnergyDeployTimeUTC;
        public int Level;
        public int LevelXpLowerBound;
        public int LevelXpUpperBound;
        public int PlayerId;
        public int Qredits;
        public int RankId;
        public DateTime ServerTimeUTC;
    }

    [Serializable]
    public class PlayerProfile
    {
        public bool AllowPublicMessages;
        public string Codename;
        public int FactionId;
        public string ImageUrl;
        public int Level;
        public int PlayerId;
        public int RankId;
        public int TotalZonesCaptured;
        public int ZonesCurrentlyLeading;
    }

    [Serializable]
    public class Zone
    {
        public string CapturedByCodename;
        public string CapturedByPlayerId;
        public int ControlState;
        public string CountryCode;
        public int CountryId;
        public string CountryName;
        public DateTime DateCapturedUTC;
        public int FacelessCount;
        public double? Latitude;
        public string LeaderCodename;
        public int LeaderPlayerId;
        public DateTime LeaderSinceDateUTC;
        public int LegionCount;
        public double? Longitude;
        public string RegionCode;
        public int RegionId;
        public string RegionName;
        public int SwarmCount;
        public int ZoneId;
        public string ZoneName;
    }

    [Serializable]
    public class Summary
    {
        public Breakdown Breakdown { get; set; }
        public int PlayerFaction { get; set; }
        public Rewards Rewards { get; set; }
        public List<Rivals> Rivals { get; set; }
    }

    [Serializable]
    public class Breakdown
    {
        public int BattleHistoryId;
        public int BotsKilledInAction;
        public int BotsLaunched;
        public int EnemyBotsDestroyed;
        public int EnemyBotsFaced;
        public int FinalBotsInZone;
        public int InitialPlayerBotsInZone;
        public string KillRatio;
        public int LaunchedFormationId;
        public string SurvivalRatio;
    }

    [Serializable]
    public class Rewards
    {
        public bool LeveledUp;
        public string LeveldUpMessage;
        public int NewRankId;
        public bool PlayerCapturedZone;
        public bool PlayerTookLeadInZone;
        public bool RankedUp;
        public int XpGained;
    }

    [Serializable]
    public class Rivals
    {
        public int BotsKilled;
        public string Codename;
        public int Faction;
        public string ImageUrl;
        public int InitialBots;
        public bool KnockedOut;
        public int PlayerId;
    }

}

