namespace Qonqr
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Cache;
    using System.Web.Script.Serialization;

    public class ApiCall
    {
        private enum RequestType
        {
            GET,
            POST,
        }

        private const string USER_AGENT = "User-Agent: QONQR/1.7.4642.40034 (WindowsPhone 7.10.-1; NOKIA Lumia 900; Unknown)";
        private const string CLIENT_APP_VERSION = "1.7.4642.40034";
        private const string REFERER = @"file:///Applications/Install/DFE7FCEF-4904-4C9A-8423-927A8D5DED18/Install/";
        private string _username;
        private string _password;
        private string _deviceId;

        /// <summary>
        /// The WebRequest constructor that will be used to construct all web requests
        /// </summary>
        public ApiCall()
        { }

        /// <summary>
        /// The method used to log into the QONQR service
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        /// <returns>Bot Capacity</returns>
        public LoginApiCall Login(string username, string password, string deviceId)
        {
            _username = username;
            _password = password;
            _deviceId = deviceId;

            DateTime now = DateTime.Now;
            string ampm = now.Hour > 12 ? "AM" : "PM";
            string loginUrl = string.Format(@"https://api.qonqr.com/v1/Login?nocache=" +
            "{0}%2F{1}%2F{2}%20{3}%3A{4}%3A{5}%20{6}", now.Month, now.Day, now.Year,
            now.Hour, now.Minute, now.Second, ampm);

            HttpWebRequest request = CreateBasicRequest(RequestType.GET, loginUrl);
            request.Headers = SetHeaders(string.Empty, string.Empty);
            string responseBody = SendRequestGetResponse(RequestType.GET, request);
            if (responseBody == string.Empty)
            {
                return null;
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Deserialize<LoginApiCall>(responseBody);

            //string name = FindPropertyValueInBody(responseBody, "User");
            //string level = FindPropertyValueInBody(responseBody, "Level");
            //int curXP = 0;
            //int.TryParse(FindPropertyValueInBody(responseBody, "Experience"), out curXP);
            //int XP_UpperBound = 0;
            //int.TryParse(FindPropertyValueInBody(responseBody, "LevelXpUpperBound"), out XP_UpperBound);
            //int expLeft = XP_UpperBound - curXP;

            //LoginInfo info = new LoginInfo()
            //{
            //    BotCapacity = FindPropertyValueInBody(responseBody, "BotCapacity"),
            //    BotCountAfterLastDeployment = FindPropertyValueInBody(responseBody, "BotCountAfterLastDeployment"),
            //    BotsPerSecond = FindPropertyValueInBody(responseBody, "BotsPerSecond"),
            //    Codename = FindPropertyValueInBody(responseBody, "Codename"),
            //    EnergyCapacity = FindPropertyValueInBody(responseBody, "EnergyCapacity"),
            //    EnergyCountAfterLastDeployment = FindPropertyValueInBody(responseBody, "EnergyCountAfterLastDeployment"),
            //    EnergyPerSecond = FindPropertyValueInBody(responseBody, "EnergyPerSecond"),
            //    Experience = FindPropertyValueInBody(responseBody, "Experience"),
            //    FactionId = FindPropertyValueInBody(responseBody, "FactionId"),
            //    LastBotDeployTimeUTC = FindPropertyValueInBody(responseBody, "LastBotDeployTimeUTC"),
            //    LastEnergyDeployTimeUTC = FindPropertyValueInBody(responseBody, "LastEnergyDeployTimeUTC"),
            //    Level = FindPropertyValueInBody(responseBody, "Level"),
            //    LevelXpUpperBound = FindPropertyValueInBody(responseBody, "LevelXpUpperBound"),
            //    PlayerId = FindPropertyValueInBody(responseBody, "PlayerId"),
            //    Qredits = FindPropertyValueInBody(responseBody, "Qredits"),
            //    ServerTimeUTC = FindPropertyValueInBody(responseBody, "ServerTimeUTC"),
            //    AllowPublicMessages = FindPropertyValueInBody(responseBody, "AllowPublicMessages"),
            //    ImageUrl = FindPropertyValueInBody(responseBody, "ImageUrl"),
            //    TotalZonesCaptured = FindPropertyValueInBody(responseBody, "TotalZonesCaptured"),
            //    ZonesCurrentlyLeading = FindPropertyValueInBody(responseBody, "ZonesCurrentlyLeading"),
            //};
            //return info;
        }

        public FortsApiCall Forts(string latitude, string longitude)
        {
            string fortsUrl = string.Format(@"https://api.qonqr.com/v1/Players/Forts");

            HttpWebRequest request = CreateBasicRequest(RequestType.POST, fortsUrl);
            request.Headers = SetHeaders(latitude, longitude);
            string responseBody = SendRequestGetResponse(RequestType.POST, request);
            if (responseBody == string.Empty)
            {
                return null;
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Deserialize<FortsApiCall>(responseBody);
        }

        public HarvestAll HarvestAll(string latitude, string longitude)
        {
            string harvestAllUrl = string.Format(@"https://api.qonqr.com/v1/Forts/HarvestAll");

            HttpWebRequest request = CreateBasicRequest(RequestType.POST, harvestAllUrl);
            request.Headers = SetHeaders(latitude, longitude);
            string responseBody = SendRequestGetResponse(RequestType.POST, request);
            if (responseBody == string.Empty)
            {
                return null;
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Deserialize<HarvestAll>(responseBody);
        }

        public LaunchApiCall Launch(string latitude, string longitude, string zoneId, string formationId)
        {
            string launchUrl = string.Format(@"https://api.qonqr.com/v1/Deployments/Launch");

            HttpWebRequest request = CreateBasicRequest(RequestType.POST, launchUrl);
            request.Headers = SetHeaders(latitude, longitude);
            request.ContentType = "application/x-www-form-urlencoded";
            string payload = string.Format("ZoneId={0}&FormationId={1}", zoneId, formationId);
            string responseBody = SendRequestGetResponse(RequestType.POST, request, payload);
            if (responseBody == string.Empty)
            {
                return null;
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Deserialize<LaunchApiCall>(responseBody);
        }

        internal ZonesPinsApiCall ZonesPins(string latitude, string longitude)
        {
            double lat1 = 0;
            double long1 = 0;
            double.TryParse(latitude, out lat1);
            double.TryParse(longitude, out long1);
            double lat2 = lat1 - 0.2;
            double long2 = long1 + 0.2;

            DateTime now = DateTime.Now;
            string ampm = now.Hour <= 12 ? "AM" : "PM";

            string zonesPinsUrl = string.Format(@"https://api.qonqr.com/v1/Zones/Pins/" + "{0}/{1}/{2}/{3}?nocache=" + "{4}%2F{5}%2F{6}%20{7}%3A{8}%3A{9}%20{10}",
                lat1, long1, lat2, long2, now.Month, now.Day, now.Year, now.Hour, now.Minute, now.Second, ampm);

            HttpWebRequest request = CreateBasicRequest(RequestType.GET, zonesPinsUrl);
            request.Headers = SetHeaders(latitude, longitude);
            string responseBody = SendRequestGetResponse(RequestType.GET, request);
            if (responseBody == string.Empty)
            {
                return null;
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Deserialize<ZonesPinsApiCall>(responseBody);
        }

        private string FindPropertyValueInBody(string body, string propertyName, int startIndex = 0)
        {
            int indexOfProperty = body.IndexOf("\"" + propertyName + "\":", startIndex);
            if (indexOfProperty == -1)
            {
                return string.Empty;
            }
            int start = indexOfProperty + propertyName.Length + 3;
            int end = body.IndexOf(',', start);
            return body.Substring(start, end - start);
        }

        private WebHeaderCollection SetHeaders(string latitude, string longitude)
        {
            WebHeaderCollection headers = new WebHeaderCollection();
            headers.Add(HttpRequestHeader.AcceptEncoding, "identity");
            headers.Add("ClientAppVersion", CLIENT_APP_VERSION);
            headers.Add("DeviceId", _deviceId);
            headers.Add("DeviceType", "3");
            headers.Add("Password", _password);
            headers.Add("Username", _username);

            //headers.Add("Password", "snuggles");
            //headers.Add("Username", "Luck");

            if (!string.IsNullOrEmpty(latitude))
            {
                headers.Add("Latitude", latitude);
            }
            if (!string.IsNullOrEmpty(longitude))
            {
                headers.Add("Longitude", longitude);
            }

            return headers;
        }

        private string SendRequestGetResponse(RequestType requestType, HttpWebRequest request)
        {
            return SendRequestGetResponse(requestType, request, string.Empty);
        }

        private string SendRequestGetResponse(RequestType requestType, HttpWebRequest request, string payload)
        {
            if (requestType == RequestType.POST)
            {
                Stream s = request.GetRequestStream();
                if (!string.IsNullOrEmpty(payload))
                {
                    StreamWriter sw = new StreamWriter(s);
                    sw.Write(payload);
                    sw.Dispose();
                }
                s.Dispose();
            }
            string responseBody = string.Empty;
            try
            {
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                Stream s = response.GetResponseStream();
                StreamReader sr = new StreamReader(s);
                responseBody = sr.ReadToEnd();
                sr.Dispose();
                s.Dispose();
            }
            catch (WebException e)
            {
                string status = e.Status.ToString();
                return string.Empty;
            }
            return responseBody;
        }
    }
}