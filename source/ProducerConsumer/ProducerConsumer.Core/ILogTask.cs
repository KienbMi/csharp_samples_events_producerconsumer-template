using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerConsumer.Core
{
    public interface ILogTask
    {
        void SendTextLine(object source, string line);
    }
}
