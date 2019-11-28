using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DisposingINotify.Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Subscriber1 Subscriber1 { get; set; } = new Subscriber1();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Subscribe_Click(object sender, RoutedEventArgs e)
        {
            this.DelegatesList.ItemsSource = Delegates;
            this.DelegatesTypesList.ItemsSource = DelegatesTypes;
        }

        private void Dispose_Click(object sender, RoutedEventArgs e)
        {
            Subscriber1.Publisher1.Dispose();
        }

        public List<string> Delegates { get { return Subscriber1?.Publisher1?.GetSubscribersList()?.ToList() ?? new List<string> {"<none>"}; } }
        public List<Type> DelegatesTypes { get { return Subscriber1?.Publisher1?.GetSubscribersTypesList()?.ToList(); } }
    }
}
