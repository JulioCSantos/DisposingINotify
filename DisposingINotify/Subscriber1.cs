using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisposingINotify.Gui
{
    public class Subscriber1
    {
        public Publisher1 Publisher1 { get; } = new Publisher1();
        public Subscriber1()
        {
            Publisher1.PropertyChanged += Publisher1_PropertyChanged;
            //Publisher1.NotifierObjectDisposing += (d, a) => Publisher1.PropertyChanged -= Publisher1_PropertyChanged;
        }

        private void Publisher1_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            
        }
    }
}
