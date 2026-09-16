using DemoProject.Debugging;

namespace DemoProject;

public class DemoProjectConsts
{
    public const string LocalizationSourceName = "DemoProject";

    public const string ConnectionStringName = "Default";

    public const bool MultiTenancyEnabled = true;


    /// <summary>
    /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
    /// </summary>
    public static readonly string DefaultPassPhrase =
        DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "3374a2109c004d949ed5a582aa44987b";
}
