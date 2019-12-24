using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Common
{
    public interface INotifierBase : INotifyPropertyChanged
    {
        void RaisePropertyChanged([CallerMemberName] string propertyName = null);
        bool SetProperty<T>(ref T prevValue, T value, [CallerMemberName] string propertyName = null);

        //bool SetPropertyAndDispose<T>(ref T prevValue, T value, [CallerMemberName] string propertyName = null); 
        //    //where T : IDisposableNotifier;

        IEnumerable<string> GetMethodsNamesList();
        IEnumerable<Type> GetSubscribersTypesList();
        IEnumerable<Delegate> GetInvocationList();
    }
}