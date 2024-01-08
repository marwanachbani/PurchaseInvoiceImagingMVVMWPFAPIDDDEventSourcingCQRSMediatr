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
using WpfApp1.ViewModels;

namespace WpfApp1.Views.PictureZneViews
{
    /// <summary>
    /// Interaction logic for ImageView.xaml
    /// </summary>
    public partial class ImageView : UserControl
    {
        public ImageView()
        {
            InitializeComponent();
            var viewModel = new ImageViewModel();
            DataContext = viewModel;
        }
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point startPoint = e.GetPosition(selectableImage);
            ImageViewModel viewModel = (ImageViewModel)DataContext;

            // Set initial selection coordinates
            viewModel.SelectionX = startPoint.X;
            viewModel.SelectionY = startPoint.Y;

            // Set the selected image
            viewModel.SelectedImage = (BitmapImage)selectableImage.Source;

            // Subscribe to the MouseMove and MouseLeftButtonUp events
            selectableImage.MouseMove += Image_MouseMove;
            selectableImage.MouseLeftButtonUp += Image_MouseLeftButtonUp;
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            Point currentPoint = e.GetPosition(selectableImage);
            ImageViewModel viewModel = (ImageViewModel)DataContext;

            // Calculate selection dimensions
            viewModel.SelectionWidth = currentPoint.X - viewModel.SelectionX;
            viewModel.SelectionHeight = currentPoint.Y - viewModel.SelectionY;
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Unsubscribe from the MouseMove and MouseLeftButtonUp events
            selectableImage.MouseMove -= Image_MouseMove;
            selectableImage.MouseLeftButtonUp -= Image_MouseLeftButtonUp;
        }

    }
}
