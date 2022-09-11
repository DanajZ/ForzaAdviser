using ForzaAdviser.Dependencies;
using ForzaAdviser.Modals;
using System;
using System.Windows;

namespace ForzaAdviser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StartupWizard StartupWizard { get; set; }

        public MainWindow(StartupWizard startupWizard)
        {
            StartupWizard = startupWizard;

            InitializeComponent();

            StartupWizard.IsVisibleChanged += StartupWizard_IsVisibleChanged;

            if (Settings.Default.ShowStartupWizard)
            {
                StartupWizard.Show();
            }
        }

        private void StartupWizard_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var suw = (StartupWizard)sender;

            if (!suw.IsVisible)
            {
                this.Focus();
            }
        }

        private void MiClose_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void MiStartupWizard_Click(object sender, RoutedEventArgs e)
        {
            StartupWizard.Show();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            App.Current.Shutdown();
        }
    }
}
