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
        DateTime _lastLaunchTime = DateTime.MinValue;
        bool _successfullyLoggedIn = false;

        #endregion

        #region Constructor

        public ConquererForm()
        {
            InitializeComponent();

            _qonqrManager = new QonqrManager();

            // load previous settings
            if (decimal.TryParse(App.Default.lattitude, out decimal savedLat))
            {
                lattitudeNumericUpDown.Value = savedLat;
            }
            if (decimal.TryParse(App.Default.longitude, out decimal savedLong))
            {
                longitudeNumericUpDown.Value = savedLong;
            }
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

        private async void loginButton_Click(object sender, EventArgs e)
        {
            loginButton.Enabled = false;
            loginLabel.Text = "Logging in...";

            try
            {
                _successfullyLoggedIn = await _qonqrManager.LoginAllAccountsAsync();
                
                if (_successfullyLoggedIn)
                {
                    loginLabel.Text = "Login Successful";
                    loginGroupBox.Enabled = false;

                    // save login settings
                    App.Default.username = usernameTextBox.Text;
                    App.Default.password = passwordTextBox.Text;
                    App.Default.Save();

                    await UpdateStatsAsync();
                    await LoadBasesAsync();

                    UpdateZoneDropDown();
                    
                    // grow screen
                    this.Width = 654;
                    this.Height = 534;
                }
                else
                {
                    loginLabel.Text = "Login Failed";
                    loginGroupBox.Enabled = true;
                    loginButton.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                loginLabel.Text = $"Error: {ex.Message}";
                loginGroupBox.Enabled = true;
                loginButton.Enabled = true;
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

            if (decimal.TryParse(_qonqrManager.Lattitude, out decimal lat))
            {
                lattitudeNumericUpDown.Value = lat;
            }
            if (decimal.TryParse(_qonqrManager.Longitude, out decimal lon))
            {
                longitudeNumericUpDown.Value = lon;
            }
        }

        private async void harvestButton_Click(object sender, EventArgs e)
        {
            await PerformHarvestAsync();
        }

        private void autoharvest_Checked(object sender, EventArgs e)
        {
            tenMinuteUpdateTimer.Enabled = autoHarvestCheckbox.Checked;
        }


        private async void tenMinuteUpdateTimer_Tick(object sender, EventArgs e)
        {
            if (!_successfullyLoggedIn || _busy)
            {
                return; // skip if we are not logged in or already busy doing something else
            }

            // Disable timer during execution to prevent overlap
            tenMinuteUpdateTimer.Enabled = false;

            try
            {
                await UpdateStatsAsync();
                await LoadBasesAsync();

                // if a base is full then harvest all
                if (autoHarvestCheckbox.Checked && _fullBaseExists)
                {
                    await PerformHarvestAsync();
                }
            }
            catch (Exception ex)
            {
                // Log error but don't stop the timer
                baseStatusLabel.Text = $"Update Error: {ex.Message}";
            }
            finally
            {
                // Re-enable timer after completion
                tenMinuteUpdateTimer.Enabled = autoHarvestCheckbox.Checked;
            }
        }

        /// <summary>
        /// Performs harvest operation - extracted from harvestButton_Click for reusability
        /// </summary>
        private async Task PerformHarvestAsync()
        {
            LockScreen();
            
            try
            {
                bool successful = await _qonqrManager.PerformHarvestAllAsync();
                UpdateHarvestControls(successful);
            }
            catch (Exception ex)
            {
                baseStatusLabel.Text = $"Harvest Error: {ex.Message}";
            }
            finally
            {
                UnlockScreen();
            }
        }

        private async void button_Scan_Click(object sender, EventArgs e)
        {
            LockScreen();

            try
            {
                _qonqrManager.Lattitude = lattitudeNumericUpDown.Value.ToString();
                _qonqrManager.Longitude = longitudeNumericUpDown.Value.ToString();

                bool successful = await _qonqrManager.ScanZonesAsync();
                UpdateZoneControls(successful);
            }
            catch (Exception ex)
            {
                mapScanStatusLabel.Text = $"Scan Error: {ex.Message}";
            }
            finally
            {
                UnlockScreen();
            }
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

        private async void launchBotsButton_Click(object sender, EventArgs e)
        {
            LockScreen();

            try
            {
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

                bool successful = await _qonqrManager.LaunchBotsAsync(zone, myLevel);
                UpdateLocationControls(successful);
            }
            catch (Exception ex)
            {
                launchStatusLabel.Text = $"Launch Error: {ex.Message}";
            }
            finally
            {
                UnlockScreen();
            }
        }

        private void secondUpdateTimer_Tick(object sender, EventArgs e)
        {
            if (_successfullyLoggedIn && !_busy) // skip if we are not logged in or already busy doing something else
            {
                UpdateResourceDisplays();
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
        
        private async Task UpdateStatsAsync()
        {
            // need to re-login to see updated stats
            bool successful = await _qonqrManager.LoginAllAccountsAsync();
            
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

        private async Task LoadBasesAsync()
        {
            LockScreen();

            try
            {
                // reset flags
                _fullBaseExists = false;
                _canHarvestBase = false;

                bool successful = await _qonqrManager.GetAllFortsAsync();
                UpdateBaseControls(successful);
            }
            finally
            {
                UnlockScreen();
            }
        }

        private void UpdateBaseControls(bool successful)
        {
            if (!successful) // failed getting forts
            {
                baseStatusLabel.Text = "Loading Bases Failed";
                ResetBaseLabels();
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
                            case Constants.ZoneControlStates.Uncaptured:
                                label.ForeColor = System.Drawing.Color.Gray;
                                break;
                            case Constants.ZoneControlStates.Legion:
                                label.ForeColor = System.Drawing.Color.Red;
                                break;
                            case Constants.ZoneControlStates.Swarm:
                                label.ForeColor = System.Drawing.Color.Green;
                                break;
                            case Constants.ZoneControlStates.Faceless:
                                label.ForeColor = System.Drawing.Color.Purple;
                                break;
                            default:
                                break;
                        }

                        string labelText = fort.ZoneName + string.Format(" [{0}]", fort.CurrentGasInTank);
                        labelText = labelText.Replace('"', ' ');
                        label.Text = labelText;

                        if (fort.CurrentGasInTank == Constants.Bases.FullGasCapacity && label.ForeColor == System.Drawing.Color.Red)
                            _fullBaseExists = true;

                        if (fort.CurrentGasInTank > 0 && label.ForeColor == System.Drawing.Color.Red)
                            _canHarvestBase = true;
                    }
                }

            }
        }

        private async void UpdateHarvestControls(bool successful)
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

                await LoadBasesAsync();
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

                // Track launch time for calculated regeneration
                _lastLaunchTime = DateTime.Now;
            }
        }

        /// <summary>
        /// Updates resource displays using calculated regeneration instead of polling
        /// </summary>
        private void UpdateResourceDisplays()
        {
            if (!int.TryParse(_qonqrManager.Statistics.BotCapacity, out int botCapacity) || botCapacity == 0)
            {
                return; // Not initialized yet
            }

            if (!int.TryParse(_qonqrManager.Statistics.EnergyCapacity, out int energyCapacity) || energyCapacity == 0)
            {
                return; // Not initialized yet
            }

            // Calculate current resources based on time elapsed since last launch
            int currentBots = ResourceCalculator.CalculateCurrentBots(
                _qonqrManager.Launch.BotsAfterLaunch,
                _qonqrManager.Launch.BotsPerSecond,
                _lastLaunchTime,
                botCapacity
            );

            int currentEnergy = ResourceCalculator.CalculateCurrentEnergy(
                _qonqrManager.Launch.EnergyAfterLaunch,
                _qonqrManager.Launch.EnergyPerSecond,
                _lastLaunchTime,
                energyCapacity
            );

            // Update UI
            botsRegenRateLabel.Text = $"Bots: {currentBots} / Regeneration Rate: {_qonqrManager.Launch.BotsPerSecond}";
            
            botsProgressBar.Maximum = botCapacity;
            energyProgressBar.Maximum = energyCapacity;
            botsProgressBar.Value = currentBots;
            energyProgressBar.Value = currentEnergy;

            // Check for auto-launch when resources are full
            if (autoLaunchCheckBox.Checked && 
                ResourceCalculator.AreResourcesFull(currentBots, botCapacity, currentEnergy, energyCapacity))
            {
                launchBotsButton_Click(null, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Helper method to reset all base labels to their default state
        /// Eliminates code duplication by using a loop instead of 20 individual statements
        /// </summary>
        private void ResetBaseLabels()
        {
            List<Label> baseLabels = new List<Label>
            {
                base1Label, base2Label, base3Label, base4Label, base5Label,
                base6Label, base7Label, base8Label, base9Label, base10Label,
                base11Label, base12Label, base13Label, base14Label, base15Label,
                base16Label, base17Label, base18Label, base19Label, base20Label
            };

            for (int i = 0; i < baseLabels.Count; i++)
            {
                baseLabels[i].Text = $"Base {i + 1}";
                baseLabels[i].ForeColor = Color.Black;
            }
        }

        #endregion

    }
}
