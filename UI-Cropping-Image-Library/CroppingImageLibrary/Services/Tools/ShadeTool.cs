using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CroppingImageLibrary.Services.Tools
{
    internal class ShadeTool
    {
        private readonly CropTool _cropTool;
        private readonly Canvas _canvas;
        private readonly RectangleGeometry _rectangleGeo;
        private readonly RectangleGeometry _grayArea;

        public Path ShadeOverlay { get; set; }

        public ShadeTool(Canvas canvas, CropTool cropTool)
        {
            _cropTool = cropTool;
            _canvas = canvas;

            ShadeOverlay = new Path
            {
                Fill = Brushes.Black,
                Opacity = 0.5
            };

            var geometryGroup = new GeometryGroup();
            _grayArea = new RectangleGeometry(new Rect(new Size(canvas.Width, canvas.Height)));
            _rectangleGeo = new RectangleGeometry(
                new Rect(
                    _cropTool.TopLeftX,
                    _cropTool.TopLeftY,
                    _cropTool.Width,
                    _cropTool.Height
                )
            );
            geometryGroup.Children.Add(_grayArea);
            geometryGroup.Children.Add(_rectangleGeo);
            ShadeOverlay.Data = geometryGroup;
        }

        public void Redraw()
        {
            _grayArea.Rect = new Rect(new Size(_canvas.Width, _canvas.Height));
            _rectangleGeo.Rect = new Rect(
                _cropTool.TopLeftX,
                _cropTool.TopLeftY,
                _cropTool.Width,
                _cropTool.Height
            );
        }
    }
}
