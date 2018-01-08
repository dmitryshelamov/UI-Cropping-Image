using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace CroppingImageLibrary.SampleApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CroppingWindow _croppingWindow;
        public MainWindow()
        {
            InitializeComponent();
            Topmost = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_croppingWindow != null)
                return;            
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                        "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                        "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                _croppingWindow = new CroppingWindow();
                _croppingWindow.Closed += (a, b) => _croppingWindow = null;
                _croppingWindow.Height = new BitmapImage(new Uri(op.FileName)).Height;
                _croppingWindow.Width = new BitmapImage(new Uri(op.FileName)).Width;

                _croppingWindow.SourceImage.Source = new BitmapImage(new Uri(op.FileName));
                _croppingWindow.SourceImage.Height = new BitmapImage(new Uri(op.FileName)).Height;
                _croppingWindow.SourceImage.Width = new BitmapImage(new Uri(op.FileName)).Width;

                _croppingWindow.Show();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BitmapFrame croppedBitmapFrame = _croppingWindow.CroppingAdorner.GetCroppedBitmapFrame();
            //create PNG image
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(croppedBitmapFrame));
            //save image to file

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "TestCropping"; // Default file name
            dlg.DefaultExt = ".png"; // Default file extension
            dlg.Filter = "Image png (.png)|*.png"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
                using (FileStream imageFile =
                    new FileStream(filename, FileMode.Create, FileAccess.Write))
                {
                    encoder.Save(imageFile);
                    imageFile.Flush();
                    imageFile.Close();
                }
            }
        }
    }
}
