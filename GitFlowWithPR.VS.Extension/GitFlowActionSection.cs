using GitFlowWithPRVS.Extension.UI;
using GitFlowWithPRVS.Extension.ViewModels;
using Microsoft.TeamFoundation.Controls;
using TeamExplorer.Common;

namespace GitFlowWithPRVS.Extension
{
    [TeamExplorerSection(GuidList.GitFlowActionSection, GuidList.GitFlowPage, 110)]
    public class GitFlowActionSection : TeamExplorerBaseSection, IGitFlowSection
    {
        private readonly ActionViewModel model;

        public GitFlowActionSection()
        {
            Title = "Recommended actions";
            IsVisible = false;
            if (GitFlowPage.ActiveRepoPath != null)
            {
                model = new ActionViewModel(this);
            }
            UpdateVisibleState();
        }

        public void UpdateVisibleState()
        {
            if (!GitFlowPage.GitFlowIsInstalled || GitFlowPage.ActiveRepo == null)
            {
                IsVisible = false;
                return;
            }

            var gf = new VsGitFlowWrapper(GitFlowPage.ActiveRepo.RepositoryPath, GitFlowPage.OutputWindow);
            if (gf.IsInitialized)
            {
                if (!IsVisible)
                {
                    SectionContent = new GitFlowActionsUI(model);
                    IsVisible = true;
                }
                model.Update();
            }
            else
            {
                IsVisible = false;
            }
        }

    }
}