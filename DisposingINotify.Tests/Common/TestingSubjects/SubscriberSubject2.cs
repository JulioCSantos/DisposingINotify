using Common;

namespace DisposingINotify.Tests.Common.TestingSubjects
{
    public class SubscriberSubject2 : DisposableNotifiersBase
    {
        private PublisherSubject1 _publisher1 = new PublisherSubject1();

        public PublisherSubject1 Publisher1
        {
            //get { return _publisher1 ?? (Publisher1 = new PublisherSubject1()); }
            get { return _publisher1; }
            set
            {
                //if (value == null) _publisher1?.Dispose();
                //_publisher1 = value;
                SetProperty(ref _publisher1, value);

            }
        }

        private PublisherSubject1 _publisher2 = new PublisherSubject1();

        public PublisherSubject1 Publisher2
        {
            //get { return _publisher2 ?? (Publisher2 = new PublisherSubject1()); ; }
            get { return _publisher2; }
            set
            {
                //if (value == null) _publisher2?.Dispose();
                //_publisher2 = value;
                SetProperty(ref _publisher2, value);

            }
        }


        public void SubscribePublisher1() {
            Publisher1.PropertyChanged += Publisher1_PropertyChanged;
        }

        public void SubscribePublisher2() {
            Publisher2.PropertyChanged += Publisher2_PropertyChanged;
        }


        public void UnSubscribePublisher1()
        {
            // this unsubscribe just this subscriber instance
            Publisher1.PropertyChanged -= Publisher1_PropertyChanged;
        }


        public void UnSubscribePublisher2()
        {
            // this unsubscribe just this subscriber instance
            Publisher2.PropertyChanged -= Publisher2_PropertyChanged;
        }

        private void Publisher1_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        { Pub1PropertyChanges++; }
        public int Pub1PropertyChanges { get; set; }
        
        private void Publisher2_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        { Pub2PropertyChanges++; }
        public int Pub2PropertyChanges { get; set; }

    }
}