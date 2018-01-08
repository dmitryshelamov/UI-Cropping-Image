using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CroppingImageLibrary.Thumbs
{
    internal class ThumbFactory
    {
        /// <summary>
        /// Available thumbs positions
        /// </summary>
        public enum ThumbPosition
        {
            TopLeft,
            TopMiddle,
            TopRight,
            RightMiddle,
            BottomRight,
            BottomMiddle,
            BottomLeft,
            LeftMiddle
        }

        /// <summary>
        /// Thumb factory
        /// </summary>
        /// <param name="thumbPosition">Thumb positions</param>
        /// <param name="canvas">Parent UI element that we will attach thumb as child</param>
        /// <param name="size">Size of thumb</param>
        /// <returns></returns>
        public static ThumbCrop CreateThumb(ThumbPosition thumbPosition, Canvas canvas, double size)
        {
            ThumbCrop customThumb = new ThumbCrop(size);
            customThumb.Cursor = GetCursor(thumbPosition);
            customThumb.Visibility = Visibility.Hidden;
            canvas.Children.Add(customThumb);
            return customThumb;
        }

        /// <summary>
        /// Display proper cursor to corresponding thumb
        /// </summary>
        /// <param name="thumbPosition">Thumb position</param>
        /// <returns></returns>
        private static Cursor GetCursor(ThumbPosition thumbPosition)
        {
            switch (thumbPosition)
            {
                case (ThumbPosition.TopLeft):
                    return Cursors.SizeNWSE;
                case (ThumbPosition.TopMiddle):
                    return Cursors.SizeNS;
                case (ThumbPosition.TopRight):
                    return Cursors.SizeNESW;
                case (ThumbPosition.RightMiddle):
                    return Cursors.SizeWE;
                case (ThumbPosition.BottomRight):
                    return Cursors.SizeNWSE;
                case (ThumbPosition.BottomMiddle):
                    return Cursors.SizeNS;
                case (ThumbPosition.BottomLeft):
                    return Cursors.SizeNESW;
                case (ThumbPosition.LeftMiddle):
                    return Cursors.SizeWE;
                default:
                    return null;
            }
        }
    }
}
