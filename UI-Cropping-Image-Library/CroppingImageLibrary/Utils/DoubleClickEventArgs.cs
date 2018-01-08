using System;
using System.Windows.Media.Imaging;

namespace CroppingImageLibrary.Utils
{
    public class DoubleClickEventArgs : EventArgs
    {
        public BitmapFrame BitmapFrame { get; set; }
    }
}
