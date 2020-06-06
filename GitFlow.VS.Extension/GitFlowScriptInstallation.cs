using GitFlow.VS;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace GitFlowVS.Extension
{
    public static class GitFlowScriptInstallation
    {
        public static int Install()
        {
            var installationPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string cmd = Path.Combine(installationPath, "Dependencies\\install.ps1");

            var gitInstallPath = GitHelper.GetGitInstallationPath();
            if (Directory.Exists(Path.Combine(gitInstallPath, "usr")))
            {
                gitInstallPath = Path.Combine(gitInstallPath, "usr");
            }

            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    WorkingDirectory = Path.Combine(installationPath, "Dependencies"),
                    UseShellExecute = true,
                    Arguments = String.Format("-ExecutionPolicy ByPass -NoLogo -NoProfile -File \"" + cmd + "\" \"{0}\"", gitInstallPath),
                    Verb = "runas",
                    LoadUserProfile = false
                }
            };
            proc.Start();
            proc.WaitForExit();

            return proc.ExitCode;
        }

    }
}
