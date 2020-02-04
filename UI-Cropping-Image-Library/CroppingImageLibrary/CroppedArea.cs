using System.Windows;

namespace CroppingImageLibrary
{
    /// <summary>
    ///     Represents cropping area.
    /// </summary>
    public class CroppedArea
    {
        /// <summary>
        ///     Original size (WPF units).
        /// </summary>
        public readonly Size OriginalSize;

        /// <summary>
        ///     Cropped rect (WPF units)
        /// </summary>
        public readonly Rect CroppedRectAbsolute;

        /// <summary>
        ///     Normalized cropped area (all values from 0 to 1).
        /// </summary>
        public readonly Rect CroppedRectRelative;

        public CroppedArea(Size originalSize, Rect croppedRectAbsolute)
        {
            OriginalSize        = originalSize;
            CroppedRectAbsolute = croppedRectAbsolute;

            CroppedRectRelative = new Rect(
                new Point(croppedRectAbsolute.X    / originalSize.Width, croppedRectAbsolute.Y      / originalSize.Height),
                new Size(croppedRectAbsolute.Width / originalSize.Width, croppedRectAbsolute.Height / originalSize.Height)
            );
        }
    }
}