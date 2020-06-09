using System.Windows.Controls;
using GitFlowWithPRVS.Extension.ViewModels;

namespace GitFlowWithPRVS.Extension.UI
{
    public partial class InitUi : UserControl
    {
        private readonly InitModel model;

        public InitUi(InitModel model)
        {
			Logger.PageView("Init");

			this.model = model;
            InitializeComponent();
            DataContext = model;
        }

    }
}
