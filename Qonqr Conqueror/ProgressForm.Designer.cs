namespace Qonqr
{
    using System.Windows.Forms;

    /// <summary>
    /// The partial class for ProgressForm
    /// </summary>
    public partial class ProgressForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Tool Tips
        /// </summary>
        private ToolTip toolTip;

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
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panel_login = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label_var_Zones = new System.Windows.Forms.Label();
            this.label_var_ExperienceToLevel = new System.Windows.Forms.Label();
            this.label_var_ExperienceCurrent = new System.Windows.Forms.Label();
            this.label_var_Level = new System.Windows.Forms.Label();
            this.label_var_EnergyCapacity = new System.Windows.Forms.Label();
            this.label_var_BotCapacity = new System.Windows.Forms.Label();
            this.label_BotCapacity = new System.Windows.Forms.Label();
            this.label_EnergyCapacity = new System.Windows.Forms.Label();
            this.label_Level = new System.Windows.Forms.Label();
            this.label_ExperienceCurrent = new System.Windows.Forms.Label();
            this.label_ExperienceToLevel = new System.Windows.Forms.Label();
            this.label_Zones = new System.Windows.Forms.Label();
            this.button_Login = new System.Windows.Forms.Button();
            this.label_loginStatus = new System.Windows.Forms.Label();
            this.textBox_PasswordField = new System.Windows.Forms.TextBox();
            this.textBox_UsernameField = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.LoginTab = new System.Windows.Forms.TabPage();
            this.FortsTab = new System.Windows.Forms.TabPage();
            this.autoHarvestCheckbox = new System.Windows.Forms.CheckBox();
            this.label_var_qreditsEarnedFromHarvest = new System.Windows.Forms.Label();
            this.button_HarvestAll = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label_var_fort20 = new System.Windows.Forms.Label();
            this.label_var_fort19 = new System.Windows.Forms.Label();
            this.label_var_fort18 = new System.Windows.Forms.Label();
            this.label_var_fort17 = new System.Windows.Forms.Label();
            this.label_var_fort16 = new System.Windows.Forms.Label();
            this.label_var_fort15 = new System.Windows.Forms.Label();
            this.label_var_fort14 = new System.Windows.Forms.Label();
            this.label_var_fort13 = new System.Windows.Forms.Label();
            this.label_var_fort12 = new System.Windows.Forms.Label();
            this.label_var_fort11 = new System.Windows.Forms.Label();
            this.label_var_fort10 = new System.Windows.Forms.Label();
            this.label_var_fort9 = new System.Windows.Forms.Label();
            this.label_var_fort8 = new System.Windows.Forms.Label();
            this.label_var_fort7 = new System.Windows.Forms.Label();
            this.label_var_fort6 = new System.Windows.Forms.Label();
            this.label_var_fort5 = new System.Windows.Forms.Label();
            this.label_var_fort4 = new System.Windows.Forms.Label();
            this.label_var_fort3 = new System.Windows.Forms.Label();
            this.label_var_fort2 = new System.Windows.Forms.Label();
            this.label_var_fort1 = new System.Windows.Forms.Label();
            this.AttackTab = new System.Windows.Forms.TabPage();
            this.comboBox_scanArea = new System.Windows.Forms.ComboBox();
            this.button_Set = new System.Windows.Forms.Button();
            this.button_Scan = new System.Windows.Forms.Button();
            this.textBox_var_Longitude = new System.Windows.Forms.TextBox();
            this.textBox_var_Latitude = new System.Windows.Forms.TextBox();
            this.label_var_botsRegenRate = new System.Windows.Forms.Label();
            this.progressBar_nanobots = new System.Windows.Forms.ProgressBar();
            this.button_LaunchAttack = new System.Windows.Forms.Button();
            this.label_WhoAreYouAttacking = new System.Windows.Forms.Label();
            this.comboBox_attack = new System.Windows.Forms.ComboBox();
            this.label_codename_qredits = new System.Windows.Forms.Label();
            this.panel_login.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.LoginTab.SuspendLayout();
            this.FortsTab.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.AttackTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_login
            // 
            this.panel_login.AutoSize = true;
            this.panel_login.Controls.Add(this.tableLayoutPanel1);
            this.panel_login.Controls.Add(this.button_Login);
            this.panel_login.Controls.Add(this.label_loginStatus);
            this.panel_login.Controls.Add(this.textBox_PasswordField);
            this.panel_login.Controls.Add(this.textBox_UsernameField);
            this.panel_login.Location = new System.Drawing.Point(0, 6);
            this.panel_login.Name = "panel_login";
            this.panel_login.Size = new System.Drawing.Size(302, 214);
            this.panel_login.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label_var_Zones, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label_var_ExperienceToLevel, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label_var_ExperienceCurrent, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label_var_Level, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label_var_EnergyCapacity, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label_var_BotCapacity, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_BotCapacity, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_EnergyCapacity, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label_Level, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label_ExperienceCurrent, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label_ExperienceToLevel, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label_Zones, 0, 5);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 79);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(279, 127);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // label_var_Zones
            // 
            this.label_var_Zones.AutoSize = true;
            this.label_var_Zones.Location = new System.Drawing.Point(143, 106);
            this.label_var_Zones.Name = "label_var_Zones";
            this.label_var_Zones.Size = new System.Drawing.Size(0, 13);
            this.label_var_Zones.TabIndex = 13;
            // 
            // label_var_ExperienceToLevel
            // 
            this.label_var_ExperienceToLevel.AutoSize = true;
            this.label_var_ExperienceToLevel.Location = new System.Drawing.Point(143, 85);
            this.label_var_ExperienceToLevel.Name = "label_var_ExperienceToLevel";
            this.label_var_ExperienceToLevel.Size = new System.Drawing.Size(0, 13);
            this.label_var_ExperienceToLevel.TabIndex = 12;
            // 
            // label_var_ExperienceCurrent
            // 
            this.label_var_ExperienceCurrent.AutoSize = true;
            this.label_var_ExperienceCurrent.Location = new System.Drawing.Point(143, 64);
            this.label_var_ExperienceCurrent.Name = "label_var_ExperienceCurrent";
            this.label_var_ExperienceCurrent.Size = new System.Drawing.Size(0, 13);
            this.label_var_ExperienceCurrent.TabIndex = 11;
            // 
            // label_var_Level
            // 
            this.label_var_Level.AutoSize = true;
            this.label_var_Level.Location = new System.Drawing.Point(143, 43);
            this.label_var_Level.Name = "label_var_Level";
            this.label_var_Level.Size = new System.Drawing.Size(0, 13);
            this.label_var_Level.TabIndex = 10;
            // 
            // label_var_EnergyCapacity
            // 
            this.label_var_EnergyCapacity.AutoSize = true;
            this.label_var_EnergyCapacity.Location = new System.Drawing.Point(143, 22);
            this.label_var_EnergyCapacity.Name = "label_var_EnergyCapacity";
            this.label_var_EnergyCapacity.Size = new System.Drawing.Size(0, 13);
            this.label_var_EnergyCapacity.TabIndex = 9;
            // 
            // label_var_BotCapacity
            // 
            this.label_var_BotCapacity.AutoSize = true;
            this.label_var_BotCapacity.Location = new System.Drawing.Point(143, 1);
            this.label_var_BotCapacity.Name = "label_var_BotCapacity";
            this.label_var_BotCapacity.Size = new System.Drawing.Size(0, 13);
            this.label_var_BotCapacity.TabIndex = 8;
            // 
            // label_BotCapacity
            // 
            this.label_BotCapacity.AutoSize = true;
            this.label_BotCapacity.Location = new System.Drawing.Point(4, 1);
            this.label_BotCapacity.Name = "label_BotCapacity";
            this.label_BotCapacity.Size = new System.Drawing.Size(67, 13);
            this.label_BotCapacity.TabIndex = 1;
            this.label_BotCapacity.Text = "Bot Capacity";
            // 
            // label_EnergyCapacity
            // 
            this.label_EnergyCapacity.AutoSize = true;
            this.label_EnergyCapacity.Location = new System.Drawing.Point(4, 22);
            this.label_EnergyCapacity.Name = "label_EnergyCapacity";
            this.label_EnergyCapacity.Size = new System.Drawing.Size(84, 13);
            this.label_EnergyCapacity.TabIndex = 2;
            this.label_EnergyCapacity.Text = "Energy Capacity";
            // 
            // label_Level
            // 
            this.label_Level.AutoSize = true;
            this.label_Level.Location = new System.Drawing.Point(4, 43);
            this.label_Level.Name = "label_Level";
            this.label_Level.Size = new System.Drawing.Size(33, 13);
            this.label_Level.TabIndex = 3;
            this.label_Level.Text = "Level";
            // 
            // label_ExperienceCurrent
            // 
            this.label_ExperienceCurrent.AutoSize = true;
            this.label_ExperienceCurrent.Location = new System.Drawing.Point(4, 64);
            this.label_ExperienceCurrent.Name = "label_ExperienceCurrent";
            this.label_ExperienceCurrent.Size = new System.Drawing.Size(103, 13);
            this.label_ExperienceCurrent.TabIndex = 4;
            this.label_ExperienceCurrent.Text = "Experience (Current)";
            // 
            // label_ExperienceToLevel
            // 
            this.label_ExperienceToLevel.AutoSize = true;
            this.label_ExperienceToLevel.Location = new System.Drawing.Point(4, 85);
            this.label_ExperienceToLevel.Name = "label_ExperienceToLevel";
            this.label_ExperienceToLevel.Size = new System.Drawing.Size(111, 13);
            this.label_ExperienceToLevel.TabIndex = 5;
            this.label_ExperienceToLevel.Text = "Experience (To Level)";
            // 
            // label_Zones
            // 
            this.label_Zones.AutoSize = true;
            this.label_Zones.Location = new System.Drawing.Point(4, 106);
            this.label_Zones.Name = "label_Zones";
            this.label_Zones.Size = new System.Drawing.Size(132, 13);
            this.label_Zones.TabIndex = 6;
            this.label_Zones.Text = "Zones Captured / Leading";
            // 
            // button_Login
            // 
            this.button_Login.Location = new System.Drawing.Point(176, 40);
            this.button_Login.Name = "button_Login";
            this.button_Login.Size = new System.Drawing.Size(75, 23);
            this.button_Login.TabIndex = 2;
            this.button_Login.Text = "Log In";
            this.button_Login.UseVisualStyleBackColor = true;
            // 
            // label_loginStatus
            // 
            this.label_loginStatus.AutoSize = true;
            this.label_loginStatus.Location = new System.Drawing.Point(176, 14);
            this.label_loginStatus.Name = "label_loginStatus";
            this.label_loginStatus.Size = new System.Drawing.Size(75, 13);
            this.label_loginStatus.TabIndex = 2;
            this.label_loginStatus.Text = "Not Logged In";
            // 
            // textBox_PasswordField
            // 
            this.textBox_PasswordField.Location = new System.Drawing.Point(12, 43);
            this.textBox_PasswordField.Name = "textBox_PasswordField";
            this.textBox_PasswordField.PasswordChar = '●';
            this.textBox_PasswordField.Size = new System.Drawing.Size(100, 20);
            this.textBox_PasswordField.TabIndex = 1;
            this.textBox_PasswordField.Text = "Password";
            // 
            // textBox_UsernameField
            // 
            this.textBox_UsernameField.Location = new System.Drawing.Point(12, 14);
            this.textBox_UsernameField.Name = "textBox_UsernameField";
            this.textBox_UsernameField.Size = new System.Drawing.Size(100, 20);
            this.textBox_UsernameField.TabIndex = 0;
            this.textBox_UsernameField.Text = "Username";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.LoginTab);
            this.tabControl1.Controls.Add(this.FortsTab);
            this.tabControl1.Controls.Add(this.AttackTab);
            this.tabControl1.Location = new System.Drawing.Point(12, 9);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(311, 289);
            this.tabControl1.TabIndex = 1;
            // 
            // LoginTab
            // 
            this.LoginTab.Controls.Add(this.panel_login);
            this.LoginTab.Location = new System.Drawing.Point(4, 22);
            this.LoginTab.Name = "LoginTab";
            this.LoginTab.Padding = new System.Windows.Forms.Padding(3);
            this.LoginTab.Size = new System.Drawing.Size(303, 263);
            this.LoginTab.TabIndex = 0;
            this.LoginTab.Text = "Login";
            this.LoginTab.UseVisualStyleBackColor = true;
            // 
            // FortsTab
            // 
            this.FortsTab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.FortsTab.Controls.Add(this.autoHarvestCheckbox);
            this.FortsTab.Controls.Add(this.label_var_qreditsEarnedFromHarvest);
            this.FortsTab.Controls.Add(this.button_HarvestAll);
            this.FortsTab.Controls.Add(this.tableLayoutPanel2);
            this.FortsTab.Location = new System.Drawing.Point(4, 22);
            this.FortsTab.Name = "FortsTab";
            this.FortsTab.Padding = new System.Windows.Forms.Padding(3);
            this.FortsTab.Size = new System.Drawing.Size(303, 263);
            this.FortsTab.TabIndex = 1;
            this.FortsTab.Text = "Forts";
            this.FortsTab.UseVisualStyleBackColor = true;
            // 
            // autoHarvestCheckbox
            // 
            this.autoHarvestCheckbox.AutoSize = true;
            this.autoHarvestCheckbox.Location = new System.Drawing.Point(136, 229);
            this.autoHarvestCheckbox.Name = "autoHarvestCheckbox";
            this.autoHarvestCheckbox.Size = new System.Drawing.Size(88, 17);
            this.autoHarvestCheckbox.TabIndex = 5;
            this.autoHarvestCheckbox.Text = "Auto Harvest";
            this.autoHarvestCheckbox.UseVisualStyleBackColor = true;
            // 
            // label_var_qreditsEarnedFromHarvest
            // 
            this.label_var_qreditsEarnedFromHarvest.Location = new System.Drawing.Point(230, 217);
            this.label_var_qreditsEarnedFromHarvest.Name = "label_var_qreditsEarnedFromHarvest";
            this.label_var_qreditsEarnedFromHarvest.Size = new System.Drawing.Size(66, 40);
            this.label_var_qreditsEarnedFromHarvest.TabIndex = 4;
            this.label_var_qreditsEarnedFromHarvest.Text = "Qredits Earned: 0";
            // 
            // button_HarvestAll
            // 
            this.button_HarvestAll.Location = new System.Drawing.Point(7, 216);
            this.button_HarvestAll.Name = "button_HarvestAll";
            this.button_HarvestAll.Size = new System.Drawing.Size(118, 41);
            this.button_HarvestAll.TabIndex = 3;
            this.button_HarvestAll.Text = "Harvest All";
            this.button_HarvestAll.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.label_var_fort20, 1, 9);
            this.tableLayoutPanel2.Controls.Add(this.label_var_fort19, 0, 9);
            this.tableLayoutPanel2.Controls.Add(this.label_var_fort18, 1, 8);
            this.tableLayoutPanel2.Controls.Add(this.label_var_fort17, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.label_var_fort16, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.label_var_fort15, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.label_var_fort14, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.label_var_fort13, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.label_var_fort12, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.label_var_fort11, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.label_var_fort10, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.label_var_fort9, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.label_var_fort8, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.label_var_fort7, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label_var_fort6, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label_var_fort5, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label_var_fort4, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label_var_fort3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label_var_fort2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label_var_fort1, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(7, 6);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 11;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(289, 204);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // label_var_fort20
            // 
            this.label_var_fort20.AutoSize = true;
            this.label_var_fort20.Location = new System.Drawing.Point(147, 180);
            this.label_var_fort20.Name = "label_var_fort20";
            this.label_var_fort20.Size = new System.Drawing.Size(40, 13);
            this.label_var_fort20.TabIndex = 19;
            this.label_var_fort20.Text = "Fort 20";
            // 
            // label_var_fort19
            // 
            this.label_var_fort19.AutoSize = true;
            this.label_var_fort19.Location = new System.Drawing.Point(3, 180);
            this.label_var_fort19.Name = "label_var_fort19";
            this.label_var_fort19.Size = new System.Drawing.Size(40, 13);
            this.label_var_fort19.TabIndex = 17;
            this.label_var_fort19.Text = "Fort 19";
            // 
            // label_var_fort18
            // 
            this.label_var_fort18.AutoSize = true;
            this.label_var_fort18.Location = new System.Drawing.Point(147, 160);
            this.label_var_fort18.Name = "label_var_fort18";
            this.label_var_fort18.Size = new System.Drawing.Size(40, 13);
            this.label_var_fort18.TabIndex = 15;
            this.label_var_fort18.Text = "Fort 18";
            // 
            // label_var_fort17
            // 
            this.label_var_fort17.AutoSize = true;
            this.label_var_fort17.Location = new System.Drawing.Point(3, 160);
            this.label_var_fort17.Name = "label_var_fort17";
            this.label_var_fort17.Size = new System.Drawing.Size(40, 13);
            this.label_var_fort17.TabIndex = 13;
            this.label_var_fort17.Text = "Fort 17";
            // 
            // label_var_fort16
            // 
            this.label_var_fort16.AutoSize = true;
            this.label_var_fort16.Location = new System.Drawing.Point(147, 140);
            this.label_var_fort16.Name = "label_var_fort16";
            this.label_var_fort16.Size = new System.Drawing.Size(40, 13);
            this.label_var_fort16.TabIndex = 11;
            this.label_var_fort16.Text = "Fort 16";
            // 
            // label_var_fort15
            // 
            this.label_var_fort15.AutoSize = true;
            this.label_var_fort15.Location = new System.Drawing.Point(3, 140);
            this.label_var_fort15.Name = "label_var_fort15";
            this.label_var_fort15.Size = new System.Drawing.Size(40, 13);
            this.label_var_fort15.TabIndex = 9;
            this.label_var_fort15.Text = "Fort 15";
            // 
            // label_var_fort14
            // 
            this.label_var_fort14.AutoSize = true;
            this.label_var_fort14.Location = new System.Drawing.Point(147, 120);
            this.label_var_fort14.Name = "label_var_fort14";
            this.label_var_fort14.Size = new System.Drawing.Size(40, 13);
            this.label_var_fort14.TabIndex = 7;
            this.label_var_fort14.Text = "Fort 14";
            // 
            // label_var_fort13
            // 
            this.label_var_fort13.AutoSize = true;
            this.label_var_fort13.Location = new System.Drawing.Point(3, 120);
            this.label_var_fort13.Name = "label_var_fort13";
            this.label_var_fort13.Size = new System.Drawing.Size(40, 13);
            this.label_var_fort13.TabIndex = 5;
            this.label_var_fort13.Text = "Fort 13";
            // 
            // label_var_fort12
            // 
            this.label_var_fort12.AutoSize = true;
            this.label_var_fort12.Location = new System.Drawing.Point(147, 100);
            this.label_var_fort12.Name = "label_var_fort12";
            this.label_var_fort12.Size = new System.Drawing.Size(40, 13);
            this.label_var_fort12.TabIndex = 3;
            this.label_var_fort12.Text = "Fort 12";
            // 
            // label_var_fort11
            // 
            this.label_var_fort11.AutoSize = true;
            this.label_var_fort11.Location = new System.Drawing.Point(3, 100);
            this.label_var_fort11.Name = "label_var_fort11";
            this.label_var_fort11.Size = new System.Drawing.Size(40, 13);
            this.label_var_fort11.TabIndex = 1;
            this.label_var_fort11.Text = "Fort 11";
            // 
            // label_var_fort10
            // 
            this.label_var_fort10.AutoSize = true;
            this.label_var_fort10.Location = new System.Drawing.Point(147, 80);
            this.label_var_fort10.Name = "label_var_fort10";
            this.label_var_fort10.Size = new System.Drawing.Size(40, 13);
            this.label_var_fort10.TabIndex = 18;
            this.label_var_fort10.Text = "Fort 10";
            // 
            // label_var_fort9
            // 
            this.label_var_fort9.AutoSize = true;
            this.label_var_fort9.Location = new System.Drawing.Point(3, 80);
            this.label_var_fort9.Name = "label_var_fort9";
            this.label_var_fort9.Size = new System.Drawing.Size(34, 13);
            this.label_var_fort9.TabIndex = 16;
            this.label_var_fort9.Text = "Fort 9";
            // 
            // label_var_fort8
            // 
            this.label_var_fort8.AutoSize = true;
            this.label_var_fort8.Location = new System.Drawing.Point(147, 60);
            this.label_var_fort8.Name = "label_var_fort8";
            this.label_var_fort8.Size = new System.Drawing.Size(34, 13);
            this.label_var_fort8.TabIndex = 14;
            this.label_var_fort8.Text = "Fort 8";
            // 
            // label_var_fort7
            // 
            this.label_var_fort7.AutoSize = true;
            this.label_var_fort7.Location = new System.Drawing.Point(3, 60);
            this.label_var_fort7.Name = "label_var_fort7";
            this.label_var_fort7.Size = new System.Drawing.Size(34, 13);
            this.label_var_fort7.TabIndex = 12;
            this.label_var_fort7.Text = "Fort 7";
            // 
            // label_var_fort6
            // 
            this.label_var_fort6.AutoSize = true;
            this.label_var_fort6.Location = new System.Drawing.Point(147, 40);
            this.label_var_fort6.Name = "label_var_fort6";
            this.label_var_fort6.Size = new System.Drawing.Size(34, 13);
            this.label_var_fort6.TabIndex = 10;
            this.label_var_fort6.Text = "Fort 6";
            // 
            // label_var_fort5
            // 
            this.label_var_fort5.AutoSize = true;
            this.label_var_fort5.Location = new System.Drawing.Point(3, 40);
            this.label_var_fort5.Name = "label_var_fort5";
            this.label_var_fort5.Size = new System.Drawing.Size(34, 13);
            this.label_var_fort5.TabIndex = 8;
            this.label_var_fort5.Text = "Fort 5";
            // 
            // label_var_fort4
            // 
            this.label_var_fort4.AutoSize = true;
            this.label_var_fort4.Location = new System.Drawing.Point(147, 20);
            this.label_var_fort4.Name = "label_var_fort4";
            this.label_var_fort4.Size = new System.Drawing.Size(34, 13);
            this.label_var_fort4.TabIndex = 6;
            this.label_var_fort4.Text = "Fort 4";
            // 
            // label_var_fort3
            // 
            this.label_var_fort3.AutoSize = true;
            this.label_var_fort3.Location = new System.Drawing.Point(3, 20);
            this.label_var_fort3.Name = "label_var_fort3";
            this.label_var_fort3.Size = new System.Drawing.Size(34, 13);
            this.label_var_fort3.TabIndex = 4;
            this.label_var_fort3.Text = "Fort 3";
            // 
            // label_var_fort2
            // 
            this.label_var_fort2.AutoSize = true;
            this.label_var_fort2.Location = new System.Drawing.Point(147, 0);
            this.label_var_fort2.Name = "label_var_fort2";
            this.label_var_fort2.Size = new System.Drawing.Size(34, 13);
            this.label_var_fort2.TabIndex = 2;
            this.label_var_fort2.Text = "Fort 2";
            // 
            // label_var_fort1
            // 
            this.label_var_fort1.AutoSize = true;
            this.label_var_fort1.Location = new System.Drawing.Point(3, 0);
            this.label_var_fort1.Name = "label_var_fort1";
            this.label_var_fort1.Size = new System.Drawing.Size(31, 13);
            this.label_var_fort1.TabIndex = 0;
            this.label_var_fort1.Text = "Fort1";
            // 
            // AttackTab
            // 
            this.AttackTab.Controls.Add(this.comboBox_scanArea);
            this.AttackTab.Controls.Add(this.button_Set);
            this.AttackTab.Controls.Add(this.button_Scan);
            this.AttackTab.Controls.Add(this.textBox_var_Longitude);
            this.AttackTab.Controls.Add(this.textBox_var_Latitude);
            this.AttackTab.Controls.Add(this.label_var_botsRegenRate);
            this.AttackTab.Controls.Add(this.progressBar_nanobots);
            this.AttackTab.Controls.Add(this.button_LaunchAttack);
            this.AttackTab.Controls.Add(this.label_WhoAreYouAttacking);
            this.AttackTab.Controls.Add(this.comboBox_attack);
            this.AttackTab.Location = new System.Drawing.Point(4, 22);
            this.AttackTab.Name = "AttackTab";
            this.AttackTab.Padding = new System.Windows.Forms.Padding(3);
            this.AttackTab.Size = new System.Drawing.Size(303, 263);
            this.AttackTab.TabIndex = 2;
            this.AttackTab.Text = "Attack";
            this.AttackTab.UseVisualStyleBackColor = true;
            // 
            // comboBox_scanArea
            // 
            this.comboBox_scanArea.FormattingEnabled = true;
            this.comboBox_scanArea.Location = new System.Drawing.Point(23, 224);
            this.comboBox_scanArea.Name = "comboBox_scanArea";
            this.comboBox_scanArea.Size = new System.Drawing.Size(257, 21);
            this.comboBox_scanArea.TabIndex = 10;
            // 
            // button_Set
            // 
            this.button_Set.Location = new System.Drawing.Point(215, 162);
            this.button_Set.Name = "button_Set";
            this.button_Set.Size = new System.Drawing.Size(65, 46);
            this.button_Set.TabIndex = 9;
            this.button_Set.Text = "Set";
            this.button_Set.UseVisualStyleBackColor = true;
            // 
            // button_Scan
            // 
            this.button_Scan.Location = new System.Drawing.Point(144, 162);
            this.button_Scan.Name = "button_Scan";
            this.button_Scan.Size = new System.Drawing.Size(65, 46);
            this.button_Scan.TabIndex = 7;
            this.button_Scan.Text = "Scan";
            this.button_Scan.UseVisualStyleBackColor = true;
            // 
            // textBox_var_Longitude
            // 
            this.textBox_var_Longitude.Location = new System.Drawing.Point(23, 188);
            this.textBox_var_Longitude.Name = "textBox_var_Longitude";
            this.textBox_var_Longitude.Size = new System.Drawing.Size(115, 20);
            this.textBox_var_Longitude.TabIndex = 6;
            this.textBox_var_Longitude.Text = "Longitude: -122.133738517761";
            // 
            // textBox_var_Latitude
            // 
            this.textBox_var_Latitude.Location = new System.Drawing.Point(23, 162);
            this.textBox_var_Latitude.Name = "textBox_var_Latitude";
            this.textBox_var_Latitude.Size = new System.Drawing.Size(115, 20);
            this.textBox_var_Latitude.TabIndex = 5;
            this.textBox_var_Latitude.Text = "Latitude: 47.6469383239746";
            // 
            // label_var_botsRegenRate
            // 
            this.label_var_botsRegenRate.AutoSize = true;
            this.label_var_botsRegenRate.Location = new System.Drawing.Point(23, 81);
            this.label_var_botsRegenRate.Name = "label_var_botsRegenRate";
            this.label_var_botsRegenRate.Size = new System.Drawing.Size(153, 13);
            this.label_var_botsRegenRate.TabIndex = 4;
            this.label_var_botsRegenRate.Text = "[ Bots ] / [ Regeneration Rate ]";
            // 
            // progressBar_nanobots
            // 
            this.progressBar_nanobots.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBar_nanobots.ForeColor = System.Drawing.Color.Lime;
            this.progressBar_nanobots.Location = new System.Drawing.Point(3, 3);
            this.progressBar_nanobots.Name = "progressBar_nanobots";
            this.progressBar_nanobots.Size = new System.Drawing.Size(297, 24);
            this.progressBar_nanobots.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar_nanobots.TabIndex = 3;
            // 
            // button_LaunchAttack
            // 
            this.button_LaunchAttack.Location = new System.Drawing.Point(23, 107);
            this.button_LaunchAttack.Name = "button_LaunchAttack";
            this.button_LaunchAttack.Size = new System.Drawing.Size(257, 37);
            this.button_LaunchAttack.TabIndex = 2;
            this.button_LaunchAttack.Text = "Launch Attack";
            this.button_LaunchAttack.UseVisualStyleBackColor = true;
            // 
            // label_WhoAreYouAttacking
            // 
            this.label_WhoAreYouAttacking.AutoSize = true;
            this.label_WhoAreYouAttacking.Location = new System.Drawing.Point(23, 30);
            this.label_WhoAreYouAttacking.Name = "label_WhoAreYouAttacking";
            this.label_WhoAreYouAttacking.Size = new System.Drawing.Size(121, 13);
            this.label_WhoAreYouAttacking.TabIndex = 1;
            this.label_WhoAreYouAttacking.Text = "Who are you attacking?";
            // 
            // comboBox_attack
            // 
            this.comboBox_attack.FormattingEnabled = true;
            this.comboBox_attack.Location = new System.Drawing.Point(23, 46);
            this.comboBox_attack.Name = "comboBox_attack";
            this.comboBox_attack.Size = new System.Drawing.Size(257, 21);
            this.comboBox_attack.TabIndex = 0;
            // 
            // label_codename_qredits
            // 
            this.label_codename_qredits.AutoSize = true;
            this.label_codename_qredits.Location = new System.Drawing.Point(198, 13);
            this.label_codename_qredits.Name = "label_codename_qredits";
            this.label_codename_qredits.Size = new System.Drawing.Size(114, 13);
            this.label_codename_qredits.TabIndex = 2;
            this.label_codename_qredits.Text = "[Codename] / [Qredits]";
            // 
            // ProgressForm
            // 
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(335, 310);
            this.Controls.Add(this.label_codename_qredits);
            this.Controls.Add(this.tabControl1);
            this.Name = "ProgressForm";
            this.Text = "Qonqr Conquerer";
            this.panel_login.ResumeLayout(false);
            this.panel_login.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.LoginTab.ResumeLayout(false);
            this.LoginTab.PerformLayout();
            this.FortsTab.ResumeLayout(false);
            this.FortsTab.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.AttackTab.ResumeLayout(false);
            this.AttackTab.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel_login;
        public Button button_Login;
        public Label label_loginStatus;
        public TextBox textBox_PasswordField;
        public TextBox textBox_UsernameField;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label_BotCapacity;
        private Label label_EnergyCapacity;
        private Label label_Level;
        private Label label_ExperienceCurrent;
        private Label label_ExperienceToLevel;
        private Label label_Zones;
        public Label label_var_Zones;
        public Label label_var_ExperienceToLevel;
        public Label label_var_ExperienceCurrent;
        public Label label_var_Level;
        public Label label_var_EnergyCapacity;
        public Label label_var_BotCapacity;
        private TabControl tabControl1;
        private TabPage LoginTab;
        private TabPage FortsTab;
        public Label label_codename_qredits;
        private TableLayoutPanel tableLayoutPanel2;
        public Label label_var_fort20;
        public Label label_var_fort19;
        public Label label_var_fort18;
        public Label label_var_fort17;
        public Label label_var_fort16;
        public Label label_var_fort15;
        public Label label_var_fort14;
        public Label label_var_fort13;
        public Label label_var_fort12;
        public Label label_var_fort11;
        public Label label_var_fort10;
        public Label label_var_fort9;
        public Label label_var_fort8;
        public Label label_var_fort7;
        public Label label_var_fort6;
        public Label label_var_fort5;
        public Label label_var_fort4;
        public Label label_var_fort3;
        public Label label_var_fort2;
        public Label label_var_fort1;
        public Button button_HarvestAll;
        public Label label_var_qreditsEarnedFromHarvest;
        private TabPage AttackTab;
        private Label label_WhoAreYouAttacking;
        public ComboBox comboBox_attack;
        public Button button_LaunchAttack;
        public ProgressBar progressBar_nanobots;
        public Label label_var_botsRegenRate;
        public ComboBox comboBox_scanArea;
        public Button button_Set;
        public Button button_Scan;
        public TextBox textBox_var_Longitude;
        public TextBox textBox_var_Latitude;
        public CheckBox autoHarvestCheckbox;
    }
}