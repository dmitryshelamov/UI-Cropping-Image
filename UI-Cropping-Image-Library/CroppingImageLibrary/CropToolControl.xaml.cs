using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using CroppingImageLibrary.Services;

namespace CroppingImageLibrary
{
    /// <summary>
    /// Interaction logic for CropToolControl.xaml
    /// </summary>
    public partial class CropToolControl : UserControl
    {
        public CropService WorkSpace { get; private set; }

        public CropToolControl()
        {
            InitializeComponent();
        }

        private void RootGrid_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WorkSpace.Adorner.RaiseEvent(e);
        }

        private void RootGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            WorkSpace.Adorner.RaiseEvent(e);
        }

        private void RootGrid_Loaded(object sender, RoutedEventArgs e)
        {
            WorkSpace = new CropService(this);
        }

        public void SetImage(BitmapImage bitmapImage)
        {
            SourceImage.Source = bitmapImage;
            RootGrid.Height = bitmapImage.Height;
            RootGrid.Width = bitmapImage.Width;
        }
    }
}
