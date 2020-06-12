using System.IO;
using System.Reflection;

namespace GitFlowWithPRVS.Extension
{
    public static class GitFlowVersion
    {

        private static string GetFileInstallation()
        {
            return Path.Combine(Assembly.GetExecutingAssembly().Location, "IsFirstInstallation");
        }

        public static bool IsFirstInstallation()
        {
            var path = GetFileInstallation();
            Logger.Event("IsFirstInstallation -" + path);

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
