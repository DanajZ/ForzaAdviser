using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;

namespace ForzaAdviser.Dependencies
{
    public class PacketReceiver : IDisposable, IPacketReceiver, INotifyPropertyChanged
    {
        #region Properties
        private UdpClient UClient { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Constructors
        public PacketReceiver()
        {
            if(UClient == null)
            {
                UClient = new();
            }

            if(PropertyChanged == null)
            {
                PropertyChanged += PacketReceiver_PropertyChanged;
                Settings.Default.PropertyChanged += PropertyChanged;
            }
        }

        private void PacketReceiver_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {

            if (sender!.GetType() == typeof(Settings) && e!.PropertyName!.Equals(nameof(Settings.Default.Port)))
            {
                Settings settings = (Settings)sender;
                int currport = settings.Port;

                if(currport == 0)
                {
                    string ipAddress = settings.IPAddress;
                    int port = GeneratePort(ipAddress);
                    Settings.Default.Port = port;
                    settings.Save();
                }   
            }
        }

        #endregion

        public int GeneratePort(string ipAddress)
        {
            if(UClient != null)
            {
                Dispose();
            }

            IPAddress iPAddress = IPAddress.Parse(ipAddress);

            IPEndPoint iEndPoint = new(iPAddress, Settings.Default.Port);

            UClient = new(iEndPoint);

            return (UClient.Client.LocalEndPoint as IPEndPoint)!.Port;
        }

        public void Dispose()
        {
            UClient.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
