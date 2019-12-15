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
    public class PublisherSubject2 : DisposableNotifiersBase
    {
        private string _oneProperty;

        public string OneProperty {
            get => _oneProperty;
            set => SetProperty(ref _oneProperty, value);
        }
    }

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

    public class SubscriberSubject2
    {
        private PublisherSubject1 _publisher1;
        public PublisherSubject1 Publisher1 => _publisher1 ?? (_publisher1 = new PublisherSubject1());

        private PublisherSubject2 _publisher2;
        public PublisherSubject2 Publisher2 => _publisher2 ?? (_publisher2 = new PublisherSubject2());


        public void SubscribePublisher1() {
            Publisher1.PropertyChanged += Publisher1_PropertyChanged;
        }

        public void SubscribePublisher2() {
            Publisher2.PropertyChanged += Publisher2_PropertyChanged;
        }

        private void Publisher1_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            { Pub1PropertyChanged = true; }

        public bool Pub1PropertyChanged { get; set; }

        private void Publisher2_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            { Pub2PropertyChanged = true; }
        public bool Pub2PropertyChanged { get; set; }
    }
}
