using System;

namespace ProducerConsumer.Core
{
    public class Task
    {
        public DateTime CreationTime { get; private set; }

        public DateTime BeginConsumptionTime { get; private set; }
        public int TaskNumber { get; private set; }

        public event EventHandler<string> LogTask;

        public Task(DateTime creationTime, int taskNumber)
        {
            CreationTime = creationTime;
            TaskNumber = taskNumber;
        }

        public void Start(int queueLength)
        {
            LogTask?.Invoke(this, $"Queuelength: {queueLength}, Task {TaskNumber} erzeugt!");
        }

        public void BeginConsumption(int queueLength, DateTime beginConsumptionTime)
        {
            BeginConsumptionTime = beginConsumptionTime;
            LogTask?.Invoke(this, $"Queuelength: {queueLength}, Task {TaskNumber} wird bearbeitet!");
        }

        public void Finish(int queueLength, DateTime finishConsumptionTime)
        {
            LogTask?.Invoke(this, $"Queuelength: {queueLength}, Task {TaskNumber} wurde um {CreationTime.ToShortTimeString()} erzeugt und von {BeginConsumptionTime.ToShortTimeString()} - {finishConsumptionTime.ToShortTimeString()} bearbeitet!");
        }
    }
}
