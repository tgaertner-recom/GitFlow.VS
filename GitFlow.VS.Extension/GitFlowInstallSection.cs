using System;
using System.ComponentModel.Composition;
using System.Linq;
using GitFlowVS.Extension.UI;
using Microsoft.TeamFoundation.Controls;
using Microsoft.VisualStudio.ExtensionManager;
using Microsoft.VisualStudio.Shell;
using TeamExplorer.Common;

namespace GitFlowVS.Extension
{
    [TeamExplorerSection(GuidList.GitFlowInstallSection, GuidList.GitFlowPage, 100)]
    public class GitFlowInstallSection : TeamExplorerBaseSection, IGitFlowSection
    {
        [ImportingConstructor]
        public GitFlowInstallSection([Import(typeof(SVsServiceProvider))] IServiceProvider serviceProvider)
        {
            try
            {
                Title = "GitFlow with PR";
                IVsExtensionManager manager = serviceProvider.GetService(typeof(SVsExtensionManager)) as IVsExtensionManager;
                // get your extension by Product Id
                IInstalledExtension myExtension = manager.GetInstalledExtensions().FirstOrDefault();
                // get current version
                var currentVersion = myExtension.Header.Version;
                SectionContent = new InstallGitFlowUI(this, currentVersion);

                UpdateVisibleState();

            }
            catch (Exception e)
            {
                HandleException(e);
            }
        }

		private void HandleException(Exception ex)
		{
			Logger.Exception(ex);
			ShowNotification(ex.Message, NotificationType.Error);
		}

        public void UpdateVisibleState()
        {
            IsVisible = !GitFlowPage.GitFlowIsInstalled || GitFlowPage.ActiveRepo == null;
        }
    }
}