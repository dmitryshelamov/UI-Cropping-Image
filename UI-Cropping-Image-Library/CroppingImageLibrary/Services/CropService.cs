using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using CroppingImageLibrary.Services.State;
using CroppingImageLibrary.Services.Tools;

namespace CroppingImageLibrary.Services
{
    public class CropArea
    {
        public readonly Size OriginalSize;
        public readonly Rect CroppedRectAbsolute;

        public CropArea(Size originalSize, Rect croppedRectAbsolute)
        {
            OriginalSize = originalSize;
            CroppedRectAbsolute = croppedRectAbsolute;
        }
    }

    public class CropService
    {
        private readonly CropAdorner _cropAdorner;
        private readonly Canvas _canvas;
        private readonly Tools.CropTool _cropTool;

        private IToolState _currentToolState;
        private readonly IToolState _createState;
        private readonly IToolState _dragState;
        private readonly IToolState _completeState;

        public Adorner Adorner => _cropAdorner;

        private enum TouchPoint
        {
            OutsideRectangle,
            InsideRectangle
        }

        public CropService(FrameworkElement adornedElement)
        {
            _canvas = new Canvas
            {
                Height = adornedElement.ActualHeight,
                Width = adornedElement.ActualWidth
            };
            _cropAdorner = new CropAdorner(adornedElement, _canvas);
            var adornerLayer = AdornerLayer.GetAdornerLayer(adornedElement);
            Debug.Assert(adornerLayer != null, nameof(adornerLayer) + " != null");
            adornerLayer.Add(_cropAdorner);

            var cropShape = new CropShape(
                new Rectangle
                {
                    Height = 0,
                    Width = 0,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1.5
                },
                new Rectangle
                {
                    Stroke = Brushes.White,
                    StrokeDashArray = new DoubleCollection(new double[] { 4, 4 })
                }
            );
            _cropTool = new CropTool(_canvas);
            _createState = new CreateState(_cropTool, _canvas);
            _completeState = new CompleteState();
            _dragState = new DragState(_cropTool, _canvas);
            _currentToolState = _completeState;

            _cropAdorner.MouseLeftButtonDown += AdornerOnMouseLeftButtonDown;
            _cropAdorner.MouseMove += AdornerOnMouseMove;
            _cropAdorner.MouseLeftButtonUp += AdornerOnMouseLeftButtonUp;

            _cropTool.Redraw(0, 0, 0, 0);
        }

        public CropArea GetCroppedArea() =>
            new CropArea(
                _cropAdorner.RenderSize,
                new Rect(_cropTool.TopLeftX, _cropTool.TopLeftY, _cropTool.Width, _cropTool.Height)
            );

        private void AdornerOnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _canvas.ReleaseMouseCapture();
            _currentToolState = _completeState;
        }

        private void AdornerOnMouseMove(object sender, MouseEventArgs e)
        {
            var point = e.GetPosition(_canvas);
            var newPosition = _currentToolState.OnMouseMove(point);
            if (newPosition.HasValue)
            {
                _cropTool.Redraw(newPosition.Value.Left, newPosition.Value.Top, newPosition.Value.Width, newPosition.Value.Height);
            }

        }

        private void AdornerOnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _canvas.CaptureMouse();
            var point = e.GetPosition(_canvas);
            var touch = GetTouchPoint(point);
            if (touch == TouchPoint.OutsideRectangle)
            {
                _currentToolState = _createState;
            }
            else if (touch == TouchPoint.InsideRectangle)
            {
                _currentToolState = _dragState;
            }
            _currentToolState.OnMouseDown(point);
        }

        private TouchPoint GetTouchPoint(Point mousePoint)
        {
            //left
            if (mousePoint.X < _cropTool.TopLeftX)
                return TouchPoint.OutsideRectangle;
            //right
            if (mousePoint.X > _cropTool.BottomRightX)
                return TouchPoint.OutsideRectangle;
            //top
            if (mousePoint.Y < _cropTool.TopLeftY)
                return TouchPoint.OutsideRectangle;
            //bottom
            if (mousePoint.Y > _cropTool.BottomRightY)
                return TouchPoint.OutsideRectangle;

            return TouchPoint.InsideRectangle;
        }
    }
}
