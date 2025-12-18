namespace Qonqr.Models;

/// <summary>
/// Represents zone information for display and operations.
/// Renamed from "Zoneski" to follow proper naming conventions.
/// </summary>
public class ZoneInfo
{
    public string ZoneName { get; set; } = string.Empty;
    public string ZoneId { get; set; } = string.Empty;
    public string Latitude { get; set; } = string.Empty;
    public string Longitude { get; set; } = string.Empty;
    public string ControlState { get; set; } = string.Empty;
    public int CurrentGasInTank { get; set; }

    /// <summary>
    /// Returns a formatted string for display in combo boxes
    /// Format: "{ControlState} {ZoneName} [{ZoneId}] <{Latitude}/{Longitude}>"
    /// </summary>
    public string ToDisplayString()
    {
        return $"{ControlState} {ZoneName} [{ZoneId}] <{Latitude}/{Longitude}>";
    }

    /// <summary>
    /// Creates a ZoneInfo from an API Zone object
    /// </summary>
    public static ZoneInfo FromApiZone(Zone zone, Func<int, string> controlStateConverter)
    {
        return new ZoneInfo
        {
            ControlState = controlStateConverter(zone.ControlState),
            ZoneName = zone.ZoneName,
            ZoneId = zone.ZoneId.ToString(),
            Latitude = zone.Latitude?.ToString() ?? string.Empty,
            Longitude = zone.Longitude?.ToString() ?? string.Empty
        };
    }

    /// <summary>
    /// Creates a ZoneInfo from an API Forts object
    /// </summary>
    public static ZoneInfo FromApiFort(Forts fort, Func<int, string> controlStateConverter)
    {
        return new ZoneInfo
        {
            ControlState = controlStateConverter(fort.ZoneControlState),
            ZoneName = fort.ZoneName,
            ZoneId = fort.ZoneId.ToString(),
            Latitude = fort.Latitude.ToString(),
            Longitude = fort.Longitude.ToString(),
            CurrentGasInTank = fort.CurrentGasInTank
        };
    }
}
