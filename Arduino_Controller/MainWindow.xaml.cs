using CommandMessenger;
using CommandMessenger.Transport.Serial;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;

namespace Arduino_Controller
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new WindowViewModel(this);
        }
    }

}
