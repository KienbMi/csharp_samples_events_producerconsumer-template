using System;
using System.Collections.Generic;

namespace ProducerConsumer.Core
{
    public class Producer
    {
        private IObserver _logTask;
        Queue<Task> _queue;
        FastClock _fastClock;
        int _minutesToNextProduction;
        int _maxDuration;
        int _minDuration;
        Random _random;
        int _taskNumber;

        public Producer(int min, int max, FastClock fastClock, IObserver logTask, Queue<Task> queue)
        {
            _logTask = logTask;
            _fastClock = fastClock;
            _queue = queue;
            _maxDuration = max;
            _minDuration = min;
            
            _random = new Random(DateTime.Now.Millisecond);
            _fastClock.OneMinuteIsOver += Instance_OneMinuteIsOver;
        }

        private void Instance_OneMinuteIsOver (object source, DateTime time)
        {
            if (_minutesToNextProduction > 0)
            {
                _minutesToNextProduction--;
            }
            else
            {
                Task newTask = new Task(time, _taskNumber++);
                newTask.LogTask += _logTask.OnNewTextLine;
                _queue.Enqueue(newTask);
                newTask.Start(_queue.Count);

                _minutesToNextProduction = _random.Next(_minDuration, _maxDuration + 1);
            }
        }
    }
}
