using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CroppingImageLibrary.Thumbs
{
    internal class ThumbFactory
    {
        /// <summary>
        ///     Available thumbs positions
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
        ///     Thumb factory
        /// </summary>
        /// <param name="thumbPosition">Thumb positions</param>
        /// <param name="canvas">Parent UI element that we will attach thumb as child</param>
        /// <param name="size">Size of thumb</param>
        /// <returns></returns>
        public static ThumbCrop CreateThumb(ThumbPosition thumbPosition, Canvas canvas, double size)
        {
            var customThumb = new ThumbCrop(size)
            {
                Cursor     = GetCursor(thumbPosition),
                Visibility = Visibility.Hidden
            };
            canvas.Children.Add(customThumb);
            return customThumb;
        }

        /// <summary>
        ///     Display proper cursor to corresponding thumb
        /// </summary>
        /// <param name="thumbPosition">Thumb position</param>
        /// <returns></returns>
        private static Cursor GetCursor(ThumbPosition thumbPosition)
        {
            return thumbPosition switch
            {
                ThumbPosition.TopLeft      => Cursors.SizeNWSE,
                ThumbPosition.TopMiddle    => Cursors.SizeNS,
                ThumbPosition.TopRight     => Cursors.SizeNESW,
                ThumbPosition.RightMiddle  => Cursors.SizeWE,
                ThumbPosition.BottomRight  => Cursors.SizeNWSE,
                ThumbPosition.BottomMiddle => Cursors.SizeNS,
                ThumbPosition.BottomLeft   => Cursors.SizeNESW,
                ThumbPosition.LeftMiddle   => Cursors.SizeWE,
                _                          => null
            };
        }
    }
}