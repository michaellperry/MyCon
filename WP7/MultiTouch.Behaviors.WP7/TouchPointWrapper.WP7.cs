using System.Windows;
using System.Windows.Input;

namespace MultiTouch.Behaviors.WP7
{
#if DEBUG
    public class TouchPointWrapper : ITouchPoint
    {
        private readonly TouchPoint _realPoint;
        private readonly ITouchDevice _device;
        private readonly Point _position;
        private readonly TouchAction _action;

        public TouchPointWrapper(TouchPoint real)
        {
            _realPoint = real;
        }

        public TouchPointWrapper(Point position, TouchAction action, int id)
        {
            _position = position;
            _action = action;
            _device = new TouchDeviceWrapper(id);
        }

        public ITouchDevice TouchDevice
        {
            get 
            {
                if (_realPoint != null)
                {
                    return new TouchDeviceWrapper(_realPoint.TouchDevice);
                }

                return _device;
            }
        }

        public Point Position
        {
            get 
            {
                if (_realPoint != null)
                {
                    return _realPoint.Position;
                }

                return _position;
            }
        }

        public TouchAction Action
        {
            get 
            {
                if (_realPoint != null)
                {
                    return _realPoint.Action;
                }

                return _action;
            }
        }
    }
#endif
}