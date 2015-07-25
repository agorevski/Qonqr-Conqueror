
namespace Qonqr
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// This is the class where all (most) of the Harvesting Functionality and UI elements lives
    /// </summary>
    public class HarvestResources 
    {
        /// <summary>
        /// The speed at which we want to update the progresss bar
        /// </summary>
        private const int INTERVAL_MILLISECONDS = 500;

        /// <summary>
        /// The amount of time we want to wait before harvesting (currently set to 10 minutes)
        /// </summary>
        private const int MAX = 10 * 60 * 2;

        /// <summary>
        /// A list of the accounts
        /// </summary>
        private List<AccountData> _accounts;

        /// <summary>
        /// The timer for the progress bar updater
        /// </summary>
        private Timer _time;

        /// <summary>
        /// The number of times the harvester has run
        /// </summary>
        private int _iterations;

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="accounts">A list of accounts that we want to perform harvests for</param>
        public HarvestResources(List<AccountData> accounts)
        {
            _iterations = 0;
            _accounts = accounts;
            _time = new Timer();
            _time.Interval = INTERVAL_MILLISECONDS;
            _time.Tick += Time_Tick;

            Program.Form.progressBar_harvestAll.Minimum = 0;
            Program.Form.progressBar_harvestAll.Maximum = MAX;
        }

        /// <summary>
        /// The function that allows you to begin the harvesting progress
        /// </summary>
        public void Start()
        {
            Harvest();
            _time.Enabled = true;
        }

        /// <summary>
        /// The function that allows you to stop the harvesting progress
        /// </summary>
        public void Stop()
        {
            _time.Enabled = false;
            Program.Form.progressBar_harvestAll.Value = 0;
        }

        /// <summary>
        /// The Time_Tick function that tracks the progress bar
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event args</param>
        private void Time_Tick(object sender, EventArgs e)
        {
            Program.Form.progressBar_harvestAll.Value++;

            if (Program.Form.progressBar_harvestAll.Value == MAX)
            {
                _time.Enabled = false;
                Harvest();
                Program.Form.progressBar_harvestAll.Value = 0;
                _time.Enabled = true;
            }
        }

        /// <summary>
        /// This function does the actual harvesting as well as
        /// outputting the results to the rich text box
        /// </summary>
        private void Harvest()
        {
            _iterations++;
            ApiCall api = new ApiCall();
            Program.Form.richTextBox_var_harvestData.ResetText();
            Program.Form.richTextBox_var_harvestData.AppendText(Environment.NewLine + 
                string.Format("Harvest #{0} for all accounts...", _iterations));
            Program.Form.richTextBox_var_harvestData.Refresh();
                        
            foreach (AccountData account in _accounts)
            {
                HarvestAll ha = api.HarvestAll(account);
                string logLine = string.Format("{0} harvested {1} - Total: {2}", account.Username, ha.QreditsEarned, ha.HUD.Qredits);
                Program.Form.richTextBox_var_harvestData.AppendText(Environment.NewLine + logLine);
                Program.Form.richTextBox_var_harvestData.Refresh();
            }
        }
    }
}
