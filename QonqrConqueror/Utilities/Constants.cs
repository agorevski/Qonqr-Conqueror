namespace Qonqr
{
    /// <summary>
    /// Application-wide constants to avoid magic strings and numbers
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Zone control state identifiers
        /// </summary>
        public static class ZoneControlStates
        {
            public const string Uncaptured = "*U*";
            public const string Legion = "*L*";
            public const string Swarm = "*S*";
            public const string Faceless = "*F*";

            public const int UncapturedValue = 0;
            public const int LegionValue = 1;
            public const int SwarmValue = 2;
            public const int FacelessValue = 3;
        }

        /// <summary>
        /// Attack formation IDs with their level requirements
        /// </summary>
        public static class AttackFormations
        {
            public const string ZoneAssault1 = "1011";
            public const string Shockwave1 = "1021";
            public const string Shockwave2 = "1022";
            public const string Shockwave3 = "1023";
            public const string Shockwave4 = "1024";

            public const int ZoneAssault1MinLevel = 0;
            public const int Shockwave1MinLevel = 3;
            public const int Shockwave2MinLevel = 13;
            public const int Shockwave3MinLevel = 29;
            public const int Shockwave4MinLevel = 42;
        }

        /// <summary>
        /// API-related constants
        /// </summary>
        public static class Api
        {
            public const string UserAgent = "QONQR/1.7.4642.40034 (WindowsPhone 7.10.-1; NOKIA Lumia 900; Unknown)";
            public const string ClientAppVersion = "1.7.4642.40034";
            public const string Referer = @"file:///Applications/Install/DFE7FCEF-4904-4C9A-8423-927A8D5DED18/Install/";
            public const string BaseUrl = "https://api.qonqr.com/v1";
            public const string DeviceType = "3";
        }

        /// <summary>
        /// Base capacity limits
        /// </summary>
        public static class Bases
        {
            public const int MaxBaseCount = 20;
            public const int FullGasCapacity = 100;
        }

        /// <summary>
        /// Default coordinates (Seattle area)
        /// </summary>
        public static class DefaultCoordinates
        {
            public const string Latitude = "47.6469383239746";
            public const string Longitude = "-122.133738517761";
        }
    }
}
