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
    public partial class AyudaWindow : Window
    {
        public AyudaWindow()
        {
            InitializeComponent();
            ListBox_ayudas.ItemsSource = MainWindow.Ayudas;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.button_ayuda.IsEnabled = true;
            PrincipalWindow.button_ayuda.IsEnabled = true;
        }

        private void listbox_ayudas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Ayuda ayuda_selec = ListBox_ayudas.SelectedItem as Ayuda;
            if (ayuda_selec != null)
            {
                txt_contenido_ayuda.Text = ayuda_selec.Contenido;
            }
            else
            {
                ListBox_ayudas.SelectedIndex = 0;
            }
        }
    }
}
