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

        #region Handlers
        public List<PropertyChangedEventHandler> Handlers = new List<PropertyChangedEventHandler>();
        #endregion Handlers

        #region Events
        #region PropertyChanged
        // ReSharper disable once InconsistentNaming
        private PropertyChangedEventHandler _propertyChanged;
        public event PropertyChangedEventHandler PropertyChanged
        {
            add { _propertyChanged += value; Handlers.Add(value);
            }
            remove {
                // ReSharper disable once DelegateSubtraction
                if (_propertyChanged != null) _propertyChanged -= value;
                Handlers.Remove(value);
            }
        }
        #endregion PropertyChanged

        #region DisposingNotifier
        // ReSharper disable once InconsistentNaming
        private CancelEventHandler DisposingNotifier;
        public event CancelEventHandler NotifierObjectDisposing
        {
            add { DisposingNotifier += value; }
            remove { DisposingNotifier -= value; }
        }
        #endregion DisposingNotifier

        #region NotifierDisposed

        public event EventHandler NotifierDisposed;
        #endregion NotifierDisposed

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
        public IEnumerable<string> GetMethodsNamesList() {
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
            var cancelEventArgs = new CancelEventArgs();
            DisposingNotifier?.Invoke(this, cancelEventArgs);
            if (cancelEventArgs.Cancel) return;
            
            // ReSharper disable once DelegateSubtraction
            Handlers.ForEach(dh => _propertyChanged -= dh);
            Handlers.Clear();

            NotifierDisposed?.Invoke(this, null);

            DisposingNotifier = delegate { }; //Clear event
            NotifierDisposed = delegate { }; //Clear event
        }
        #endregion IDisposable

    }

}
