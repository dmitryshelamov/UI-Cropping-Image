using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace CroppingImageLibrary.Thumbs
{
    internal class ThumbCrop : Thumb
    {
        public double ThumbSize { get; }

        public ThumbCrop(double thumbSize)
        {
            ThumbSize = thumbSize;
        }

        protected override Visual GetVisualChild(int index)
        {
            return null;
        }

        /// <summary>
        /// Custom visual style of thumb
        /// </summary>
        /// <param name="drawingContext"></param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawRectangle(Brushes.White, new Pen(Brushes.Black, 2), new Rect(new Size(ThumbSize, ThumbSize)));
            drawingContext.DrawRectangle(Brushes.Black, new Pen(Brushes.Black, 0), new Rect(2, 2, 6, 6));
        }

        /// <summary>
        /// Set thumb to corresponding positions
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public void SetPosition(double x, double y)
        {
            Canvas.SetTop(this, y - ThumbSize / 2);
            Canvas.SetLeft(this, x - ThumbSize / 2);
        }
    }
}
