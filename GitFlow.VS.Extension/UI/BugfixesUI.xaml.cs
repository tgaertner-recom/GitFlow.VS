using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GitFlowWithPRVS.Extension.ViewModels;

namespace GitFlowWithPRVS.Extension.UI
{
    /// <summary>
    /// Interaction logic for BugfixesUI.xaml
    /// </summary>
    public partial class BugfixesUI : UserControl
    {
        public BugfixesUI(BugfixesViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }

        private void BugfixesGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var model = (BugfixesViewModel)DataContext;
            model.CheckoutBugfixBranch();
        }
    }
}
