namespace Qonqr;

/// <summary>
/// Centralized API configuration with support for different environments
/// </summary>
public class ApiConfiguration
{
    public string BaseUrl { get; set; }
    public string UserAgent { get; set; }
    public string ClientAppVersion { get; set; }
    public string Referer { get; set; }
    public string DeviceType { get; set; }
    public int TimeoutSeconds { get; set; }
    public int MaxRetries { get; set; }

    /// <summary>
    /// Production configuration (default)
    /// </summary>
    public static ApiConfiguration Production => new ApiConfiguration
    {
        BaseUrl = "https://api.qonqr.com/v1",
        UserAgent = "QONQR/1.7.4642.40034 (WindowsPhone 7.10.-1; NOKIA Lumia 900; Unknown)",
        ClientAppVersion = "1.7.4642.40034",
        Referer = @"file:///Applications/Install/DFE7FCEF-4904-4C9A-8423-927A8D5DED18/Install/",
        DeviceType = "3",
        TimeoutSeconds = 30,
        MaxRetries = 3
    };

    /// <summary>
    /// Development configuration (if needed)
    /// </summary>
    public static ApiConfiguration Development => new ApiConfiguration
    {
        BaseUrl = "https://api.qonqr.com/v1",  // Could point to test environment
        UserAgent = "QONQR/1.7.4642.40034 (WindowsPhone 7.10.-1; NOKIA Lumia 900; Unknown)",
        ClientAppVersion = "1.7.4642.40034",
        Referer = @"file:///Applications/Install/DFE7FCEF-4904-4C9A-8423-927A8D5DED18/Install/",
        DeviceType = "3",
        TimeoutSeconds = 60,  // Longer timeout for development
        MaxRetries = 1  // Fewer retries for faster development feedback
    };

    /// <summary>
    /// Gets the current configuration based on environment or settings
    /// </summary>
    public static ApiConfiguration Current => Production;

    /// <summary>
    /// Builds a complete API endpoint URL
    /// </summary>
    public string BuildUrl(string endpoint)
    {
        if (string.IsNullOrWhiteSpace(endpoint))
        {
            throw new ArgumentException("Endpoint cannot be null or empty", nameof(endpoint));
        }

        // Ensure endpoint starts with /
        if (!endpoint.StartsWith("/"))
        {
            endpoint = "/" + endpoint;
        }

        return BaseUrl + endpoint;
    }

    /// <summary>
    /// Validates that the configuration is complete and valid
    /// </summary>
    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(BaseUrl) &&
               !string.IsNullOrWhiteSpace(UserAgent) &&
               !string.IsNullOrWhiteSpace(ClientAppVersion) &&
               !string.IsNullOrWhiteSpace(DeviceType) &&
               TimeoutSeconds > 0 &&
               MaxRetries >= 0;
    }
}
