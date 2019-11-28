using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace DisposingINotify.Gui
{
    public class Publisher1 : CPropertyChangedBase
    {
        //public event EventHandler NotifierObjectDisposing;
        //public event PropertyChangedEventHandler PropertyChanged;

        //public void Dispose()
        //{
        //    NotifierObjectDisposing?.Invoke(this, null);
        //}

        //public void SetProperty<T>(ref T fieldValue, T value, [CallerMemberName] string propertyName = null)
        //{
        //    if (value.Equals(fieldValue)) return;
        //    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { SetProperty(ref myVar, value); }
        }

    }
}
