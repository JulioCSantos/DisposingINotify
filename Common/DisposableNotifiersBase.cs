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
    public abstract class DisposableNotifiersBase : DisposablePatternBase, INotifierBase
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
        #endregion Events

        [DebuggerStepThrough]
        public void RaisePropertyChanged([CallerMemberName] string propertyName = null) {
            _propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [DebuggerStepThrough]
        public virtual bool SetProperty<T>(ref T prevValue, T value, [CallerMemberName] string propertyName = null) {
            if (Equals(prevValue, value)) return false;
            prevValue = value;
            this.RaisePropertyChanged(propertyName);
            return true;
        }


        ////[DebuggerStepThrough]
        //public virtual bool SetPropertyAndDispose<T>(ref T prevValue, T value, [CallerMemberName] string propertyName = null) 
        //    //where T : IDisposableNotifier
        //{
        //    if (Equals(prevValue, value)) return false;
        //    if (prevValue == null) return SetProperty(ref prevValue, value, propertyName); 
        //    var disposableValue = prevValue as IDisposableNotifier;
        //    disposableValue?.Dispose();
        //    return true;
        //}

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
