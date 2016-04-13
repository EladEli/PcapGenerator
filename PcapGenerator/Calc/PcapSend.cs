using PcapDotNet.Core;
using PcapDotNet.Packets;
using System.IO;
using System.Windows;

namespace PcapGenerator.Calc
{
    internal class PcapSend
    {
        public MessageBoxResult Send(string PcapPath, PacketDevice Nic)
        {
            try
            {
                var capLength = new FileInfo(PcapPath).Length;
                var selectedPcap = new OfflinePacketDevice(PcapPath);
                using (var inputCommunicator = selectedPcap.Open(65536, PacketDeviceOpenAttributes.Promiscuous, 1000))
                {
                    using (var outputCommunicator = Nic.Open(100, PacketDeviceOpenAttributes.Promiscuous, 1000))
                    {
                        using (var sendBuffer = new PacketSendBuffer((uint)capLength))
                        {
                            var numPackets = 0;
                            var falsePackets = 0;
                            Packet packet;
                            while (inputCommunicator.ReceivePacket(out packet) == PacketCommunicatorReceiveResult.Ok)
                            {
                                if ((packet.Count > 1514))
                                {
                                    ++falsePackets;
                                }
                                else
                                {
                                    sendBuffer.Enqueue(packet);
                                    ++numPackets;
                                }

                            }
                            outputCommunicator.Transmit(sendBuffer, false);
                            return MessageBox.Show(numPackets.ToString() + " " + "Packets were sent successfully,\n"+
                                                   falsePackets.ToString() +" " + "Packets weren't sent successfully,\n"+
                                                   " Would you like to exit?","Success",MessageBoxButton.YesNo);
                        }
                    }
                }
            }
            catch(System.ArgumentException)
            {
                return MessageBox.Show("User Must Choose a file");
            }
            catch(System.InvalidOperationException)
            {
                return MessageBox.Show("Wrong File, only .pcap file supperted");
            }
        }
    }
}
