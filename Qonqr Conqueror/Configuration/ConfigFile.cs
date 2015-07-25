namespace Qonqr
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    public class ConfigFile
    {
        public ConfigFile()
        {
        }

        /// <summary>
        /// A function that reads accounts.txt from the current directory and lists its contents out in a line
        /// </summary>
        /// <returns>A list of strings in the accounts file</returns>
        public List<string> ReadAccountsFile()
        {
            string filepath = Directory.GetCurrentDirectory() + "\\accounts.txt";
            List<string> accounts = new List<string>();
            using (StreamReader sr = new StreamReader(filepath))
            {
                while (!sr.EndOfStream)
                {
                    accounts.Add(sr.ReadLine());
                }
            }

            return accounts;
        }

        public List<Player> GetAccounts()
        {
            List<Player> retval = new List<Player>();

            List<string> accountStrings = ReadAccountsFile();
            int lineNumber = 1;
            foreach (string account in accountStrings)
            {
                string[] properties = account.Split(',');
                if (properties.Count() != 5)
                {
                    MessageBox.Show(string.Format("Invalid account information on line {0}", lineNumber),
                        "Problem reading accounts.txt", MessageBoxButtons.OK);
                }
                else
                {
                    retval.Add(new Player()
                    {
                        Username = properties[0],
                        Password = properties[1],
                        DeviceId = properties[2],
                        Latitude = properties[3],
                        Longitude = properties[3],
                    });
                }

                lineNumber++;
            }
            return retval;
        }
    }
}
