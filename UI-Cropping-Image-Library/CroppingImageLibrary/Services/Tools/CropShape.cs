using System.Windows.Controls;
using System.Windows.Shapes;

namespace CroppingImageLibrary.Services.Tools
{
    internal class CropShape
    {
        public Shape Shape { get; }
        public Shape DashedShape { get; }
        private readonly Canvas _originalCanvas;
        private readonly bool _squareSelection;

        public CropShape(Shape shape, Shape dashedShape, bool squareSelection = false , Canvas overlayCanvas= null)
        {
            Shape = shape;
            DashedShape = dashedShape;
            _originalCanvas = overlayCanvas;
            _squareSelection = squareSelection;
        }

        public void Redraw(double newX, double newY, double newWidth, double newHeight)
        {
            //dont use negative value
            if (newHeight >= 0 && newWidth >= 0)
            {
                if(_squareSelection)
                UpdateRectangle(newX, newY, newWidth, newHeight);
                else
                  RedrawSolidShape(newX, newY, newWidth, newHeight);
                //
                RedrawDashedShape();
            }
        }

        private void RedrawSolidShape(double newX, double newY, double newWidth, double newHeight)
        {
            Canvas.SetLeft(Shape, newX);
            Canvas.SetTop(Shape, newY);
            Shape.Width = newWidth;
            Shape.Height = newHeight;
        }

        private void RedrawDashedShape()
        {
            DashedShape.Height = Shape.Height;
            DashedShape.Width = Shape.Width;
            Canvas.SetLeft(DashedShape, Canvas.GetLeft(Shape));
            Canvas.SetTop(DashedShape, Canvas.GetTop(Shape));
        }


        public void UpdateRectangle(double newX, double newY, double newWidth, double newHeight)
        {
            //dont use negative value
            if (newHeight >= 0 && newWidth >= 0)
            {
                Canvas.SetLeft(Shape, newX);
                Canvas.SetTop(Shape, newY);
                Shape.Height = newHeight;
                Shape.Width = newHeight;

                if (Shape.Height > _originalCanvas.ActualWidth)
                {
                    Canvas.SetLeft(Shape, 0);
                    Shape.Height = _originalCanvas.ActualWidth;
                    Shape.Width = _originalCanvas.ActualWidth;
                }

                if (Shape.Height + newX > _originalCanvas.ActualWidth)
                {
                    Canvas.SetLeft(Shape, _originalCanvas.ActualWidth - Shape.ActualWidth);
                }
                //we need to update dashed rectangle too
                // UpdateDashedRectangle();
            }
        }

    }
}
