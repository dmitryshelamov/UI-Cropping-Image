using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CroppingImageLibrary.Services.Tools
{
    internal class TextTool
    {
        const double OffsetTop = 2;
        const double OffsetLeft = 5;

        public TextBlock TextBlock { get; }
        private readonly CropTool _cropTool;

        public TextTool(CropTool cropTool)
        {
            _cropTool = cropTool;
            TextBlock = new TextBlock
            {
                Text = "Size counter",
                FontSize = 14,
                Foreground = Brushes.White,
                Background = Brushes.Black,
                Visibility = Visibility.Hidden
            };
        }


        /// <summary>
        ///     Manage visibility of text
        /// </summary>
        /// <param name="isVisible">Set current visibility</param>
        public void ShowText(bool isVisible)
        {
            TextBlock.Visibility = isVisible ? Visibility.Visible : Visibility.Hidden;
        }

        /// <summary>
        ///     Update (redraw) text label
        /// </summary>
        public void Redraw()
        {
            if (_cropTool.Height <= 0 && _cropTool.Width <= 0)
                ShowText(false);
            else
                ShowText(true);

            double calculateTop = _cropTool.TopLeftY - TextBlock.ActualHeight - OffsetTop;
            if (calculateTop < 0)
                calculateTop = OffsetTop;

            Canvas.SetLeft(TextBlock, _cropTool.TopLeftX + OffsetLeft);
            Canvas.SetTop(TextBlock, calculateTop);
            TextBlock.Text = $"w: {_cropTool.Width}, h: {_cropTool.Height}";
        }
    }
}
