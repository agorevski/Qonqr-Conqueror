namespace Qonqr.Models;

/// <summary>
/// Represents harvest operation results and totals.
/// Renamed from "Harvester" to better reflect its purpose as a result container.
/// </summary>
public class HarvestResult
{
    /// <summary>
    /// Credits earned from the most recent harvest operation
    /// </summary>
    public int CreditsEarned { get; set; }

    /// <summary>
    /// Total credits earned across all harvest operations in this session
    /// </summary>
    public int TotalCreditsEarned { get; set; }

    /// <summary>
    /// Resets the current harvest earnings (typically called before a new harvest)
    /// </summary>
    public void ResetCurrentEarnings()
    {
        CreditsEarned = 0;
    }

    /// <summary>
    /// Records a harvest operation result
    /// </summary>
    public void RecordHarvest(int creditsEarned)
    {
        CreditsEarned = creditsEarned;
        TotalCreditsEarned += creditsEarned;
    }
}
