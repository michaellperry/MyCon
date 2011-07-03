using System;
using System.Windows.Threading;
using UpdateControls.Fields;

namespace FacetedWorlds.MyCon.Models
{
    public class Clock
    {
        private Independent<DateTime> _time = new Independent<DateTime>();
        private DispatcherTimer _timer;

        public Clock()
        {
            UpdateTime();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMinutes(1.0);
            _timer.Tick += (sender, e) => UpdateTime();
            _timer.Start();
        }

        public DateTime Time
        {
            get { return _time; }
        }

        private void UpdateTime()
        {
            _time.Value = (new DateTime(2009, 11, 6) + DateTime.Now.TimeOfDay).AddHours(-12.0);
        }
    }
}
