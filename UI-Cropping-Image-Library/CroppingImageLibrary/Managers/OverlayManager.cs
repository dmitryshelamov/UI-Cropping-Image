using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CroppingImageLibrary.Managers
{
    /// <summary>
    /// Class that response for adding shadow area outside of cropping rectangle)
    /// </summary>
    internal class OverlayManager
    {
        private readonly Canvas _canvas;
        private readonly RectangleManager _rectangleManager;

        private readonly Path _pathOverlay;
        private GeometryGroup _geometryGroup;

        public OverlayManager(Canvas canvas, RectangleManager rectangleManager)
        {
            _canvas = canvas;
            _rectangleManager = rectangleManager;

            _pathOverlay = new Path
            {
                Fill = Brushes.Black,
                Opacity = 0.5
            };

            _canvas.Children.Add(_pathOverlay);
        }

        /// <summary>
        /// Update (redraw) overlay
        /// </summary>
        public void UpdateOverlay()
        {
            _geometryGroup = new GeometryGroup();
            RectangleGeometry geometry1 =
                new RectangleGeometry(new Rect(new Size(_canvas.ActualWidth, _canvas.ActualHeight)));
            RectangleGeometry geometry2 = new RectangleGeometry(new Rect(_rectangleManager.TopLeft.X,
                _rectangleManager.TopLeft.Y, _rectangleManager.RectangleWidth, _rectangleManager.RectangleHeight));
            _geometryGroup.Children.Add(geometry1);
            _geometryGroup.Children.Add(geometry2);
            _pathOverlay.Data = _geometryGroup;
        }
    }
}
