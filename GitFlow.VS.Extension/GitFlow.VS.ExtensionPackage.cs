using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using TeamExplorer.Common;

namespace GitFlowVS.Extension
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideAutoLoad(UIContextGuids80.NoSolution, PackageAutoLoadFlags.BackgroundLoad)]
    [Guid(GuidList.GuidGitFlowVsExtensionPkgString)]
    public sealed class GitFlowVSExtension : AsyncPackage
    {

        public GitFlowVSExtension()
        {
        }

        protected override async System.Threading.Tasks.Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            UserSettings.ServiceProvider = this;
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await base.InitializeAsync(cancellationToken, progress);
        }

    }
}
