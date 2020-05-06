using System.Windows.Controls;
using System.Windows.Shapes;

namespace CroppingImageLibrary.Services.Tools
{
    internal class CropShape
    {
        public Shape Shape { get; }
        public Shape DashedShape { get; }

        public CropShape(Shape shape, Shape dashedShape)
        {
            Shape = shape;
            DashedShape = dashedShape;
        }

        public void Redraw(double newX, double newY, double newWidth, double newHeight)
        {
            //dont use negative value
            if (newHeight >= 0 && newWidth >= 0)
            {
                RedrawSolidShape(newX, newY, newWidth, newHeight);
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
    }
}
