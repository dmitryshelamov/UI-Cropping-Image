using System;
using System.ComponentModel;
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
        public CropService CropService { get; private set; }





        private bool squareSelection = false;
        [Description("Whether to force the user to select only square or not"), Category("Data")]
        public bool SquareSelection
        {
            get { return squareSelection; }
            set { squareSelection = value; }
        }



        public CropToolControl()
        {
            InitializeComponent();
        }

        private void RootGrid_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CropService.Adorner.RaiseEvent(e);
        }

        private void RootGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CropService.Adorner.RaiseEvent(e);
        }

        private void RootGrid_Loaded(object sender, RoutedEventArgs e)
        {
            CropService = new CropService(this , SquareSelection);
        }

        public void SetImage(BitmapImage bitmapImage)
        {
            SourceImage.Source = bitmapImage;
            RootGrid.Height = bitmapImage.Height;
            RootGrid.Width = bitmapImage.Width;
        }
    }
}
