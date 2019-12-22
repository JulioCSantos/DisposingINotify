using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IDisposableNotifier : IDisposable
    {
        event CancelEventHandler DisposingHandlers;
        event EventHandler DisposedHandlers;
    }
}
