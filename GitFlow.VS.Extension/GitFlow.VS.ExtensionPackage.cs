using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using TeamExplorer.Common;

namespace GitFlowWithPRVS.Extension
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideAutoLoad(UIContextGuids80.NoSolution, PackageAutoLoadFlags.BackgroundLoad)]
    [Guid(GuidList.GuidGitFlowWithPRVsExtensionPkgString)]
    public sealed class GitFlowWithPRVSExtension : AsyncPackage
    {

        public GitFlowWithPRVSExtension()
        {
        }

        protected override System.Threading.Tasks.Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            UserSettings.ServiceProvider = this;
            return System.Threading.Tasks.Task.FromResult<object>(null);
        }

    }
}
