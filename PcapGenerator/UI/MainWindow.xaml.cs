using System.Windows;
using System.Windows.Controls;
using PcapGenerator.UI.Send;

namespace PcapGenerator
{
    public partial class MainWindow : Window
    {
        public SendPcapUserControl _SendPcapWindow { get; set; }
        public MainWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            _SendPcapWindow = new SendPcapUserControl();
            DataContext = this;
        }

    }
}
