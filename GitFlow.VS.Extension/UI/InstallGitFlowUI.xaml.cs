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

        public InstallGitFlowUI(GitFlowInstallSection parent)
        {
			Logger.PageView("InstallGitFlow");
            this.parent = parent;
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
                var exitCode = GitFlowScriptInstallation.Install();

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
