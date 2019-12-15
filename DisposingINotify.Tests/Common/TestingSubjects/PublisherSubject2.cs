using Common;

namespace DisposingINotify.Tests.Common
{
    public class PublisherSubject2 : DisposableNotifiersBase
    {
        private string _oneProperty;

        public string OneProperty {
            get => _oneProperty;
            set => SetProperty(ref _oneProperty, value);
        }
    }
}