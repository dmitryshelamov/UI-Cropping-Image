using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CroppingImageLibrary.Managers
{
    /// <summary>
    /// Display text information
    /// </summary>
    internal class DisplayTextManager
    {
        private readonly TextBlock _sizeTextBlock;
        private readonly RectangleManager _rectangleManager;
        public DisplayTextManager(Canvas canvas, RectangleManager rectangleManager)
        {
            _rectangleManager = rectangleManager;
            _sizeTextBlock = new TextBlock()
            {
                Text = "Size counter",
                FontSize = 14,
                Foreground = Brushes.White,
                Background = Brushes.Black,
                Visibility = Visibility.Hidden
            };
            canvas.Children.Add(_sizeTextBlock);
        }


        /// <summary>
        /// Manage visibility of text
        /// </summary>
        /// <param name="isVisble">Set current visibility</param>
        public void ShowText(bool isVisble)
        {
            _sizeTextBlock.Visibility = isVisble ? Visibility.Visible : Visibility.Hidden;
        }

        /// <summary>
        /// Update (redraw) text label
        /// </summary>
        public void UpdateSizeText()
        {
            double offsetTop = 2;
            double offsetLeft = 5;

            var calculateTop = _rectangleManager.TopLeft.Y - _sizeTextBlock.ActualHeight - offsetTop;
            if (calculateTop < 0)
                calculateTop = offsetTop;
            
            Canvas.SetLeft(_sizeTextBlock, _rectangleManager.TopLeft.X + offsetLeft);
            Canvas.SetTop(_sizeTextBlock, calculateTop);
            _sizeTextBlock.Text = $"w: {_rectangleManager.RectangleWidth}, h: {_rectangleManager.RectangleHeight}";
        }
    }
}
