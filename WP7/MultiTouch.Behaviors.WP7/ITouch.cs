using System.Windows;
using System.Windows.Input;

namespace MultiTouch.Behaviors.WP7
{
    public interface ITouchPoint
    {
        ITouchDevice TouchDevice 
        { 
            get; 
        }

        Point Position 
        { 
            get; 
        }

        TouchAction Action 
        { 
            get; 
        }
    }

    public interface ITouchDevice
    {
        int Id 
        { 
            get; 
        }
    }
}
