namespace Qonqr;

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
        string filepath = Path.Combine(Directory.GetCurrentDirectory(), "accounts.txt");
        List<string> accounts = new();
        
        using (StreamReader sr = new(filepath))
        {
            while (!sr.EndOfStream)
            {
                string? line = sr.ReadLine();
                if (line != null)
                {
                    accounts.Add(line);
                }
            }
        }

        return accounts;
    }

    public List<Player> GetAccounts()
    {
        List<Player> retval = new();

        List<string> accountStrings = ReadAccountsFile();
        int lineNumber = 1;
        foreach (string account in accountStrings)
        {
            string[] properties = account.Split(',');
            if (properties.Length != 5)
            {
                MessageBox.Show($"Invalid account information on line {lineNumber}",
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
                    Longitude = properties[4],
                });
            }

            lineNumber++;
        }
        return retval;
    }
}
