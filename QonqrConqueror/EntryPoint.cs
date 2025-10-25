namespace Qonqr;

public class EntryPoint
{
    public static int Main(string[] args)
    {
        AppDomain currentDomain = AppDomain.CurrentDomain;
        currentDomain.UnhandledException += currentDomain_UnhandledException;

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        ConquererForm conquererForm = new();

        Application.Run(conquererForm);

        return 0;
    }

    private static void currentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        MessageBox.Show(e.ToString());
    }
}
