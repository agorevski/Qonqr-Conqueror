namespace Qonqr
{
    partial class ConquererForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.loginButton = new System.Windows.Forms.Button();
            this.loginGroupBox = new System.Windows.Forms.GroupBox();
            this.loginLabel = new System.Windows.Forms.Label();
            this.basesGroupBox = new System.Windows.Forms.GroupBox();
            this.baseStatusLabel = new System.Windows.Forms.Label();
            this.creditsEarnedLabel = new System.Windows.Forms.Label();
            this.autoHarvestCheckbox = new System.Windows.Forms.CheckBox();
            this.basesTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.base20Label = new System.Windows.Forms.Label();
            this.base19Label = new System.Windows.Forms.Label();
            this.base18Label = new System.Windows.Forms.Label();
            this.base17Label = new System.Windows.Forms.Label();
            this.base16Label = new System.Windows.Forms.Label();
            this.base15Label = new System.Windows.Forms.Label();
            this.base14Label = new System.Windows.Forms.Label();
            this.base13Label = new System.Windows.Forms.Label();
            this.base12Label = new System.Windows.Forms.Label();
            this.base11Label = new System.Windows.Forms.Label();
            this.base10Label = new System.Windows.Forms.Label();
            this.base9Label = new System.Windows.Forms.Label();
            this.base8Label = new System.Windows.Forms.Label();
            this.base7Label = new System.Windows.Forms.Label();
            this.base6Label = new System.Windows.Forms.Label();
            this.base5Label = new System.Windows.Forms.Label();
            this.base4Label = new System.Windows.Forms.Label();
            this.base3Label = new System.Windows.Forms.Label();
            this.base2Label = new System.Windows.Forms.Label();
            this.base1Label = new System.Windows.Forms.Label();
            this.harvestButton = new System.Windows.Forms.Button();
            this.codenameLabel = new System.Windows.Forms.Label();
            this.currentStatsGroupBox = new System.Windows.Forms.GroupBox();
            this.statsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.currentExperienceValueLabel = new System.Windows.Forms.Label();
            this.creditsLabel = new System.Windows.Forms.Label();
            this.levelValueLabel = new System.Windows.Forms.Label();
            this.energyCapacityValueLabel = new System.Windows.Forms.Label();
            this.botCapacityValueLabel = new System.Windows.Forms.Label();
            this.creditsValueLabel = new System.Windows.Forms.Label();
            this.codenameValueLabel = new System.Windows.Forms.Label();
            this.nextLevelExperienceLabel = new System.Windows.Forms.Label();
            this.currentExperienceLabel = new System.Windows.Forms.Label();
            this.levelLabel = new System.Windows.Forms.Label();
            this.energyCapacityLabel = new System.Windows.Forms.Label();
            this.botCapacityLabel = new System.Windows.Forms.Label();
            this.nextLevelExperienceValueLabel = new System.Windows.Forms.Label();
            this.zonesValueLabel = new System.Windows.Forms.Label();
            this.zonesLabel = new System.Windows.Forms.Label();
            this.zoneGroupBox = new System.Windows.Forms.GroupBox();
            this.energyLabel = new System.Windows.Forms.Label();
            this.autoLaunchCheckBox = new System.Windows.Forms.CheckBox();
            this.energyProgressBar = new System.Windows.Forms.ProgressBar();
            this.zoneComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.currentZoneLabel = new System.Windows.Forms.Label();
            this.botsProgressBar = new System.Windows.Forms.ProgressBar();
            this.launchBotsButton = new System.Windows.Forms.Button();
            this.botsRegenRateLabel = new System.Windows.Forms.Label();
            this.scanAreaComboBox = new System.Windows.Forms.ComboBox();
            this.button_Set = new System.Windows.Forms.Button();
            this.button_Scan = new System.Windows.Forms.Button();
            this.mapGroupBox = new System.Windows.Forms.GroupBox();
            this.lattitudeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.mapSetStatusLabel = new System.Windows.Forms.Label();
            this.mapScanStatusLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.longitudeLabel = new System.Windows.Forms.Label();
            this.resetCoordinatesButton = new System.Windows.Forms.Button();
            this.lattitudeLabel = new System.Windows.Forms.Label();
            this.tenMinuteUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.longitudeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.secondUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.launchStatusLabel = new System.Windows.Forms.Label();
            this.loginGroupBox.SuspendLayout();
            this.basesGroupBox.SuspendLayout();
            this.basesTableLayoutPanel.SuspendLayout();
            this.currentStatsGroupBox.SuspendLayout();
            this.statsTableLayoutPanel.SuspendLayout();
            this.zoneGroupBox.SuspendLayout();
            this.mapGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lattitudeNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.longitudeNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(155, 19);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '?';
            this.passwordTextBox.Size = new System.Drawing.Size(139, 20);
            this.passwordTextBox.TabIndex = 3;
            this.passwordTextBox.Text = "Password";
            this.passwordTextBox.TextChanged += new System.EventHandler(this.passwordTextBox_TextChanged);
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Location = new System.Drawing.Point(6, 19);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(139, 20);
            this.usernameTextBox.TabIndex = 2;
            this.usernameTextBox.Text = "Username";
            this.usernameTextBox.TextChanged += new System.EventHandler(this.usernameTextBox_TextChanged);
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(6, 45);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(139, 23);
            this.loginButton.TabIndex = 1;
            this.loginButton.Text = "Log In";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // loginGroupBox
            // 
            this.loginGroupBox.Controls.Add(this.loginLabel);
            this.loginGroupBox.Controls.Add(this.usernameTextBox);
            this.loginGroupBox.Controls.Add(this.passwordTextBox);
            this.loginGroupBox.Controls.Add(this.loginButton);
            this.loginGroupBox.Location = new System.Drawing.Point(12, 12);
            this.loginGroupBox.Name = "loginGroupBox";
            this.loginGroupBox.Size = new System.Drawing.Size(305, 72);
            this.loginGroupBox.TabIndex = 6;
            this.loginGroupBox.TabStop = false;
            this.loginGroupBox.Text = "Login";
            // 
            // loginLabel
            // 
            this.loginLabel.Location = new System.Drawing.Point(152, 43);
            this.loginLabel.Name = "loginLabel";
            this.loginLabel.Size = new System.Drawing.Size(142, 26);
            this.loginLabel.TabIndex = 4;
            this.loginLabel.Text = "Not logged in";
            this.loginLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // basesGroupBox
            // 
            this.basesGroupBox.Controls.Add(this.baseStatusLabel);
            this.basesGroupBox.Controls.Add(this.creditsEarnedLabel);
            this.basesGroupBox.Controls.Add(this.autoHarvestCheckbox);
            this.basesGroupBox.Controls.Add(this.basesTableLayoutPanel);
            this.basesGroupBox.Controls.Add(this.harvestButton);
            this.basesGroupBox.Enabled = false;
            this.basesGroupBox.Location = new System.Drawing.Point(323, 12);
            this.basesGroupBox.Name = "basesGroupBox";
            this.basesGroupBox.Size = new System.Drawing.Size(303, 281);
            this.basesGroupBox.TabIndex = 7;
            this.basesGroupBox.TabStop = false;
            this.basesGroupBox.Text = "Bases";
            // 
            // baseStatusLabel
            // 
            this.baseStatusLabel.Location = new System.Drawing.Point(149, 232);
            this.baseStatusLabel.Name = "baseStatusLabel";
            this.baseStatusLabel.Size = new System.Drawing.Size(146, 17);
            this.baseStatusLabel.TabIndex = 13;
            this.baseStatusLabel.Text = "Nothing Harvested";
            this.baseStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // creditsEarnedLabel
            // 
            this.creditsEarnedLabel.Location = new System.Drawing.Point(149, 258);
            this.creditsEarnedLabel.Name = "creditsEarnedLabel";
            this.creditsEarnedLabel.Size = new System.Drawing.Size(146, 17);
            this.creditsEarnedLabel.TabIndex = 11;
            this.creditsEarnedLabel.Text = "Credits Harvested:";
            this.creditsEarnedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // autoHarvestCheckbox
            // 
            this.autoHarvestCheckbox.Location = new System.Drawing.Point(6, 258);
            this.autoHarvestCheckbox.Name = "autoHarvestCheckbox";
            this.autoHarvestCheckbox.Size = new System.Drawing.Size(137, 17);
            this.autoHarvestCheckbox.TabIndex = 12;
            this.autoHarvestCheckbox.Text = "Auto Harvest";
            this.autoHarvestCheckbox.UseVisualStyleBackColor = true;
            this.autoHarvestCheckbox.CheckedChanged += new System.EventHandler(autoharvest_Checked);

            // 
            // basesTableLayoutPanel
            // 
            this.basesTableLayoutPanel.ColumnCount = 2;
            this.basesTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.basesTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.basesTableLayoutPanel.Controls.Add(this.base20Label, 1, 9);
            this.basesTableLayoutPanel.Controls.Add(this.base19Label, 0, 9);
            this.basesTableLayoutPanel.Controls.Add(this.base18Label, 1, 8);
            this.basesTableLayoutPanel.Controls.Add(this.base17Label, 0, 8);
            this.basesTableLayoutPanel.Controls.Add(this.base16Label, 1, 7);
            this.basesTableLayoutPanel.Controls.Add(this.base15Label, 0, 7);
            this.basesTableLayoutPanel.Controls.Add(this.base14Label, 1, 6);
            this.basesTableLayoutPanel.Controls.Add(this.base13Label, 0, 6);
            this.basesTableLayoutPanel.Controls.Add(this.base12Label, 1, 5);
            this.basesTableLayoutPanel.Controls.Add(this.base11Label, 0, 5);
            this.basesTableLayoutPanel.Controls.Add(this.base10Label, 1, 4);
            this.basesTableLayoutPanel.Controls.Add(this.base9Label, 0, 4);
            this.basesTableLayoutPanel.Controls.Add(this.base8Label, 1, 3);
            this.basesTableLayoutPanel.Controls.Add(this.base7Label, 0, 3);
            this.basesTableLayoutPanel.Controls.Add(this.base6Label, 1, 2);
            this.basesTableLayoutPanel.Controls.Add(this.base5Label, 0, 2);
            this.basesTableLayoutPanel.Controls.Add(this.base4Label, 1, 1);
            this.basesTableLayoutPanel.Controls.Add(this.base3Label, 0, 1);
            this.basesTableLayoutPanel.Controls.Add(this.base2Label, 1, 0);
            this.basesTableLayoutPanel.Controls.Add(this.base1Label, 0, 0);
            this.basesTableLayoutPanel.Location = new System.Drawing.Point(6, 19);
            this.basesTableLayoutPanel.Name = "basesTableLayoutPanel";
            this.basesTableLayoutPanel.RowCount = 11;
            this.basesTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.basesTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.basesTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.basesTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.basesTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.basesTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.basesTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.basesTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.basesTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.basesTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.basesTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.basesTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.basesTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.basesTableLayoutPanel.Size = new System.Drawing.Size(289, 204);
            this.basesTableLayoutPanel.TabIndex = 8;
            // 
            // base20Label
            // 
            this.base20Label.AutoSize = true;
            this.base20Label.Location = new System.Drawing.Point(147, 180);
            this.base20Label.Name = "base20Label";
            this.base20Label.Size = new System.Drawing.Size(46, 13);
            this.base20Label.TabIndex = 19;
            this.base20Label.Text = "Base 20";
            // 
            // base19Label
            // 
            this.base19Label.AutoSize = true;
            this.base19Label.Location = new System.Drawing.Point(3, 180);
            this.base19Label.Name = "base19Label";
            this.base19Label.Size = new System.Drawing.Size(46, 13);
            this.base19Label.TabIndex = 17;
            this.base19Label.Text = "Base 19";
            // 
            // base18Label
            // 
            this.base18Label.AutoSize = true;
            this.base18Label.Location = new System.Drawing.Point(147, 160);
            this.base18Label.Name = "base18Label";
            this.base18Label.Size = new System.Drawing.Size(46, 13);
            this.base18Label.TabIndex = 15;
            this.base18Label.Text = "Base 18";
            // 
            // base17Label
            // 
            this.base17Label.AutoSize = true;
            this.base17Label.Location = new System.Drawing.Point(3, 160);
            this.base17Label.Name = "base17Label";
            this.base17Label.Size = new System.Drawing.Size(46, 13);
            this.base17Label.TabIndex = 13;
            this.base17Label.Text = "Base 17";
            // 
            // base16Label
            // 
            this.base16Label.AutoSize = true;
            this.base16Label.Location = new System.Drawing.Point(147, 140);
            this.base16Label.Name = "base16Label";
            this.base16Label.Size = new System.Drawing.Size(46, 13);
            this.base16Label.TabIndex = 11;
            this.base16Label.Text = "Base 16";
            // 
            // base15Label
            // 
            this.base15Label.AutoSize = true;
            this.base15Label.Location = new System.Drawing.Point(3, 140);
            this.base15Label.Name = "base15Label";
            this.base15Label.Size = new System.Drawing.Size(46, 13);
            this.base15Label.TabIndex = 9;
            this.base15Label.Text = "Base 15";
            // 
            // base14Label
            // 
            this.base14Label.AutoSize = true;
            this.base14Label.Location = new System.Drawing.Point(147, 120);
            this.base14Label.Name = "base14Label";
            this.base14Label.Size = new System.Drawing.Size(46, 13);
            this.base14Label.TabIndex = 7;
            this.base14Label.Text = "Base 14";
            // 
            // base13Label
            // 
            this.base13Label.AutoSize = true;
            this.base13Label.Location = new System.Drawing.Point(3, 120);
            this.base13Label.Name = "base13Label";
            this.base13Label.Size = new System.Drawing.Size(46, 13);
            this.base13Label.TabIndex = 5;
            this.base13Label.Text = "Base 13";
            // 
            // base12Label
            // 
            this.base12Label.AutoSize = true;
            this.base12Label.Location = new System.Drawing.Point(147, 100);
            this.base12Label.Name = "base12Label";
            this.base12Label.Size = new System.Drawing.Size(46, 13);
            this.base12Label.TabIndex = 3;
            this.base12Label.Text = "Base 12";
            // 
            // base11Label
            // 
            this.base11Label.AutoSize = true;
            this.base11Label.Location = new System.Drawing.Point(3, 100);
            this.base11Label.Name = "base11Label";
            this.base11Label.Size = new System.Drawing.Size(46, 13);
            this.base11Label.TabIndex = 1;
            this.base11Label.Text = "Base 11";
            // 
            // base10Label
            // 
            this.base10Label.AutoSize = true;
            this.base10Label.Location = new System.Drawing.Point(147, 80);
            this.base10Label.Name = "base10Label";
            this.base10Label.Size = new System.Drawing.Size(46, 13);
            this.base10Label.TabIndex = 18;
            this.base10Label.Text = "Base 10";
            // 
            // base9Label
            // 
            this.base9Label.AutoSize = true;
            this.base9Label.Location = new System.Drawing.Point(3, 80);
            this.base9Label.Name = "base9Label";
            this.base9Label.Size = new System.Drawing.Size(40, 13);
            this.base9Label.TabIndex = 16;
            this.base9Label.Text = "Base 9";
            // 
            // base8Label
            // 
            this.base8Label.AutoSize = true;
            this.base8Label.Location = new System.Drawing.Point(147, 60);
            this.base8Label.Name = "base8Label";
            this.base8Label.Size = new System.Drawing.Size(40, 13);
            this.base8Label.TabIndex = 14;
            this.base8Label.Text = "Base 8";
            // 
            // base7Label
            // 
            this.base7Label.AutoSize = true;
            this.base7Label.Location = new System.Drawing.Point(3, 60);
            this.base7Label.Name = "base7Label";
            this.base7Label.Size = new System.Drawing.Size(40, 13);
            this.base7Label.TabIndex = 12;
            this.base7Label.Text = "Base 7";
            // 
            // base6Label
            // 
            this.base6Label.AutoSize = true;
            this.base6Label.Location = new System.Drawing.Point(147, 40);
            this.base6Label.Name = "base6Label";
            this.base6Label.Size = new System.Drawing.Size(40, 13);
            this.base6Label.TabIndex = 10;
            this.base6Label.Text = "Base 6";
            // 
            // base5Label
            // 
            this.base5Label.AutoSize = true;
            this.base5Label.Location = new System.Drawing.Point(3, 40);
            this.base5Label.Name = "base5Label";
            this.base5Label.Size = new System.Drawing.Size(40, 13);
            this.base5Label.TabIndex = 8;
            this.base5Label.Text = "Base 5";
            // 
            // base4Label
            // 
            this.base4Label.AutoSize = true;
            this.base4Label.Location = new System.Drawing.Point(147, 20);
            this.base4Label.Name = "base4Label";
            this.base4Label.Size = new System.Drawing.Size(40, 13);
            this.base4Label.TabIndex = 6;
            this.base4Label.Text = "Base 4";
            // 
            // base3Label
            // 
            this.base3Label.AutoSize = true;
            this.base3Label.Location = new System.Drawing.Point(3, 20);
            this.base3Label.Name = "base3Label";
            this.base3Label.Size = new System.Drawing.Size(40, 13);
            this.base3Label.TabIndex = 4;
            this.base3Label.Text = "Base 3";
            // 
            // base2Label
            // 
            this.base2Label.AutoSize = true;
            this.base2Label.Location = new System.Drawing.Point(147, 0);
            this.base2Label.Name = "base2Label";
            this.base2Label.Size = new System.Drawing.Size(40, 13);
            this.base2Label.TabIndex = 2;
            this.base2Label.Text = "Base 2";
            // 
            // base1Label
            // 
            this.base1Label.AutoSize = true;
            this.base1Label.Location = new System.Drawing.Point(3, 0);
            this.base1Label.Name = "base1Label";
            this.base1Label.Size = new System.Drawing.Size(40, 13);
            this.base1Label.TabIndex = 0;
            this.base1Label.Text = "Base 1";
            // 
            // harvestButton
            // 
            this.harvestButton.Location = new System.Drawing.Point(6, 229);
            this.harvestButton.Name = "harvestButton";
            this.harvestButton.Size = new System.Drawing.Size(142, 23);
            this.harvestButton.TabIndex = 10;
            this.harvestButton.Text = "Harvest";
            this.harvestButton.UseVisualStyleBackColor = true;
            this.harvestButton.Click += new System.EventHandler(this.harvestButton_Click);
            // 
            // codenameLabel
            // 
            this.codenameLabel.Location = new System.Drawing.Point(4, 1);
            this.codenameLabel.Name = "codenameLabel";
            this.codenameLabel.Size = new System.Drawing.Size(140, 13);
            this.codenameLabel.TabIndex = 8;
            this.codenameLabel.Text = "Codename: ";
            // 
            // currentStatsGroupBox
            // 
            this.currentStatsGroupBox.Controls.Add(this.statsTableLayoutPanel);
            this.currentStatsGroupBox.Location = new System.Drawing.Point(12, 92);
            this.currentStatsGroupBox.Name = "currentStatsGroupBox";
            this.currentStatsGroupBox.Size = new System.Drawing.Size(305, 201);
            this.currentStatsGroupBox.TabIndex = 9;
            this.currentStatsGroupBox.TabStop = false;
            this.currentStatsGroupBox.Text = "Current Stats";
            // 
            // statsTableLayoutPanel
            // 
            this.statsTableLayoutPanel.AutoSize = true;
            this.statsTableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.statsTableLayoutPanel.ColumnCount = 2;
            this.statsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.statsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.statsTableLayoutPanel.Controls.Add(this.currentExperienceValueLabel, 1, 5);
            this.statsTableLayoutPanel.Controls.Add(this.creditsLabel, 0, 1);
            this.statsTableLayoutPanel.Controls.Add(this.levelValueLabel, 1, 4);
            this.statsTableLayoutPanel.Controls.Add(this.energyCapacityValueLabel, 1, 3);
            this.statsTableLayoutPanel.Controls.Add(this.botCapacityValueLabel, 1, 2);
            this.statsTableLayoutPanel.Controls.Add(this.creditsValueLabel, 1, 1);
            this.statsTableLayoutPanel.Controls.Add(this.codenameValueLabel, 1, 0);
            this.statsTableLayoutPanel.Controls.Add(this.nextLevelExperienceLabel, 0, 6);
            this.statsTableLayoutPanel.Controls.Add(this.currentExperienceLabel, 0, 5);
            this.statsTableLayoutPanel.Controls.Add(this.levelLabel, 0, 4);
            this.statsTableLayoutPanel.Controls.Add(this.energyCapacityLabel, 0, 3);
            this.statsTableLayoutPanel.Controls.Add(this.botCapacityLabel, 0, 2);
            this.statsTableLayoutPanel.Controls.Add(this.codenameLabel, 0, 0);
            this.statsTableLayoutPanel.Controls.Add(this.nextLevelExperienceValueLabel, 1, 6);
            this.statsTableLayoutPanel.Controls.Add(this.zonesValueLabel, 1, 7);
            this.statsTableLayoutPanel.Controls.Add(this.zonesLabel, 0, 7);
            this.statsTableLayoutPanel.Location = new System.Drawing.Point(6, 19);
            this.statsTableLayoutPanel.Name = "statsTableLayoutPanel";
            this.statsTableLayoutPanel.RowCount = 8;
            this.statsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.statsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.statsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.statsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.statsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.statsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.statsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.statsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.statsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.statsTableLayoutPanel.Size = new System.Drawing.Size(295, 169);
            this.statsTableLayoutPanel.TabIndex = 10;
            // 
            // currentExperienceValueLabel
            // 
            this.currentExperienceValueLabel.AutoSize = true;
            this.currentExperienceValueLabel.Location = new System.Drawing.Point(151, 106);
            this.currentExperienceValueLabel.Name = "currentExperienceValueLabel";
            this.currentExperienceValueLabel.Size = new System.Drawing.Size(0, 13);
            this.currentExperienceValueLabel.TabIndex = 13;
            // 
            // creditsLabel
            // 
            this.creditsLabel.Location = new System.Drawing.Point(4, 22);
            this.creditsLabel.Name = "creditsLabel";
            this.creditsLabel.Size = new System.Drawing.Size(140, 13);
            this.creditsLabel.TabIndex = 8;
            this.creditsLabel.Text = "Credits:";
            // 
            // levelValueLabel
            // 
            this.levelValueLabel.AutoSize = true;
            this.levelValueLabel.Location = new System.Drawing.Point(151, 85);
            this.levelValueLabel.Name = "levelValueLabel";
            this.levelValueLabel.Size = new System.Drawing.Size(0, 13);
            this.levelValueLabel.TabIndex = 12;
            // 
            // energyCapacityValueLabel
            // 
            this.energyCapacityValueLabel.AutoSize = true;
            this.energyCapacityValueLabel.Location = new System.Drawing.Point(151, 64);
            this.energyCapacityValueLabel.Name = "energyCapacityValueLabel";
            this.energyCapacityValueLabel.Size = new System.Drawing.Size(0, 13);
            this.energyCapacityValueLabel.TabIndex = 11;
            // 
            // botCapacityValueLabel
            // 
            this.botCapacityValueLabel.AutoSize = true;
            this.botCapacityValueLabel.Location = new System.Drawing.Point(151, 43);
            this.botCapacityValueLabel.Name = "botCapacityValueLabel";
            this.botCapacityValueLabel.Size = new System.Drawing.Size(0, 13);
            this.botCapacityValueLabel.TabIndex = 10;
            // 
            // creditsValueLabel
            // 
            this.creditsValueLabel.AutoSize = true;
            this.creditsValueLabel.Location = new System.Drawing.Point(151, 22);
            this.creditsValueLabel.Name = "creditsValueLabel";
            this.creditsValueLabel.Size = new System.Drawing.Size(0, 13);
            this.creditsValueLabel.TabIndex = 9;
            // 
            // codenameValueLabel
            // 
            this.codenameValueLabel.AutoSize = true;
            this.codenameValueLabel.Location = new System.Drawing.Point(151, 1);
            this.codenameValueLabel.Name = "codenameValueLabel";
            this.codenameValueLabel.Size = new System.Drawing.Size(0, 13);
            this.codenameValueLabel.TabIndex = 8;
            // 
            // nextLevelExperienceLabel
            // 
            this.nextLevelExperienceLabel.AutoSize = true;
            this.nextLevelExperienceLabel.Location = new System.Drawing.Point(4, 127);
            this.nextLevelExperienceLabel.Name = "nextLevelExperienceLabel";
            this.nextLevelExperienceLabel.Size = new System.Drawing.Size(114, 13);
            this.nextLevelExperienceLabel.TabIndex = 5;
            this.nextLevelExperienceLabel.Text = "Experience (To Level):";
            // 
            // currentExperienceLabel
            // 
            this.currentExperienceLabel.AutoSize = true;
            this.currentExperienceLabel.Location = new System.Drawing.Point(4, 106);
            this.currentExperienceLabel.Name = "currentExperienceLabel";
            this.currentExperienceLabel.Size = new System.Drawing.Size(106, 13);
            this.currentExperienceLabel.TabIndex = 4;
            this.currentExperienceLabel.Text = "Experience (Current):";
            // 
            // levelLabel
            // 
            this.levelLabel.AutoSize = true;
            this.levelLabel.Location = new System.Drawing.Point(4, 85);
            this.levelLabel.Name = "levelLabel";
            this.levelLabel.Size = new System.Drawing.Size(36, 13);
            this.levelLabel.TabIndex = 3;
            this.levelLabel.Text = "Level:";
            // 
            // energyCapacityLabel
            // 
            this.energyCapacityLabel.AutoSize = true;
            this.energyCapacityLabel.Location = new System.Drawing.Point(4, 64);
            this.energyCapacityLabel.Name = "energyCapacityLabel";
            this.energyCapacityLabel.Size = new System.Drawing.Size(87, 13);
            this.energyCapacityLabel.TabIndex = 2;
            this.energyCapacityLabel.Text = "Energy Capacity:";
            // 
            // botCapacityLabel
            // 
            this.botCapacityLabel.AutoSize = true;
            this.botCapacityLabel.Location = new System.Drawing.Point(4, 43);
            this.botCapacityLabel.Name = "botCapacityLabel";
            this.botCapacityLabel.Size = new System.Drawing.Size(70, 13);
            this.botCapacityLabel.TabIndex = 1;
            this.botCapacityLabel.Text = "Bot Capacity:";
            // 
            // nextLevelExperienceValueLabel
            // 
            this.nextLevelExperienceValueLabel.AutoSize = true;
            this.nextLevelExperienceValueLabel.Location = new System.Drawing.Point(151, 127);
            this.nextLevelExperienceValueLabel.Name = "nextLevelExperienceValueLabel";
            this.nextLevelExperienceValueLabel.Size = new System.Drawing.Size(0, 13);
            this.nextLevelExperienceValueLabel.TabIndex = 15;
            // 
            // zonesValueLabel
            // 
            this.zonesValueLabel.AutoSize = true;
            this.zonesValueLabel.Location = new System.Drawing.Point(151, 148);
            this.zonesValueLabel.Name = "zonesValueLabel";
            this.zonesValueLabel.Size = new System.Drawing.Size(0, 13);
            this.zonesValueLabel.TabIndex = 16;
            // 
            // zonesLabel
            // 
            this.zonesLabel.AutoSize = true;
            this.zonesLabel.Location = new System.Drawing.Point(4, 148);
            this.zonesLabel.Name = "zonesLabel";
            this.zonesLabel.Size = new System.Drawing.Size(135, 13);
            this.zonesLabel.TabIndex = 6;
            this.zonesLabel.Text = "Zones Captured / Leading:";
            // 
            // zoneGroupBox
            // 
            this.zoneGroupBox.Controls.Add(this.launchStatusLabel);
            this.zoneGroupBox.Controls.Add(this.energyLabel);
            this.zoneGroupBox.Controls.Add(this.autoLaunchCheckBox);
            this.zoneGroupBox.Controls.Add(this.energyProgressBar);
            this.zoneGroupBox.Controls.Add(this.zoneComboBox);
            this.zoneGroupBox.Controls.Add(this.label1);
            this.zoneGroupBox.Controls.Add(this.currentZoneLabel);
            this.zoneGroupBox.Controls.Add(this.botsProgressBar);
            this.zoneGroupBox.Controls.Add(this.launchBotsButton);
            this.zoneGroupBox.Controls.Add(this.botsRegenRateLabel);
            this.zoneGroupBox.Enabled = false;
            this.zoneGroupBox.Location = new System.Drawing.Point(12, 299);
            this.zoneGroupBox.Name = "zoneGroupBox";
            this.zoneGroupBox.Size = new System.Drawing.Size(305, 190);
            this.zoneGroupBox.TabIndex = 10;
            this.zoneGroupBox.TabStop = false;
            this.zoneGroupBox.Text = "Zone";
            // 
            // energyLabel
            // 
            this.energyLabel.AutoSize = true;
            this.energyLabel.Location = new System.Drawing.Point(15, 158);
            this.energyLabel.Name = "energyLabel";
            this.energyLabel.Size = new System.Drawing.Size(43, 13);
            this.energyLabel.TabIndex = 19;
            this.energyLabel.Text = "Energy:";
            // 
            // autoLaunchCheckBox
            // 
            this.autoLaunchCheckBox.Location = new System.Drawing.Point(13, 94);
            this.autoLaunchCheckBox.Name = "autoLaunchCheckBox";
            this.autoLaunchCheckBox.Size = new System.Drawing.Size(92, 17);
            this.autoLaunchCheckBox.TabIndex = 21;
            this.autoLaunchCheckBox.Text = "Auto Launch";
            this.autoLaunchCheckBox.UseVisualStyleBackColor = true;
            // 
            // energyProgressBar
            // 
            this.energyProgressBar.ForeColor = System.Drawing.Color.Lime;
            this.energyProgressBar.Location = new System.Drawing.Point(58, 153);
            this.energyProgressBar.Name = "energyProgressBar";
            this.energyProgressBar.Size = new System.Drawing.Size(243, 24);
            this.energyProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.energyProgressBar.TabIndex = 18;
            // 
            // zoneComboBox
            // 
            this.zoneComboBox.FormattingEnabled = true;
            this.zoneComboBox.Location = new System.Drawing.Point(13, 32);
            this.zoneComboBox.Name = "zoneComboBox";
            this.zoneComboBox.Size = new System.Drawing.Size(288, 21);
            this.zoneComboBox.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Bots:";
            // 
            // currentZoneLabel
            // 
            this.currentZoneLabel.AutoSize = true;
            this.currentZoneLabel.Location = new System.Drawing.Point(13, 16);
            this.currentZoneLabel.Name = "currentZoneLabel";
            this.currentZoneLabel.Size = new System.Drawing.Size(72, 13);
            this.currentZoneLabel.TabIndex = 12;
            this.currentZoneLabel.Text = "Current Zone:";
            // 
            // botsProgressBar
            // 
            this.botsProgressBar.ForeColor = System.Drawing.Color.Lime;
            this.botsProgressBar.Location = new System.Drawing.Point(58, 123);
            this.botsProgressBar.Name = "botsProgressBar";
            this.botsProgressBar.Size = new System.Drawing.Size(243, 24);
            this.botsProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.botsProgressBar.TabIndex = 16;
            // 
            // launchBotsButton
            // 
            this.launchBotsButton.Location = new System.Drawing.Point(13, 59);
            this.launchBotsButton.Name = "launchBotsButton";
            this.launchBotsButton.Size = new System.Drawing.Size(144, 23);
            this.launchBotsButton.TabIndex = 13;
            this.launchBotsButton.Text = "Launch";
            this.launchBotsButton.UseVisualStyleBackColor = true;
            this.launchBotsButton.Click += new System.EventHandler(this.launchBotsButton_Click);
            // 
            // botsRegenRateLabel
            // 
            this.botsRegenRateLabel.Location = new System.Drawing.Point(111, 95);
            this.botsRegenRateLabel.Name = "botsRegenRateLabel";
            this.botsRegenRateLabel.Size = new System.Drawing.Size(190, 13);
            this.botsRegenRateLabel.TabIndex = 15;
            this.botsRegenRateLabel.Text = "[ Bots ] / [ Regeneration Rate ]";
            // 
            // scanAreaComboBox
            // 
            this.scanAreaComboBox.FormattingEnabled = true;
            this.scanAreaComboBox.Location = new System.Drawing.Point(12, 123);
            this.scanAreaComboBox.Name = "scanAreaComboBox";
            this.scanAreaComboBox.Size = new System.Drawing.Size(283, 21);
            this.scanAreaComboBox.TabIndex = 20;
            // 
            // button_Set
            // 
            this.button_Set.Location = new System.Drawing.Point(12, 148);
            this.button_Set.Name = "button_Set";
            this.button_Set.Size = new System.Drawing.Size(136, 23);
            this.button_Set.TabIndex = 19;
            this.button_Set.Text = "Set";
            this.button_Set.UseVisualStyleBackColor = true;
            this.button_Set.Click += new System.EventHandler(this.button_Set_Click);
            // 
            // button_Scan
            // 
            this.button_Scan.Location = new System.Drawing.Point(12, 71);
            this.button_Scan.Name = "button_Scan";
            this.button_Scan.Size = new System.Drawing.Size(136, 23);
            this.button_Scan.TabIndex = 18;
            this.button_Scan.Text = "Scan";
            this.button_Scan.UseVisualStyleBackColor = true;
            this.button_Scan.Click += new System.EventHandler(this.button_Scan_Click);
            // 
            // mapGroupBox
            // 
            this.mapGroupBox.Controls.Add(this.longitudeNumericUpDown);
            this.mapGroupBox.Controls.Add(this.lattitudeNumericUpDown);
            this.mapGroupBox.Controls.Add(this.mapSetStatusLabel);
            this.mapGroupBox.Controls.Add(this.mapScanStatusLabel);
            this.mapGroupBox.Controls.Add(this.label2);
            this.mapGroupBox.Controls.Add(this.longitudeLabel);
            this.mapGroupBox.Controls.Add(this.scanAreaComboBox);
            this.mapGroupBox.Controls.Add(this.resetCoordinatesButton);
            this.mapGroupBox.Controls.Add(this.button_Scan);
            this.mapGroupBox.Controls.Add(this.button_Set);
            this.mapGroupBox.Controls.Add(this.lattitudeLabel);
            this.mapGroupBox.Enabled = false;
            this.mapGroupBox.Location = new System.Drawing.Point(323, 299);
            this.mapGroupBox.Name = "mapGroupBox";
            this.mapGroupBox.Size = new System.Drawing.Size(303, 190);
            this.mapGroupBox.TabIndex = 11;
            this.mapGroupBox.TabStop = false;
            this.mapGroupBox.Text = "Map";
            // 
            // lattitudeNumericUpDown
            // 
            this.lattitudeNumericUpDown.DecimalPlaces = 13;
            this.lattitudeNumericUpDown.Location = new System.Drawing.Point(68, 24);
            this.lattitudeNumericUpDown.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.lattitudeNumericUpDown.Minimum = new decimal(new int[] {
            90,
            0,
            0,
            -2147483648});
            this.lattitudeNumericUpDown.Name = "lattitudeNumericUpDown";
            this.lattitudeNumericUpDown.Size = new System.Drawing.Size(166, 20);
            this.lattitudeNumericUpDown.TabIndex = 25;
            this.lattitudeNumericUpDown.Value = new decimal(new int[] {
            -1403676606,
            110936,
            0,
            851968});
            // 
            // mapSetStatusLabel
            // 
            this.mapSetStatusLabel.Location = new System.Drawing.Point(151, 151);
            this.mapSetStatusLabel.Name = "mapSetStatusLabel";
            this.mapSetStatusLabel.Size = new System.Drawing.Size(146, 17);
            this.mapSetStatusLabel.TabIndex = 24;
            this.mapSetStatusLabel.Text = "Nothing Set";
            this.mapSetStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mapScanStatusLabel
            // 
            this.mapScanStatusLabel.Location = new System.Drawing.Point(151, 74);
            this.mapScanStatusLabel.Name = "mapScanStatusLabel";
            this.mapScanStatusLabel.Size = new System.Drawing.Size(146, 17);
            this.mapScanStatusLabel.TabIndex = 23;
            this.mapScanStatusLabel.Text = "Nothing Scanned";
            this.mapScanStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Select Destination:";
            // 
            // longitudeLabel
            // 
            this.longitudeLabel.AutoSize = true;
            this.longitudeLabel.Location = new System.Drawing.Point(9, 48);
            this.longitudeLabel.Name = "longitudeLabel";
            this.longitudeLabel.Size = new System.Drawing.Size(57, 13);
            this.longitudeLabel.TabIndex = 21;
            this.longitudeLabel.Text = "Longitude:";
            // 
            // resetCoordinatesButton
            // 
            this.resetCoordinatesButton.Location = new System.Drawing.Point(240, 18);
            this.resetCoordinatesButton.Name = "resetCoordinatesButton";
            this.resetCoordinatesButton.Size = new System.Drawing.Size(55, 46);
            this.resetCoordinatesButton.TabIndex = 18;
            this.resetCoordinatesButton.Text = "Reset";
            this.resetCoordinatesButton.UseVisualStyleBackColor = true;
            this.resetCoordinatesButton.Click += new System.EventHandler(this.resetCoordinatesButton_Click);
            // 
            // lattitudeLabel
            // 
            this.lattitudeLabel.AutoSize = true;
            this.lattitudeLabel.Location = new System.Drawing.Point(9, 26);
            this.lattitudeLabel.Name = "lattitudeLabel";
            this.lattitudeLabel.Size = new System.Drawing.Size(48, 13);
            this.lattitudeLabel.TabIndex = 2;
            this.lattitudeLabel.Text = "Latitude:";
            // 
            // minuteUpdateTimer
            // 
            this.tenMinuteUpdateTimer.Enabled = false;
            this.tenMinuteUpdateTimer.Interval = 600000;
            this.tenMinuteUpdateTimer.Tick += new System.EventHandler(this.tenMinuteUpdateTimer_Tick);
            // 
            // longitudeNumericUpDown
            // 
            this.longitudeNumericUpDown.DecimalPlaces = 13;
            this.longitudeNumericUpDown.Location = new System.Drawing.Point(68, 46);
            this.longitudeNumericUpDown.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.longitudeNumericUpDown.Minimum = new decimal(new int[] {
            180,
            0,
            0,
            -2147483648});
            this.longitudeNumericUpDown.Name = "longitudeNumericUpDown";
            this.longitudeNumericUpDown.Size = new System.Drawing.Size(166, 20);
            this.longitudeNumericUpDown.TabIndex = 25;
            this.longitudeNumericUpDown.Value = new decimal(new int[] {
            2048488705,
            28436,
            0,
            -2146697216});
            // 
            // secondUpdateTimer
            // 
            this.secondUpdateTimer.Enabled = true;
            this.secondUpdateTimer.Interval = 1000;
            this.secondUpdateTimer.Tick += new System.EventHandler(this.secondUpdateTimer_Tick);
            // 
            // launchStatusLabel
            // 
            this.launchStatusLabel.Location = new System.Drawing.Point(163, 62);
            this.launchStatusLabel.Name = "launchStatusLabel";
            this.launchStatusLabel.Size = new System.Drawing.Size(138, 17);
            this.launchStatusLabel.TabIndex = 24;
            this.launchStatusLabel.Text = "Nothing Launched";
            this.launchStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ConquererForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 496);
            this.Controls.Add(this.mapGroupBox);
            this.Controls.Add(this.zoneGroupBox);
            this.Controls.Add(this.currentStatsGroupBox);
            this.Controls.Add(this.basesGroupBox);
            this.Controls.Add(this.loginGroupBox);
            this.Name = "ConquererForm";
            this.Text = "Qonquerer";
            this.loginGroupBox.ResumeLayout(false);
            this.loginGroupBox.PerformLayout();
            this.basesGroupBox.ResumeLayout(false);
            this.basesTableLayoutPanel.ResumeLayout(false);
            this.basesTableLayoutPanel.PerformLayout();
            this.currentStatsGroupBox.ResumeLayout(false);
            this.currentStatsGroupBox.PerformLayout();
            this.statsTableLayoutPanel.ResumeLayout(false);
            this.statsTableLayoutPanel.PerformLayout();
            this.zoneGroupBox.ResumeLayout(false);
            this.zoneGroupBox.PerformLayout();
            this.mapGroupBox.ResumeLayout(false);
            this.mapGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lattitudeNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.longitudeNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TextBox passwordTextBox;
        public System.Windows.Forms.TextBox usernameTextBox;
        public System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.GroupBox loginGroupBox;
        private System.Windows.Forms.Label loginLabel;
        private System.Windows.Forms.GroupBox basesGroupBox;
        private System.Windows.Forms.TableLayoutPanel basesTableLayoutPanel;
        public System.Windows.Forms.Label base20Label;
        public System.Windows.Forms.Label base19Label;
        public System.Windows.Forms.Label base18Label;
        public System.Windows.Forms.Label base17Label;
        public System.Windows.Forms.Label base16Label;
        public System.Windows.Forms.Label base15Label;
        public System.Windows.Forms.Label base14Label;
        public System.Windows.Forms.Label base13Label;
        public System.Windows.Forms.Label base12Label;
        public System.Windows.Forms.Label base11Label;
        public System.Windows.Forms.Label base10Label;
        public System.Windows.Forms.Label base9Label;
        public System.Windows.Forms.Label base8Label;
        public System.Windows.Forms.Label base7Label;
        public System.Windows.Forms.Label base6Label;
        public System.Windows.Forms.Label base5Label;
        public System.Windows.Forms.Label base4Label;
        public System.Windows.Forms.Label base3Label;
        public System.Windows.Forms.Label base2Label;
        public System.Windows.Forms.Label base1Label;
        public System.Windows.Forms.Label codenameLabel;
        private System.Windows.Forms.GroupBox currentStatsGroupBox;
        public System.Windows.Forms.Label creditsLabel;
        private System.Windows.Forms.Label creditsEarnedLabel;
        public System.Windows.Forms.Button harvestButton;
        public System.Windows.Forms.CheckBox autoHarvestCheckbox;
        private System.Windows.Forms.TableLayoutPanel statsTableLayoutPanel;
        public System.Windows.Forms.Label currentExperienceValueLabel;
        public System.Windows.Forms.Label levelValueLabel;
        public System.Windows.Forms.Label energyCapacityValueLabel;
        public System.Windows.Forms.Label botCapacityValueLabel;
        public System.Windows.Forms.Label creditsValueLabel;
        public System.Windows.Forms.Label codenameValueLabel;
        private System.Windows.Forms.Label botCapacityLabel;
        private System.Windows.Forms.Label energyCapacityLabel;
        private System.Windows.Forms.Label levelLabel;
        private System.Windows.Forms.Label currentExperienceLabel;
        private System.Windows.Forms.Label nextLevelExperienceLabel;
        private System.Windows.Forms.Label zonesLabel;
        private System.Windows.Forms.GroupBox zoneGroupBox;
        private System.Windows.Forms.Label energyLabel;
        public System.Windows.Forms.CheckBox autoLaunchCheckBox;
        public System.Windows.Forms.ProgressBar energyProgressBar;
        public System.Windows.Forms.ComboBox zoneComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label currentZoneLabel;
        public System.Windows.Forms.ProgressBar botsProgressBar;
        public System.Windows.Forms.Button launchBotsButton;
        public System.Windows.Forms.Label botsRegenRateLabel;
        public System.Windows.Forms.ComboBox scanAreaComboBox;
        public System.Windows.Forms.Button button_Set;
        public System.Windows.Forms.Button button_Scan;
        private System.Windows.Forms.GroupBox mapGroupBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label longitudeLabel;
        private System.Windows.Forms.Label lattitudeLabel;
        public System.Windows.Forms.Button resetCoordinatesButton;
        private System.Windows.Forms.Label baseStatusLabel;
        private System.Windows.Forms.Timer tenMinuteUpdateTimer;
        public System.Windows.Forms.Label nextLevelExperienceValueLabel;
        public System.Windows.Forms.Label zonesValueLabel;
        private System.Windows.Forms.Label mapScanStatusLabel;
        private System.Windows.Forms.Label mapSetStatusLabel;
        private System.Windows.Forms.NumericUpDown lattitudeNumericUpDown;
        private System.Windows.Forms.NumericUpDown longitudeNumericUpDown;
        private System.Windows.Forms.Timer secondUpdateTimer;
        private System.Windows.Forms.Label launchStatusLabel;
    }
}
