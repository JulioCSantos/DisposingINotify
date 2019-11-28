using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class DisposingINotifierObjectEventArgs : EventArgs
    {
        public INotifyPropertyChanged DisposingObject { get; private set; }

        public DisposingINotifierObjectEventArgs(INotifyPropertyChanged disposingObject)
        {
            DisposingObject = disposingObject;
        }
    }
}
