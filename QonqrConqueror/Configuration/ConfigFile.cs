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

        // Check if file exists
        if (!File.Exists(filepath))
        {
            MessageBox.Show($"accounts.txt file not found at: {filepath}\n\nPlease create an accounts.txt file with your account information.",
                "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return accounts;
        }

        try
        {
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
        }
        catch (IOException ex)
        {
            MessageBox.Show($"Error reading accounts.txt: {ex.Message}",
                "File Read Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (UnauthorizedAccessException ex)
        {
            MessageBox.Show($"Access denied when reading accounts.txt: {ex.Message}",
                "Permission Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            // Skip empty lines
            if (string.IsNullOrWhiteSpace(account))
            {
                lineNumber++;
                continue;
            }

            string[] properties = account.Split(',');
            if (properties.Length != 5)
            {
                MessageBox.Show($"Invalid account information on line {lineNumber}: Expected 5 values separated by commas (username,password,deviceId,latitude,longitude)",
                    "Problem reading accounts.txt", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Validate that required fields are not empty
                string username = properties[0].Trim();
                string password = properties[1].Trim();
                string deviceId = properties[2].Trim();
                string latitude = properties[3].Trim();
                string longitude = properties[4].Trim();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(deviceId))
                {
                    MessageBox.Show($"Invalid account information on line {lineNumber}: Username, password, and deviceId cannot be empty",
                        "Problem reading accounts.txt", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lineNumber++;
                    continue;
                }

                // Validate latitude and longitude ranges
                if (!ValidateCoordinate(latitude, -90, 90, "latitude", lineNumber) ||
                    !ValidateCoordinate(longitude, -180, 180, "longitude", lineNumber))
                {
                    lineNumber++;
                    continue;
                }

                retval.Add(new Player()
                {
                    Username = username,
                    Password = password,
                    DeviceId = deviceId,
                    Latitude = latitude,
                    Longitude = longitude,
                });
            }

            lineNumber++;
        }
        return retval;
    }

    /// <summary>
    /// Validates that a coordinate value is within the valid range
    /// </summary>
    private bool ValidateCoordinate(string value, double min, double max, string coordinateName, int lineNumber)
    {
        if (!double.TryParse(value, out double coordinate))
        {
            MessageBox.Show($"Invalid {coordinateName} on line {lineNumber}: '{value}' is not a valid number",
                "Problem reading accounts.txt", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        if (coordinate < min || coordinate > max)
        {
            MessageBox.Show($"Invalid {coordinateName} on line {lineNumber}: {coordinate} must be between {min} and {max}",
                "Problem reading accounts.txt", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        return true;
    }
}
