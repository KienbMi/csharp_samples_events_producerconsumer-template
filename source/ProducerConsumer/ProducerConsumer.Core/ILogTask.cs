using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerConsumer.Core
{
    public interface ILogTask
    {
        void AddLineToTextBox(object source, string line);
    }
}
