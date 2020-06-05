using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;
using TeamExplorer.Common;
using System;
using System.Threading;
using EnvDTE80;
using EnvDTE;

namespace GitFlowVS.Extension
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideAutoLoad(UIContextGuids80.NoSolution, PackageAutoLoadFlags.BackgroundLoad)]
    [Guid(GuidList.GuidGitFlowVsExtensionPkgString)]
    public sealed class GitFlowVSExtension : AsyncPackage
    {
        private DTEEvents _dteEvents;

        public GitFlowVSExtension()
        {
        }

        protected override System.Threading.Tasks.Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            UserSettings.ServiceProvider = this;
            base.Initialize();

            var dte = (DTE2)GetService(typeof(SDTE));
            _dteEvents = dte.Events.DTEEvents;
            _dteEvents.OnStartupComplete += OnStartupComplete;
            return System.Threading.Tasks.Task.FromResult<object>(null);
        }


        private void OnStartupComplete()
        {
            _dteEvents.OnStartupComplete -= OnStartupComplete;
            _dteEvents = null;
            GitFlowScriptInstallation.Install();
        }
    }
}
