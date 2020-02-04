using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace CroppingImageLibrary.SampleApp
{
    /// <summary>
    ///     Interaction logic for CroppingWindow.xaml
    /// </summary>
    public partial class CroppingWindow : Window
    {
        public CroppingAdorner CroppingAdorner;

        public CroppingWindow()
        {
            InitializeComponent();
        }

        private void RootGrid_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CroppingAdorner.CaptureMouse();
            CroppingAdorner.MouseLeftButtonDownEventHandler(sender, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CroppingAdorner = new CroppingAdorner(RootGrid);

            var adornerLayer = AdornerLayer.GetAdornerLayer(RootGrid);
            Debug.Assert(adornerLayer != null, nameof(adornerLayer) + " != null");
            adornerLayer.Add(CroppingAdorner);
        }
    }
}