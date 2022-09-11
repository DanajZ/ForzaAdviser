using ForzaAdviser.Dependencies;
using System;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace ForzaAdviser.Modals
{
    /// <summary>
    /// Interaction logic for StartupWizard.xaml
    /// </summary>
    public partial class StartupWizard : Window
    {
        private Settings Settings = Settings.Default;
        public IPacketReceiver PacketReceiver { get; }
        public StartupWizard(IPacketReceiver packetReceiver)
        {
            InitializeComponent();
            PacketReceiver = packetReceiver;
            StartConfig();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            this.Focus();
        }

        private void StartConfig()
        {
            if (string.IsNullOrWhiteSpace(Settings.IPAddress))
            {
                Settings.IPAddress = IPAddress.Loopback.ToString();
            }

            if(Settings.Port == default)
            {
                Settings.Port = PacketReceiver.GeneratePort(Settings.IPAddress);
            }
        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            if (Settings.Port == default)
            {
                Settings.Port = PacketReceiver.GeneratePort(Settings.IPAddress);
            }

            Settings.Save();

            base.Close();
        }

        private void TBIPAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            Settings.Save();
        }

        private void TBPort_TextChanged(object sender, TextChangedEventArgs e)
        {
            Settings.Save();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.Visibility = Visibility.Hidden;
            e.Cancel = true;
        }
    }
}
