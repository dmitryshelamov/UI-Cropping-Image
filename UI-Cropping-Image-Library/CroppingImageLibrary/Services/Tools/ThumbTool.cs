using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace CroppingImageLibrary.Services.Tools
{
    internal class ThumbCrop : Thumb
    {
        public ThumbCrop(double thumbSize, Cursor cursor)
        {
            ThumbSize = thumbSize;
            Cursor = cursor;
            PreviewMouseLeftButtonDown += ThumbCrop_PreviewMouseLeftButtonDown;
            PreviewMouseLeftButtonUp += ThumbCrop_PreviewMouseLeftButtonUp;
        }

        private void ThumbCrop_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ThumbCrop thumb = sender as ThumbCrop;
            thumb?.ReleaseMouseCapture();
        }

        private void ThumbCrop_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ThumbCrop thumb = sender as ThumbCrop;
            thumb?.CaptureMouse();
        }

        public double ThumbSize { get; }

        /// <summary>
        ///     Set thumb to corresponding positions
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public void Redraw(double x, double y)
        {
            Canvas.SetTop(this, y - ThumbSize / 2);
            Canvas.SetLeft(this, x - ThumbSize / 2);
        }

        protected override Visual GetVisualChild(int index) => null;

        /// <summary>
        ///     Custom visual style of thumb
        /// </summary>
        /// <param name="drawingContext"></param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawRectangle(Brushes.White, new Pen(Brushes.Black, 2), new Rect(new Size(ThumbSize, ThumbSize)));
            drawingContext.DrawRectangle(Brushes.Black, new Pen(Brushes.Black, 0), new Rect(2, 2, 6, 6));
        }
    }
    internal class ThumbTool
    {
        private readonly CropTool _cropTool;

        public ThumbCrop BottomMiddle { get; }
        public ThumbCrop LeftMiddle { get; }
        public ThumbCrop TopMiddle { get; }
        public ThumbCrop RightMiddle { get; }
        public ThumbCrop TopLeft { get; }
        public ThumbCrop TopRight { get; }
        public ThumbCrop BottomLeft { get; }
        public ThumbCrop BottomRight { get; }
        private readonly Canvas _canvas;
        private readonly double _thumbSize = 10;

        public ThumbTool(Canvas canvas, CropTool cropTool)
        {
            _canvas = canvas;
            _cropTool = cropTool;
            BottomMiddle = new ThumbCrop(_thumbSize, Cursors.SizeNS);
            LeftMiddle = new ThumbCrop(_thumbSize, Cursors.SizeWE);
            TopMiddle = new ThumbCrop(_thumbSize, Cursors.SizeNS);
            RightMiddle = new ThumbCrop(_thumbSize, Cursors.SizeWE);
            TopLeft = new ThumbCrop(_thumbSize, Cursors.SizeNWSE);
            TopRight = new ThumbCrop(_thumbSize, Cursors.SizeNESW);
            BottomLeft = new ThumbCrop(_thumbSize, Cursors.SizeNESW);
            BottomRight = new ThumbCrop(_thumbSize, Cursors.SizeNWSE);

            LeftMiddle.DragDelta += LeftMiddle_DragDelta;
            BottomMiddle.DragDelta += BottomMiddle_DragDelta;
            TopMiddle.DragDelta += TopMiddle_DragDelta;
            RightMiddle.DragDelta += RightMiddle_DragDelta;
            TopLeft.DragDelta += TopLeft_DragDelta;
            TopRight.DragDelta += TopRight_DragDelta;
            BottomLeft.DragDelta += BottomLeft_DragDelta;
            BottomRight.DragDelta += BottomRight_DragDelta;
        }

        private void BottomRight_DragDelta(object sender, DragDeltaEventArgs e)
        {
            ThumbCrop thumb = sender as ThumbCrop;

            double resultThumbLeft = Canvas.GetLeft(thumb) + e.HorizontalChange;

            if (resultThumbLeft > _canvas.ActualWidth)
                resultThumbLeft = _canvas.ActualWidth;

            double thumbResultTop = Canvas.GetTop(thumb) + e.VerticalChange;
            if (thumbResultTop + _thumbSize / 2 > _canvas.ActualHeight)
                thumbResultTop = _canvas.ActualHeight - _thumbSize / 2;

            double resultHeight = thumbResultTop - _cropTool.TopLeftY + _thumbSize / 2;
            double resultWidth = resultThumbLeft - _cropTool.TopLeftX;

            _cropTool.Redraw(_cropTool.TopLeftX, _cropTool.TopLeftY, resultWidth, resultHeight);
        }

        private void BottomLeft_DragDelta(object sender, DragDeltaEventArgs e)
        {
            ThumbCrop thumb = sender as ThumbCrop;

            double thumbResultTop = Canvas.GetTop(thumb) + e.VerticalChange;
            if (thumbResultTop + _thumbSize / 2 > _canvas.ActualHeight)
                thumbResultTop = _canvas.ActualHeight - _thumbSize / 2;

            double resultHeight = thumbResultTop - _cropTool.TopLeftY + _thumbSize / 2;

            double resultThumbLeft = Canvas.GetLeft(thumb) + e.HorizontalChange;
            if (resultThumbLeft < 0)
                resultThumbLeft = -_thumbSize / 2;

            double offset = Canvas.GetLeft(thumb) - resultThumbLeft;
            double resultLeft = resultThumbLeft + _thumbSize / 2;
            double resultWidth = _cropTool.Width + offset;
            _cropTool.Redraw(resultLeft, _cropTool.TopLeftY, resultWidth, resultHeight);
        }

        private void TopRight_DragDelta(object sender, DragDeltaEventArgs e)
        {
            ThumbCrop thumb = sender as ThumbCrop;
            double newTop = Canvas.GetTop(thumb) + e.VerticalChange;
            double newLeft = Canvas.GetLeft(thumb) + e.HorizontalChange;

            if (newTop < 0)
                newTop = -_thumbSize / 2;

            double offset = Canvas.GetTop(thumb) - newTop;
            double resultHeight = _cropTool.Height + offset;
            double resultTop = newTop + _thumbSize / 2;



            if (newLeft > _canvas.ActualWidth)
                newLeft = _canvas.ActualWidth;

            double resultWidth = newLeft - _cropTool.TopLeftX;
            _cropTool.Redraw(_cropTool.TopLeftX, resultTop, resultWidth, resultHeight);
        }

        private void TopLeft_DragDelta(object sender, DragDeltaEventArgs e)
        {
            ThumbCrop thumb = sender as ThumbCrop;
            double newTop = Canvas.GetTop(thumb) + e.VerticalChange;
            double newLeft = Canvas.GetLeft(thumb) + e.HorizontalChange;

            if (newTop < 0)
                newTop = -_thumbSize / 2;
            if (newLeft < 0)
                newLeft = -_thumbSize / 2;

            double offsetTop = Canvas.GetTop(thumb) - newTop;
            double resultHeight = _cropTool.Height + offsetTop;
            double resultTop = newTop + _thumbSize / 2;

            double offsetLeft = Canvas.GetLeft(thumb) - newLeft;
            double resultLeft = newLeft + _thumbSize / 2;
            double resultWidth = _cropTool.Width + offsetLeft;

            _cropTool.Redraw(resultLeft, resultTop, resultWidth, resultHeight);
        }

        private void RightMiddle_DragDelta(object sender, DragDeltaEventArgs e)
        {
            ThumbCrop thumb = sender as ThumbCrop;
            double resultThumbLeft = Canvas.GetLeft(thumb) + e.HorizontalChange;

            if (resultThumbLeft > _canvas.ActualWidth)
                resultThumbLeft = _canvas.ActualWidth;

            double resultWidth = resultThumbLeft - _cropTool.TopLeftX;
            _cropTool.Redraw(_cropTool.TopLeftX, _cropTool.TopLeftY, resultWidth, _cropTool.Height);
        }

        private void TopMiddle_DragDelta(object sender, DragDeltaEventArgs e)
        {
            ThumbCrop thumb = sender as ThumbCrop;
            double resultThumbTop = Canvas.GetTop(thumb) + e.VerticalChange;

            if (resultThumbTop < 0)
                resultThumbTop = -_thumbSize / 2;

            double offset = Canvas.GetTop(thumb) - resultThumbTop;
            double resultHeight = _cropTool.Height + offset;
            double resultTop = resultThumbTop + _thumbSize / 2;
            _cropTool.Redraw(_cropTool.TopLeftX, resultTop, _cropTool.Width, resultHeight);
        }

        private void LeftMiddle_DragDelta(object sender, DragDeltaEventArgs e)
        {
            ThumbCrop thumb = sender as ThumbCrop;
            double resultThumbLeft = Canvas.GetLeft(thumb) + e.HorizontalChange;

            if (resultThumbLeft < 0)
                resultThumbLeft = -_thumbSize / 2;

            double offset = Canvas.GetLeft(thumb) - resultThumbLeft;
            double resultLeft = resultThumbLeft + _thumbSize / 2;
            double resultWidth = _cropTool.Width + offset;
            _cropTool.Redraw(resultLeft, _cropTool.TopLeftY, resultWidth, _cropTool.Height);
        }

        private void BottomMiddle_DragDelta(object sender, DragDeltaEventArgs e)
        {
            ThumbCrop thumb = sender as ThumbCrop;
            double thumbResultTop = Canvas.GetTop(thumb) + e.VerticalChange;

            if (thumbResultTop > _canvas.ActualHeight)
                thumbResultTop = _canvas.ActualHeight;

            _cropTool.Redraw(_cropTool.TopLeftX, _cropTool.TopLeftY, _cropTool.Width, thumbResultTop - _cropTool.TopLeftY);
        }


        public void Redraw()
        {
            if (_cropTool.Height <= 0 && _cropTool.Width <= 0)
            {
                ShowThumbs(false);
                return;
            }

            BottomMiddle.Redraw(_cropTool.TopLeftX + _cropTool.Width / 2, _cropTool.TopLeftY + _cropTool.Height);
            LeftMiddle.Redraw(_cropTool.TopLeftX, _cropTool.TopLeftY + _cropTool.Height / 2);
            TopMiddle.Redraw(_cropTool.TopLeftX + _cropTool.Width / 2, _cropTool.TopLeftY);
            RightMiddle.Redraw(_cropTool.TopLeftX + _cropTool.Width, _cropTool.TopLeftY + _cropTool.Height / 2);
            TopLeft.Redraw(_cropTool.TopLeftX, _cropTool.TopLeftY);
            TopRight.Redraw(_cropTool.TopLeftX + _cropTool.Width, _cropTool.TopLeftY);
            BottomLeft.Redraw(_cropTool.TopLeftX, _cropTool.TopLeftY + _cropTool.Height);
            BottomRight.Redraw(_cropTool.TopLeftX + _cropTool.Width, _cropTool.TopLeftY + _cropTool.Height);
            ShowThumbs(true);
        }

        private void ShowThumbs(bool isVisible)
        {
            if (_cropTool.Height > 0 && _cropTool.Width > 0)
            {
                BottomMiddle.Visibility = Visibility.Visible;
                LeftMiddle.Visibility = Visibility.Visible;
                TopMiddle.Visibility = Visibility.Visible;
                RightMiddle.Visibility = Visibility.Visible;
                TopLeft.Visibility = Visibility.Visible;
                TopRight.Visibility = Visibility.Visible;
                BottomLeft.Visibility = Visibility.Visible;
                BottomRight.Visibility = Visibility.Visible;
            }
            else
            {
                BottomMiddle.Visibility = Visibility.Hidden;
                LeftMiddle.Visibility = Visibility.Hidden;
                TopMiddle.Visibility = Visibility.Hidden;
                RightMiddle.Visibility = Visibility.Hidden;
                TopLeft.Visibility = Visibility.Hidden;
                TopRight.Visibility = Visibility.Hidden;
                BottomLeft.Visibility = Visibility.Hidden;
                BottomRight.Visibility = Visibility.Hidden;
            }
        }
    }
}
