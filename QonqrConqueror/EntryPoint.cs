namespace Qonqr;

public class EntryPoint
{
    public static int Main(string[] args)
    {
        AppDomain currentDomain = AppDomain.CurrentDomain;
        currentDomain.UnhandledException += currentDomain_UnhandledException;

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        try
        {
            ConquererForm conquererForm = new();
            Application.Run(conquererForm);
        }
        catch (Exception ex)
        {
            HandleApplicationError(ex);
            return 1;
        }

        return 0;
    }

    private static void currentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        if (e.ExceptionObject is Exception ex)
        {
            HandleApplicationError(ex);
        }
        else
        {
            Logger.LogError("UnhandledException", "Unknown error occurred");
            MessageBox.Show(
                "A critical error occurred. Please check the error logs in the Logs directory.",
                "Application Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }

    private static void HandleApplicationError(Exception ex)
    {
        // Log the error with full details
        Logger.LogError("Application", ex);

        // Show user-friendly message without technical details
        MessageBox.Show(
            $"An unexpected error occurred: {ex.Message}\n\n" +
            "Details have been logged to the Logs directory. Please check the error log for more information.",
            "Application Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        );
    }
}
