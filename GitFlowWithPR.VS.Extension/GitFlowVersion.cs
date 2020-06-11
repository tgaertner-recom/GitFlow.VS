using System.IO;
using System.Reflection;

namespace GitFlowWithPRVS.Extension
{
    public static class GitFlowVersion
    {

        private static string GetVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            Logger.Event("Version -"+version);
            return version;
        }

        private static string GetFileInstallation()
        {
            return Path.Combine(Assembly.GetExecutingAssembly().Location, GetVersion());
        }

        public static bool IsFirstInstallation()
        {
            var path = GetFileInstallation();
            Logger.Event("IsFirstInstallation -" + GetVersion());

            if (!File.Exists(path))
                return true;
            return false;
        }


        public static void InstallFileVersion()
        {
            var path = GetFileInstallation();

            File.Create(path);
        }

    }
}
