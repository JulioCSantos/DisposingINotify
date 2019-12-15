using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

//based on  https://blog.ploeh.dk/2009/08/06/AFluentInterfaceForTestingINotifyPropertyChanged
namespace UnitTestExtensions
{
    public class NotifyExpectation<T>
        where T : INotifyPropertyChanged
    {
        private readonly T _owner;
        private readonly string _propertyName;
        private readonly bool _eventExpected;

        public NotifyExpectation(T owner,
            string propertyName, bool eventExpected)
        {
            this._owner = owner;
            this._propertyName = propertyName;
            this._eventExpected = eventExpected;
        }

        public void When(Action<T> action)
        {
            bool eventWasRaised = false;

            //void Handler(object sender, PropertyChangedEventArgs e)
            //{
            //    if (e.PropertyName == this._propertyName) eventWasRaised = true;
            //    this._owner.PropertyChanged -= Handler;
            //}

            PropertyChangedEventHandler handler = null;
            handler = (s, e) => {
                if (e.PropertyName == this._propertyName) eventWasRaised = true;
                this._owner.PropertyChanged -= handler;
            };

            this._owner.PropertyChanged += handler;
            //this.owner.PropertyChanged += (sender, e) =>
            //{
            //    if (e.PropertyName == this.propertyName) eventWasRaised = true;
            //};
            action(this._owner);

            Assert.AreEqual<bool>(this._eventExpected,
                eventWasRaised,
                "PropertyChanged on {0}", this._propertyName);
        }

        public bool WhenTriggerInvoked(Action<T> trigger)
        {
            bool eventWasRaised = false;
 
            void Handler(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == this._propertyName) eventWasRaised = true;
                this._owner.PropertyChanged -= Handler;
            }

            this._owner.PropertyChanged += Handler;
            
            trigger(this._owner);

            return eventWasRaised;

        }
    }
}
