# Qonqr Conqueror

A Windows desktop application that automates gameplay actions for the mobile game QONQR. This tool manages multiple accounts simultaneously and provides automated resource management, zone scanning, and bot deployment capabilities.

## ⚠️ Important Notice

This application automates gameplay actions for QONQR. Use of automated tools may violate the game's Terms of Service. Use at your own risk. The authors of this software are not responsible for any account actions taken by the game developers.

## Features

- **Multi-Account Management**: Manage and control multiple QONQR accounts simultaneously
- **Player Statistics**: View real-time stats including bot capacity, energy, level, experience, and credits
- **Zone Scanning**: Scan map areas to discover zones and their control status (Uncaptured, Legion, Swarm, Faceless)
- **Base Management**: Monitor up to 20 bases per account with resource levels
- **Auto-Harvest**: Automatically harvest resources when bases reach full capacity
- **Bot Deployment**: Launch bot attacks on zones with appropriate formations based on player level
- **Auto-Launch**: Automatically deploy bots when resources regenerate to full capacity
- **Resource Tracking**: Real-time calculation of bot and energy regeneration

## Prerequisites

- **Operating System**: Windows 11 or Windows 10
- **.NET 8.0 SDK**: Required to build the application
  - Download from [Microsoft](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Visual Studio 2022** (optional): For development and debugging
  - Community Edition is free and sufficient

## Configuration

Before running the application, you must configure your QONQR accounts.

### Setting Up accounts.txt

1. Create or edit the `accounts.txt` file in the `QonqrConqueror/Resources/` directory
2. Add one account per line in the following comma-separated format:

```text
username,password,deviceId,latitude,longitude
```

**Example:**

```text
Luck,snuggles,kKclsrVbbzxOvLllULLOHKN7gdI=,47.6469383239746,-122.133738517761
OverlyAttachedGF,q1w2e3r4,lrZOroQLymPLscCscBK8RlPG3lM=,47.6469383239746,-122.133738517761
```

**Field Descriptions:**

- `username`: Your QONQR account username
- `password`: Your QONQR account password
- `deviceId`: Base64-encoded device identifier for authentication
- `latitude`: Starting latitude coordinate (-90 to 90)
- `longitude`: Starting longitude coordinate (-180 to 180)

**Notes:**

- Empty lines are ignored
- All fields are required
- Coordinates must be valid GPS coordinates
- The file is copied to the output directory during build

## Building the Application

### Using Command Line (.NET CLI)

1. **Clone the repository** (if applicable):

   ```cmd
   git clone <repository-url>
   cd Qonqr-Conqueror
   ```

2. **Restore dependencies**:

   ```cmd
   dotnet restore
   ```

3. **Build the solution**:

   ```cmd
   dotnet build QonqrConqueror.sln --configuration Release
   ```

   The compiled executable will be located at:

   ```text
   QonqrConqueror\bin\Release\net8.0-windows\Qonqr.exe
   ```

### Using Visual Studio

1. Open `QonqrConqueror.sln` in Visual Studio 2022
2. Select `Release` configuration from the toolbar
3. Build → Build Solution (or press `Ctrl+Shift+B`)
4. The executable will be in `QonqrConqueror\bin\Release\net8.0-windows\`

## Running the Application

### From Command Line

Navigate to the build output directory and run:

```cmd
cd QonqrConqueror\bin\Release\net8.0-windows
Qonqr.exe
```

Or from the project root:

```cmd
dotnet run --project QonqrConqueror\QonqrConqueror.csproj
```

### From Visual Studio

Press `F5` to run with debugging, or `Ctrl+F5` to run without debugging.

## Usage

### Initial Login

1. **Launch the application** - The login screen will appear
2. **Enter credentials** - Username and password fields
3. **Set coordinates** (optional) - Or use default coordinates
4. **Click "Login"** - The application will authenticate all accounts from `accounts.txt`

### Main Interface

Once logged in, the interface expands to show:

#### Statistics Panel

- Bot Capacity / Energy Capacity
- Current Experience / Level
- Experience to Next Level
- Zones Captured / Currently Leading
- Credits (Qredits)
- Codename

#### Bases Panel

- Lists up to 20 bases with resource levels
- Color-coded by faction:
  - **Red**: Legion
  - **Green**: Swarm
  - **Purple**: Faceless
  - **Gray**: Uncaptured
- Shows current gas in tank `[value]`
- **Harvest Button**: Manually harvest all bases
- **Auto Harvest Checkbox**: Automatically harvest when any base is full

#### Map Scanning Panel

- **Set Coordinates**: Enter latitude/longitude
- **Reset Coordinates**: Return to default location
- **Scan Button**: Discover zones in the area
- **Zone List**: Shows discovered zones with control status

#### Zone Operations Panel

- **Zone Selection**: Choose from discovered zones or bases
- **Launch Bots Button**: Deploy bots to selected zone
- **Auto Launch Checkbox**: Automatically launch when resources are full
- Attack formation automatically selected based on player level:
  - Level 1-24: Zone Assault 1
  - Level 25-49: Shockwave 1
  - Level 50-74: Shockwave 2
  - Level 75-99: Shockwave 3
  - Level 100+: Shockwave 4

### Automation Features

- **10-Minute Timer**: When auto-harvest is enabled, the application checks bases every 10 minutes
- **1-Second Timer**: Updates resource regeneration displays in real-time
- **Auto-Harvest**: Triggers when any base reaches full capacity (10,000 gas)
- **Auto-Launch**: Triggers when both bots and energy reach maximum capacity

## Project Structure

```text
Qonqr-Conqueror/
├── QonqrConqueror.sln           # Solution file
├── QonqrConqueror/
│   ├── Configuration/            # Configuration management
│   │   ├── ApiConfiguration.cs   # API endpoint configuration
│   │   └── ConfigFile.cs         # Accounts file parser
│   ├── Exceptions/               # Custom exception types
│   │   └── QonqrException.cs     # QONQR-specific exceptions
│   ├── Forms/                    # Windows Forms UI
│   │   ├── ConquererForm.cs      # Main application form
│   │   └── ProgressForm.cs       # Progress tracking form
│   ├── Models/                   # Data models
│   │   ├── ApplicationState.cs   # Application state management
│   │   ├── Coordinate.cs         # GPS coordinate model
│   │   ├── Player.cs             # Player account model
│   │   └── QOM.cs                # QONQR Object Model (API responses)
│   ├── Services/                 # Business logic
│   │   ├── ApiCall.cs            # QONQR API client
│   │   └── QonqrManager.cs       # Main service coordinator
│   ├── Utilities/                # Helper utilities
│   │   ├── Constants.cs          # Application constants
│   │   ├── Logger.cs             # Logging functionality
│   │   └── ResourceCalculator.cs # Resource regeneration calculator
│   ├── Resources/                # Application resources
│   │   └── accounts.txt          # Account configuration file
│   ├── EntryPoint.cs             # Application entry point
│   └── QonqrConqueror.csproj     # Project file
```

## Testing

Currently, this project does not include automated unit tests or integration tests. Testing is performed manually through the application interface.

### Manual Testing Checklist

- [ ] Login with valid credentials succeeds
- [ ] Login with invalid credentials fails gracefully
- [ ] Statistics display correctly after login
- [ ] Bases load and display with correct colors
- [ ] Zone scanning discovers zones in specified area
- [ ] Harvesting collects resources from bases
- [ ] Bot launching deploys to selected zone
- [ ] Auto-harvest triggers when bases are full
- [ ] Auto-launch triggers when resources are full
- [ ] Resource regeneration calculations are accurate

### Future Testing Improvements

Consider adding:

- Unit tests for `ResourceCalculator` logic
- Unit tests for `ConfigFile` parsing
- Mock API tests for `QonqrManager`
- Integration tests for the complete workflow

## Technical Details

- **Framework**: .NET 8.0 with Windows Forms
- **Target**: Windows desktop (net8.0-windows)
- **Language**: C# 12
- **Architecture**: Service-oriented with separation of concerns
- **Logging**: File-based logging to `Logs/` directory
- **Error Handling**: Global exception handling with user notifications

## Troubleshooting

### Application won't start

- Ensure .NET 8.0 Runtime is installed
- Check that `accounts.txt` exists in the correct location
- Review error logs in the `Logs/` directory

### Login fails

- Verify credentials in `accounts.txt` are correct
- Check deviceId is properly formatted (Base64)
- Ensure coordinates are within valid ranges
- Check internet connectivity

### Bases or zones don't load

- Verify coordinates are valid GPS coordinates
- Check API connectivity
- Review application logs for specific errors

## License

This project does not include a license file. Please contact the repository owner for usage terms.

## Disclaimer

This software is provided "as-is" without any warranty. Use of automation tools with online games may violate terms of service and result in account suspension or ban. The developers of this software accept no responsibility for any consequences resulting from its use.
