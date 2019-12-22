using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    ////https://stackoverflow.com/questions/25786964/how-do-i-cast-an-event-handler-delegate-to-one-with-a-different-signature
    //public abstract class DisposableNotifiersBase : IDisposableNotifier
    public abstract class DisposableNotifiersBase : DisposablePatternBase, INotifyPropertyChanged
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

        //#region NotifierDisposing
        //// ReSharper disable once InconsistentNaming
        //private CancelEventHandler DisposingNotifier;
        //public event CancelEventHandler NotifierDisposing
        //{
        //    add { DisposingNotifier += value; }
        //    remove { DisposingNotifier -= value; }
        //}
        //#endregion NotifierDisposing

        //#region NotifierDisposed
        //private EventInfo _notifierDisposedEventInfo;
        //public EventInfo NotifierDisposedEventInfo {
        //    get { return _notifierDisposedEventInfo ?? (_notifierDisposedEventInfo = this.GetType().GetEvent(nameof(NotifierDisposed))); }
        //}
        //public event EventHandler NotifierDisposed;
        //#endregion NotifierDisposed

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


        [DebuggerStepThrough]
        protected virtual bool SetPropertyAndDispose<T>(ref T prevValue, T value, [CallerMemberName] string propertyName = null) 
            where T : IDisposableNotifier
        {
            if (Equals(prevValue, value)) return false;
            var isSet = SetProperty(ref prevValue, value, propertyName);
            if (isSet) prevValue.Dispose();
            return isSet;
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

        protected override void Dispose(bool disposing)
        {
            // ReSharper disable once DelegateSubtraction
            Handlers.ForEach(dh => _propertyChanged -= dh);
            Handlers.Clear();
        }

        #endregion methods

    }

}
