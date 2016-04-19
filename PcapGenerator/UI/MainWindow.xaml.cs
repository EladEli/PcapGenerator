using System.Windows;
using PcapGenerator.UI.Send;

namespace PcapGenerator
{
    public partial class MainWindow : Window
    {
        public SendPcapUserControl _SendPcapWindow { get; set; }
        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            _SendPcapWindow = new SendPcapUserControl();
            DataContext = this;
        }

    }
}
