using Senderismo.Clases;
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

namespace Senderismo
{

    public partial class PreferenciasWindow : Window
    {
        private Preferencia preferencias;
        public PreferenciasWindow()
        {
            InitializeComponent();
            preferencias = MainWindow.PConfig;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.button_pref.IsEnabled = true;
            PrincipalWindow.button_pref.IsEnabled = true;
        }

    }
}