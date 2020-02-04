using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CroppingImageLibrary.Managers;
using CroppingImageLibrary.Utils;

namespace CroppingImageLibrary
{
    /// <summary>
    ///     Adorner that provides image cropping overlay on adorned <see cref="UIElement"/>.
    /// </summary>
    public class CroppingAdorner : Adorner
    {
        private readonly RectangleManager   _rectangleManager;
        private readonly OverlayManager     _overlayManager;
        private readonly ThumbManager       _thumbManager;
        private readonly DisplayTextManager _displayTextManager;
        private readonly VisualCollection   _visualCollection;
        private readonly Canvas             _canvasOverlay;

        private bool _isMouseLeftButtonDown;

        public CroppingAdorner(UIElement adornedElement) : base(adornedElement)
        {
            _visualCollection   = new VisualCollection(this);
            _canvasOverlay      = new Canvas();
            _rectangleManager   = new RectangleManager(_canvasOverlay);
            _overlayManager     = new OverlayManager(_canvasOverlay, _rectangleManager);
            _thumbManager       = new ThumbManager(_canvasOverlay, _rectangleManager);
            _displayTextManager = new DisplayTextManager(_canvasOverlay, _rectangleManager);
            _visualCollection.Add(_canvasOverlay);

            MouseLeftButtonDown += MouseLeftButtonDownEventHandler;
            MouseMove           += MouseMoveEventHandler;
            MouseLeftButtonUp   += MouseLeftButtonUpEventHandler;

            //add overlay
            Loaded += (sender, args) => { _overlayManager.UpdateOverlay(); };

            //if rectangle size changed, re-draw overlay
            _rectangleManager.RectangleSizeChanged += (sender, args) =>
            {
                _overlayManager.UpdateOverlay();
                _displayTextManager.UpdateSizeText();
            };
            _rectangleManager.OnRectangleDoubleClickEvent += (sender, args) => OnRectangleDoubleClickEvent?.Invoke(sender, new DoubleClickEventArgs {BitmapFrame = GetCroppedBitmapFrame()});
        }

        public event EventHandler<DoubleClickEventArgs> OnRectangleDoubleClickEvent;

        // Override the VisualChildrenCount properties to interface with 
        // the adorner's visual collection.
        protected override int VisualChildrenCount => _visualCollection.Count;

        public CroppedArea GetCroppedArea() =>
            new CroppedArea(
                AdornedElement.RenderSize,
                new Rect(_rectangleManager.TopLeft, new Size(_rectangleManager.RectangleWidth, _rectangleManager.RectangleHeight))
            );

        /// <summary>
        ///     Get cropping areas as BitmapFrame
        /// </summary>
        /// <returns></returns>
        public BitmapFrame GetCroppedBitmapFrame()
        {
            var parent = VisualTreeHelper.GetParent(AdornedElement) as UIElement;
            // 1) get current dpi
            var    pSource = PresentationSource.FromVisual(Application.Current.MainWindow);
            Matrix m       = pSource.CompositionTarget.TransformToDevice;
            double dpiX    = m.M11 * 96;
            double dpiY    = m.M22 * 96;

            // 2) create RenderTargetBitmap
            var elementBitmap = new RenderTargetBitmap(
                (int) AdornedElement.RenderSize.Width,
                (int) AdornedElement.RenderSize.Height,
                dpiX,
                dpiY,
                PixelFormats.Default
            );

            //Important
            AdornedElement.Measure(AdornedElement.RenderSize);
            AdornedElement.Arrange(new Rect(AdornedElement.RenderSize));

            // 3) draw element
            elementBitmap.Render(AdornedElement);

            if (parent != null)
            {
                //Important
                parent.Measure(AdornedElement.RenderSize);
                parent.Arrange(new Rect(AdornedElement.RenderSize));
            }

            var crop = new CroppedBitmap(
                elementBitmap,
                new Int32Rect(
                    (int) _rectangleManager.TopLeft.X,
                    (int) _rectangleManager.TopLeft.Y,
                    (int) _rectangleManager.RectangleWidth,
                    (int) _rectangleManager.RectangleHeight
                )
            );
            return BitmapFrame.Create(crop);
        }

        /// <summary>
        ///     Mouse left button down event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseLeftButtonDownEventHandler(object sender, MouseButtonEventArgs e)
        {
            _rectangleManager.MouseLeftButtonDownEventHandler(e);
            _overlayManager.UpdateOverlay();

            const double tolerance = 0.00001;
            if (Math.Abs(_rectangleManager.RectangleWidth) < tolerance && Math.Abs(_rectangleManager.RectangleHeight) < tolerance)
            {
                _thumbManager.ShowThumbs(false);
                _displayTextManager.ShowText(false);
            }

            _isMouseLeftButtonDown = true;
        }

        // Override the GetVisualChild properties to interface with 
        // the adorner's visual collection.
        protected override Visual GetVisualChild(int index) => _visualCollection[index];

        // Positions child elements and determines a size
        protected override Size ArrangeOverride(Size size)
        {
            Size finalSize = base.ArrangeOverride(size);
            _canvasOverlay.Arrange(new Rect(0, 0, AdornedElement.RenderSize.Width, AdornedElement.RenderSize.Height));
            return finalSize;
        }

        private void MouseMoveEventHandler(object sender, MouseEventArgs e)
        {
            if (!_isMouseLeftButtonDown)
                return;

            _rectangleManager.MouseMoveEventHandler(e);
            _overlayManager.UpdateOverlay();
            _thumbManager.ShowThumbs(true);
            _displayTextManager.ShowText(true);
            _displayTextManager.UpdateSizeText();
            _thumbManager.UpdateThumbsPosition();
        }

        private void MouseLeftButtonUpEventHandler(object sender, MouseButtonEventArgs e)
        {
            _rectangleManager.MouseLeftButtonUpEventHandler();
            ReleaseMouseCapture();
            _isMouseLeftButtonDown = false;
        }
    }
}