using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IDisposableObservableObjectBase : INotifyPropertyChanged, IDisposable
    {
        event CancelEventHandler NotifierObjectDisposing;
    }
}
