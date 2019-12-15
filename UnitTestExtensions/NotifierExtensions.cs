using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestExtensions
{
    public static class NotifierExtensions
    {
        public static NotifyExpectation<T>
            NotifiesPropertyChanged<T, TProperty>(this T owner,
                Expression<Func<T, TProperty>> propertyPicker)
            where T : INotifyPropertyChanged
        {
            return NotifierExtensions.CreateExpectation(owner,
                propertyPicker, true);
        }

        public static NotifyExpectation<T>
            ShouldNotNotifyOn<T, TProperty>(this T owner,
                Expression<Func<T, TProperty>> propertyPicker)
            where T : INotifyPropertyChanged
        {
            return NotifierExtensions.CreateExpectation(owner,
                propertyPicker, false);
        }

        private static NotifyExpectation<T>
            CreateExpectation<T, TProperty>(T owner,
                Expression<Func<T, TProperty>> pickProperty,
                bool eventExpected) where T : INotifyPropertyChanged
        {
            string propertyName =
                ((MemberExpression)pickProperty.Body).Member.Name;
            return new NotifyExpectation<T>(owner,
                propertyName, eventExpected);
        }
    }
}
