using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace DisposingINotify.Tests.Common
{
    public class PublisherSubject1 : DisposableNotifiersBase
    {
        private string _oneProperty;

        public string OneProperty {
            get => _oneProperty;
            set => SetProperty(ref _oneProperty, value);
        }
    }
}
