using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public abstract class CPropertyChangedBase : IDisposingINotifierObject
    {

        private Dictionary<object, PropertyChangedEventHandler> handlers = new Dictionary<object, PropertyChangedEventHandler>();
        private event PropertyChangedEventHandler propertyChanged;
        public event PropertyChangedEventHandler PropertyChanged
        {
            add { propertyChanged += value; handlers.Add(this, value); }
            remove { propertyChanged -= value; handlers.Remove(value); }
        }
        
        private event EventHandler notifierObjectDisposing;
        public event EventHandler NotifierObjectDisposing
        {
            add { notifierObjectDisposing += value; }
            remove { notifierObjectDisposing -= value; }
        }

        [DebuggerStepThrough]
        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        [DebuggerStepThrough]
        protected virtual bool SetProperty<T>(ref T prevValue, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(prevValue, value)) return false;
            prevValue = value;
            this.RaisePropertyChanged(propertyName);
            return true;
        }

        public List<string> GetSubscribersList()
        {
            var names = propertyChanged?.GetInvocationList().Select(d => d.Method.Name).ToList();
            return names;
        }

        public IEnumerable<Type> GetSubscribersTypesList()
        {
            var types = propertyChanged?.GetInvocationList()?.ToList().Select(d => d.Target.GetType());
            return types;
        }

        public void Dispose()
        {
            notifierObjectDisposing?.Invoke(this, null);
            var handlersClone = handlers.Where(kv => kv.Key == this).Select(kv => kv.Value).ToList();
            handlersClone.ForEach(eh => PropertyChanged -= eh);
            
        }
    }

}
