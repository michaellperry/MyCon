using System;
using System.Collections.Generic;
using System.Threading;

namespace FacetedWorlds.MyCon.ImageUtilities
{
    public class RequestQueue
    {
        private Queue<Action<Action>> _requests = new Queue<Action<Action>>();

        public void QueueRequest(Action<Action> request)
        {
            bool wasEmpty;
            lock (this)
            {
                wasEmpty = _requests.Count == 0;
                _requests.Enqueue(request);
            }
            if (wasEmpty)
            {
                request(InvokeNextRequest);
            }
        }

        private void InvokeNextRequest()
        {
            Action<Action> nextRequest = null;
            lock (this)
            {
                if (_requests.Count > 0)
                    nextRequest = _requests.Dequeue();
            }
            if (nextRequest != null)
            {
                Thread.Sleep(100);
                nextRequest(InvokeNextRequest);
            }
        }
    }
}
