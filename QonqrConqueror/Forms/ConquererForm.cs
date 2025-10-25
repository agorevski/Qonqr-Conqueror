using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Qonqr
{
    public partial class ConquererForm : Form
    {
        #region Variables

        QonqrManager _qonqrManager;
        bool _fullBaseExists = false;
        bool _canHarvestBase = false;
        bool _busy = false;
        double _botCount = 0;
        double _energyCount = 0;
        bool _successfullyLoggedIn = false;

        #endregion

        #region Constructor

        public ConquererForm()
        {
            InitializeComponent();

            _qonqrManager = new QonqrManager();

            // load default coordinates
            //lattitudeNumericUpDown.Value = int.Parse(_qonqrManager.Lattitude);
            //longitudeNumericUpDown.Value = int.Parse(_qonqrManager.Longitude);

            // load previous settings
            lattitudeNumericUpDown.Value = decimal.Parse(App.Default.lattitude);
            longitudeNumericUpDown.Value = decimal.Parse(App.Default.longitude);
            usernameTextBox.Text = App.Default.username;
            passwordTextBox.Text = App.Default.password;

            // only show login area
            this.Width = 338;
            this.Height = 128;

            // disable the UI areas that are not ready
            LockScreen();
        }

        #endregion

        #region Event Handlers

        private void loginButton_Click(object sender, EventArgs e)
        {
            loginLabel.Text = "Please Wait...";
            loginGroupBox.Enabled = false;

            Refresh();

            //_successfullyLoggedIn = _qonqrManager.Login(usernameTextBox.Text, passwordTextBox.Text);
            _successfullyLoggedIn = _qonqrManager.LoginAllAccounts();
            
            if (_successfullyLoggedIn)
            {
                loginLabel.Text = "Login Successful";

                loginGroupBox.Enabled = false;

                // save login settings
                App.Default.username = usernameTextBox.Text;
                App.Default.password = passwordTextBox.Text;
                App.Default.Save();

                UpdateStats();
                LoadBases();

                UpdateZoneDropDown();
                
                // grow screen
                this.Width = 654;
                this.Height = 534;
            }
            else
            {
                loginLabel.Text = "Login Failed";

                loginGroupBox.Enabled = true;
            }
        }
        
        private void usernameTextBox_TextChanged(object sender, EventArgs e)
        {
            loginButton.Enabled = usernameTextBox.Text.Length > 0 && passwordTextBox.Text.Length > 0;
        }

        private void passwordTextBox_TextChanged(object sender, EventArgs e)
        {
            loginButton.Enabled = usernameTextBox.Text.Length > 0 && passwordTextBox.Text.Length > 0;
        }

        private void resetCoordinatesButton_Click(object sender, EventArgs e)
        {
            _qonqrManager.ResetCoordinates();

            lattitudeNumericUpDown.Value = decimal.Parse(_qonqrManager.Lattitude);
            longitudeNumericUpDown.Value = decimal.Parse(_qonqrManager.Longitude);
        }

        private void harvestButton_Click(object sender, EventArgs e)
        {
            LockScreen();
            
            bool successful = _qonqrManager.PerformHarvestAll();

            UpdateHarvestControls(successful);
            
            UnlockScreen();
        }

        private void autoharvest_Checked(object sender, EventArgs e)
        {
            tenMinuteUpdateTimer.Enabled = autoHarvestCheckbox.Checked;
        }


        private void tenMinuteUpdateTimer_Tick(object sender, EventArgs e)
        {
            if (_successfullyLoggedIn && !_busy) // skip if we are not logged in or already busy doing something else
            {
                UpdateStats();
                LoadBases();

                // if a base is full then harvest all
                if (autoHarvestCheckbox.Checked && _fullBaseExists)
                    harvestButton_Click(null, new EventArgs());
                
            }
        }

        private void button_Scan_Click(object sender, EventArgs e)
        {
            LockScreen();

            _qonqrManager.Lattitude = lattitudeNumericUpDown.Value.ToString();
            _qonqrManager.Longitude = longitudeNumericUpDown.Value.ToString();

            bool successful = _qonqrManager.ScanZones();

            UpdateZoneControls(successful);

            UnlockScreen();
        }

        private void button_Set_Click(object sender, EventArgs e)
        {
            // save settings
            App.Default.longitude = longitudeNumericUpDown.Value.ToString();
            App.Default.lattitude = lattitudeNumericUpDown.Value.ToString();
            App.Default.Save();

            zoneComboBox.Items.Clear();
            zoneComboBox.Text = "";

            UpdateZoneDropDown();

            if (scanAreaComboBox.Items.Count > 0)
            {
                for (int i = 0; i < scanAreaComboBox.Items.Count; i++)
                {
                    zoneComboBox.Items.Add(scanAreaComboBox.Items[i]);
                }

                if (zoneComboBox.Items.Count > 0)
                    zoneComboBox.SelectedIndex = 0;
            }
        }

        private void launchBotsButton_Click(object sender, EventArgs e)
        {
            LockScreen();

            QonqrManager.Zoneski zone = new QonqrManager.Zoneski();

            // get attacking zone info
            int selectedIndex = zoneComboBox.SelectedIndex;
            string attackDone = zoneComboBox.Items[selectedIndex].ToString();
            int start = attackDone.IndexOf("[") + 1;
            int end = attackDone.IndexOf("]");
            string zoneId = attackDone.Substring(start, end - start);
            int startLatLong = attackDone.IndexOf("<") + 1;
            int endLatLong = attackDone.IndexOf(">");
            string latLong = attackDone.Substring(startLatLong, endLatLong - startLatLong);
            string[] latLongArray = latLong.Split('/');
            string latitude = latLongArray[0];
            string longitude = latLongArray[1];
            zone.Latitude = latitude;
            zone.Longitude = longitude;
            zone.ZoneId = zoneId;

            int myLevel = 0;
            int.TryParse(levelValueLabel.Text, out myLevel);

            bool successful = _qonqrManager.LaunchBots(zone, myLevel);

            UpdateLocationControls(successful);

            UnlockScreen();
        }

        private void secondUpdateTimer_Tick(object sender, EventArgs e)
        {
            if (_successfullyLoggedIn && !_busy) // skip if we are not logged in or already busy doing something else
            {
                _botCount += _qonqrManager.Launch.BotsPerSecond;
                _energyCount += _qonqrManager.Launch.EnergyPerSecond;

                botsRegenRateLabel.Text = string.Format("Bots: {0} / Regeneration Rate: {1}", _botCount, _qonqrManager.Launch.BotsPerSecond);

                if (_botCount != 0)
                {
                    double botProgress = _botCount;
                    double energyProgress = _energyCount;

                    botsProgressBar.Maximum = int.Parse(_qonqrManager.Statistics.BotCapacity);
                    energyProgressBar.Maximum = int.Parse(_qonqrManager.Statistics.EnergyCapacity);

                    // don't let our counting exceed what the progress bar allows
                    if (botProgress > botsProgressBar.Maximum)
                        botProgress = botsProgressBar.Maximum;

                    if (energyProgress > energyProgressBar.Maximum)
                        energyProgress = energyProgressBar.Maximum;

                    botsProgressBar.Value = (int)botProgress;
                    energyProgressBar.Value = (int)energyProgress;

                    if (botProgress == botsProgressBar.Maximum && energyProgress == energyProgressBar.Maximum && autoLaunchCheckBox.Checked)
                    {
                        botProgress = 0;
                        energyProgress = 0;

                        launchBotsButton_Click(null, new EventArgs());
                    }

                }
            }
        }
        
        #endregion
        
        #region Helper Methods

        private void LockScreen()
        {
            _busy = true;

            basesGroupBox.Enabled = false;
            zoneGroupBox.Enabled = false;
            mapGroupBox.Enabled = false;
        }

        private void UnlockScreen()
        {
            _busy = false;

            basesGroupBox.Enabled = true;
            zoneGroupBox.Enabled = true;
            mapGroupBox.Enabled = true;
        }
        
        private void UpdateStats()
        {
            // need to re-login to see updated stats
            //bool successful = _qonqrManager.Login(usernameTextBox.Text, passwordTextBox.Text);
            bool successful = _qonqrManager.LoginAllAccounts();
            
            // the qonqr manager stats are cleared if we failed to login,
            // they are either set to their corresponding values or to blank

            botCapacityValueLabel.Text = _qonqrManager.Statistics.BotCapacity;
            energyCapacityValueLabel.Text = _qonqrManager.Statistics.EnergyCapacity;
            currentExperienceValueLabel.Text = _qonqrManager.Statistics.CurrentExperience;
            levelValueLabel.Text = _qonqrManager.Statistics.Level;
            nextLevelExperienceValueLabel.Text = _qonqrManager.Statistics.ExperienceToLevel;
            zonesValueLabel.Text = _qonqrManager.Statistics.Zones;
            creditsValueLabel.Text = _qonqrManager.Statistics.Credits;
            codenameValueLabel.Text = _qonqrManager.Statistics.CodeName;
        }

        private void LoadBases()
        {
            LockScreen();

            // reset flags
            _fullBaseExists = false;
            _canHarvestBase = false;

            bool successful = _qonqrManager.GetAllForts();
            //bool successful = _qonqrManager.GetForts();

            UpdateBaseControls(successful);
            
            UnlockScreen();
        }

        private void UpdateBaseControls(bool successful)
        {
            if (!successful) // failed getting forts
            {
                baseStatusLabel.Text = "Loading Bases Failed";

                // clear forts
                base1Label.Text = "Base 1";
                base1Label.ForeColor = Color.Black;
                base2Label.Text = "Base 2";
                base2Label.ForeColor = Color.Black;
                base3Label.Text = "Base 3";
                base3Label.ForeColor = Color.Black;
                base4Label.Text = "Base 4";
                base4Label.ForeColor = Color.Black;
                base5Label.Text = "Base 5";
                base5Label.ForeColor = Color.Black;
                base6Label.Text = "Base 6";
                base6Label.ForeColor = Color.Black;
                base7Label.Text = "Base 7";
                base7Label.ForeColor = Color.Black;
                base8Label.Text = "Base 8";
                base8Label.ForeColor = Color.Black;
                base9Label.Text = "Base 9";
                base9Label.ForeColor = Color.Black;
                base10Label.Text = "Base 10";
                base10Label.ForeColor = Color.Black;
                base11Label.Text = "Base 11";
                base11Label.ForeColor = Color.Black;
                base12Label.Text = "Base 12";
                base12Label.ForeColor = Color.Black;
                base13Label.Text = "Base 13";
                base13Label.ForeColor = Color.Black;
                base14Label.Text = "Base 14";
                base14Label.ForeColor = Color.Black;
                base15Label.Text = "Base 15";
                base15Label.ForeColor = Color.Black;
                base16Label.Text = "Base 16";
                base16Label.ForeColor = Color.Black;
                base17Label.Text = "Base 17";
                base17Label.ForeColor = Color.Black;
                base18Label.Text = "Base 18";
                base18Label.ForeColor = Color.Black;
                base19Label.Text = "Base 19";
                base19Label.ForeColor = Color.Black;
                base20Label.Text = "Base 20";
                base20Label.ForeColor = Color.Black;
            }
            else
            {
                // update UI

                List<Label> basesList = new List<Label>()
                {
                    base1Label,
                    base2Label,
                    base3Label,
                    base4Label,
                    base5Label,
                    base6Label,
                    base7Label,
                    base8Label,
                    base9Label,
                    base10Label,
                    base11Label,
                    base12Label,
                    base13Label,
                    base14Label,
                    base15Label,
                    base16Label,
                    base17Label,
                    base18Label,
                    base19Label,
                    base20Label
                };

                for (int i = 0; i < _qonqrManager.Forts.Count; i++)
                {
                    QonqrManager.Zoneski fort = _qonqrManager.Forts[i];

                    if (i < basesList.Count) // make sure we don't go over the label limit (we assume 20 is max fort count)
                    {
                        Label label = basesList[i];

                        switch (fort.ControlState)
                        {
                            case "*U*":
                                label.ForeColor = System.Drawing.Color.Gray;
                                break;
                            case "*L*":
                                label.ForeColor = System.Drawing.Color.Red;
                                break;
                            case "*S*":
                                label.ForeColor = System.Drawing.Color.Green;
                                break;
                            case "*F*":
                                label.ForeColor = System.Drawing.Color.Purple;
                                break;
                            default:
                                break;
                        }

                        string labelText = fort.ZoneName + string.Format(" [{0}]", fort.CurrentGasInTank);
                        labelText = labelText.Replace('"', ' ');
                        label.Text = labelText;

                        if (fort.CurrentGasInTank == 100 && label.ForeColor == System.Drawing.Color.Red)
                            _fullBaseExists = true;

                        if (fort.CurrentGasInTank > 0 && label.ForeColor == System.Drawing.Color.Red)
                            _canHarvestBase = true;
                    }
                }

            }
        }

        private void UpdateHarvestControls(bool successful)
        {
            if (!successful) // failed harvesting bases
            {
                // do nothing except tell user we failed

                baseStatusLabel.Text = "Harvest Failed";
            }
            else
            {
                baseStatusLabel.Text = "Harvest Successful!";

                creditsEarnedLabel.ForeColor = Color.Green;
                creditsEarnedLabel.Text = string.Format("Credits Harvested: {0}", _qonqrManager.Harvest.TotalCreditsEarned);

                LoadBases();
            }
        }
        
        private void UpdateZoneControls(bool successful)
        {
            scanAreaComboBox.Items.Clear();
            scanAreaComboBox.Text = "";

            if (!successful)
            {
                // do nothing except inform user that the call failed

                mapScanStatusLabel.Text = "Scan Failed";
            }
            else
            {
                mapScanStatusLabel.Text = "Scan Successful!";

                foreach (QonqrManager.Zoneski zone in _qonqrManager.Zones)
                {
                    scanAreaComboBox.Items.Add(string.Format("{0} {1} [{2}] <{3}/{4}>", zone.ControlState, zone.ZoneName, zone.ZoneId, zone.Latitude, zone.Longitude));
                }

                if (scanAreaComboBox.Items.Count > 0)
                    scanAreaComboBox.SelectedIndex = 0;

            }
        }

        private void UpdateZoneDropDown()
        {
            // update the zone combo box once
            foreach (QonqrManager.Zoneski fort in _qonqrManager.Forts)
            {
                zoneComboBox.Items.Add(string.Format("{0} {1} [{2}] <{3}/{4}>", fort.ControlState, fort.ZoneName, fort.ZoneId, fort.Latitude, fort.Longitude));
            }

            if (zoneComboBox.Items.Count > 0)
                zoneComboBox.SelectedIndex = 0;
        }

        private void UpdateLocationControls(bool successful)
        {
            if (!successful)// failed launching bots
            {
                // do nothing except tell user we failed

                launchStatusLabel.Text = "Launch Failed";
            }
            else
            {
                launchStatusLabel.Text = "Launch Successful!";

                _botCount = _qonqrManager.Launch.BotsAfterLaunch;
                _energyCount = _qonqrManager.Launch.EnergyAfterLaunch;
            }
        }

        #endregion

    }
}

