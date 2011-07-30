using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace FacetedWorlds.MyCon.ImageUtilities
{
    public class RequestQueue
    {
        private Queue<Action<Action>> _requests = new Queue<Action<Action>>();
        private DispatcherTimer _timer = new DispatcherTimer();
        private bool _running = false;

        public RequestQueue()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(250);
            _timer.Tick += new EventHandler(TimerTick);
        }

        public void QueueRequest(Action<Action> request)
        {
            lock (this)
            {
                bool wasEmpty = _requests.Count == 0;
                _requests.Enqueue(request);
                if (wasEmpty)
                {
                    _timer.Start();
                }
            }
        }

        private void InvokeNextRequest()
        {
            lock (this)
            {
                _running = false;
            }
        }

        private void TimerTick(object sender, EventArgs e)
        {
            lock (this)
            {
                if (!_running)
                {
                    if (_requests.Count > 0)
                    {
                        _running = true;
                        var request = _requests.Dequeue();
                        request(InvokeNextRequest);
                    }
                    else
                    {
                        _timer.Stop();
                    }
                }
            }
        }
    }
}
