using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IDisposingINotifierObject : INotifyPropertyChanged, IDisposable
    {
        event EventHandler NotifierObjectDisposing;
    }
}
