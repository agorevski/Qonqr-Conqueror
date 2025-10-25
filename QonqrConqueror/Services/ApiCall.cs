using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Qonqr;

public class ApiCall : IDisposable
    {
        private const string USER_AGENT = "QONQR/1.7.4642.40034 (WindowsPhone 7.10.-1; NOKIA Lumia 900; Unknown)";
        private const string CLIENT_APP_VERSION = "1.7.4642.40034";
        private const string REFERER = @"file:///Applications/Install/DFE7FCEF-4904-4C9A-8423-927A8D5DED18/Install/";
        private const string BASE_URL = "https://api.qonqr.com/v1";
        
        private readonly HttpClient _httpClient;
        private string _username;
        private string _password;
        private string _deviceId;

        /// <summary>
        /// Constructor that initializes the HttpClient with default headers
        /// </summary>
        public ApiCall()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", USER_AGENT);
            _httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "identity");
            _httpClient.DefaultRequestHeaders.Add("Referer", REFERER);
            _httpClient.DefaultRequestHeaders.Add("ClientAppVersion", CLIENT_APP_VERSION);
            _httpClient.DefaultRequestHeaders.Add("DeviceType", "3");
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
            string loginUrl = $"{BASE_URL}/Login?nocache={now.Month}%2F{now.Day}%2F{now.Year}%20{now.Hour}%3A{now.Minute}%3A{now.Second}%20{ampm}";

            using var request = new HttpRequestMessage(HttpMethod.Get, loginUrl);
            AddAuthHeaders(request, string.Empty, string.Empty);

            string responseBody = await SendHttpRequestAsync(request, cancellationToken);
            if (string.IsNullOrEmpty(responseBody))
            {
                return null;
            }

            return JsonSerializer.Deserialize<LoginApiCall>(responseBody);
        }

        public async Task<FortsApiCall> FortsAsync(string latitude, string longitude, CancellationToken cancellationToken = default)
        {
            string fortsUrl = $"{BASE_URL}/Players/Forts";

            using var request = new HttpRequestMessage(HttpMethod.Post, fortsUrl);
            AddAuthHeaders(request, latitude, longitude);

            string responseBody = await SendHttpRequestAsync(request, cancellationToken);
            if (string.IsNullOrEmpty(responseBody))
            {
                return null;
            }

            return JsonSerializer.Deserialize<FortsApiCall>(responseBody);
        }

        public async Task<HarvestAll> HarvestAllAsync(string latitude, string longitude, CancellationToken cancellationToken = default)
        {
            string harvestAllUrl = $"{BASE_URL}/Forts/HarvestAll";

            using var request = new HttpRequestMessage(HttpMethod.Post, harvestAllUrl);
            AddAuthHeaders(request, latitude, longitude);

            string responseBody = await SendHttpRequestAsync(request, cancellationToken);
            if (string.IsNullOrEmpty(responseBody))
            {
                return null;
            }

            return JsonSerializer.Deserialize<HarvestAll>(responseBody);
        }

        public async Task<LaunchApiCall> LaunchAsync(string latitude, string longitude, string zoneId, string formationId, CancellationToken cancellationToken = default)
        {
            string launchUrl = $"{BASE_URL}/Deployments/Launch";

            using var request = new HttpRequestMessage(HttpMethod.Post, launchUrl);
            AddAuthHeaders(request, latitude, longitude);
            
            string payload = $"ZoneId={zoneId}&FormationId={formationId}";
            request.Content = new StringContent(payload, Encoding.UTF8, "application/x-www-form-urlencoded");

            string responseBody = await SendHttpRequestAsync(request, cancellationToken);
            if (string.IsNullOrEmpty(responseBody))
            {
                return null;
            }

            return JsonSerializer.Deserialize<LaunchApiCall>(responseBody);
        }

        internal async Task<ZonesPinsApiCall> ZonesPinsAsync(string latitude, string longitude, CancellationToken cancellationToken = default)
        {
            double.TryParse(latitude, out double lat1);
            double.TryParse(longitude, out double long1);
            double lat2 = lat1 - 0.2;
            double long2 = long1 + 0.2;

            DateTime now = DateTime.Now;
            string ampm = now.Hour <= 12 ? "AM" : "PM";

            string zonesPinsUrl = $"{BASE_URL}/Zones/Pins/{lat1}/{long1}/{lat2}/{long2}?nocache={now.Month}%2F{now.Day}%2F{now.Year}%20{now.Hour}%3A{now.Minute}%3A{now.Second}%20{ampm}";

            using var request = new HttpRequestMessage(HttpMethod.Get, zonesPinsUrl);
            AddAuthHeaders(request, latitude, longitude);

            string responseBody = await SendHttpRequestAsync(request, cancellationToken);
            if (string.IsNullOrEmpty(responseBody))
            {
                return null;
            }

            return JsonSerializer.Deserialize<ZonesPinsApiCall>(responseBody);
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
                using var response = await _httpClient.SendAsync(request, cancellationToken);
                
                if (!response.IsSuccessStatusCode)
                {
                    return string.Empty;
                }

                return await response.Content.ReadAsStringAsync(cancellationToken);
            }
            catch (HttpRequestException)
            {
                return string.Empty;
            }
            catch (OperationCanceledException)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Disposes of the HttpClient
        /// </summary>
        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
