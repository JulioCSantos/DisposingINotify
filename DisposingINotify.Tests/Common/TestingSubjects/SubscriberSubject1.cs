namespace DisposingINotify.Tests.Common.TestingSubjects
{
    public class SubscriberSubject1
    {
        private PublisherSubject1 _publisher1;

        public PublisherSubject1 Publisher1 => _publisher1 ?? (_publisher1 = new PublisherSubject1());


        public void SubscribePublisher1() {
            Publisher1.PropertyChanged += Publisher1_PropertyChanged;
        }

        public void UnSubscribePublisher1() {
            Publisher1.PropertyChanged -= Publisher1_PropertyChanged;
        }

        private void Publisher1_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        { Pub1PropertyChanged = true; }

        public bool Pub1PropertyChanged { get; set; }
    }
}