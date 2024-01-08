
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UIApi.Responses;
using WpfApp1.ViewModels;

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for AppZone.xaml
    /// </summary>
    public partial class AppZone : UserControl
    {
        public AppZone()
        {
            InitializeComponent();
            DataContext = new AppZoneViewModel();
        }
        private void TreeViewItem_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is TreeViewItem treeViewItem)
            {
                var viewModel = DataContext as AppZoneViewModel;
                if (viewModel != null)
                {
                   var id = treeViewItem.Header.ToString();
                   var cheakid = Guid.TryParse(id, out var cheak);
                    if(cheakid == true)
                    {
                        viewModel.OnTreeViewItemSelected(treeViewItem.Header.ToString());
                    }
                    else
                    {
                        viewModel.OnUnSelectedTreeviewItem();
                    }
                    
                }
            }
            
            
        }

    }
}
