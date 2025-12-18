using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Qonqr.Models;

namespace Qonqr;

public partial class ConquererForm : Form
{
    #region Constants

    private const int LoginFormWidth = 338;
    private const int LoginFormHeight = 128;
    private const int MainFormWidth = 654;
    private const int MainFormHeight = 534;

    #endregion

    #region Private Fields

    private readonly QonqrManager _qonqrManager;
    private readonly StateManager _stateManager;
    private bool _successfullyLoggedIn = false;
    private bool _busy = false;
    private bool _fullBaseExists = false;
    private bool _canHarvestBase = false;
    private DateTime _lastLaunchTime = DateTime.MinValue;

    #endregion

    #region Constructor

    public ConquererForm()
    {
        InitializeComponent();

        _qonqrManager = new QonqrManager();
        _stateManager = new StateManager();

        // Subscribe to state changes
        _stateManager.StateChanged += OnStateChanged;

        // Load previous settings
        if (decimal.TryParse(App.Default.latitude, out decimal savedLat))
        {
            lattitudeNumericUpDown.Value = savedLat;
        }
        if (decimal.TryParse(App.Default.longitude, out decimal savedLong))
        {
            longitudeNumericUpDown.Value = savedLong;
        }
        usernameTextBox.Text = App.Default.username;
        passwordTextBox.Text = App.Default.password;

        // Only show login area initially
        this.Width = LoginFormWidth;
        this.Height = LoginFormHeight;

        // Disable UI areas that are not ready
        LockScreen();
    }

    #endregion

    #region State Management

    /// <summary>
    /// Handles state changes from the StateManager
    /// </summary>
    private void OnStateChanged(object sender, StateChangedEventArgs e)
    {
        // Ensure we're on the UI thread
        if (InvokeRequired)
        {
            Invoke(new Action(() => OnStateChanged(sender, e)));
            return;
        }

        switch (e.NewState)
        {
            case ApplicationState.NotLoggedIn:
                loginGroupBox.Enabled = true;
                loginButton.Enabled = true;
                basesGroupBox.Enabled = false;
                zoneGroupBox.Enabled = false;
                mapGroupBox.Enabled = false;
                break;

            case ApplicationState.LoggingIn:
                loginButton.Enabled = false;
                loginLabel.Text = "Logging in...";
                break;

            case ApplicationState.LoggedIn:
                loginGroupBox.Enabled = false;
                basesGroupBox.Enabled = true;
                zoneGroupBox.Enabled = true;
                mapGroupBox.Enabled = true;
                break;

            case ApplicationState.Busy:
                basesGroupBox.Enabled = false;
                zoneGroupBox.Enabled = false;
                mapGroupBox.Enabled = false;
                break;

            case ApplicationState.Error:
                loginGroupBox.Enabled = true;
                loginButton.Enabled = true;
                break;
        }
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

                // Save login settings
                App.Default.username = usernameTextBox.Text;
                App.Default.password = passwordTextBox.Text;
                App.Default.Save();

                await UpdateStatsAsync();
                await LoadBasesAsync();

                UpdateZoneDropDown();

                // Expand form to show all controls
                this.Width = MainFormWidth;
                this.Height = MainFormHeight;
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

        if (decimal.TryParse(_qonqrManager.Latitude, out decimal lat))
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
            return;
        }

        // Disable timer during execution to prevent overlap
        tenMinuteUpdateTimer.Enabled = false;

        try
        {
            await UpdateStatsAsync();
            await LoadBasesAsync();

            // If a base is full then harvest all
            if (autoHarvestCheckbox.Checked && _fullBaseExists)
            {
                await PerformHarvestAsync();
            }
        }
        catch (Exception ex)
        {
            baseStatusLabel.Text = $"Update Error: {ex.Message}";
        }
        finally
        {
            // Re-enable timer after completion
            tenMinuteUpdateTimer.Enabled = autoHarvestCheckbox.Checked;
        }
    }

    private async void button_Scan_Click(object sender, EventArgs e)
    {
        LockScreen();

        try
        {
            _qonqrManager.Latitude = lattitudeNumericUpDown.Value.ToString();
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
        // Save settings
        App.Default.longitude = longitudeNumericUpDown.Value.ToString();
        App.Default.latitude = lattitudeNumericUpDown.Value.ToString();
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
            {
                zoneComboBox.SelectedIndex = 0;
            }
        }
    }

    private async void launchBotsButton_Click(object sender, EventArgs e)
    {
        LockScreen();

        try
        {
            ZoneInfo zone = GetSelectedZone();
            if (zone == null)
            {
                launchStatusLabel.Text = "No zone selected";
                return;
            }

            int.TryParse(levelValueLabel.Text, out int myLevel);

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
        if (_successfullyLoggedIn && !_busy)
        {
            UpdateResourceDisplays();
        }
    }

    #endregion

    #region Helper Methods

    /// <summary>
    /// Performs harvest operation
    /// </summary>
    private async Task PerformHarvestAsync()
    {
        LockScreen();

        try
        {
            bool successful = await _qonqrManager.PerformHarvestAllAsync();
            await UpdateHarvestControlsAsync(successful);
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

    /// <summary>
    /// Parses the selected zone from the combo box
    /// </summary>
    private ZoneInfo GetSelectedZone()
    {
        if (zoneComboBox.SelectedIndex < 0)
        {
            return null;
        }

        string selectedText = zoneComboBox.Items[zoneComboBox.SelectedIndex].ToString();

        // Parse zone info from display string format: "{ControlState} {ZoneName} [{ZoneId}] <{Latitude}/{Longitude}>"
        int zoneIdStart = selectedText.IndexOf("[") + 1;
        int zoneIdEnd = selectedText.IndexOf("]");
        int latLongStart = selectedText.IndexOf("<") + 1;
        int latLongEnd = selectedText.IndexOf(">");

        if (zoneIdStart <= 0 || zoneIdEnd <= 0 || latLongStart <= 0 || latLongEnd <= 0)
        {
            return null;
        }

        string zoneId = selectedText.Substring(zoneIdStart, zoneIdEnd - zoneIdStart);
        string latLong = selectedText.Substring(latLongStart, latLongEnd - latLongStart);
        string[] latLongParts = latLong.Split('/');

        if (latLongParts.Length != 2)
        {
            return null;
        }

        return new ZoneInfo
        {
            ZoneId = zoneId,
            Latitude = latLongParts[0],
            Longitude = latLongParts[1]
        };
    }

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
        bool successful = await _qonqrManager.LoginAllAccountsAsync();

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
        if (!successful)
        {
            baseStatusLabel.Text = "Loading Bases Failed";
            ResetBaseLabels();
            return;
        }

        List<Label> basesList = GetBaseLabels();

        for (int i = 0; i < _qonqrManager.Forts.Count; i++)
        {
            ZoneInfo fort = _qonqrManager.Forts[i];

            if (i >= basesList.Count)
            {
                break;
            }

            Label label = basesList[i];
            label.ForeColor = GetFactionColor(fort.ControlState);

            string labelText = $"{fort.ZoneName} [{fort.CurrentGasInTank}]";
            labelText = labelText.Replace('"', ' ');
            label.Text = labelText;

            // Check for full base (Legion faction only for auto-harvest)
            if (fort.CurrentGasInTank == Constants.Bases.FullGasCapacity &&
                fort.ControlState == Constants.ZoneControlStates.Legion)
            {
                _fullBaseExists = true;
            }

            if (fort.CurrentGasInTank > 0 && fort.ControlState == Constants.ZoneControlStates.Legion)
            {
                _canHarvestBase = true;
            }
        }
    }

    private static Color GetFactionColor(string controlState)
    {
        return controlState switch
        {
            Constants.ZoneControlStates.Uncaptured => Color.Gray,
            Constants.ZoneControlStates.Legion => Color.Red,
            Constants.ZoneControlStates.Swarm => Color.Green,
            Constants.ZoneControlStates.Faceless => Color.Purple,
            _ => Color.Black
        };
    }

    private async Task UpdateHarvestControlsAsync(bool successful)
    {
        if (!successful)
        {
            baseStatusLabel.Text = "Harvest Failed";
            return;
        }

        baseStatusLabel.Text = "Harvest Successful!";
        creditsEarnedLabel.ForeColor = Color.Green;
        creditsEarnedLabel.Text = $"Credits Harvested: {_qonqrManager.Harvest.TotalCreditsEarned}";

        await LoadBasesAsync();
    }

    private void UpdateZoneControls(bool successful)
    {
        scanAreaComboBox.Items.Clear();
        scanAreaComboBox.Text = "";

        if (!successful)
        {
            mapScanStatusLabel.Text = "Scan Failed";
            return;
        }

        mapScanStatusLabel.Text = "Scan Successful!";

        foreach (ZoneInfo zone in _qonqrManager.Zones)
        {
            scanAreaComboBox.Items.Add(zone.ToDisplayString());
        }

        if (scanAreaComboBox.Items.Count > 0)
        {
            scanAreaComboBox.SelectedIndex = 0;
        }
    }

    private void UpdateZoneDropDown()
    {
        foreach (ZoneInfo fort in _qonqrManager.Forts)
        {
            zoneComboBox.Items.Add(fort.ToDisplayString());
        }

        if (zoneComboBox.Items.Count > 0)
        {
            zoneComboBox.SelectedIndex = 0;
        }
    }

    private void UpdateLocationControls(bool successful)
    {
        if (!successful)
        {
            launchStatusLabel.Text = "Launch Failed";
            return;
        }

        launchStatusLabel.Text = "Launch Successful!";
        _lastLaunchTime = DateTime.Now;
    }

    /// <summary>
    /// Updates resource displays using calculated regeneration
    /// </summary>
    private void UpdateResourceDisplays()
    {
        if (!int.TryParse(_qonqrManager.Statistics.BotCapacity, out int botCapacity) || botCapacity == 0)
        {
            return;
        }

        if (!int.TryParse(_qonqrManager.Statistics.EnergyCapacity, out int energyCapacity) || energyCapacity == 0)
        {
            return;
        }

        int currentBots = ResourceCalculator.CalculateCurrentBots(
            _qonqrManager.Launch.BotsAfterLaunch,
            _qonqrManager.Launch.BotsPerSecond,
            _lastLaunchTime,
            botCapacity);

        int currentEnergy = ResourceCalculator.CalculateCurrentEnergy(
            _qonqrManager.Launch.EnergyAfterLaunch,
            _qonqrManager.Launch.EnergyPerSecond,
            _lastLaunchTime,
            energyCapacity);

        botsRegenRateLabel.Text = $"Bots: {currentBots} / Regeneration Rate: {_qonqrManager.Launch.BotsPerSecond}";

        botsProgressBar.Maximum = botCapacity;
        energyProgressBar.Maximum = energyCapacity;
        botsProgressBar.Value = Math.Min(currentBots, botCapacity);
        energyProgressBar.Value = Math.Min(currentEnergy, energyCapacity);

        // Check for auto-launch when resources are full
        if (autoLaunchCheckBox.Checked &&
            ResourceCalculator.AreResourcesFull(currentBots, botCapacity, currentEnergy, energyCapacity))
        {
            launchBotsButton_Click(null, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Gets the list of base labels for iteration
    /// </summary>
    private List<Label> GetBaseLabels()
    {
        return new List<Label>
        {
            base1Label, base2Label, base3Label, base4Label, base5Label,
            base6Label, base7Label, base8Label, base9Label, base10Label,
            base11Label, base12Label, base13Label, base14Label, base15Label,
            base16Label, base17Label, base18Label, base19Label, base20Label
        };
    }

    /// <summary>
    /// Resets all base labels to their default state
    /// </summary>
    private void ResetBaseLabels()
    {
        List<Label> baseLabels = GetBaseLabels();

        for (int i = 0; i < baseLabels.Count; i++)
        {
            baseLabels[i].Text = $"Base {i + 1}";
            baseLabels[i].ForeColor = Color.Black;
        }
    }

    #endregion
}
