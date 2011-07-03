using System.Windows.Input;

namespace MultiTouch.Behaviors.WP7
{
#if DEBUG
    public class TouchDeviceWrapper : ITouchDevice
    {
        private readonly TouchDevice _device;
        private readonly int _id;

        public TouchDeviceWrapper(TouchDevice device)
        {
            _device = device;
        }

        public TouchDeviceWrapper(int id)
        {
            _id = id;
        }

        public int Id
        {
            get
            {
                if (_device != null)
                {
                    return _device.Id;
                }

                return _id;
            }
        }
    }
#endif
}