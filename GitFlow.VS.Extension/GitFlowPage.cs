﻿using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Threading;
using GitFlow.VS;
using GitFlowVS.Extension.UI;
using Microsoft.TeamFoundation.Controls;
using Microsoft.VisualStudio.ExtensionManager;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TeamFoundation.Git.Extensibility;
using TeamExplorer.Common;

namespace GitFlowVS.Extension
{
    [TeamExplorerPage(GuidList.GitFlowPage, Undockable = true)]
    public class GitFlowPage : TeamExplorerBasePage
    {
        private static IGitExt gitService;
        private static ITeamExplorer teamExplorer;
        private static IVsOutputWindowPane outputWindow;
        private GitFlowPageUI ui;
        private static Version currentVersion;

        public static IGitRepositoryInfo ActiveRepo
        {
            get
            {
                return gitService.ActiveRepositories.FirstOrDefault();
            }
        }

        public static IVsOutputWindowPane OutputWindow
        {
            get { return outputWindow; }
        }

        public static string ActiveRepoPath
        {
            get { return ActiveRepo?.RepositoryPath; }
        }

        public override void Refresh()
        {
            ITeamExplorerSection[] teamExplorerSections = this.GetSections();
            foreach (var section in teamExplorerSections.Where(s => s is IGitFlowSection))
            {
                ITeamExplorerSection section1 = section;
                System.Windows.Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                    new Action(() =>
                        ((IGitFlowSection)section1).UpdateVisibleState()));
            }
            ui.Refresh();
        }

        [ImportingConstructor]
        public GitFlowPage([Import(typeof(SVsServiceProvider))] IServiceProvider serviceProvider)
        {
            Title = "GitFlow with PR";
            gitService = (IGitExt)serviceProvider.GetService(typeof(IGitExt));
            teamExplorer = (ITeamExplorer) serviceProvider.GetService(typeof (ITeamExplorer));
            gitService.PropertyChanged += OnGitServicePropertyChanged;
            // get ExtensionManager
            IVsExtensionManager manager = serviceProvider.GetService(typeof(SVsExtensionManager)) as IVsExtensionManager;
            // get your extension by Product Id
            IInstalledExtension myExtension = manager.GetInstalledExtensions().FirstOrDefault();
            // get current version
            currentVersion = myExtension.Header.Version;
            var outWindow = Package.GetGlobalService(typeof(SVsOutputWindow)) as IVsOutputWindow;
            var customGuid = new Guid("4BEF3E2A-F42D-4BFB-A99B-7EACAC914C50");
            outWindow.CreatePane(ref customGuid, "GitFlow.VS", 1, 1);
            outWindow.GetPane(ref customGuid, out outputWindow);

            ui = new GitFlowPageUI();
            PageContent = ui;
        }

        private void OnGitServicePropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            Refresh();
        }

        public static void ActiveOutputWindow()
        {
            OutputWindow.Activate();
        }

        public static bool GitFlowIsInstalled
        {
            get
            {
                if (!File.Exists(string.Format("GitFlowWithPr.{0}.{1}.{2}", currentVersion.Major, currentVersion.Minor, currentVersion.Revision)))
                    return false;
                //Read PATH to find git installation path
                //Check if extension has been configured
                string binariesPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Dependencies\\binaries");
                if (!Directory.Exists(binariesPath))
                    return false;

                var gitBinPath = GitHelper.GetGitBinPath();
                if (gitBinPath == null)
                    return false;

                string gitFlowFile = Path.Combine(gitBinPath,"git-flow");
                if (!File.Exists(gitFlowFile))
                    return false;
                
                return true;
            }
        }

        public static void ShowPage(string page)
        {
            System.Windows.Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                new Action(() =>
                    teamExplorer.NavigateToPage(new Guid(page), null)));

        }
    }
}
