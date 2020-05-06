using System;
using System.Windows;
using System.Windows.Controls;
using CroppingImageLibrary.Services.Tools;

namespace CroppingImageLibrary.Services.State
{
    internal class CreateState : IToolState
    {
        private readonly CropTool _cropTool;
        private readonly Canvas _canvas;

        private Point _startPoint;

        public CreateState(CropTool cropTool, Canvas canvas)
        {
            _cropTool = cropTool;
            _canvas = canvas;
        }

        public void OnMouseDown(Point point)
        {
            _startPoint = point;
        }

        public Position? OnMouseMove(Point point)
        {
            double left = Math.Min(point.X, _startPoint.X);
            double top = Math.Min(point.Y, _startPoint.Y);
            double width = Math.Abs(point.X - _startPoint.X);
            double height = Math.Abs(point.Y - _startPoint.Y);

            SetCreateBorderLimit(ref left, ref top, ref width, ref height);

            return new Position(left, top, width, height);
        }

        public void OnMouseUp(Point point)
        {
            
        }

        private void SetCreateBorderLimit(ref double newX, ref double newY, ref double newWidth, ref double newHeight)
        {
            //  set drawing limits(canvas borders)
            //  so we can't select area outside of canvas border
            //set top limit
            if (newY < 0)
            {
                newY = 0;
                newHeight = _cropTool.Height;
            }

            //set right limit
            if (newX + newWidth > _canvas.ActualWidth)
            {
                newWidth = _canvas.ActualWidth - newX;
            }

            //set left limit
            if (newX < 0)
            {
                newX = 0;
                newWidth = _cropTool.Width;
            }

            //set bottom limit
            if (newY + newHeight > _canvas.ActualHeight)
            {
                newHeight = _canvas.ActualHeight - newY;
            }
        }
    }
}
