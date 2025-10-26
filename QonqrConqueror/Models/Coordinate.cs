namespace Qonqr;

/// <summary>
/// Represents a geographic coordinate with latitude and longitude
/// </summary>
public class Coordinate
{
    private const double MinLatitude = -90.0;
    private const double MaxLatitude = 90.0;
    private const double MinLongitude = -180.0;
    private const double MaxLongitude = 180.0;

    public double Latitude { get; }
    public double Longitude { get; }

    /// <summary>
    /// Creates a new Coordinate with validation
    /// </summary>
    public Coordinate(double latitude, double longitude)
    {
        if (latitude < MinLatitude || latitude > MaxLatitude)
        {
            throw new ArgumentOutOfRangeException(nameof(latitude), 
                $"Latitude must be between {MinLatitude} and {MaxLatitude}");
        }

        if (longitude < MinLongitude || longitude > MaxLongitude)
        {
            throw new ArgumentOutOfRangeException(nameof(longitude), 
                $"Longitude must be between {MinLongitude} and {MaxLongitude}");
        }

        Latitude = latitude;
        Longitude = longitude;
    }

    /// <summary>
    /// Creates a Coordinate from string values with validation
    /// </summary>
    public static Coordinate FromStrings(string latitude, string longitude)
    {
        if (!double.TryParse(latitude, out double lat))
        {
            throw new ArgumentException($"Invalid latitude value: '{latitude}'", nameof(latitude));
        }

        if (!double.TryParse(longitude, out double lon))
        {
            throw new ArgumentException($"Invalid longitude value: '{longitude}'", nameof(longitude));
        }

        return new Coordinate(lat, lon);
    }

    /// <summary>
    /// Tries to create a Coordinate from string values
    /// </summary>
    public static bool TryFromStrings(string latitude, string longitude, out Coordinate? coordinate)
    {
        coordinate = null;

        if (!double.TryParse(latitude, out double lat))
        {
            return false;
        }

        if (!double.TryParse(longitude, out double lon))
        {
            return false;
        }

        if (lat < MinLatitude || lat > MaxLatitude || lon < MinLongitude || lon > MaxLongitude)
        {
            return false;
        }

        coordinate = new Coordinate(lat, lon);
        return true;
    }

    public override string ToString()
    {
        return $"{Latitude}, {Longitude}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is Coordinate other)
        {
            return Math.Abs(Latitude - other.Latitude) < 0.0000001 && 
                   Math.Abs(Longitude - other.Longitude) < 0.0000001;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Latitude, Longitude);
    }
}
