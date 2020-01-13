using System;
using System.Collections.Generic;

namespace ProducerConsumer.Core
{
    public class Consumer
    {
        Task _currentTask;
        FastClock _fastClock;
        int _minDuration;
        int _maxDuration;
        int _minutesToFinishConsumption;
        Queue<Task> _queue;
        Random _random;

        public bool IsBusy { get; set; }

        public Consumer(int min, int max, FastClock fastClock, Queue<Task> queue)
        {
            _fastClock = fastClock;
            _queue = queue;
            _maxDuration = max;
            _minDuration = min;

            _random = new Random(DateTime.Now.Millisecond);
            _fastClock.OneMinuteIsOver += Instance_OneMinuteIsOver;
        }

        private void Instance_OneMinuteIsOver(object sender, DateTime time)
        {
            if (_minutesToFinishConsumption > 0)
            {
                _minutesToFinishConsumption--;
            }
            else
            {
                if (IsBusy == false && _queue.Count > 0)
                {
                    _currentTask = _queue.Dequeue();
                    _currentTask.BeginConsumption(_queue.Count, time);
                    IsBusy = true;
                }
                else if (IsBusy)
                {
                    _currentTask.Finish(_queue.Count, time);
                    IsBusy = false;
                    _currentTask = null;
                }

                _minutesToFinishConsumption = _random.Next(_minDuration, _maxDuration + 1);
            }
        }

        public void DetachFromFastClock()
        {
            _fastClock.OneMinuteIsOver -= Instance_OneMinuteIsOver;
        }
    }
}
