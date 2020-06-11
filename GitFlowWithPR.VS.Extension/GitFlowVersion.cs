using System.IO;
using System.Reflection;

namespace GitFlowWithPRVS.Extension
{
    public static class GitFlowVersion
    {

        private static string GetVersion => VSVersion.FullVersion.ToString();
        

        private static string GetFileInstallation =>  Path.Combine(Assembly.GetExecutingAssembly().Location, GetVersion);

        public static bool IsFirstInstallation()
        {
            var path = GetFileInstallation;

            if (!File.Exists(path))
                return true;
            return false;
        }


        public static void InstallFileVersion()
        {
            var path = GetFileInstallation;

            File.Create(path);
        }

    }
}
