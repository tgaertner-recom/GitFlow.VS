using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GitFlowWithPRVS.Extension.ViewModels;

namespace GitFlowWithPRVS.Extension.UI
{
    /// <summary>
    /// Interaction logic for ReleasesUI.xaml
    /// </summary>
    public partial class ReleasesUI : UserControl
    {
        public ReleasesUI(ReleasesViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }

    }
}
