using System.Windows;
using System.Windows.Media.Imaging;

namespace CroppingImageLibrary.SampleApp
{
    /// <summary>
    ///     Interaction logic for CroppingWindow.xaml
    /// </summary>
    public partial class CroppingWindow : Window
    {
        public CroppingWindow()
        {
            InitializeComponent();
        }

        public CroppingWindow(BitmapImage bitmapImage)
        {
            InitializeComponent();
            //  pass data to custom user control
            CropTool.SetImage(bitmapImage);
        }
    }
}