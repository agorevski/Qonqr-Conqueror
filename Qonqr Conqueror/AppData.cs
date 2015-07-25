namespace Qonqr
{
    using System.Collections.Generic;
    using System.Windows.Forms;

    public class AppData
    {
        public AppData()
        { }

        public void Login(LoginApiCall login)
        {
            //UseHudData(login.HUD);
            UsePlayerProfileData(login.PlayerProfile);
            Logger.LogLogin(login.PlayerProfile.Codename);

            //foreach (FormationsContentResponses responses in login.Content.FormationsContentResponses)
            //{
            //    Logger.LogData(string.Format("{0},{1},{2},{3},{4}", responses.Name, responses.LevelRequired, responses.Id, responses.BotCost, responses.ConsumableRequired));
            //}
        }

        public void Forts(FortsApiCall fac)
        {
            UseFortData(fac.PlayerForts.Forts);
        }

        private void UsePlayerProfileData(PlayerProfile profile)
        {
            //Program.Form.label_var_Zones.Text = profile.TotalZonesCaptured.ToString() + "/" + profile.ZonesCurrentlyLeading.ToString();
            Program.Form.label_codename.Text = profile.Codename;
            Program.Form.Refresh();
        }

        public void UseFortData(List<Forts> fortsList)
        {
            Program.Form.comboBox_attack.Items.Clear();

            for (int i = 0; i < fortsList.Count; i++)
            {
                Forts fort = fortsList[i];
                string labelText = fortsList[i].ZoneName + string.Format(" [{0}]", fort.CurrentGasInTank);
                labelText = labelText.Replace('"', ' ');

                string controlState = ZoneControlStateConverter(fort.ZoneControlState);
                Program.Form.comboBox_attack.Items.Add(string.Format("{0} {1} [{2}] <{3}/{4}>", controlState, fort.ZoneName, fort.ZoneId, fort.Latitude, fort.Longitude));
            }
            Program.Form.Refresh();
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
    }
}
