namespace DisposingINotify.Tests.Common.TestingSubjects
{
    public class SubscriberSubject1
    {

        private PublisherSubject1 _publisher1;

        public PublisherSubject1 Publisher1
        {
            get { return _publisher1 ?? (_publisher1 = new PublisherSubject1()); ; }
            set { _publisher1 = value; }
        }

        public void SubscribePublisher1() {
            Publisher1.PropertyChanged += Publisher1_PropertyChanged;
        }

        public void UnSubscribePublisher1() {
            // this unsubscribe just this subscriber instance
            Publisher1.PropertyChanged -= Publisher1_PropertyChanged;
        }

        private void Publisher1_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        { Pub1PropertyChanges++; }

        public int Pub1PropertyChanges { get; set; }
    }
}