using System.Windows;

namespace CroppingImageLibrary.Services.State
{
    internal interface IToolState
    {
        void OnMouseDown(Point point);
        Position? OnMouseMove(Point point);
        void OnMouseUp(Point point);
    }
}