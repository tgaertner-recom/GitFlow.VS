using GitFlow.VS;
using Microsoft.TeamFoundation.Controls;
using System;
using System.Windows;
using System.Windows.Controls;

namespace GitFlowVS.Extension.UI
{
    /// <summary>
    /// Interaction logic for InstallGitFlowUI.xaml
    /// </summary>
    public partial class InstallGitFlowUI : UserControl
    {
        private readonly GitFlowInstallSection parent;
        private readonly Version currentVersion;

        public InstallGitFlowUI(GitFlowInstallSection parent, Version currentVersion)
        {
			Logger.PageView("InstallGitFlow");
            this.parent = parent;
            this.currentVersion = currentVersion;
            InitializeComponent();

            if (GitHelper.GetGitInstallationPath() == null)
            {
                GitInstallation.Visibility = Visibility.Visible;
                GitFlowInstallation.Visibility = Visibility.Collapsed;
                GitFlowInstallationButton.Visibility = Visibility.Collapsed;
                GitNoRepo.Visibility = Visibility.Collapsed;
            }
            else if(GitFlowPage.ActiveRepo == null)
            {
                GitNoRepo.Visibility = Visibility.Visible;
                GitInstallation.Visibility = Visibility.Collapsed;
                GitFlowInstallation.Visibility = Visibility.Collapsed;
                GitFlowInstallationButton.Visibility = Visibility.Collapsed;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
	            Logger.Event("Install");
                Error.Visibility = Visibility.Hidden;
                var exitCode = GitFlowScriptInstallation.Install(this.currentVersion);

                if (exitCode != 0)
                {
                    Error.Text = Error.Text.Replace("{0}", exitCode.ToString());
                    Error.Visibility = Visibility.Visible;
                }
                else
                {
                    parent.Refresh();
                }
            }
            catch (Exception ex)
            {
                parent.ShowNotification(ex.ToString(), NotificationType.Error);
				Logger.Exception(ex);
            }
        }
    }
}
