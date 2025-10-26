using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Qonqr.Exceptions;

namespace Qonqr;

    public class ApiCall : IDisposable
    {
        // Singleton HttpClient to avoid socket exhaustion
        private static readonly HttpClient _sharedHttpClient;
        private readonly ApiConfiguration _apiConfig;
        
        private string _username;
        private string _password;
        private string _deviceId;

        /// <summary>
        /// Static constructor to initialize the shared HttpClient with default headers
        /// </summary>
        static ApiCall()
        {
            var config = ApiConfiguration.Current;
            _sharedHttpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(config.TimeoutSeconds)
            };
            _sharedHttpClient.DefaultRequestHeaders.Add("User-Agent", config.UserAgent);
            _sharedHttpClient.DefaultRequestHeaders.Add("Accept-Encoding", "identity");
            _sharedHttpClient.DefaultRequestHeaders.Add("Referer", config.Referer);
            _sharedHttpClient.DefaultRequestHeaders.Add("ClientAppVersion", config.ClientAppVersion);
            _sharedHttpClient.DefaultRequestHeaders.Add("DeviceType", config.DeviceType);
        }

        /// <summary>
        /// Instance constructor with optional configuration
        /// </summary>
        public ApiCall(ApiConfiguration? config = null)
        {
            _apiConfig = config ?? ApiConfiguration.Current;
        }

        /// <summary>
        /// The method used to log into the QONQR service
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        /// <param name="deviceId">The device ID</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Bot Capacity</returns>
        public async Task<LoginApiCall> LoginAsync(string username, string password, string deviceId, CancellationToken cancellationToken = default)
        {
            _username = username;
            _password = password;
            _deviceId = deviceId;

            DateTime now = DateTime.Now;
            string ampm = now.Hour > 12 ? "AM" : "PM";
            string loginUrl = $"{_apiConfig.BaseUrl}/Login?nocache={now.Month}%2F{now.Day}%2F{now.Year}%20{now.Hour}%3A{now.Minute}%3A{now.Second}%20{ampm}";

            using var request = new HttpRequestMessage(HttpMethod.Get, loginUrl);
            AddAuthHeaders(request, string.Empty, string.Empty);

            string responseBody = await SendHttpRequestAsync(request, cancellationToken);
            if (string.IsNullOrEmpty(responseBody))
            {
                Logger.LogError("LoginAsync", $"Login failed for user: {username}", username);
                throw new LoginFailedException(username, "Login request failed - no response received from server");
            }

            var loginResponse = JsonSerializer.Deserialize<LoginApiCall>(responseBody);
            if (loginResponse == null)
            {
                throw new LoginFailedException(username, "Failed to deserialize login response");
            }

            Logger.LogInfo($"Login successful for user: {username}", username);
            return loginResponse;
        }

        public async Task<FortsApiCall> FortsAsync(string latitude, string longitude, CancellationToken cancellationToken = default)
        {
            string fortsUrl = $"{_apiConfig.BaseUrl}/Players/Forts";

            using var request = new HttpRequestMessage(HttpMethod.Post, fortsUrl);
            AddAuthHeaders(request, latitude, longitude);

            string responseBody = await SendHttpRequestAsync(request, cancellationToken);
            if (string.IsNullOrEmpty(responseBody))
            {
                Logger.LogError("FortsAsync", $"Failed to retrieve forts at {latitude}, {longitude}", _username);
                throw new QonqrApiException($"Failed to retrieve forts at {latitude}, {longitude}");
            }

            var fortsResponse = JsonSerializer.Deserialize<FortsApiCall>(responseBody);
            if (fortsResponse == null)
            {
                throw new QonqrApiException("Failed to deserialize forts response");
            }

            return fortsResponse;
        }

        public async Task<HarvestAll> HarvestAllAsync(string latitude, string longitude, CancellationToken cancellationToken = default)
        {
            string harvestAllUrl = $"{_apiConfig.BaseUrl}/Forts/HarvestAll";

            using var request = new HttpRequestMessage(HttpMethod.Post, harvestAllUrl);
            AddAuthHeaders(request, latitude, longitude);

            string responseBody = await SendHttpRequestAsync(request, cancellationToken);
            if (string.IsNullOrEmpty(responseBody))
            {
                Logger.LogError("HarvestAllAsync", "Failed to harvest all bases", _username);
                throw new QonqrApiException("Failed to harvest all bases");
            }

            var harvestResponse = JsonSerializer.Deserialize<HarvestAll>(responseBody);
            if (harvestResponse == null)
            {
                throw new QonqrApiException("Failed to deserialize harvest response");
            }

            return harvestResponse;
        }

        public async Task<LaunchApiCall> LaunchAsync(string latitude, string longitude, string zoneId, string formationId, CancellationToken cancellationToken = default)
        {
            string launchUrl = $"{_apiConfig.BaseUrl}/Deployments/Launch";

            using var request = new HttpRequestMessage(HttpMethod.Post, launchUrl);
            AddAuthHeaders(request, latitude, longitude);
            
            string payload = $"ZoneId={zoneId}&FormationId={formationId}";
            request.Content = new StringContent(payload, Encoding.UTF8, "application/x-www-form-urlencoded");

            string responseBody = await SendHttpRequestAsync(request, cancellationToken);
            if (string.IsNullOrEmpty(responseBody))
            {
                Logger.LogError("LaunchAsync", $"Failed to launch bots at zone {zoneId} with formation {formationId}", _username);
                throw new QonqrApiException($"Failed to launch bots at zone {zoneId}");
            }

            var launchResponse = JsonSerializer.Deserialize<LaunchApiCall>(responseBody);
            if (launchResponse == null)
            {
                throw new QonqrApiException("Failed to deserialize launch response");
            }

            Logger.LogInfo($"Successfully launched bots at zone {zoneId} with formation {formationId}", _username);
            return launchResponse;
        }

        internal async Task<ZonesPinsApiCall> ZonesPinsAsync(string latitude, string longitude, CancellationToken cancellationToken = default)
        {
            double.TryParse(latitude, out double lat1);
            double.TryParse(longitude, out double long1);
            double lat2 = lat1 - 0.2;
            double long2 = long1 + 0.2;

            DateTime now = DateTime.Now;
            string ampm = now.Hour <= 12 ? "AM" : "PM";

            string zonesPinsUrl = $"{_apiConfig.BaseUrl}/Zones/Pins/{lat1}/{long1}/{lat2}/{long2}?nocache={now.Month}%2F{now.Day}%2F{now.Year}%20{now.Hour}%3A{now.Minute}%3A{now.Second}%20{ampm}";

            using var request = new HttpRequestMessage(HttpMethod.Get, zonesPinsUrl);
            AddAuthHeaders(request, latitude, longitude);

            string responseBody = await SendHttpRequestAsync(request, cancellationToken);
            if (string.IsNullOrEmpty(responseBody))
            {
                Logger.LogError("ZonesPinsAsync", $"Failed to scan zones at {latitude}, {longitude}", _username);
                throw new QonqrApiException($"Failed to scan zones at {latitude}, {longitude}");
            }

            var zonesResponse = JsonSerializer.Deserialize<ZonesPinsApiCall>(responseBody);
            if (zonesResponse == null)
            {
                throw new QonqrApiException("Failed to deserialize zones response");
            }

            return zonesResponse;
        }

        /// <summary>
        /// Adds authentication and location headers to the HTTP request
        /// </summary>
        private void AddAuthHeaders(HttpRequestMessage request, string latitude, string longitude)
        {
            request.Headers.Add("DeviceId", _deviceId);
            request.Headers.Add("Password", _password);
            request.Headers.Add("Username", _username);

            if (!string.IsNullOrEmpty(latitude))
            {
                request.Headers.Add("Latitude", latitude);
            }
            if (!string.IsNullOrEmpty(longitude))
            {
                request.Headers.Add("Longitude", longitude);
            }
        }

        /// <summary>
        /// Sends an HTTP request and returns the response body
        /// </summary>
        private async Task<string> SendHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                using var response = await _sharedHttpClient.SendAsync(request, cancellationToken);
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = $"HTTP {(int)response.StatusCode} {response.ReasonPhrase} for {request.Method} {request.RequestUri}";
                    Logger.LogError("SendHttpRequestAsync", errorMessage, _username);
                    throw new QonqrApiException(errorMessage, (int)response.StatusCode, request.RequestUri?.ToString() ?? "unknown");
                }

                return await response.Content.ReadAsStringAsync(cancellationToken);
            }
            catch (HttpRequestException ex)
            {
                Logger.LogError("SendHttpRequestAsync", ex, _username);
                throw new QonqrApiException("HTTP request failed", ex);
            }
            catch (OperationCanceledException)
            {
                Logger.LogInfo("HTTP request was cancelled", _username);
                throw new QonqrApiException("HTTP request was cancelled");
            }
        }

        /// <summary>
        /// Dispose implementation - no longer needs to dispose HttpClient as it's shared
        /// </summary>
        public void Dispose()
        {
            // No disposal needed - HttpClient is static and shared
        }
    }
