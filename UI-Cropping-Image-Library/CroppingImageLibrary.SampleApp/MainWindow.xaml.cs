using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace CroppingImageLibrary.SampleApp
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private CroppingWindow _croppingWindow;
        // private BitmapImage bitmapImage;
        private Bitmap sourceBitmap;

        public MainWindow()
        {
            InitializeComponent();
            Topmost = true;
        }

        private void Button_LoadImage(object sender, RoutedEventArgs e)
        {
            if (_croppingWindow != null)
                return;
            OpenFileDialog op = new OpenFileDialog();
            op.Title  = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                sourceBitmap = new Bitmap(op.FileName);
                _croppingWindow = new CroppingWindow(new BitmapImage(new Uri(op.FileName)));
                _croppingWindow.Closed += (a, b) => _croppingWindow = null;
                _croppingWindow.Height = new BitmapImage(new Uri(op.FileName)).Height;
                _croppingWindow.Width  = new BitmapImage(new Uri(op.FileName)).Width;

                _croppingWindow.Show();
            }
        }

        private void Button_SaveImage(object sender, RoutedEventArgs e)
        {
            var cropArea = _croppingWindow.CropTool.CropService.GetCroppedArea();

            System.Drawing.Rectangle cropRect = new System.Drawing.Rectangle((int) cropArea.CroppedRectAbsolute.X,
                (int) cropArea.CroppedRectAbsolute.Y, (int) cropArea.CroppedRectAbsolute.Width,
                (int) cropArea.CroppedRectAbsolute.Height);

            Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(sourceBitmap, new System.Drawing.Rectangle(0, 0, target.Width, target.Height),
                    cropRect,
                    GraphicsUnit.Pixel);
            }

            //save image to file
            SaveFileDialog dlg = new SaveFileDialog
            {
                FileName = "TestCropping",          // Default file name
                DefaultExt = ".png",                  // Default file extension
                Filter = "Image png (.png)|*.png" // Filter files by extension
            };
            
            // Show save file dialog box
            bool? result = dlg.ShowDialog();
            
            // Process save file dialog box results
            if (result != true)
                return;
            
            // Save document
            string filename  = dlg.FileName;
            target.Save(filename);
        }
    }
}