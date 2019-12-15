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
    public abstract class DisposableNotifiersBase : IDisposableObservableObjectBase
    {

        #region _handlersTypes
        private readonly Dictionary<object, Dictionary<PropertyChangedEventHandler, object>> _handlersTypes = 
            new Dictionary<object, Dictionary<PropertyChangedEventHandler, object>>();
        #endregion _handlersTypes

        #region _handlers
        private readonly Dictionary<PropertyChangedEventHandler, Dictionary<object, PropertyChangedEventHandler>> _handlers =
            new Dictionary<PropertyChangedEventHandler, Dictionary<object, PropertyChangedEventHandler>>();
        #endregion _handlers

        #region Events
        #region PropertyChanged
        // ReSharper disable once InconsistentNaming
        private PropertyChangedEventHandler _propertyChanged;
        public event PropertyChangedEventHandler PropertyChanged
        {
            add { _propertyChanged += value;
                _handlersTypes.TryAdd(value.Target, value);
                _handlers.TryAdd(value, value.Target);
            }
            remove {
                if (_propertyChanged != null) _propertyChanged -= value;
                _handlersTypes.TryRemove(value.Target, value);
                _handlers.TryRemove(value, value.Target);
            }
        }
        #endregion PropertyChanged

        // ReSharper disable once InconsistentNaming
        private CancelEventHandler notifierObjectDisposing;
        public event CancelEventHandler NotifierObjectDisposing
        {
            add { notifierObjectDisposing += value; }
            remove { notifierObjectDisposing -= value; }
        }
        #endregion Events

        [DebuggerStepThrough]
        public void RaisePropertyChanged([CallerMemberName] string propertyName = null) {
            _propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [DebuggerStepThrough]
        protected virtual bool SetProperty<T>(ref T prevValue, T value, [CallerMemberName] string propertyName = null) {
            if (Equals(prevValue, value)) return false;
            prevValue = value;
            this.RaisePropertyChanged(propertyName);
            return true;
        }

        #region methods
        public IEnumerable<string> GetSubscribersList() {
            var names = GetInvocationList().Select(d => d.Method.Name).Distinct();
            return names;
        }

        public IEnumerable<Type> GetSubscribersTypesList() {
            var types = GetInvocationList().ToList().Select(d => d.Target.GetType()).Distinct();
            return types;
        }

        public IEnumerable<Delegate> GetInvocationList() {
            if (_propertyChanged == null) return Enumerable.Empty<Delegate>();
            return _propertyChanged?.GetInvocationList();
        }
        #endregion methods

        #region IDisposable
        public void Dispose()
        {
            notifierObjectDisposing?.Invoke(this, null);
            _propertyChanged = delegate { };
            //var handlersClone = _handlersTypes.Where(kv => kv.Key == this).Select(kv => kv.Value).ToList();
            //handlersClone.ForEach(eh => PropertyChanged -= eh);

        }
        #endregion IDisposable

    }

}
