using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerConsumer.Core
{
    public interface IObserver
    {
        void OnNewTextLine(object source, string line);
    }
}
