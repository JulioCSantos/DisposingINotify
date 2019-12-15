using Common;

namespace DisposingINotify.Tests.Common.TestingSubjects
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
