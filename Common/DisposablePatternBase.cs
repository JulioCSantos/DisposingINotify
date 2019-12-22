using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    //https://docs.microsoft.com/en-us/visualstudio/code-quality/ca1063?view=vs-2019
    //https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose?redirectedfrom=MSDN
    //https://www.viva64.com/en/b/0437/
    //https://stackoverflow.com/questions/25786964/how-do-i-cast-an-event-handler-delegate-to-one-with-a-different-signature
    public abstract class DisposablePatternBase : IDisposableNotifier
    {
        //Has Dispose already been called?
        public bool Disposed { get; private set; }

        #region Events

        #region DisposingHandlers
        // ReSharper disable once InconsistentNaming
        private CancelEventHandler _disposingHandlers;
        public event CancelEventHandler DisposingHandlers
        {
            add { _disposingHandlers += value; }
            remove { _disposingHandlers -= value; }
        }
        #endregion DisposingHandlers

        #region NotifierDisposed
        private EventInfo _notifierDisposedEventInfo;
        public EventInfo DisposedEventInfo
        {
            get { return _notifierDisposedEventInfo ?? (_notifierDisposedEventInfo = this.GetType().GetEvent(nameof(DisposedHandlers))); }
        }
        public event EventHandler DisposedHandlers;
        #endregion NotifierDisposed

        #endregion Events

#pragma warning disable CA1063 // Implement IDisposable Correctly. IDisposable is correctly implemented.
        public void Dispose()
        {
            if (Disposed) return;
            if (CanContinueDisposing() == false) return;
            
            try
            {
                InvokeDispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            Disposed = true;
            _disposingHandlers = delegate { }; //Clear Disposing's subscribers if Dispose satisfied.

            NotifyDisposedHandlers();
        }
#pragma warning restore CA1063 // Implement IDisposable Correctly
        protected abstract void Dispose(bool disposing);

        protected void InvokeDispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool CanContinueDisposing()
        {
            var cancelEventArgs = new CancelEventArgs();
            _disposingHandlers?.Invoke(this, cancelEventArgs);
         
            return cancelEventArgs.Cancel == false;
        }


        protected virtual void NotifyDisposedHandlers()
        {
            if (DisposedHandlers != null)
            {
                // must be done one at a time to prevent infinite loops
                foreach (var @delegate in DisposedHandlers.GetInvocationList())
                {
                    DisposedEventInfo.RemoveEventHandler(this, @delegate); // prevents infinite loops
                    @delegate.DynamicInvoke(this, null);
                }
            }

            DisposedHandlers = delegate { }; //Clear subscribers
        }

    }
}
