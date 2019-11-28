using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DisposingINotify
{
    public class Subscriber2 : ICommandSource
    {
        public ICommand Command => throw new NotImplementedException();

        public object CommandParameter => throw new NotImplementedException();

        public IInputElement CommandTarget => throw new NotImplementedException();
    }
}
