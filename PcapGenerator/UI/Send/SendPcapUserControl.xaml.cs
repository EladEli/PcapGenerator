using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Controls;
using PcapDotNet.Core.Extensions;
using PcapGenerator.Calc;

namespace PcapGenerator.UI.Send
{
    public partial class SendPcapUserControl
    {
        public NetworkInterface [] _nics { get; set; }
        public SendPcapUserControl()
        {
            InitializeComponent();
            _nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var nic in _nics)
            {
                NicList.Items.Add(nic.Name);
            }
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var browseWindow = new System.Windows.Forms.OpenFileDialog();
            browseWindow.ShowDialog();
            PathBox.Text = browseWindow.FileName;
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedNic = (from NetworkInterface nic in _nics
                    where nic.Name == (NicList.SelectedItem.ToString())
                    select nic.GetLivePacketDevice()).Single();
                var pcapHandler = new PcapSend();
                var sendResult = pcapHandler.Send(PathBox.Text, selectedNic);
                switch (sendResult)
                {
                    case MessageBoxResult.Yes:
                        Application.Current.Shutdown();
                        break;
                    case MessageBoxResult.No:
                        CleanFields();
                        break;
                    case MessageBoxResult.None:
                        CleanFields();
                        break;
                }

            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("User Must Select A NIC");
            }

        }
        public void CleanFields()
        {
            PathBox.Text = null;
            NicList.SelectedItem = null;
        }

    }
}
