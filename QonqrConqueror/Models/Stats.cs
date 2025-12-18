namespace Qonqr.Models;

/// <summary>
/// Represents player statistics displayed in the UI
/// </summary>
public class Stats
{
    public string BotCapacity { get; set; } = string.Empty;
    public string EnergyCapacity { get; set; } = string.Empty;
    public string CurrentExperience { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
    public string ExperienceToLevel { get; set; } = string.Empty;
    public string Zones { get; set; } = string.Empty;
    public string Credits { get; set; } = string.Empty;
    public string CodeName { get; set; } = string.Empty;
}
