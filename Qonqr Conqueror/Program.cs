namespace Qonqr
{
    using System;
    using System.Collections.Generic;
    using System.Security.Permissions;
    using System.Web.Script.Serialization;
    using System.Windows.Forms;

    /// <summary>
    /// The main program
    /// </summary>
    public class Program
    {
        public static ProgressForm Form;
        public static ApiCall APICall;
        public static Timer Time;
        public static Timer HarvestTimer;
        public static double BotsAfterLaunch;
        public static double BotsPerSecond;
        public static double BotsLaunched;
        public static string Username;
        public static string Password;
        public static string DeviceId;
        public static List<Label> LabelList;
        public static int FactionId;
        public static bool AttackLoopEnabled;
        public static bool FullBaseExists;
        public static int totalCreditsHarvested = 0;
        public static List<Player> Players;


        /// <summary>
        /// The Main() Function
        /// </summary>
        /// <param name="args">Multiple arguments</param>
        /// <returns>the return value as an int</returns>
        [STAThread]
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
        public static int Main(string[] args)
        {
            RunTests();

            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(UncaughtHandler);

            APICall = new ApiCall();

            ConfigFile configFile = new ConfigFile();
            Players = configFile.GetAccounts();

#if DEBUG
            foreach (Player player in Players)
            {
                LoginApiCall login = APICall.Login(
                    player.Username, player.Password, player.DeviceId);


            }
#endif

            BotsAfterLaunch = 0;
            BotsPerSecond = 0;
            BotsLaunched = 0;

            Time = new Timer();
            Time.Interval = 1000;
            Time.Enabled = true;
            Time.Tick += Time_Tick;

            HarvestTimer = new Timer();
            HarvestTimer.Interval = 60000;
            HarvestTimer.Enabled = true;
            HarvestTimer.Tick += HarvestTimer_Tick;

            Form = new ProgressForm();

            LabelList = new List<Label>()
            {
                Form.label_var_fort1,
                Form.label_var_fort2,
                Form.label_var_fort3,
                Form.label_var_fort4,
                Form.label_var_fort5,
                Form.label_var_fort6,
                Form.label_var_fort7,
                Form.label_var_fort8,
                Form.label_var_fort9,
                Form.label_var_fort10,
                Form.label_var_fort11,
                Form.label_var_fort12,
                Form.label_var_fort13,
                Form.label_var_fort14,
                Form.label_var_fort15,
                Form.label_var_fort16,
                Form.label_var_fort17,
                Form.label_var_fort18,
                Form.label_var_fort19,
                Form.label_var_fort20,
            };
            
            Form.button_Login.Click += new EventHandler(LoginButtonClick);
            Form.textBox_PasswordField.Click += new EventHandler(PasswordFieldClick);
            Form.button_HarvestAll.Click += new EventHandler(HarvestAllButtonClick);
            Form.button_LaunchAttack.Click += new EventHandler(LaunchAttackButtonClick);
            Form.button_Scan.Click += new EventHandler(ScanButtonClick);
            Form.button_Set.Click += new EventHandler(SetButtonClick);
                        

            if (args.Length == 3)
            {
                Username = args[0];
                Password = args[1];
                DeviceId = args[2];

                Form.textBox_UsernameField.Text = args[0];
                Form.textBox_PasswordField.Text = args[1];

                Login(Username, Password, DeviceId);
            }
            Application.Run(Form);

            return 0;
        }

        /// <summary>
        /// The handler for Unhandled Exception Events
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="args">The arguments</param>
        private static void UncaughtHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            MessageBox.Show(e.ToString(), "Exception Caught!");
        }

        /// <summary>
        /// The event handler for clicking the ok button - generates the text log and then closes the form
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">event e</param>
        private static void LoginButtonClick(object sender, EventArgs e)
        {
            Login(Form.textBox_UsernameField.Text, Form.textBox_PasswordField.Text,
                "masdkAhsmGPs" + Form.textBox_UsernameField.Text + "=");
        }

        private static void Login(string username, string password, string deviceId)
        {
            Form.label_loginStatus.ForeColor = System.Drawing.Color.Orange;
            Form.label_loginStatus.Text = "Logging in...";
            Form.textBox_UsernameField.Enabled = false;
            Form.textBox_PasswordField.Enabled = false;
            Form.button_Login.Enabled = false;
            Form.Refresh();

            LoginApiCall loginData = APICall.Login(username, password, deviceId);
            PopulateFortData(APICall.Forts("47.6469383239746", "-122.133738517761").PlayerForts.Forts);

            if (null != loginData)
            {
                Form.label_loginStatus.ForeColor = System.Drawing.Color.Green;
                Form.label_loginStatus.Text = string.Format("Logged In!");
                Form.Refresh();

                Form.label_var_BotCapacity.Text = loginData.HUD.BotCapacity.ToString();
                Form.label_var_EnergyCapacity.Text = loginData.HUD.EnergyCapacity.ToString();
                Form.label_var_ExperienceCurrent.Text = loginData.HUD.Experience.ToString();
                Form.label_var_Level.Text = loginData.PlayerProfile.Level.ToString();
                Form.label_var_ExperienceToLevel.Text = (loginData.HUD.LevelXpUpperBound - loginData.HUD.Experience).ToString();
                Form.label_var_Zones.Text = loginData.PlayerProfile.TotalZonesCaptured.ToString() + "/" + loginData.PlayerProfile.ZonesCurrentlyLeading.ToString();
                Form.label_codename_qredits.Text = loginData.PlayerProfile.Codename + " / " + loginData.HUD.Qredits;

                FactionId = loginData.PlayerProfile.FactionId;
                Form.Refresh();
                Logger.LogLogin(Form.textBox_UsernameField.Text);

            }
            else
            {
                Form.label_loginStatus.ForeColor = System.Drawing.Color.Red;
                Form.label_loginStatus.Text = string.Format("Login Failed!");
                Form.textBox_UsernameField.Enabled = true;
                Form.textBox_PasswordField.Enabled = true;
                Form.button_Login.Enabled = true;
                Form.Refresh();
            }
        }

        private static void SetButtonClick(object sender, EventArgs e)
        {
            if (Form.comboBox_scanArea.Items.Count > 0)
            {
                Form.comboBox_attack.Items.Clear();
                for (int i = 0; i < Form.comboBox_scanArea.Items.Count; i++)
                {
                    Form.comboBox_attack.Items.Add(Form.comboBox_scanArea.Items[i]);
                }
                Form.Refresh();
            }
        }

        private static void ScanButtonClick(object sender, EventArgs e)
        {
            Form.button_Scan.Enabled = false;
            Form.Refresh();

            ZonesPinsApiCall zonesPins = APICall.ZonesPins(Form.textBox_var_Latitude.Text, Form.textBox_var_Longitude.Text);

            Form.button_Scan.Enabled = true;
            Form.Refresh();

            if (null != zonesPins)
            {
                UpdateScanAreaDropdown(zonesPins);
            }
        }

        private static string ZoneControlStateConverter(int zoneControlState)
        {
            switch (zoneControlState)
            {
                case 0:
                    return "*U*";
                case 1:
                    return "*L*";
                case 2:
                    return "*S*";
                case 3:
                    return "*F*";
                default:
                    break;
            };
            return string.Empty;
        }

        private static void UpdateScanAreaDropdown(ZonesPinsApiCall zonesPins)
        {
            Form.comboBox_scanArea.Items.Clear();

            foreach (Zone zone in zonesPins.Zones)
            {
                string controlState = ZoneControlStateConverter(zone.ControlState);
                Form.comboBox_scanArea.Items.Add(string.Format("{0} {1} [{2}] <{3}/{4}>", 
                    controlState, zone.ZoneName, zone.ZoneId, zone.Latitude, zone.Longitude));
            }
            Form.Refresh();
        }

#if DEBUG
        public static void RunTests()
        {
            string zonesPinsData = "{\"Count\":18,\"Zones\":[{\"ZoneId\":2299338,\"ZoneName\":\"Mint Grove\",\"RegionId\":3578,\"RegionName\":\"Washington\",\"RegionCode\":\"WA\",\"CountryId\":233,\"CountryName\":\"United States\",\"CountryCode\":\"US\",\"Latitude\":47.59843,\"Longitude\":-122.081787,\"ControlState\":2,\"CapturedByPlayerId\":5737,\"CapturedByCodename\":\"Danielrocks2000\",\"DateCapturedUTC\":\"2012-10-08 17:34:31Z\",\"LeaderPlayerId\":5737,\"LeaderCodename\":\"Danielrocks2000\",\"LeaderSinceDateUTC\":\"2012-10-08 17:34:31Z\",\"LegionCount\":0,\"SwarmCount\":1506,\"FacelessCount\":0},{\"ZoneId\":2299971,\"ZoneName\":\"Wilburton\",\"RegionId\":3578,\"RegionName\":\"Washington\",\"RegionCode\":\"WA\",\"CountryId\":233,\"CountryName\":\"United States\",\"CountryCode\":\"US\",\"Latitude\":47.60315,\"Longitude\":-122.180962,\"ControlState\":2,\"CapturedByPlayerId\":5736,\"CapturedByCodename\":\"PhilNi\",\"DateCapturedUTC\":\"2012-09-20 21:12:32Z\",\"LeaderPlayerId\":5736,\"LeaderCodename\":\"PhilNi\",\"LeaderSinceDateUTC\":\"2012-09-20 21:12:32Z\",\"LegionCount\":0,\"SwarmCount\":17818,\"FacelessCount\":0},{\"ZoneId\":2300125,\"ZoneName\":\"Belridge\",\"RegionId\":3578,\"RegionName\":\"Washington\",\"RegionCode\":\"WA\",\"CountryId\":233,\"CountryName\":\"United States\",\"CountryCode\":\"US\",\"Latitude\":47.61343,\"Longitude\":-122.167618,\"ControlState\":2,\"CapturedByPlayerId\":5737,\"CapturedByCodename\":\"Danielrocks2000\",\"DateCapturedUTC\":\"2012-10-08 19:49:44Z\",\"LeaderPlayerId\":5859,\"LeaderCodename\":\"Moirvae\",\"LeaderSinceDateUTC\":\"2012-10-08 19:50:11Z\",\"LegionCount\":0,\"SwarmCount\":13877,\"FacelessCount\":0},{\"ZoneId\":2300126,\"ZoneName\":\"Lake Hills\",\"RegionId\":3578,\"RegionName\":\"Washington\",\"RegionCode\":\"WA\",\"CountryId\":233,\"CountryName\":\"United States\",\"CountryCode\":\"US\",\"Latitude\":47.61343,\"Longitude\":-122.121513,\"ControlState\":2,\"CapturedByPlayerId\":5740,\"CapturedByCodename\":\"Pierrette\",\"DateCapturedUTC\":\"2012-09-20 16:57:57Z\",\"LeaderPlayerId\":5740,\"LeaderCodename\":\"Pierrette\",\"LeaderSinceDateUTC\":\"2012-09-20 16:57:57Z\",\"LegionCount\":0,\"SwarmCount\":80947,\"FacelessCount\":0},{\"ZoneId\":2300168,\"ZoneName\":\"Midlakes\",\"RegionId\":3578,\"RegionName\":\"Washington\",\"RegionCode\":\"WA\",\"CountryId\":233,\"CountryName\":\"United States\",\"CountryCode\":\"US\",\"Latitude\":47.61593,\"Longitude\":-122.182617,\"ControlState\":2,\"CapturedByPlayerId\":5737,\"CapturedByCodename\":\"Danielrocks2000\",\"DateCapturedUTC\":\"2012-10-08 19:17:39Z\",\"LeaderPlayerId\":5737,\"LeaderCodename\":\"Danielrocks2000\",\"LeaderSinceDateUTC\":\"2012-10-08 19:17:39Z\",\"LegionCount\":0,\"SwarmCount\":4946,\"FacelessCount\":0},{\"ZoneId\":2300801,\"ZoneName\":\"Crossroads\",\"RegionId\":3578,\"RegionName\":\"Washington\",\"RegionCode\":\"WA\",\"CountryId\":233,\"CountryName\":\"United States\",\"CountryCode\":\"US\",\"Latitude\":47.61954,\"Longitude\":-122.118729,\"ControlState\":2,\"CapturedByPlayerId\":5740,\"CapturedByCodename\":\"Pierrette\",\"DateCapturedUTC\":\"2012-09-22 01:13:45Z\",\"LeaderPlayerId\":5737,\"LeaderCodename\":\"Danielrocks2000\",\"LeaderSinceDateUTC\":\"2012-09-23 00:39:28Z\",\"LegionCount\":0,\"SwarmCount\":148288,\"FacelessCount\":0},{\"ZoneId\":2300826,\"ZoneName\":\"Highlands\",\"RegionId\":3578,\"RegionName\":\"Washington\",\"RegionCode\":\"WA\",\"CountryId\":233,\"CountryName\":\"United States\",\"CountryCode\":\"US\",\"Latitude\":47.62177,\"Longitude\":-122.149567,\"ControlState\":2,\"CapturedByPlayerId\":5736,\"CapturedByCodename\":\"PhilNi\",\"DateCapturedUTC\":\"2012-10-08 03:01:42Z\",\"LeaderPlayerId\":5740,\"LeaderCodename\":\"Pierrette\",\"LeaderSinceDateUTC\":\"2012-10-08 20:32:21Z\",\"LegionCount\":0,\"SwarmCount\":26462,\"FacelessCount\":0},{\"ZoneId\":2300929,\"ZoneName\":\"Ashwood\",\"RegionId\":3578,\"RegionName\":\"Washington\",\"RegionCode\":\"WA\",\"CountryId\":233,\"CountryName\":\"United States\",\"CountryCode\":\"US\",\"Latitude\":47.62954,\"Longitude\":-122.1954,\"ControlState\":2,\"CapturedByPlayerId\":5737,\"CapturedByCodename\":\"Danielrocks2000\",\"DateCapturedUTC\":\"2012-10-08 19:50:10Z\",\"LeaderPlayerId\":5737,\"LeaderCodename\":\"Danielrocks2000\",\"LeaderSinceDateUTC\":\"2012-10-08 19:50:10Z\",\"LegionCount\":0,\"SwarmCount\":1193,\"FacelessCount\":0},{\"ZoneId\":2300970,\"ZoneName\":\"Kenilworth\",\"RegionId\":3578,\"RegionName\":\"Washington\",\"RegionCode\":\"WA\",\"CountryId\":233,\"CountryName\":\"United States\",\"CountryCode\":\"US\",\"Latitude\":47.63232,\"Longitude\":-122.106789,\"ControlState\":2,\"CapturedByPlayerId\":5736,\"CapturedByCodename\":\"PhilNi\",\"DateCapturedUTC\":\"2012-10-01 08:48:04Z\",\"LeaderPlayerId\":5888,\"LeaderCodename\":\"uggles\",\"LeaderSinceDateUTC\":\"2012-10-01 16:19:44Z\",\"LegionCount\":251,\"SwarmCount\":160315,\"FacelessCount\":0},{\"ZoneId\":2300980,\"ZoneName\":\"Northrup\",\"RegionId\":3578,\"RegionName\":\"Washington\",\"RegionCode\":\"WA\",\"CountryId\":233,\"CountryName\":\"United States\",\"CountryCode\":\"US\",\"Latitude\":47.63288,\"Longitude\":-122.1854,\"ControlState\":2,\"CapturedByPlayerId\":5736,\"CapturedByCodename\":\"PhilNi\",\"DateCapturedUTC\":\"2012-10-08 19:49:33Z\",\"LeaderPlayerId\":5736,\"LeaderCodename\":\"PhilNi\",\"LeaderSinceDateUTC\":\"2012-10-08 19:49:34Z\",\"LegionCount\":0,\"SwarmCount\":6197,\"FacelessCount\":0},{\"ZoneId\":2301679,\"ZoneName\":\"Sammamish\",\"RegionId\":3578,\"RegionName\":\"Washington\",\"RegionCode\":\"WA\",\"CountryId\":233,\"CountryName\":\"United States\",\"CountryCode\":\"US\",\"Latitude\":47.64177,\"Longitude\":-122.0804,\"ControlState\":2,\"CapturedByPlayerId\":5736,\"CapturedByCodename\":\"PhilNi\",\"DateCapturedUTC\":\"2012-10-08 20:10:13Z\",\"LeaderPlayerId\":5736,\"LeaderCodename\":\"PhilNi\",\"LeaderSinceDateUTC\":\"2012-10-08 20:10:14Z\",\"LegionCount\":0,\"SwarmCount\":1157,\"FacelessCount\":0},{\"ZoneId\":2301705,\"ZoneName\":\"Overlake\",\"RegionId\":3578,\"RegionName\":\"Washington\",\"RegionCode\":\"WA\",\"CountryId\":233,\"CountryName\":\"United States\",\"CountryCode\":\"US\",\"Latitude\":47.64343,\"Longitude\":-122.143181,\"ControlState\":2,\"CapturedByPlayerId\":5859,\"CapturedByCodename\":\"Moirvae\",\"DateCapturedUTC\":\"2012-10-08 16:31:56Z\",\"LeaderPlayerId\":5736,\"LeaderCodename\":\"PhilNi\",\"LeaderSinceDateUTC\":\"2012-10-08 16:55:40Z\",\"LegionCount\":0,\"SwarmCount\":85573,\"FacelessCount\":0},{\"ZoneId\":2301795,\"ZoneName\":\"Adelaide\",\"RegionId\":3578,\"RegionName\":\"Washington\",\"RegionCode\":\"WA\",\"CountryId\":233,\"CountryName\":\"United States\",\"CountryCode\":\"US\",\"Latitude\":47.64982,\"Longitude\":-122.08873,\"ControlState\":2,\"CapturedByPlayerId\":1647,\"CapturedByCodename\":\"Oknos\",\"DateCapturedUTC\":\"2012-10-08 11:37:49Z\",\"LeaderPlayerId\":1647,\"LeaderCodename\":\"Oknos\",\"LeaderSinceDateUTC\":\"2012-10-08 11:37:49Z\",\"LegionCount\":0,\"SwarmCount\":1226,\"FacelessCount\":0},{\"ZoneId\":2302551,\"ZoneName\":\"Campton\",\"RegionId\":3578,\"RegionName\":\"Washington\",\"RegionCode\":\"WA\",\"CountryId\":233,\"CountryName\":\"United States\",\"CountryCode\":\"US\",\"Latitude\":47.66232,\"Longitude\":-122.100121,\"ControlState\":2,\"CapturedByPlayerId\":1647,\"CapturedByCodename\":\"Oknos\",\"DateCapturedUTC\":\"2012-10-08 11:36:19Z\",\"LeaderPlayerId\":1647,\"LeaderCodename\":\"Oknos\",\"LeaderSinceDateUTC\":\"2012-10-08 11:36:19Z\",\"LegionCount\":0,\"SwarmCount\":1209,\"FacelessCount\":0},{\"ZoneId\":2302607,\"ZoneName\":\"Snyders Corner\",\"RegionId\":3578,\"RegionName\":\"Washington\",\"RegionCode\":\"WA\",\"CountryId\":233,\"CountryName\":\"United States\",\"CountryCode\":\"US\",\"Latitude\":47.66649,\"Longitude\":-122.16346,\"ControlState\":2,\"CapturedByPlayerId\":5737,\"CapturedByCodename\":\"Danielrocks2000\",\"DateCapturedUTC\":\"2012-09-23 17:36:52Z\",\"LeaderPlayerId\":5737,\"LeaderCodename\":\"Danielrocks2000\",\"LeaderSinceDateUTC\":\"2012-09-23 17:36:52Z\",\"LegionCount\":0,\"SwarmCount\":32566,\"FacelessCount\":0},{\"ZoneId\":2303258,\"ZoneName\":\"Redmond\",\"RegionId\":3578,\"RegionName\":\"Washington\",\"RegionCode\":\"WA\",\"CountryId\":233,\"CountryName\":\"United States\",\"CountryCode\":\"US\",\"Latitude\":47.67399,\"Longitude\":-122.121513,\"ControlState\":2,\"CapturedByPlayerId\":5740,\"CapturedByCodename\":\"Pierrette\",\"DateCapturedUTC\":\"2012-10-07 14:23:01Z\",\"LeaderPlayerId\":5740,\"LeaderCodename\":\"Pierrette\",\"LeaderSinceDateUTC\":\"2012-10-07 16:57:35Z\",\"LegionCount\":0,\"SwarmCount\":461143,\"FacelessCount\":0},{\"ZoneId\":2303291,\"ZoneName\":\"Rose Hill\",\"RegionId\":3578,\"RegionName\":\"Washington\",\"RegionCode\":\"WA\",\"CountryId\":233,\"CountryName\":\"United States\",\"CountryCode\":\"US\",\"Latitude\":47.67593,\"Longitude\":-122.169006,\"ControlState\":2,\"CapturedByPlayerId\":5737,\"CapturedByCodename\":\"Danielrocks2000\",\"DateCapturedUTC\":\"2012-09-21 13:57:49Z\",\"LeaderPlayerId\":5736,\"LeaderCodename\":\"PhilNi\",\"LeaderSinceDateUTC\":\"2012-09-23 21:26:02Z\",\"LegionCount\":0,\"SwarmCount\":21750,\"FacelessCount\":0},{\"ZoneId\":2304131,\"ZoneName\":\"Earlmont\",\"RegionId\":3578,\"RegionName\":\"Washington\",\"RegionCode\":\"WA\",\"CountryId\":233,\"CountryName\":\"United States\",\"CountryCode\":\"US\",\"Latitude\":47.69343,\"Longitude\":-122.1504,\"ControlState\":2,\"CapturedByPlayerId\":5859,\"CapturedByCodename\":\"Moirvae\",\"DateCapturedUTC\":\"2012-10-05 12:19:03Z\",\"LeaderPlayerId\":5859,\"LeaderCodename\":\"Moirvae\",\"LeaderSinceDateUTC\":\"2012-10-05 12:19:03Z\",\"LegionCount\":0,\"SwarmCount\":11190,\"FacelessCount\":0}]}";
            JavaScriptSerializer js = new JavaScriptSerializer();
            ZonesPinsApiCall zones = js.Deserialize<ZonesPinsApiCall>(zonesPinsData);

            string fortsData = "{\"PlayerForts\":{\"Forts\":[{\"FortId\":16262,\"ZoneId\":1060725,\"ZoneControlState\":1,\"ZoneName\":\"\u0027Ualapu\u0027e\",\"RegionName\":\"Hawaii\",\"CountryName\":\"United States\",\"ZoneOperatingAtPercent\":\"100%\",\"Latitude\":21.06583,\"Longitude\":-156.8325,\"LastHarvestDateUTC\":\"2012-10-08 19:57:02Z\",\"LastFiredDateUTC\":\"2012-10-08 23:01:24Z\",\"CreateDateUTC\":\"2012-10-05 18:11:34Z\",\"CurrentGasInTank\":100,\"TankCapacity\":0},{\"FortId\":16263,\"ZoneId\":1061081,\"ZoneControlState\":1,\"ZoneName\":\"Kawela\",\"RegionName\":\"Hawaii\",\"CountryName\":\"United States\",\"ZoneOperatingAtPercent\":\"100%\",\"Latitude\":21.06889,\"Longitude\":-156.952774,\"LastHarvestDateUTC\":\"2012-10-08 19:57:02Z\",\"LastFiredDateUTC\":\"2012-10-08 23:01:24Z\",\"CreateDateUTC\":\"2012-10-05 18:11:56Z\",\"CurrentGasInTank\":100,\"TankCapacity\":0},{\"FortId\":16264,\"ZoneId\":1061093,\"ZoneControlState\":1,\"ZoneName\":\"Kalua\u0027aha\",\"RegionName\":\"Hawaii\",\"CountryName\":\"United States\",\"ZoneOperatingAtPercent\":\"100%\",\"Latitude\":21.07,\"Longitude\":-156.8211,\"LastHarvestDateUTC\":\"2012-10-08 19:57:02Z\",\"LastFiredDateUTC\":\"2012-10-08 23:01:24Z\",\"CreateDateUTC\":\"2012-10-05 18:12:17Z\",\"CurrentGasInTank\":100,\"TankCapacity\":0},{\"FortId\":16266,\"ZoneId\":1061150,\"ZoneControlState\":1,\"ZoneName\":\"Puko\u0027o\",\"RegionName\":\"Hawaii\",\"CountryName\":\"United States\",\"ZoneOperatingAtPercent\":\"100%\",\"Latitude\":21.07667,\"Longitude\":-156.795837,\"LastHarvestDateUTC\":\"2012-10-08 19:57:02Z\",\"LastFiredDateUTC\":\"2012-10-08 23:01:24Z\",\"CreateDateUTC\":\"2012-10-05 18:13:05Z\",\"CurrentGasInTank\":100,\"TankCapacity\":0},{\"FortId\":16267,\"ZoneId\":1061519,\"ZoneControlState\":1,\"ZoneName\":\"Kamiloloa\",\"RegionName\":\"Hawaii\",\"CountryName\":\"United States\",\"ZoneOperatingAtPercent\":\"100%\",\"Latitude\":21.08389,\"Longitude\":-157.002213,\"LastHarvestDateUTC\":\"2012-10-08 19:57:02Z\",\"LastFiredDateUTC\":\"2012-10-08 23:01:24Z\",\"CreateDateUTC\":\"2012-10-05 18:13:15Z\",\"CurrentGasInTank\":100,\"TankCapacity\":0},{\"FortId\":16268,\"ZoneId\":1061542,\"ZoneControlState\":1,\"ZoneName\":\"Kapa\u0027akea Colony\",\"RegionName\":\"Hawaii\",\"CountryName\":\"United States\",\"ZoneOperatingAtPercent\":\"100%\",\"Latitude\":21.08667,\"Longitude\":-157.013611,\"LastHarvestDateUTC\":\"2012-10-08 19:57:02Z\",\"LastFiredDateUTC\":\"2012-10-08 23:01:24Z\",\"CreateDateUTC\":\"2012-10-05 18:13:27Z\",\"CurrentGasInTank\":100,\"TankCapacity\":0},{\"FortId\":16269,\"ZoneId\":1061551,\"ZoneControlState\":1,\"ZoneName\":\"Koheo\",\"RegionName\":\"Hawaii\",\"CountryName\":\"United States\",\"ZoneOperatingAtPercent\":\"100%\",\"Latitude\":21.08722,\"Longitude\":-157.016113,\"LastHarvestDateUTC\":\"2012-10-08 19:57:02Z\",\"LastFiredDateUTC\":\"2012-10-08 23:01:24Z\",\"CreateDateUTC\":\"2012-10-05 18:13:38Z\",\"CurrentGasInTank\":100,\"TankCapacity\":0},{\"FortId\":16270,\"ZoneId\":1061566,\"ZoneControlState\":1,\"ZoneName\":\"Kaunakakai\",\"RegionName\":\"Hawaii\",\"CountryName\":\"United States\",\"ZoneOperatingAtPercent\":\"100%\",\"Latitude\":21.08923,\"Longitude\":-157.013718,\"LastHarvestDateUTC\":\"2012-10-08 19:57:02Z\",\"LastFiredDateUTC\":\"2012-10-08 23:01:24Z\",\"CreateDateUTC\":\"2012-10-05 18:13:53Z\",\"CurrentGasInTank\":100,\"TankCapacity\":0},{\"FortId\":16271,\"ZoneId\":1061580,\"ZoneControlState\":1,\"ZoneName\":\"Pauwalu\",\"RegionName\":\"Hawaii\",\"CountryName\":\"United States\",\"ZoneOperatingAtPercent\":\"100%\",\"Latitude\":21.09139,\"Longitude\":-156.776672,\"LastHarvestDateUTC\":\"2012-10-08 19:57:02Z\",\"LastFiredDateUTC\":\"2012-10-08 23:01:24Z\",\"CreateDateUTC\":\"2012-10-05 18:15:06Z\",\"CurrentGasInTank\":100,\"TankCapacity\":0},{\"FortId\":16272,\"ZoneId\":1061592,\"ZoneControlState\":1,\"ZoneName\":\"Halena\",\"RegionName\":\"Hawaii\",\"CountryName\":\"United States\",\"ZoneOperatingAtPercent\":\"100%\",\"Latitude\":21.0925,\"Longitude\":-157.234436,\"LastHarvestDateUTC\":\"2012-10-08 19:57:02Z\",\"LastFiredDateUTC\":\"2012-10-08 23:01:24Z\",\"CreateDateUTC\":\"2012-10-05 18:15:15Z\",\"CurrentGasInTank\":100,\"TankCapacity\":0},{\"FortId\":16273,\"ZoneId\":1061599,\"ZoneControlState\":1,\"ZoneName\":\"Kaunakakai\",\"RegionName\":\"Hawaii\",\"CountryName\":\"United States\",\"ZoneOperatingAtPercent\":\"100%\",\"Latitude\":21.09333,\"Longitude\":-157.0239,\"LastHarvestDateUTC\":\"2012-10-08 19:57:02Z\",\"LastFiredDateUTC\":\"2012-10-08 23:01:24Z\",\"CreateDateUTC\":\"2012-10-05 18:15:24Z\",\"CurrentGasInTank\":100,\"TankCapacity\":0},{\"FortId\":16274,\"ZoneId\":1061620,\"ZoneControlState\":1,\"ZoneName\":\"Ranch Camp\",\"RegionName\":\"Hawaii\",\"CountryName\":\"United States\",\"ZoneOperatingAtPercent\":\"100%\",\"Latitude\":21.09694,\"Longitude\":-157.017776,\"LastHarvestDateUTC\":\"2012-10-08 19:57:02Z\",\"LastFiredDateUTC\":\"2012-10-08 23:01:24Z\",\"CreateDateUTC\":\"2012-10-05 18:15:34Z\",\"CurrentGasInTank\":100,\"TankCapacity\":0},{\"FortId\":16275,\"ZoneId\":1061626,\"ZoneControlState\":1,\"ZoneName\":\"Manila Camp\",\"RegionName\":\"Hawaii\",\"CountryName\":\"United States\",\"ZoneOperatingAtPercent\":\"100%\",\"Latitude\":21.09806,\"Longitude\":-157.026382,\"LastHarvestDateUTC\":\"2012-10-08 19:57:02Z\",\"LastFiredDateUTC\":\"2012-10-08 23:01:24Z\",\"CreateDateUTC\":\"2012-10-05 18:15:46Z\",\"CurrentGasInTank\":100,\"TankCapacity\":0},{\"FortId\":16276,\"ZoneId\":1062007,\"ZoneControlState\":1,\"ZoneName\":\"Waialua\",\"RegionName\":\"Hawaii\",\"CountryName\":\"United States\",\"ZoneOperatingAtPercent\":\"100%\",\"Latitude\":21.10028,\"Longitude\":-156.761383,\"LastHarvestDateUTC\":\"2012-10-08 19:57:02Z\",\"LastFiredDateUTC\":\"2012-10-08 23:01:24Z\",\"CreateDateUTC\":\"2012-10-05 18:16:48Z\",\"CurrentGasInTank\":100,\"TankCapacity\":0},{\"FortId\":16277,\"ZoneId\":1062091,\"ZoneControlState\":1,\"ZoneName\":\"\u0027Umipa\u0027a\",\"RegionName\":\"Hawaii\",\"CountryName\":\"United States\",\"ZoneOperatingAtPercent\":\"100%\",\"Latitude\":21.10972,\"Longitude\":-157.055557,\"LastHarvestDateUTC\":\"2012-10-08 19:57:02Z\",\"LastFiredDateUTC\":\"2012-10-08 23:01:24Z\",\"CreateDateUTC\":\"2012-10-05 18:16:58Z\",\"CurrentGasInTank\":100,\"TankCapacity\":0},{\"FortId\":16886,\"ZoneId\":2301705,\"ZoneControlState\":2,\"ZoneName\":\"Overlake\",\"RegionName\":\"Washington\",\"CountryName\":\"United States\",\"ZoneOperatingAtPercent\":\"50%\",\"Latitude\":47.6520576,\"Longitude\":-122.133911,\"LastHarvestDateUTC\":\"2012-10-08 19:57:18Z\",\"LastFiredDateUTC\":\"2012-10-08 23:21:55Z\",\"CreateDateUTC\":\"2012-10-08 19:57:18Z\",\"CurrentGasInTank\":50,\"TankCapacity\":0}],\"TotalFortsEstablished\":16,\"TotalFortsUnused\":0},\"ResponseStatuses\":[],\"Achievements\":[]}";
            FortsApiCall pfs = js.Deserialize<FortsApiCall>(fortsData);

            string harvestData = "{\"HUD\":{\"PlayerId\":5776,\"Codename\":\"Luck\",\"FactionId\":1,\"Qredits\":36770,\"Cubes\":0,\"Experience\":97649,\"LevelXpLowerBound\":94550,\"LevelXpUpperBound\":97650,\"Level\":62,\"RankId\":1,\"BotCapacity\":4800,\"BotsPerSecond\":14.15,\"BotCountAfterLastDeployment\":1471,\"LastBotDeployTimeUTC\":\"2012-09-30 23:48:36Z\",\"EnergyCapacity\":100,\"EnergyPerSecond\":1.6,\"EnergyCountAfterLastDeployment\":74,\"LastEnergyDeployTimeUTC\":\"2012-09-30 23:48:36Z\",\"ServerTimeUTC\":\"2012-10-02 17:21:41Z\"},\"HarvestedResourceses\":[],\"PlayerForts\":{\"Forts\":[{\"FortId\":13623,\"ZoneId\":2304131,\"ZoneControlState\":2,\"ZoneName\":\"Earlmont\",\"RegionName\":\"Washington\",\"CountryName\":\"United States\",\"ZoneOperatingAtPercent\":\"50%\",\"Latitude\":47.68361,\"Longitude\":-122.142044,\"LastHarvestDateUTC\":\"2012-10-02 17:21:41Z\",\"LastFiredDateUTC\":\"2012-10-01 06:22:50Z\",\"CreateDateUTC\":\"2012-09-18 01:28:58Z\",\"CurrentGasInTank\":0,\"TankCapacity\":0},{\"FortId\":13641,\"ZoneId\":2300845,\"ZoneControlState\":1,\"ZoneName\":\"Capitol Hill\",\"RegionName\":\"Washington\",\"CountryName\":\"United States\",\"ZoneOperatingAtPercent\":\"100%\",\"Latitude\":47.6213951,\"Longitude\":-122.323013,\"LastHarvestDateUTC\":\"2012-10-02 17:21:41Z\",\"LastFiredDateUTC\":\"2012-10-01 02:56:18Z\",\"CreateDateUTC\":\"2012-09-18 04:19:17Z\",\"CurrentGasInTank\":0,\"TankCapacity\":0},{\"FortId\":13652,\"ZoneId\":2300170,\"ZoneControlState\":3,\"ZoneName\":\"Denny Regrade\",\"RegionName\":\"Washington\",\"CountryName\":\"United States\",\"ZoneOperatingAtPercent\":\"50%\",\"Latitude\":47.6165,\"Longitude\":-122.328484,\"LastHarvestDateUTC\":\"2012-10-02 17:21:41Z\",\"LastFiredDateUTC\":\"2012-10-01 06:22:50Z\",\"CreateDateUTC\":\"2012-09-18 07:22:18Z\",\"CurrentGasInTank\":0,\"TankCapacity\":0},{\"FortId\":13764,\"ZoneId\":2300826,\"ZoneControlState\":2,\"ZoneName\":\"Highlands\",\"RegionName\":\"Washington\",\"CountryName\":\"United States\",\"ZoneOperatingAtPercent\":\"50%\",\"Latitude\":47.6315,\"Longitude\":-122.144814,\"LastHarvestDateUTC\":\"2012-10-02 17:21:41Z\",\"LastFiredDateUTC\":\"2012-10-01 06:22:50Z\",\"CreateDateUTC\":\"2012-09-18 23:42:09Z\",\"CurrentGasInTank\":0,\"TankCapacity\":0},{\"FortId\":13772,\"ZoneId\":2300027,\"ZoneControlState\":3,\"ZoneName\":\"Seattle\",\"RegionName\":\"Washington\",\"CountryName\":\"United States\",\"ZoneOperatingAtPercent\":\"50%\",\"Latitude\":47.6053467,\"Longitude\":-122.32901,\"LastHarvestDateUTC\":\"2012-10-02 17:21:41Z\",\"LastFiredDateUTC\":\"2012-10-01 06:22:50Z\",\"CreateDateUTC\":\"2012-09-19 01:56:34Z\",\"CurrentGasInTank\":0,\"TankCapacity\":0},{\"FortId\":13793,\"ZoneId\":2302607,\"ZoneControlState\":2,\"ZoneName\":\"Snyders Corner\",\"RegionName\":\"Washington\",\"CountryName\":\"United States\",\"ZoneOperatingAtPercent\":\"50%\",\"Latitude\":47.66645,\"Longitude\":-122.165161,\"LastHarvestDateUTC\":\"2012-10-02 17:21:41Z\",\"LastFiredDateUTC\":\"2012-10-01 06:22:50Z\",\"CreateDateUTC\":\"2012-09-19 06:19:28Z\",\"CurrentGasInTank\":0,\"TankCapacity\":0},{\"FortId\":13939,\"ZoneId\":2301705,\"ZoneControlState\":2,\"ZoneName\":\"Overlake\",\"RegionName\":\"Washington\",\"CountryName\":\"United States\",\"ZoneOperatingAtPercent\":\"50%\",\"Latitude\":47.6520576,\"Longitude\":-122.133911,\"LastHarvestDateUTC\":\"2012-10-02 17:21:41Z\",\"LastFiredDateUTC\":\"2012-10-01 06:22:50Z\",\"CreateDateUTC\":\"2012-09-20 19:55:10Z\",\"CurrentGasInTank\":0,\"TankCapacity\":0},{\"FortId\":14190,\"ZoneId\":2298419,\"ZoneControlState\":2,\"ZoneName\":\"Newport\",\"RegionName\":\"Washington\",\"CountryName\":\"United States\",\"ZoneOperatingAtPercent\":\"50%\",\"Latitude\":47.5800629,\"Longitude\":-122.183594,\"LastHarvestDateUTC\":\"2012-10-02 17:21:41Z\",\"LastFiredDateUTC\":\"2012-10-01 06:22:50Z\",\"CreateDateUTC\":\"2012-09-22 20:15:24Z\",\"CurrentGasInTank\":0,\"TankCapacity\":0},{\"FortId\":14191,\"ZoneId\":2299129,\"ZoneControlState\":2,\"ZoneName\":\"Beaux Arts Village\",\"RegionName\":\"Washington\",\"CountryName\":\"United States\",\"ZoneOperatingAtPercent\":\"50%\",\"Latitude\":47.57949,\"Longitude\":-122.188873,\"LastHarvestDateUTC\":\"2012-10-02 17:21:41Z\",\"LastFiredDateUTC\":\"2012-10-01 04:38:35Z\",\"CreateDateUTC\":\"2012-09-22 20:15:36Z\",\"CurrentGasInTank\":0,\"TankCapacity\":0},{\"FortId\":14325,\"ZoneId\":2282162,\"ZoneControlState\":3,\"ZoneName\":\"Benroy\",\"RegionName\":\"Washington\",\"CountryName\":\"United States\",\"ZoneOperatingAtPercent\":\"50%\",\"Latitude\":47.2264977,\"Longitude\":-122.24749,\"LastHarvestDateUTC\":\"2012-10-02 17:21:41Z\",\"LastFiredDateUTC\":\"2012-10-01 06:22:50Z\",\"CreateDateUTC\":\"2012-09-23 18:35:26Z\",\"CurrentGasInTank\":0,\"TankCapacity\":0},{\"FortId\":14523,\"ZoneId\":2303258,\"ZoneControlState\":3,\"ZoneName\":\"Redmond\",\"RegionName\":\"Washington\",\"CountryName\":\"United States\",\"ZoneOperatingAtPercent\":\"50%\",\"Latitude\":47.6751556,\"Longitude\":-122.143112,\"LastHarvestDateUTC\":\"2012-10-02 17:21:41Z\",\"LastFiredDateUTC\":\"2012-10-01 06:22:50Z\",\"CreateDateUTC\":\"2012-09-25 02:56:14Z\",\"CurrentGasInTank\":0,\"TankCapacity\":0},{\"FortId\":14739,\"ZoneId\":2300980,\"ZoneControlState\":2,\"ZoneName\":\"Northrup\",\"RegionName\":\"Washington\",\"CountryName\":\"United States\",\"ZoneOperatingAtPercent\":\"50%\",\"Latitude\":47.6291,\"Longitude\":-122.188721,\"LastHarvestDateUTC\":\"2012-10-02 17:21:41Z\",\"LastFiredDateUTC\":\"2012-10-01 05:40:58Z\",\"CreateDateUTC\":\"2012-09-27 02:42:45Z\",\"CurrentGasInTank\":0,\"TankCapacity\":0}],\"TotalFortsEstablished\":12,\"TotalFortsUnused\":1},\"TotalResourceAmount\":0,\"QreditsEarned\":1,\"ResponseStatuses\":[{\"Code\":504,\"ResultType\":1,\"Message\":\"Qredits earned: 0.\"}],\"Achievements\":[]}";
            HarvestAll ha = js.Deserialize<HarvestAll>(harvestData);

            string loginData = "{\"User\":\"Luck\",\"HUD\":{\"PlayerId\":5776,\"Codename\":\"Luck\",\"FactionId\":1,\"Qredits\":36770,\"Cubes\":0,\"Experience\":97649,\"LevelXpLowerBound\":94550,\"LevelXpUpperBound\":97650,\"Level\":62,\"RankId\":1,\"BotCapacity\":4800,\"BotsPerSecond\":14.15,\"BotCountAfterLastDeployment\":1471,\"LastBotDeployTimeUTC\":\"2012-09-30 23:48:36Z\",\"EnergyCapacity\":100,\"EnergyPerSecond\":1.6,\"EnergyCountAfterLastDeployment\":74,\"LastEnergyDeployTimeUTC\":\"2012-09-30 23:48:36Z\",\"ServerTimeUTC\":\"2012-10-02 17:21:27Z\"},\"Zone\":null,\"Content\":{\"MedalsContentResponses\":[],\"FormationsContentResponses\":[],\"RanksContentResponses\":[],\"ConsumableItemsContentResponses\":[],\"ScopeUpgradesContentResponses\":[],\"ExchangeRateContentResponses\":null,\"Skus\":[],\"MedalsVersionNumber\":\"1.1\",\"MedalsRefreshDateUTC\":\"2012-02-19 00:00:00Z\",\"FormationsVersionNumber\":\"2.0\",\"FormationsRefreshDateUTC\":\"2012-05-24 23:00:00Z\",\"RanksVersionNumber\":\"3.5\",\"RanksRefreshDateUTC\":\"2012-02-19 00:00:00Z\",\"ConsumableItemsVersionNumber\":\"4.8\",\"ConsumableItemsRefreshDateUTC\":\"2012-04-29 21:00:00Z\",\"ScopeUpgradesVersionNumber\":\"1.2\",\"ScopeUpgradesRefreshDateUTC\":\"2012-06-22 23:00:00Z\",\"ExchangeRateRefreshDateUTC\":\"2012-02-19 00:00:00Z\",\"ActiveSkusRefreshDateUTC\":\"2012-02-19 00:00:00Z\"},\"PlayerAccount\":{\"PlayerId\":5776,\"CreatedOnUTC\":\"2012-09-17 21:50:17Z\",\"Email\":\"alex.gorevski@live.com\",\"HomeZoneId\":0,\"BirthDate\":\"1985-06-27 00:00:00Z\",\"Gender\":\"M\",\"FacebookUserId\":0,\"FacebookAuthorizationCode\":\"\",\"IsAdmin\":false},\"PlayerProfile\":{\"PlayerId\":5776,\"Codename\":\"Luck\",\"ImageUrl\":\"https://qonqr.blob.core.windows.net/profilepictures/00ee2197-38ac-4afd-b696-3213d3da7394.jpg\",\"FactionId\":1,\"Level\":62,\"RankId\":1,\"TotalZonesCaptured\":103,\"ZonesCurrentlyLeading\":0,\"AllowPublicMessages\":true,\"ResponseStatuses\":[],\"Achievements\":[]},\"PlayerInventory\":{\"Entries\":[{\"ConsumableItemId\":10,\"Quantity\":0},{\"ConsumableItemId\":20,\"Quantity\":3},{\"ConsumableItemId\":103,\"Quantity\":0},{\"ConsumableItemId\":106,\"Quantity\":0},{\"ConsumableItemId\":202,\"Quantity\":0},{\"ConsumableItemId\":203,\"Quantity\":0},{\"ConsumableItemId\":205,\"Quantity\":0},{\"ConsumableItemId\":301,\"Quantity\":0},{\"ConsumableItemId\":302,\"Quantity\":0}]},\"PlayerUpgrades\":{\"RangeBonusPercentage\":0,\"MissileRangeBonusPercentage\":0,\"Entries\":[{\"UpgradeFamilyId\":10,\"UpgradeId\":103},{\"UpgradeFamilyId\":30,\"UpgradeId\":0},{\"UpgradeFamilyId\":100,\"UpgradeId\":1003},{\"UpgradeFamilyId\":110,\"UpgradeId\":1101},{\"UpgradeFamilyId\":120,\"UpgradeId\":1201},{\"UpgradeFamilyId\":210,\"UpgradeId\":0},{\"UpgradeFamilyId\":300,\"UpgradeId\":0},{\"UpgradeFamilyId\":310,\"UpgradeId\":0},{\"UpgradeFamilyId\":500,\"UpgradeId\":5006},{\"UpgradeFamilyId\":510,\"UpgradeId\":5101}]},\"ResponseStatuses\":[],\"Achievements\":[]}";
            LoginApiCall login = js.Deserialize<LoginApiCall>(loginData);
        }
#endif

        private static void LaunchAttackButtonClick(object sender, EventArgs e)
        {
            LaunchAttack();
        }

        private static void LaunchAttack()
        {
            Form.button_LaunchAttack.Enabled = false;
            Form.Refresh();

            int selectedIndex = Form.comboBox_attack.SelectedIndex;
            string attackDone = Form.comboBox_attack.Items[selectedIndex].ToString();
            int start = attackDone.IndexOf("[") + 1;
            int end = attackDone.IndexOf("]");
            string zoneId = attackDone.Substring(start, end - start);

            int startLatLong = attackDone.IndexOf("<") + 1;
            int endLatLong = attackDone.IndexOf(">");
            string latLong = attackDone.Substring(startLatLong, endLatLong - startLatLong);
            string[] latLongArray = latLong.Split('/');
            string latitude = latLongArray[0];
            string longitude = latLongArray[1];

            int level = 0;
            int.TryParse(Form.label_var_Level.Text, out level);

            string attackFormation = "0";
            if (level >= 42)
            {
                attackFormation = "1024"; // Shockwave 4
            }
            else if (level >= 29)
            {
                attackFormation = "1023"; // Shockwave 3
            }
            else if (level >= 13)
            {
                attackFormation = "1022"; // Shockwave 2
            }
            else if (level >= 3)
            {
                attackFormation = "1021"; // Shockwave 1
            }
            else
            {
                attackFormation = "1011"; // Zone Assault 1
            }

            LaunchApiCall launchData = APICall.Launch(latitude, longitude, zoneId, attackFormation);
            BotsAfterLaunch = launchData.HUD.BotCountAfterLastDeployment;
            BotsPerSecond = launchData.HUD.BotsPerSecond;
            BotsLaunched = launchData.Summary.Breakdown.BotsLaunched;
            AttackLoopEnabled = true;

            //if (launchData.Summary.Rewards.PlayerCapturedZone)
            //{
            //    AttackLoopEnabled = false;
            //    MessageBox.Show("Player Captured Zone! Attack another base now...");
            //}

            Form.button_LaunchAttack.Enabled = true;
            Form.Refresh();

            Logger.LogAttack(Form.textBox_UsernameField.Text, launchData);
        }

        private static void Time_Tick(object sender, EventArgs e)
        {
            if (AttackLoopEnabled)
            {
                BotsAfterLaunch += BotsPerSecond;
                Form.label_var_botsRegenRate.Text = string.Format("[ {0} ] / [ {1} ]", BotsAfterLaunch, BotsPerSecond);
                Form.Refresh();
                if (BotsAfterLaunch != 0)
                {
                    double progress = (double)BotsAfterLaunch / (double)BotsLaunched * 100;
                    if (progress >= 100)
                    {
                        progress = 100;
                    }
                    UpdateProgressBar((int)progress);

                    if (progress == 100)
                    {
                        BotsPerSecond = 0;  // Pause the addition for a moment.
                        // The LaunchAttack call will reset it anyway
                        LaunchAttack();
                    }
                }
            }
        }

         private static void HarvestTimer_Tick(object sender, EventArgs e)
        {
            // update fort data
            PopulateFortData(APICall.Forts("47.6469383239746", "-122.133738517761").PlayerForts.Forts);
             
            // if a base is full then harvest all
            if (Form.autoHarvestCheckbox.Checked && FullBaseExists)
                HarvestAllButtonClick(null, new EventArgs());
        }

        private static void UpdateProgressBar(int progress)
        {
            Form.progressBar_nanobots.Value = progress;
        }

        public static void HarvestAllButtonClick(object sender, EventArgs e)
        {
            Form.button_HarvestAll.Enabled = false;
            Form.Refresh();

            HarvestAll harvestAll = APICall.HarvestAll(Form.textBox_var_Latitude.Text, Form.textBox_var_Longitude.Text);

            if (null != harvestAll)
            {
                totalCreditsHarvested += harvestAll.QreditsEarned;
                PopulateLatestBaseData(System.Drawing.Color.Green, string.Format("Harvested: {0}", totalCreditsHarvested));
                PopulateFortData(harvestAll.PlayerForts.Forts);

                FullBaseExists = false;
            }
            else
            {
                PopulateLatestBaseData(System.Drawing.Color.Red, "Nothing to Harvest");
            }
            Form.button_HarvestAll.Enabled = true;
            Form.Refresh();

            Logger.LogHarvest(Form.textBox_UsernameField.Text, harvestAll.QreditsEarned.ToString());
        }

        private static void PopulateLatestBaseData(System.Drawing.Color color, string harvestString)
        {
            Form.label_var_qreditsEarnedFromHarvest.ForeColor = color;
            Form.label_var_qreditsEarnedFromHarvest.Text = harvestString;
            Form.Refresh();
        }

        public static void PopulateFortData(List<Forts> fortsList)
        {
            Form.comboBox_attack.Items.Clear();

            for (int i = 0; i < fortsList.Count; i++)
            {
                Forts fort = fortsList[i];

                Label label = LabelList[i];
                //switch (i)
                //{


                //    case 1:
                //        label = Form.label_var_fort1;
                //        break;
                //    case 2:
                //        label = Form.label_var_fort2;
                //        break;
                //    case 3:
                //        label = Form.label_var_fort3;
                //        break;
                //    case 4:
                //        label = Form.label_var_fort4;
                //        break;
                //    case 5:
                //        label = Form.label_var_fort5;
                //        break;
                //    case 6:
                //        label = Form.label_var_fort6;
                //        break;
                //    case 7:
                //        label = Form.label_var_fort7;
                //        break;
                //    case 8:
                //        label = Form.label_var_fort8;
                //        break;
                //    case 9:
                //        label = Form.label_var_fort9;
                //        break;
                //    case 10:
                //        label = Form.label_var_fort10;
                //        break;
                //    case 11:
                //        label = Form.label_var_fort11;
                //        break;
                //    case 12:
                //        label = Form.label_var_fort12;
                //        break;
                //    case 13:
                //        label = Form.label_var_fort13;
                //        break;
                //    case 14:
                //        label = Form.label_var_fort14;
                //        break;
                //    case 15:
                //        label = Form.label_var_fort15;
                //        break;
                //    case 16:
                //        label = Form.label_var_fort16;
                //        break;
                //    case 17:
                //        label = Form.label_var_fort17;
                //        break;
                //    case 18:
                //        label = Form.label_var_fort18;
                //        break;
                //    case 19:
                //        label = Form.label_var_fort19;
                //        break;
                //    case 20:
                //        label = Form.label_var_fort20;
                //        break;
                //    default:
                //        break;
                //}

                switch (fortsList[i].ZoneControlState)
                {
                    case 0:
                        label.ForeColor = System.Drawing.Color.Gray;
                        break;
                    case 1:
                        label.ForeColor = System.Drawing.Color.Red;
                        break;
                    case 2:
                        label.ForeColor = System.Drawing.Color.Green;
                        break;
                    case 3:
                        label.ForeColor = System.Drawing.Color.Purple;
                        break;
                    default:
                        break;
                }

                string labelText = fortsList[i].ZoneName + string.Format(" [{0}]", fort.CurrentGasInTank);
                labelText = labelText.Replace('"', ' ');
                label.Text = labelText;

                string controlState = ZoneControlStateConverter(fort.ZoneControlState);
                Form.comboBox_attack.Items.Add(string.Format("{0} {1} [{2}] <{3}/{4}>", controlState, fort.ZoneName, fort.ZoneId, fort.Latitude, fort.Longitude));

                if (fort.CurrentGasInTank == 100 && label.ForeColor == System.Drawing.Color.Red)
                    FullBaseExists = true;
            }
            Form.Refresh();
        }

        public static void PasswordFieldClick(object sender, EventArgs e)
        {
            Form.textBox_PasswordField.Text = string.Empty;
            Form.Refresh();
        }

    }
}
