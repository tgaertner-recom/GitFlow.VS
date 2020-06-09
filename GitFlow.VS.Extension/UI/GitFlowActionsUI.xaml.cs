using System.Windows.Controls;
using GitFlowWithPRVS.Extension.ViewModels;

namespace GitFlowWithPRVS.Extension.UI
{
    public partial class GitFlowActionsUI : UserControl
    {
        private ActionViewModel model;
        public GitFlowActionsUI(ActionViewModel model)
        {
            this.model = model;
            InitializeComponent();
            DataContext = model;
		}
    }
}
