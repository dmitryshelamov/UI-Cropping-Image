using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace CroppingImageLibrary.SampleApp
{
    /// <summary>
    /// Interaction logic for CroppingWindow.xaml
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
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(CanvasPanel);
            CroppingAdorner = new CroppingAdorner(CanvasPanel);
            adornerLayer.Add(CroppingAdorner);
        }
    }
}
