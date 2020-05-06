using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CroppingImageLibrary.Services.Tools
{
    internal class CropTool
    {
        private readonly Canvas _canvas;
        private readonly CropShape _cropShape;
        private readonly ShadeTool _shadeService;
        private readonly ThumbTool _thumbService;
        private readonly TextTool _textService;

        public double TopLeftX => Canvas.GetLeft(_cropShape.Shape);
        public double TopLeftY => Canvas.GetTop(_cropShape.Shape);
        public double BottomRightX => Canvas.GetLeft(_cropShape.Shape) + _cropShape.Shape.Width;
        public double BottomRightY => Canvas.GetTop(_cropShape.Shape) + _cropShape.Shape.Height;
        public double Height => _cropShape.Shape.Height;
        public double Width => _cropShape.Shape.Width;

        public CropTool(Canvas canvas)
        {
            _canvas = canvas;
            _cropShape = new CropShape(new Rectangle {
                    Height = 0,
                    Width = 0,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1.5
                },
                new Rectangle {
                    Stroke = Brushes.White,
                    StrokeDashArray = new DoubleCollection(new double[] { 4, 4 })
                });

            _shadeService = new ShadeTool(canvas, this);
            _thumbService = new ThumbTool(canvas, this);
            _textService = new TextTool(this);

            _canvas.Children.Add(_cropShape.Shape);
            _canvas.Children.Add(_cropShape.DashedShape);


            _canvas.Children.Add(_shadeService.ShadeOverlay);

            _canvas.Children.Add(_thumbService.BottomMiddle);
            _canvas.Children.Add(_thumbService.LeftMiddle);
            _canvas.Children.Add(_thumbService.TopMiddle);
            _canvas.Children.Add(_thumbService.RightMiddle);
            _canvas.Children.Add(_thumbService.TopLeft);
            _canvas.Children.Add(_thumbService.TopRight);
            _canvas.Children.Add(_thumbService.BottomLeft);
            _canvas.Children.Add(_thumbService.BottomRight);

            _canvas.Children.Add(_textService.TextBlock);
        }

        public void Redraw(double newX, double newY, double newWidth, double newHeight)
        {
            _cropShape.Redraw(newX, newY, newWidth, newHeight);
            _shadeService.Redraw();
            _thumbService.Redraw();
            _textService.Redraw();
        }
    }
}
