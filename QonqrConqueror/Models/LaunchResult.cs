namespace Qonqr.Models;

/// <summary>
/// Represents the result of a bot launch operation.
/// Renamed from "Launcher" to better reflect its purpose as a result container.
/// </summary>
public class LaunchResult
{
    /// <summary>
    /// Number of bots remaining after the launch
    /// </summary>
    public int BotsAfterLaunch { get; set; }

    /// <summary>
    /// Bot regeneration rate per second
    /// </summary>
    public double BotsPerSecond { get; set; }

    /// <summary>
    /// Number of bots launched in this operation
    /// </summary>
    public int BotsLaunched { get; set; }

    /// <summary>
    /// Whether the player captured the zone with this launch
    /// </summary>
    public bool PlayerCapturedZone { get; set; }

    /// <summary>
    /// Energy remaining after the launch
    /// </summary>
    public int EnergyAfterLaunch { get; set; }

    /// <summary>
    /// Energy regeneration rate per second
    /// </summary>
    public double EnergyPerSecond { get; set; }

    /// <summary>
    /// Updates the launch result from API response data
    /// </summary>
    public void UpdateFromApiResponse(LaunchApiCall launchData)
    {
        if (launchData?.HUD == null || launchData?.Summary?.Breakdown == null)
        {
            return;
        }

        BotsAfterLaunch = launchData.HUD.BotCountAfterLastDeployment;
        BotsPerSecond = launchData.HUD.BotsPerSecond;
        BotsLaunched = launchData.Summary.Breakdown.BotsLaunched;
        EnergyAfterLaunch = launchData.HUD.EnergyCountAfterLastDeployment;
        EnergyPerSecond = launchData.HUD.EnergyPerSecond;
        PlayerCapturedZone = launchData.Summary.Rewards?.PlayerCapturedZone ?? false;
    }
}
