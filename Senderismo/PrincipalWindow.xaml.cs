using Microsoft.Win32;
using Senderismo.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
using System.Xml.Linq;

namespace Senderismo
{

    public partial class PrincipalWindow : Window
    {
        public static Label nombreUsuario;
        public static Label apellidosUsuario;
        public static Image imagenAdmin;
        public static Label ultimoAcceso;
        public static TextBox nombreAdmin;
        public static TextBox apellidosAdmin;
        public static TextBox fecha_nacimientoAdmin;
        public static TextBox emailAdmin;
        public static TextBox telefonoAdmin;
        public static TextBox paisAdmin;
        public static Button button_pref;
        public static Button button_ayuda;
        public static ListView listViewImagenes;
        private MainWindow main_window;
        public static Uri imagen_punto_interes_ruta;
        public PrincipalWindow(MainWindow mw)
        {
            InitializeComponent();
            imagenAdmin = ImagenAdmin;
            ultimoAcceso = Label_ultimo_acceso;
            button_pref = Button_Preferencias;
            button_ayuda = Button_Ayuda;
            nombreAdmin = txt_nombre;
            apellidosAdmin = txt_apellidos;
            fecha_nacimientoAdmin = txt_fecha_nacimiento;
            emailAdmin = txt_email;
            telefonoAdmin = txt_telefono;
            paisAdmin = txt_pais;
            main_window = mw;
            ListBox_rutas.ItemsSource = MainWindow.Rutas;
            ListBox_rutas.SelectedIndex = 0;
            ListBox_excursionistas.ItemsSource = MainWindow.Excursionistas;
            ListBox_excursionistas.SelectedIndex = 0;
            ListBox_guias.ItemsSource = MainWindow.Guias;
            ListBox_guias.SelectedIndex = 0;
        }

        public static void asignarUSuario(Administrador a)
        {
            var bitmap = new BitmapImage(a.Imagen);
            imagenAdmin.Source = bitmap;
            ultimoAcceso.Content = a.UltimoAcceso.ToString();
            nombreAdmin.Text = a.Nombre.ToString();
            apellidosAdmin.Text = a.Apellidos.ToString();
            fecha_nacimientoAdmin.Text = a.Fecha_Nacimiento.ToString();
            emailAdmin.Text = a.Email.ToString();
            telefonoAdmin.Text = a.N_Telefono.ToString();
            paisAdmin.Text = a.Pais.ToString();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (principal.Visibility != Visibility.Visible)
            {
                MessageBoxResult result = MessageBox.Show("¿Estás seguro de que quieres cerrar la aplicación?", "Confirmación", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        MainWindow.aAux.UltimoAcceso = DateTime.Now;
                        MessageBox.Show("Hasta luego " + MainWindow.aAux.Nombre, "Cerrando la aplicación");
                        Application.Current.Shutdown();
                        break;
                    case MessageBoxResult.No:
                        e.Cancel = true;
                        break;
                }
            }
            else
            {
                if (MainWindow.aAux != null)
                {
                    MainWindow.aAux.UltimoAcceso = DateTime.Now;
                }
                MessageBox.Show("Hasta luego", "Cerrando la aplicación");

                Application.Current.Shutdown();
            }
        }

        private void Button_Preferencias_Click(object sender, RoutedEventArgs e)
        {
            PreferenciasWindow PrefWin = new PreferenciasWindow();
            PrefWin.Show();
            Button_Preferencias.IsEnabled = false;

        }

        private void Button_Ayuda_Click(object sender, RoutedEventArgs e)
        {
            AyudaWindow AyudaWin = new AyudaWindow();
            AyudaWin.Show();
            Button_Ayuda.IsEnabled = false;

        }

        private void Button_Cerrar_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show("¿Estás seguro de que quieres cerrar la sesión?", "Confirmación", MessageBoxButton.YesNo);
            switch (result)
            {

                case MessageBoxResult.Yes:

                    this.Visibility = Visibility.Collapsed;
                    main_window.Visibility = Visibility.Visible;
                    main_window.Show();

                    MessageBox.Show("Hasta luego " + MainWindow.aAux.Nombre, "Cerrando Sesión");
                    MainWindow.aAux.UltimoAcceso = DateTime.Now;
                    break;
            }
        }

        private void listbox_rutas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Ruta ruta_selec = ListBox_rutas.SelectedItem as Ruta;
            if (ruta_selec != null)
            {
                //cargarImagen
                var bitmap = new BitmapImage(ruta_selec.Foto);
                ImagenRuta.Source = bitmap;
                txt_nombre_ruta.Text = ruta_selec.Nombre;
                txt_distancia_ruta.Text = ruta_selec.Distancia;
                txt_duracion_ruta.Text = ruta_selec.Duracion;
                txt_dificultad_ruta.Text = ruta_selec.Dificultad;
                txt_estado_ruta.Text = ruta_selec.Estado;
                txt_provincia_ruta.Text = ruta_selec.Provincia;
                txt_origen_ruta.Text = ruta_selec.Origen;
                txt_destino_ruta.Text = ruta_selec.Destino;
                DatePicker_Fecha.SelectedDate = ruta_selec.FechaRealizacion.Date;
                txt_descripcion_ruta.Text = ruta_selec.Descripcion;
                cargarExcursionistaRutasListBox(ruta_selec);
                cargarGuiasRutasListBox(ruta_selec);
            }
            else
            {
                ListBox_rutas.SelectedIndex = 0;
            }
        }

        private void listbox_excursionistas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Excursionista excursionista_selec = ListBox_excursionistas.SelectedItem as Excursionista;
            if (excursionista_selec != null)
            {
                //cargarImagen
                var bitmap = new BitmapImage(excursionista_selec.Foto);
                ImagenExcursionista.Source = bitmap;
                txt_nombre_excursionista.Text = excursionista_selec.Nombre;
                txt_apellidos_excursionista.Text = excursionista_selec.Apellidos;
                txt_edad_excursionista.Text = excursionista_selec.Edad.ToString();
                txt_telefono_excursionista.Text = excursionista_selec.Telefono;
                txt_email_excursionista.Text = excursionista_selec.Email;
                cargarRutasExcursionista(excursionista_selec);
                
            }
            else
            {
                ListBox_excursionistas.SelectedIndex = 0;
            }
        }

        private void listbox_guias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Guia guia_selec = ListBox_guias.SelectedItem as Guia;
            if (guia_selec != null)
            {
                var bitmap = new BitmapImage(guia_selec.Foto);
                ImagenGuia.Source = bitmap;
                txt_nombre_guia.Text = guia_selec.Nombre;
                txt_apellidos_guia.Text = guia_selec.Apellidos;
                txt_valoracion.Text = guia_selec.Valoracion.ToString();
                txt_idiomas.Text = guia_selec.Idiomas;
                txt_disponibilidad.Text = guia_selec.Disponibilidad;
                txt_telefono_guia.Text = guia_selec.Telefono;
                txt_email_guia.Text = guia_selec.Email;
                cargarRutasGuias(guia_selec);

            }
            else
            {
                ListBox_guias.SelectedIndex = 0;
            }
        }

        private void cargarExcursionistaRutasListBox(Ruta r_selec)
        {
            List<Excursionista> exc_cargados = new List<Excursionista>();
            if (r_selec != null)
            {
                if (r_selec.Excursionistas.Length > 0)
                {
                    for (int i = 0; i < r_selec.Excursionistas.Length; i++)
                    {
                        exc_cargados.Add(r_selec.Excursionistas[i]);
                    }
                }
                ListBox_excursionistas_rutas.ItemsSource = exc_cargados;
            }
        }

        private void cargarGuiasRutasListBox(Ruta r_selec)
        {
            List<Guia> guias_cargados = new List<Guia>();
            if (r_selec != null)
            {
                if (r_selec.Guias.Length > 0)
                {
                    for (int i = 0; i < r_selec.Guias.Length; i++)
                    {
                        guias_cargados.Add(r_selec.Guias[i]);
                    }
                }
                ListBox_guias_rutas.ItemsSource = guias_cargados;
            }
        }

        private void cargarRutasExcursionista(Excursionista exc_selec)
        {
            List<Ruta> rutas_cargadas = new List<Ruta>();
            if (exc_selec != null)
            {
                if (exc_selec.Rutas.Length > 0)
                {
                    for (int i = 0; i < exc_selec.Rutas.Length; i++)
                    {
                        rutas_cargadas.Add(exc_selec.Rutas[i]);
                    }
                }
                //else
                //{
                    //rutas_cargadas.Add("Este excursionista no está apuntado a ninguna ruta.");
                //}
                ListBox_rutas_excursionistas.ItemsSource = rutas_cargadas;
            }
        }

        private void cargarRutasGuias(Guia guia_selec)
        {
            List<Ruta> rutas_cargadas = new List<Ruta>();
            if (guia_selec != null)
            {
                if (guia_selec.Rutas.Length > 0)
                {
                    for (int i = 0; i < guia_selec.Rutas.Length; i++)
                    {
                        rutas_cargadas.Add(guia_selec.Rutas[i]);
                    }
                }
                ListBox_rutas_guias.ItemsSource = rutas_cargadas;
            }
        }

        private void Anadir_Excursionista_Click(object sender, RoutedEventArgs e)
        {
            Elegir_Ruta.Visibility = Visibility.Collapsed;
            ListBox_rutas.Visibility = Visibility.Collapsed;
            Button_AnadirRuta.Visibility = Visibility.Collapsed;
            Button_EliminarRuta.Visibility = Visibility.Collapsed;
            Button_ModificarRuta.Visibility = Visibility.Collapsed;
            Button_PuntosInteres.Visibility = Visibility.Collapsed;
            Button_AnadirExcursionista.Visibility = Visibility.Collapsed;
            Button_Anadir_Guia.Visibility = Visibility.Collapsed;
            GroupBox_Ruta.Visibility = Visibility.Collapsed;
            Anadir_Excursionista.Visibility = Visibility.Visible;
            CmBox_Excursionistas_Anadir.ItemsSource = MainWindow.Excursionistas;
        }

        private void Aceptar_Exc_Click(object sender, RoutedEventArgs e)
        {
            Boolean existe = false;
            Elegir_Ruta.Visibility = Visibility.Visible;
            ListBox_rutas.Visibility = Visibility.Visible;
            Button_AnadirRuta.Visibility = Visibility.Visible;
            Button_EliminarRuta.Visibility = Visibility.Visible;
            Button_ModificarRuta.Visibility = Visibility.Visible;
            Button_PuntosInteres.Visibility = Visibility.Visible;
            Button_AnadirExcursionista.Visibility = Visibility.Visible;
            Button_Anadir_Guia.Visibility = Visibility.Visible;
            GroupBox_Ruta.Visibility = Visibility.Visible;
            Anadir_Excursionista.Visibility = Visibility.Collapsed;
            Ruta ruta_sel = ListBox_rutas.SelectedItem as Ruta;
            Excursionista excur = CmBox_Excursionistas_Anadir.SelectedItem as Excursionista;
            for (int i = 0; i<ruta_sel.Excursionistas.Length; i++)
            {
                if (excur == ruta_sel.Excursionistas[i])
                {
                    existe = true;
                    break;
                }
            }
            if (existe == false)
            {
                ruta_sel.anadirExcursionista(excur);
                for (int i = 0; i < MainWindow.Excursionistas.Count; i++)
                {
                    if (excur == MainWindow.Excursionistas[i])
                    {
                        MainWindow.Excursionistas[i].anadirRuta(ruta_sel);
                        break;
                    }
                }
            }
            ListBox_excursionistas_rutas.ItemsSource = null;
            ListBox_excursionistas_rutas.ItemsSource = ruta_sel.Excursionistas;
            Excursionista excur2 = ListBox_excursionistas.SelectedItem as Excursionista;
            ListBox_rutas_excursionistas.ItemsSource = null;
            ListBox_rutas_excursionistas.ItemsSource = excur2.Rutas;
        }

        private void Cancelar_Exc_Click(object sender, RoutedEventArgs e)
        {
            Elegir_Ruta.Visibility = Visibility.Visible;
            ListBox_rutas.Visibility = Visibility.Visible;
            Button_AnadirRuta.Visibility = Visibility.Visible;
            Button_EliminarRuta.Visibility = Visibility.Visible;
            Button_ModificarRuta.Visibility = Visibility.Visible;
            Button_PuntosInteres.Visibility = Visibility.Visible;
            Button_AnadirExcursionista.Visibility = Visibility.Visible;
            Button_Anadir_Guia.Visibility = Visibility.Visible;
            GroupBox_Ruta.Visibility = Visibility.Visible;
            Anadir_Excursionista.Visibility = Visibility.Collapsed;

        }

        private void Button_Anadir_Guia_Click(object sender, RoutedEventArgs e)
        {
            vaciarCampos_Guias();
            ListBox_guias.IsEnabled = false;
            txt_nombre_guia.IsEnabled = true;
            txt_apellidos_guia.IsEnabled = true;
            txt_valoracion.IsEnabled = true;
            txt_idiomas.IsEnabled = true;
            txt_disponibilidad.IsEnabled = true;
            txt_telefono_guia.IsEnabled = true;
            txt_email_guia.IsEnabled = true;
            Button_Cargar_Imagen_Guia.Visibility = Visibility.Visible;
            Button_AnadirGuía.Visibility = Visibility.Collapsed;
            Button_EliminarGuía.Visibility = Visibility.Collapsed;
            Button_ModificarGuía.Visibility = Visibility.Collapsed;
            Aceptar_Anadir_Guia.Visibility = Visibility.Visible;
            Cancelar_Anadir_Guia.Visibility = Visibility.Visible;

        }

        private void Aceptar_GuiaAnadir_Click(object sender, RoutedEventArgs e)
        {
            ListBox_guias.IsEnabled = true;
            txt_nombre_guia.IsEnabled = false;
            txt_apellidos_guia.IsEnabled = false;
            txt_valoracion.IsEnabled = false;
            txt_idiomas.IsEnabled = false;
            txt_disponibilidad.IsEnabled = false;
            txt_telefono_guia.IsEnabled = false;
            txt_email_guia.IsEnabled = false;
            Button_AnadirGuía.Visibility = Visibility.Visible;
            Button_EliminarGuía.Visibility = Visibility.Visible;
            Button_ModificarGuía.Visibility = Visibility.Visible;
            Aceptar_Anadir_Guia.Visibility = Visibility.Collapsed;
            Cancelar_Anadir_Guia.Visibility = Visibility.Collapsed;
            Guia guia_creado = new Guia(txt_nombre_guia.Text, txt_apellidos_guia.Text, (ImagenGuia.Source as BitmapImage).UriSource, txt_idiomas.Text,
                txt_disponibilidad.Text, txt_telefono_guia.Text, txt_email_guia.Text,
                Convert.ToDouble(txt_valoracion.Text));
            MainWindow.Guias.Add(guia_creado);
            ListBox_guias.ItemsSource = null;
            ListBox_guias.ItemsSource = MainWindow.Guias;
        }

        private void Cancelar_GuiaAnadir_Click(object sender, RoutedEventArgs e)
        {
            ListBox_guias.IsEnabled = true;
            txt_nombre_guia.IsEnabled = false;
            txt_apellidos_guia.IsEnabled = false;
            txt_valoracion.IsEnabled = false;
            txt_idiomas.IsEnabled = false;
            txt_disponibilidad.IsEnabled = false;
            txt_telefono_guia.IsEnabled = false;
            txt_email_guia.IsEnabled = false;

            Button_AnadirGuía.Visibility = Visibility.Visible;
            Button_EliminarGuía.Visibility = Visibility.Visible;
            Button_ModificarGuía.Visibility = Visibility.Visible;
            Aceptar_Anadir_Guia.Visibility = Visibility.Collapsed;
            Cancelar_Anadir_Guia.Visibility = Visibility.Collapsed;
            Guia guia_selec = ListBox_guias.SelectedItem as Guia;
            if (guia_selec != null)
            {
                var bitmap = new BitmapImage(guia_selec.Foto);
                ImagenGuia.Source = bitmap;
                txt_nombre_guia.Text = guia_selec.Nombre;
                txt_apellidos_guia.Text = guia_selec.Apellidos;
                txt_valoracion.Text = guia_selec.Valoracion.ToString();
                txt_idiomas.Text = guia_selec.Idiomas;
                txt_disponibilidad.Text = guia_selec.Disponibilidad;
                txt_telefono_guia.Text = guia_selec.Telefono;
                txt_email_guia.Text = guia_selec.Email;
                cargarRutasGuias(guia_selec);
            }
            else
            {
                ListBox_guias.SelectedIndex = 0;
            }
        }

        private void Eliminar_Guia_Click(object sender, RoutedEventArgs e)
        {
            if (ListBox_guias.Items.Count != 0)
            {
                Guia guia_sel = ListBox_guias.SelectedItem as Guia;
                MessageBoxResult result = MessageBox.Show("¿Estás seguro de que quieres borrar a " + guia_sel.Nombre + " " + guia_sel.Apellidos + "?", "Confirmación", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        MainWindow.Guias.Remove(guia_sel);
                        ListBox_guias.ItemsSource = null;
                        ListBox_guias.ItemsSource = MainWindow.Guias;
                        MessageBox.Show("Eliminación exitosa.", "Guia borrado");
                        break;
                }
                if (MainWindow.Guias.Count == 0)
                {
                    vaciarCampos_Guias();
                }
            }
            else
            {
                MessageBox.Show("No hay un guía para borrar.", "Lista vacia");
            }
        }

        private void vaciarCampos_Guias()
        {
            List<Ruta> vacia = new List<Ruta> { };
            txt_nombre_guia.Text = "";
            txt_apellidos_guia.Text = "";
            txt_valoracion.Text = "";
            txt_idiomas.Text = "";
            txt_disponibilidad.Text = "";
            txt_telefono_guia.Text = "";
            txt_email_guia.Text = "";
            ListBox_rutas_guias.ItemsSource = vacia;
            Uri imgDefecto = new Uri("Imagenes/usuario.png", UriKind.Relative);
            var bitmap = new BitmapImage(imgDefecto);
            ImagenGuia.Source = bitmap;
        }

        private void Button_ModificarGuia_Click(object sender, RoutedEventArgs e)
        {
            ListBox_guias.IsEnabled = false;
            txt_nombre_guia.IsEnabled = true;
            txt_apellidos_guia.IsEnabled = true;
            txt_valoracion.IsEnabled = true;
            txt_idiomas.IsEnabled = true;
            txt_disponibilidad.IsEnabled = true;
            txt_telefono_guia.IsEnabled = true;
            txt_email_guia.IsEnabled = true;
            Button_Cargar_Imagen_Guia.Visibility = Visibility.Visible;
            Button_AnadirGuía.Visibility = Visibility.Collapsed;
            Button_EliminarGuía.Visibility = Visibility.Collapsed;
            Button_ModificarGuía.Visibility = Visibility.Collapsed;
            Aceptar_Modificar_Guia.Visibility = Visibility.Visible;
            Cancelar_Modificar_Guia.Visibility = Visibility.Visible;
        }

        private void Button_Cargar_Imagen_Click(object sender, RoutedEventArgs e)
        {
            cargarImagen(ImagenGuia);
        }

        private void Button_Cargar_Imagen_Excursionista_Click(object sender, RoutedEventArgs e)
        {
            cargarImagen(ImagenExcursionista);
        }

        private void cargarImagen(Image imagen)
        {
            var abrirDialog = new OpenFileDialog();
            abrirDialog.Filter = "Images|*.jpg;*.gif;*.bmp;*.png";
            if (abrirDialog.ShowDialog() == true)
            {
                try
                {
                    var bitmap = new BitmapImage(new Uri(abrirDialog.FileName, UriKind.Absolute));
                    imagen.Source = bitmap;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar la imagen " + ex.Message);
                }
            }
        }

        private void Aceptar_GuiaModificar_Click(object sender, RoutedEventArgs e)
        {
            int indice = -1;
            ListBox_guias.IsEnabled = true;
            txt_nombre_guia.IsEnabled = false;
            txt_apellidos_guia.IsEnabled = false;
            txt_valoracion.IsEnabled = false;
            txt_idiomas.IsEnabled = false;
            txt_disponibilidad.IsEnabled = false;
            txt_telefono_guia.IsEnabled = false;
            txt_email_guia.IsEnabled = false;
            Button_AnadirGuía.Visibility = Visibility.Visible;
            Button_EliminarGuía.Visibility = Visibility.Visible;
            Button_ModificarGuía.Visibility = Visibility.Visible;
            Aceptar_Modificar_Guia.Visibility = Visibility.Collapsed;
            Cancelar_Modificar_Guia.Visibility = Visibility.Collapsed;
            Button_Cargar_Imagen_Guia.Visibility = Visibility.Collapsed;
            Guia guia_sel = ListBox_guias.SelectedItem as Guia;
            for (int i = 0; i < MainWindow.Guias.Count; i++)
            {
                if (MainWindow.Guias[i] == guia_sel)
                {
                    indice = i;
                }
            }
            guia_sel.actualizar(txt_nombre_guia.Text, txt_apellidos_guia.Text, (ImagenGuia.Source as BitmapImage).UriSource,txt_idiomas.Text,
                txt_disponibilidad.Text, txt_telefono_guia.Text, txt_email_guia.Text,
                Convert.ToDouble(txt_valoracion.Text));
            MainWindow.Guias[indice] = guia_sel;
            ListBox_guias.ItemsSource = null;
            ListBox_guias.ItemsSource = MainWindow.Guias;
        }

        private void Cancelar_GuiaModificar_Click(object sender, RoutedEventArgs e)
        {
            ListBox_guias.IsEnabled = true;
            txt_nombre_guia.IsEnabled = false;
            txt_apellidos_guia.IsEnabled = false;
            txt_valoracion.IsEnabled = false;
            txt_idiomas.IsEnabled = false;
            txt_disponibilidad.IsEnabled = false;
            txt_telefono_guia.IsEnabled = false;
            txt_email_guia.IsEnabled = false;
            Button_AnadirGuía.Visibility = Visibility.Visible;
            Button_EliminarGuía.Visibility = Visibility.Visible;
            Button_ModificarGuía.Visibility = Visibility.Visible;
            Aceptar_Modificar_Guia.Visibility = Visibility.Collapsed;
            Cancelar_Modificar_Guia.Visibility = Visibility.Collapsed;
            Button_Cargar_Imagen_Guia.Visibility = Visibility.Collapsed;
            Guia guia_selec = ListBox_guias.SelectedItem as Guia;
            if (guia_selec != null)
            {
                var bitmap = new BitmapImage(guia_selec.Foto);
                ImagenExcursionista.Source = bitmap;
                txt_nombre_guia.Text = guia_selec.Nombre;
                txt_apellidos_guia.Text = guia_selec.Apellidos;
                txt_valoracion.Text = guia_selec.Valoracion.ToString();
                txt_idiomas.Text = guia_selec.Idiomas;
                txt_disponibilidad.Text = guia_selec.Disponibilidad;
                txt_telefono_guia.Text = guia_selec.Telefono;
                txt_email_guia.Text = guia_selec.Email;
                cargarRutasGuias(guia_selec);
            }
            else
            {
                ListBox_guias.SelectedIndex = 0;
            }
        }

        private void Button_Anadir_Excursionista_Click(object sender, RoutedEventArgs e)
        {
            vaciarCampos_Excursionistas();
            ListBox_excursionistas.IsEnabled = false;
            txt_nombre_excursionista.IsEnabled = true;
            txt_apellidos_excursionista.IsEnabled = true;
            txt_edad_excursionista.IsEnabled = true;
            txt_telefono_excursionista.IsEnabled = true;
            txt_email_excursionista.IsEnabled = true;
            Button_Cargar_Imagen_Excursionista.Visibility = Visibility.Visible;
            Button_Anadir_Excursionista.Visibility = Visibility.Collapsed;
            Button_EliminarExcursionista.Visibility = Visibility.Collapsed;
            Button_ModificarExcursionista.Visibility = Visibility.Collapsed;
            Aceptar_Anadir_Excursionista.Visibility = Visibility.Visible;
            Cancelar_Anadir_Excursionista.Visibility = Visibility.Visible;

        }

        private void Aceptar_ExcursionistaAnadir_Click(object sender, RoutedEventArgs e)
        {
            ListBox_excursionistas.IsEnabled = true;
            txt_nombre_excursionista.IsEnabled = false;
            txt_apellidos_excursionista.IsEnabled = false;
            txt_edad_excursionista.IsEnabled = false;
            txt_telefono_excursionista.IsEnabled = false;
            txt_email_excursionista.IsEnabled = false;
            Button_Cargar_Imagen_Excursionista.Visibility = Visibility.Collapsed;
            Button_Anadir_Excursionista.Visibility = Visibility.Visible;
            Button_EliminarExcursionista.Visibility = Visibility.Visible;
            Button_ModificarExcursionista.Visibility = Visibility.Visible;
            Aceptar_Anadir_Excursionista.Visibility = Visibility.Collapsed;
            Cancelar_Anadir_Excursionista.Visibility = Visibility.Collapsed;
            Excursionista exc_creado = new Excursionista(txt_nombre_excursionista.Text, txt_apellidos_excursionista.Text,
                Convert.ToInt32(txt_edad_excursionista.Text), txt_telefono_excursionista.Text, txt_email_excursionista.Text,
                (ImagenExcursionista.Source as BitmapImage).UriSource);
            MainWindow.Excursionistas.Add(exc_creado);
            ListBox_excursionistas.ItemsSource = null;
            ListBox_excursionistas.ItemsSource = MainWindow.Excursionistas;
        }

        private void Cancelar_ExcursionistaAnadir_Click(object sender, RoutedEventArgs e)
        {
            ListBox_excursionistas.IsEnabled = true;
            txt_nombre_excursionista.IsEnabled = false;
            txt_apellidos_excursionista.IsEnabled = false;
            txt_edad_excursionista.IsEnabled = false;
            txt_telefono_excursionista.IsEnabled = false;
            txt_email_excursionista.IsEnabled = false;
            Button_Cargar_Imagen_Excursionista.Visibility = Visibility.Collapsed;
            Button_Anadir_Excursionista.Visibility = Visibility.Visible;
            Button_EliminarExcursionista.Visibility = Visibility.Visible;
            Button_ModificarExcursionista.Visibility = Visibility.Visible;
            Aceptar_Anadir_Excursionista.Visibility = Visibility.Collapsed;
            Cancelar_Anadir_Excursionista.Visibility = Visibility.Collapsed;
            Excursionista exc_selec = ListBox_excursionistas.SelectedItem as Excursionista;
            if (exc_selec != null)
            {
                var bitmap = new BitmapImage(exc_selec.Foto);
                ImagenExcursionista.Source = bitmap;
                txt_nombre_excursionista.Text = exc_selec.Nombre;
                txt_apellidos_excursionista.Text = exc_selec.Apellidos;
                txt_edad_excursionista.Text = exc_selec.Edad.ToString();
                txt_telefono_excursionista.Text = exc_selec.Telefono;
                txt_email_excursionista.Text = exc_selec.Email;
                cargarRutasExcursionista(exc_selec);
            }
            else
            {
                ListBox_excursionistas.SelectedIndex = 0;
            }
        }

        private void Eliminar_Excursionista_Click(object sender, RoutedEventArgs e)
        {
            if (ListBox_excursionistas.Items.Count != 0)
            {
                Excursionista exc_sel = ListBox_excursionistas.SelectedItem as Excursionista;
                MessageBoxResult result = MessageBox.Show("¿Estás seguro de que quieres borrar a " + exc_sel.Nombre + " " + exc_sel.Apellidos + "?", "Confirmación", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        MainWindow.Excursionistas.Remove(exc_sel);
                        ListBox_excursionistas.ItemsSource = null;
                        ListBox_excursionistas.ItemsSource = MainWindow.Excursionistas;
                        MessageBox.Show("Eliminación exitosa.", "Excursionista borrado");
                        break;
                }
                if (MainWindow.Excursionistas.Count == 0)
                {
                    vaciarCampos_Excursionistas();
                }
            }
            else
            {
                MessageBox.Show("No hay un excursionista para borrar.", "Lista vacia");
            }
        }

        private void vaciarCampos_Excursionistas()
        {
            List<Ruta> vacia = new List<Ruta> { };
            txt_nombre_excursionista.Text = "";
            txt_apellidos_excursionista.Text = "";
            txt_edad_excursionista.Text = "";
            txt_telefono_excursionista.Text = "";
            txt_email_excursionista.Text = "";
            ListBox_rutas_excursionistas.ItemsSource = vacia;
            Uri imgDefecto = new Uri("Imagenes/usuario.png", UriKind.Relative);
            var bitmap = new BitmapImage(imgDefecto);
            ImagenExcursionista.Source = bitmap;
        }

        private void Button_ModificarExcursionista_Click(object sender, RoutedEventArgs e)
        {
            ListBox_excursionistas.IsEnabled = false;
            txt_nombre_excursionista.IsEnabled = true;
            txt_apellidos_excursionista.IsEnabled = true;
            txt_edad_excursionista.IsEnabled = true;
            txt_telefono_excursionista.IsEnabled = true;
            txt_email_excursionista.IsEnabled = true;
            Button_Cargar_Imagen_Excursionista.Visibility = Visibility.Visible;
            Button_Anadir_Excursionista.Visibility = Visibility.Collapsed;
            Button_EliminarExcursionista.Visibility = Visibility.Collapsed;
            Button_ModificarExcursionista.Visibility = Visibility.Collapsed;
            Aceptar_Modificar_Excursionista.Visibility = Visibility.Visible;
            Cancelar_Modificar_Excursionista.Visibility = Visibility.Visible;

        }

        private void Aceptar_ExcursionistaModificar_Click(object sender, RoutedEventArgs e)
        {
            int indice = -1;
            ListBox_excursionistas.IsEnabled = true;
            txt_nombre_excursionista.IsEnabled = false;
            txt_apellidos_excursionista.IsEnabled = false;
            txt_edad_excursionista.IsEnabled = false;
            txt_telefono_excursionista.IsEnabled = false;
            txt_email_excursionista.IsEnabled = false;
            Button_Cargar_Imagen_Excursionista.Visibility = Visibility.Collapsed;
            Button_Anadir_Excursionista.Visibility = Visibility.Visible;
            Button_EliminarExcursionista.Visibility = Visibility.Visible;
            Button_ModificarExcursionista.Visibility = Visibility.Visible;
            Aceptar_Modificar_Excursionista.Visibility = Visibility.Collapsed;
            Cancelar_Modificar_Excursionista.Visibility = Visibility.Collapsed;
            Excursionista exc_sel = ListBox_excursionistas.SelectedItem as Excursionista;
            for (int i = 0; i < MainWindow.Excursionistas.Count; i++)
            {
                if (MainWindow.Excursionistas[i] == exc_sel)
                {
                    indice = i;
                }
            }
            exc_sel.actualizar(txt_nombre_excursionista.Text, txt_apellidos_excursionista.Text, Convert.ToInt32(txt_edad_excursionista.Text), 
                txt_telefono_excursionista.Text, txt_email_excursionista.Text, (ImagenExcursionista.Source as BitmapImage).UriSource);
            MainWindow.Excursionistas[indice] = exc_sel;
            ListBox_excursionistas.ItemsSource = null;
            ListBox_excursionistas.ItemsSource = MainWindow.Excursionistas;
        }

        private void Cancelar_ExcursionistaModificar_Click(object sender, RoutedEventArgs e)
        {
            ListBox_excursionistas.IsEnabled = true;
            txt_nombre_excursionista.IsEnabled = false;
            txt_apellidos_excursionista.IsEnabled = false;
            txt_edad_excursionista.IsEnabled = false;
            txt_telefono_excursionista.IsEnabled = false;
            txt_email_excursionista.IsEnabled = false;
            Button_Cargar_Imagen_Excursionista.Visibility = Visibility.Collapsed;
            Button_Anadir_Excursionista.Visibility = Visibility.Visible;
            Button_EliminarExcursionista.Visibility = Visibility.Visible;
            Button_ModificarExcursionista.Visibility = Visibility.Visible;
            Aceptar_Modificar_Excursionista.Visibility = Visibility.Collapsed;
            Cancelar_Modificar_Excursionista.Visibility = Visibility.Collapsed;
            Excursionista exc_selec = ListBox_excursionistas.SelectedItem as Excursionista;
            if (exc_selec != null)
            {
                var bitmap = new BitmapImage(exc_selec.Foto);
                ImagenExcursionista.Source = bitmap;
                txt_nombre_excursionista.Text = exc_selec.Nombre;
                txt_apellidos_excursionista.Text = exc_selec.Apellidos;
                txt_edad_excursionista.Text = exc_selec.Edad.ToString();
                txt_telefono_excursionista.Text = exc_selec.Telefono;
                txt_email_excursionista.Text = exc_selec.Email;
                cargarRutasExcursionista(exc_selec);
            }
            else
            {
                ListBox_excursionistas.SelectedIndex = 0;
            }
        }

        private void Eliminar_Ruta_Click(object sender, RoutedEventArgs e)
        {
            if (ListBox_rutas.Items.Count != 0)
            {
                Ruta ruta_sel = ListBox_rutas.SelectedItem as Ruta;
                MessageBoxResult result = MessageBox.Show("¿Estás seguro de que quieres borrar la ruta " + ruta_sel.Nombre + "?", "Confirmación", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        MainWindow.Rutas.Remove(ruta_sel);
                        ListBox_rutas.ItemsSource = null;
                        ListBox_rutas.ItemsSource = MainWindow.Rutas;
                        MessageBox.Show("Eliminación exitosa.", "Ruta borrada");
                        break;
                }
                if (MainWindow.Rutas.Count == 0)
                {
                    vaciarCampos_Rutas();
                }
            }
            else
            {
                MessageBox.Show("No hay una ruta para borrar.", "Lista vacia");
            }
        }

        private void vaciarCampos_Rutas()
        {
            List<Excursionista> vacia_exc = new List<Excursionista> { };
            List<Guia> vacia_guias = new List<Guia> { };
            ListBox_excursionistas_rutas.ItemsSource = vacia_exc;
            ListBox_guias_rutas.ItemsSource = vacia_guias;
            txt_nombre_ruta.Text = "";
            txt_distancia_ruta.Text = "";
            txt_estado_ruta.Text = "";
            txt_provincia_ruta.Text = "";
            txt_origen_ruta.Text = "";
            txt_destino_ruta.Text = "";
            DatePicker_Fecha.SelectedDate = DateTime.Now;
            txt_descripcion_ruta.Text = "";
            Uri imgDefecto = new Uri("Imagenes/usuario.png", UriKind.Relative);
            var bitmap = new BitmapImage(imgDefecto);
            ImagenRuta.Source = bitmap;
        }

        private void Button_Puntos_Interes_Click(object sender, RoutedEventArgs e)
        {
            grid_puntos_interes.Visibility = Visibility.Visible;
            GroupBox_Ruta.Visibility = Visibility.Collapsed;
            ListBox_rutas.IsEnabled = false;
            Button_AnadirRuta.Visibility = Visibility.Collapsed;
            Button_EliminarRuta.Visibility = Visibility.Collapsed;
            Button_ModificarRuta.Visibility = Visibility.Collapsed;
            Button_PuntosInteres.Visibility = Visibility.Collapsed;
            Button_AnadirExcursionista.Visibility = Visibility.Collapsed;
            Button_Anadir_Guia.Visibility = Visibility.Collapsed;
            Ruta ruta_selec = ListBox_rutas.SelectedItem as Ruta;
            cargarPdiListBox(ruta_selec);
            //var bitmap = new BitmapImage(ruta_selec.PuntosInteres[0].Fotos[0]);
            //ImagenPdi.Source = bitmap;
            Button_Anadir_PuntosInteres.Visibility = Visibility.Visible;
            Button_Eliminar_PuntosInteres.Visibility = Visibility.Visible;
        }

        private void cargarPdiListBox(Ruta r_selec)
        {
            List<PuntoInteres> pdi_cargados = new List<PuntoInteres>();
            if (r_selec != null)
            {
                if (r_selec.PuntosInteres.Length > 0)
                {
                    for (int i = 0; i < r_selec.PuntosInteres.Length; i++)
                    {
                        pdi_cargados.Add(r_selec.PuntosInteres[i]);
                    }
                }
                ListBox_pdi.ItemsSource = pdi_cargados;
            }
        }

        private void listbox_pdi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PuntoInteres pdi_selec = ListBox_pdi.SelectedItem as PuntoInteres;
            if (pdi_selec != null)
            {
                if (pdi_selec.Fotos == null)
                {
                    Atras_Pdi.IsEnabled = false;
                    Siguiente_Pdi.IsEnabled = false;
                    Uri imgDefecto = new Uri("Imagenes/usuario.png", UriKind.Relative);
                    var bitmap = new BitmapImage(imgDefecto);
                    ImagenPdi.Source = bitmap;
                }
                else
                {
                    Atras_Pdi.IsEnabled = true;
                    Siguiente_Pdi.IsEnabled = true;
                    var bitmap = new BitmapImage(pdi_selec.Fotos[0]);
                    ImagenPdi.Source = bitmap;
                    imagen_punto_interes_ruta = pdi_selec.Fotos[0];
                }
                txt_nombre_pdi.Text = pdi_selec.Nombre;
                txt_descripcion_pdi.Text = pdi_selec.Descripcion;
                txt_tipologia_pdi.Text = pdi_selec.Tipologia;
            }
            else
            {
                ListBox_pdi.SelectedIndex = 0;
            }
        }

        private void Button_Siguiente_Pdi_Click(object sender, RoutedEventArgs e)
        {
            int indice_pdi = -1;
            int indice = -1;
            Ruta ruta_selec = ListBox_rutas.SelectedItem as Ruta;
            PuntoInteres pdi_selec = ListBox_pdi.SelectedItem as PuntoInteres;
            for (int x = 0; x < ruta_selec.PuntosInteres.Length; x++)
            {
                if (pdi_selec == ruta_selec.PuntosInteres[x])
                {
                    indice_pdi = x;
                    break;
                }
            }
            if (ruta_selec.PuntosInteres[indice_pdi] != null)
            {
                for (int i = 0; i < ruta_selec.PuntosInteres[indice_pdi].Fotos.Length; i++)
                {
                    if (imagen_punto_interes_ruta == ruta_selec.PuntosInteres[indice_pdi].Fotos[i])
                    {
                        indice = i;
                        break;
                    }
                }
                int mod = ruta_selec.PuntosInteres[indice_pdi].Fotos.Length;
                imagen_punto_interes_ruta = ruta_selec.PuntosInteres[indice_pdi].Fotos[(indice + 1) % mod];
                var bitmap_final = new BitmapImage(ruta_selec.PuntosInteres[indice_pdi].Fotos[(indice + 1) % mod]);
                ImagenPdi.Source = bitmap_final;
            }
        }

        private void Button_Atras_Pdi_Click(object sender, RoutedEventArgs e)
        {
            int indice_pdi = -1;
            int indice = -1;
            Ruta ruta_selec = ListBox_rutas.SelectedItem as Ruta;
            PuntoInteres pdi_selec = ListBox_pdi.SelectedItem as PuntoInteres;
            for (int x = 0; x < ruta_selec.PuntosInteres.Length; x++)
            {
                if (pdi_selec == ruta_selec.PuntosInteres[x])
                {
                    indice_pdi = x;
                    break;
                }
            }
            if (ruta_selec.PuntosInteres[indice_pdi] != null)
            {
                for (int i = 0; i < ruta_selec.PuntosInteres[indice_pdi].Fotos.Length; i++)
                {
                    if (imagen_punto_interes_ruta == ruta_selec.PuntosInteres[indice_pdi].Fotos[i])
                    {
                        indice = i;
                        break;
                    }
                }
                int mod = ruta_selec.PuntosInteres[indice_pdi].Fotos.Length;
                if (indice - 1 < 0)
                {
                    indice = 2;
                }
                else
                {
                    indice = indice - 1;
                }
                imagen_punto_interes_ruta = ruta_selec.PuntosInteres[indice_pdi].Fotos[indice];
                var bitmap_final = new BitmapImage(ruta_selec.PuntosInteres[indice_pdi].Fotos[indice]);
                ImagenPdi.Source = bitmap_final;
            }                
        }

        private void Button_Cerrar_Puntos_Interes_Click(object sender, RoutedEventArgs e)
        {
            grid_puntos_interes.Visibility = Visibility.Collapsed;
            GroupBox_Ruta.Visibility = Visibility.Visible;
            ListBox_rutas.IsEnabled = true;
            Button_AnadirRuta.Visibility = Visibility.Visible;
            Button_EliminarRuta.Visibility = Visibility.Visible;
            Button_ModificarRuta.Visibility = Visibility.Visible;
            Button_PuntosInteres.Visibility = Visibility.Visible;
            Button_AnadirExcursionista.Visibility = Visibility.Visible;
            Button_Anadir_Guia.Visibility = Visibility.Visible;
        }

        private void Button_Anadir_Ruta_Click(object sender, RoutedEventArgs e)
        {
            vaciarCampos_Rutas();
            ListBox_rutas.IsEnabled = false;
            txt_nombre_ruta.IsEnabled = true;
            txt_distancia_ruta.IsEnabled = true;
            txt_duracion_ruta.IsEnabled = true;
            txt_dificultad_ruta.IsEnabled = true;
            txt_provincia_ruta.IsEnabled = true;
            txt_origen_ruta.IsEnabled = true;
            txt_destino_ruta.IsEnabled = true;
            DatePicker_Fecha.IsEnabled = true;
            txt_descripcion_ruta.IsEnabled = true;
            Button_Cargar_Imagen_Ruta.Visibility = Visibility.Visible;
            Button_AnadirRuta.Visibility = Visibility.Collapsed;
            Button_EliminarRuta.Visibility = Visibility.Collapsed;
            Button_ModificarRuta.Visibility = Visibility.Collapsed;
            Button_PuntosInteres.Visibility = Visibility.Collapsed;
            Button_AnadirExcursionista.Visibility = Visibility.Collapsed;
            Button_Anadir_Guia.Visibility = Visibility.Collapsed;
            Aceptar_Anadir_Ruta.Visibility = Visibility.Visible;
            Cancelar_Anadir_Ruta.Visibility = Visibility.Visible;
        }

        private void Anadir_Guia_Click(object sender, RoutedEventArgs e)
        {
            Elegir_Ruta.Visibility = Visibility.Collapsed;
            ListBox_rutas.Visibility = Visibility.Collapsed;
            Button_AnadirRuta.Visibility = Visibility.Collapsed;
            Button_EliminarRuta.Visibility = Visibility.Collapsed;
            Button_ModificarRuta.Visibility = Visibility.Collapsed;
            Button_PuntosInteres.Visibility = Visibility.Collapsed;
            Button_AnadirExcursionista.Visibility = Visibility.Collapsed;
            Button_Anadir_Guia.Visibility = Visibility.Collapsed;
            GroupBox_Ruta.Visibility = Visibility.Collapsed;
            Anadir_Guia.Visibility = Visibility.Visible;
            CmBox_Guias_Anadir.ItemsSource = MainWindow.Guias;
        }

        private void Aceptar_Guia_Click(object sender, RoutedEventArgs e)
        {
            Boolean existe = false;
            Elegir_Ruta.Visibility = Visibility.Visible;
            ListBox_rutas.Visibility = Visibility.Visible;
            Button_AnadirRuta.Visibility = Visibility.Visible;
            Button_EliminarRuta.Visibility = Visibility.Visible;
            Button_ModificarRuta.Visibility = Visibility.Visible;
            Button_PuntosInteres.Visibility = Visibility.Visible;
            Button_AnadirExcursionista.Visibility = Visibility.Visible;
            Button_Anadir_Guia.Visibility = Visibility.Visible;
            GroupBox_Ruta.Visibility = Visibility.Visible;
            Anadir_Guia.Visibility = Visibility.Collapsed;
            Ruta ruta_sel = ListBox_rutas.SelectedItem as Ruta;
            Guia guia = CmBox_Guias_Anadir.SelectedItem as Guia;
            for (int i = 0; i < ruta_sel.Guias.Length; i++)
            {
                if (guia == ruta_sel.Guias[i])
                {
                    existe = true;
                    break;
                }
            }
            if (existe == false)
            {
                ruta_sel.anadirGuia(guia);
                for (int i = 0; i < MainWindow.Guias.Count; i++)
                {
                    if (guia == MainWindow.Guias[i])
                    {
                        MainWindow.Guias[i].anadirRuta(ruta_sel);
                        break;
                    }
                }
            }
            ListBox_guias_rutas.ItemsSource = null;
            ListBox_guias_rutas.ItemsSource = ruta_sel.Guias;
            Guia guia2 = ListBox_guias.SelectedItem as Guia;
            ListBox_rutas_guias.ItemsSource = null;
            ListBox_rutas_guias.ItemsSource = guia2.Rutas;
        }

        private void Cancelar_Guia_Click(object sender, RoutedEventArgs e)
        {
            Elegir_Ruta.Visibility = Visibility.Visible;
            ListBox_rutas.Visibility = Visibility.Visible;
            Button_AnadirRuta.Visibility = Visibility.Visible;
            Button_EliminarRuta.Visibility = Visibility.Visible;
            Button_ModificarRuta.Visibility = Visibility.Visible;
            Button_PuntosInteres.Visibility = Visibility.Visible;
            Button_AnadirExcursionista.Visibility = Visibility.Visible;
            Button_Anadir_Guia.Visibility = Visibility.Visible;
            GroupBox_Ruta.Visibility = Visibility.Visible;
            Anadir_Guia.Visibility = Visibility.Collapsed;

        }

        private void Button_Modificar_Ruta_Click(object sender, RoutedEventArgs e)
        {
            ListBox_rutas.IsEnabled = false;
            txt_nombre_ruta.IsEnabled = true;
            txt_distancia_ruta.IsEnabled = true;
            txt_duracion_ruta.IsEnabled = true;
            txt_dificultad_ruta.IsEnabled = true;
            txt_provincia_ruta.IsEnabled = true;
            txt_origen_ruta.IsEnabled = true;
            txt_destino_ruta.IsEnabled = true;
            DatePicker_Fecha.IsEnabled = true;
            txt_descripcion_ruta.IsEnabled = true;
            Button_Cargar_Imagen_Ruta.Visibility = Visibility.Visible;
            Button_AnadirRuta.Visibility = Visibility.Collapsed;
            Button_EliminarRuta.Visibility = Visibility.Collapsed;
            Button_ModificarRuta.Visibility = Visibility.Collapsed;
            Button_PuntosInteres.Visibility = Visibility.Collapsed;
            Button_AnadirExcursionista.Visibility = Visibility.Collapsed;
            Button_Anadir_Guia.Visibility = Visibility.Collapsed;
            Aceptar_Modificar_Ruta.Visibility = Visibility.Visible;
            Cancelar_Modificar_Ruta.Visibility = Visibility.Visible;

        }

        private void Button_Cargar_Imagen_Ruta_Click(object sender, RoutedEventArgs e)
        {
            cargarImagen(ImagenRuta);
        }

        private void Aceptar_RutaModificar_Click(object sender, RoutedEventArgs e)
        {
            int indice = -1;
            ListBox_rutas.IsEnabled = true;
            txt_nombre_ruta.IsEnabled = false;
            txt_distancia_ruta.IsEnabled = false;
            txt_duracion_ruta.IsEnabled = false;
            txt_dificultad_ruta.IsEnabled = false;
            txt_provincia_ruta.IsEnabled = false;
            txt_origen_ruta.IsEnabled = false;
            txt_destino_ruta.IsEnabled = false;
            DatePicker_Fecha.IsEnabled = false;
            txt_descripcion_ruta.IsEnabled = false;
            Button_Cargar_Imagen_Ruta.Visibility = Visibility.Collapsed;
            Button_AnadirRuta.Visibility = Visibility.Visible;
            Button_EliminarRuta.Visibility = Visibility.Visible;
            Button_ModificarRuta.Visibility = Visibility.Visible;
            Button_PuntosInteres.Visibility = Visibility.Visible;
            Button_AnadirExcursionista.Visibility = Visibility.Visible;
            Button_Anadir_Guia.Visibility = Visibility.Visible;
            Aceptar_Modificar_Ruta.Visibility = Visibility.Collapsed;
            Cancelar_Modificar_Ruta.Visibility = Visibility.Collapsed;
            Ruta ruta_sel = ListBox_rutas.SelectedItem as Ruta;
            for (int i = 0; i < MainWindow.Rutas.Count; i++)
            {
                if (MainWindow.Rutas[i] == ruta_sel)
                {
                    indice = i;
                }
            }
            ruta_sel.actualizar(txt_nombre_ruta.Text, txt_distancia_ruta.Text, (ImagenRuta.Source as BitmapImage).UriSource, 
                txt_provincia_ruta.Text, txt_origen_ruta.Text, txt_destino_ruta.Text, (DateTime) DatePicker_Fecha.SelectedDate,
                txt_descripcion_ruta.Text);
            MainWindow.Rutas[indice] = ruta_sel;
            ListBox_rutas.ItemsSource = null;
            ListBox_rutas.ItemsSource = MainWindow.Rutas;
        }

        private void Cancelar_RutaModificar_Click(object sender, RoutedEventArgs e)
        {
            ListBox_rutas.IsEnabled = true;
            txt_nombre_ruta.IsEnabled = false;
            txt_distancia_ruta.IsEnabled = false;
            txt_duracion_ruta.IsEnabled = false;
            txt_dificultad_ruta.IsEnabled = false;
            txt_provincia_ruta.IsEnabled = false;
            txt_origen_ruta.IsEnabled = false;
            txt_destino_ruta.IsEnabled = false;
            DatePicker_Fecha.IsEnabled = false;
            txt_descripcion_ruta.IsEnabled = false;
            Button_Cargar_Imagen_Ruta.Visibility = Visibility.Collapsed;
            Button_AnadirRuta.Visibility = Visibility.Visible;
            Button_EliminarRuta.Visibility = Visibility.Visible;
            Button_ModificarRuta.Visibility = Visibility.Visible;
            Button_PuntosInteres.Visibility = Visibility.Visible;
            Button_AnadirExcursionista.Visibility = Visibility.Visible;
            Button_Anadir_Guia.Visibility = Visibility.Visible;
            Aceptar_Modificar_Ruta.Visibility = Visibility.Collapsed;
            Cancelar_Modificar_Ruta.Visibility = Visibility.Collapsed;
            Ruta ruta_selec = ListBox_rutas.SelectedItem as Ruta;
            if (ruta_selec != null)
            {
                var bitmap = new BitmapImage(ruta_selec.Foto);
                ImagenRuta.Source = bitmap;
                txt_nombre_ruta.Text = ruta_selec.Nombre;
                txt_distancia_ruta.Text = ruta_selec.Distancia;
                txt_provincia_ruta.Text = ruta_selec.Provincia;
                txt_origen_ruta.Text = ruta_selec.Origen;
                txt_destino_ruta.Text = ruta_selec.Destino;
                DatePicker_Fecha.SelectedDate = ruta_selec.FechaRealizacion.Date;
                txt_descripcion_ruta.Text = ruta_selec.Descripcion;
                cargarExcursionistaRutasListBox(ruta_selec);
                cargarGuiasRutasListBox(ruta_selec);
            }
            else
            {
                ListBox_rutas.SelectedIndex = 0;
            }
        }

        private void Aceptar_Ruta_Anadir_Click(object sender, RoutedEventArgs e)
        {
            ListBox_rutas.IsEnabled = true;
            txt_nombre_ruta.IsEnabled = false;
            txt_distancia_ruta.IsEnabled = false;
            txt_duracion_ruta.IsEnabled = false;
            txt_dificultad_ruta.IsEnabled = false;
            txt_provincia_ruta.IsEnabled = false;
            txt_origen_ruta.IsEnabled = false;
            txt_destino_ruta.IsEnabled = false;
            DatePicker_Fecha.IsEnabled = false;
            txt_descripcion_ruta.IsEnabled = false;
            Button_Cargar_Imagen_Ruta.Visibility = Visibility.Collapsed;
            Button_AnadirRuta.Visibility = Visibility.Visible;
            Button_EliminarRuta.Visibility = Visibility.Visible;
            Button_ModificarRuta.Visibility = Visibility.Visible;
            Button_PuntosInteres.Visibility = Visibility.Visible;
            Button_AnadirExcursionista.Visibility = Visibility.Visible;
            Button_Anadir_Guia.Visibility = Visibility.Visible;
            Aceptar_Anadir_Ruta.Visibility = Visibility.Collapsed;
            Cancelar_Anadir_Ruta.Visibility = Visibility.Collapsed;
            Ruta ruta_creada = new Ruta(txt_nombre_ruta.Text, txt_distancia_ruta.Text,
                txt_provincia_ruta.Text, txt_origen_ruta.Text, txt_destino_ruta.Text, (DateTime)DatePicker_Fecha.SelectedDate,
                txt_descripcion_ruta.Text, comprobarEstadoRuta((DateTime)DatePicker_Fecha.SelectedDate), (ImagenRuta.Source as BitmapImage).UriSource);
            MainWindow.Rutas.Add(ruta_creada);
            ListBox_rutas.ItemsSource = null;
            ListBox_rutas.ItemsSource = MainWindow.Rutas;
        }

        private string comprobarEstadoRuta(DateTime d)
        {
            string estado = "";
            if (DateTime.Compare(DateTime.Now.Date, d.Date) < 0)
            {
                estado = "Ruta no realizada todavía.";
            }
            else
            {
                estado = "Ruta ya realizada.";
            }
            return estado;
        }

        private void Cancelar_Ruta_Anadir_Click(object sender, RoutedEventArgs e)
        {
            ListBox_rutas.IsEnabled = true;
            txt_nombre_ruta.IsEnabled = false;
            txt_distancia_ruta.IsEnabled = false;
            txt_duracion_ruta.IsEnabled = false;
            txt_dificultad_ruta.IsEnabled = false;
            txt_provincia_ruta.IsEnabled = false;
            txt_origen_ruta.IsEnabled = false;
            txt_destino_ruta.IsEnabled = false;
            DatePicker_Fecha.IsEnabled = false;
            txt_descripcion_ruta.IsEnabled = false;
            Button_Cargar_Imagen_Ruta.Visibility = Visibility.Collapsed;
            Button_AnadirRuta.Visibility = Visibility.Visible;
            Button_EliminarRuta.Visibility = Visibility.Visible;
            Button_ModificarRuta.Visibility = Visibility.Visible;
            Button_PuntosInteres.Visibility = Visibility.Visible;
            Button_AnadirExcursionista.Visibility = Visibility.Visible;
            Button_Anadir_Guia.Visibility = Visibility.Visible;
            Aceptar_Anadir_Ruta.Visibility = Visibility.Collapsed;
            Cancelar_Anadir_Ruta.Visibility = Visibility.Collapsed;
            Ruta ruta_selec = ListBox_rutas.SelectedItem as Ruta;
            if (ruta_selec != null)
            {
                var bitmap = new BitmapImage(ruta_selec.Foto);
                ImagenRuta.Source = bitmap;
                txt_nombre_ruta.Text = ruta_selec.Nombre;
                txt_distancia_ruta.Text = ruta_selec.Distancia;
                txt_provincia_ruta.Text = ruta_selec.Provincia;
                txt_origen_ruta.Text = ruta_selec.Origen;
                txt_destino_ruta.Text = ruta_selec.Destino;
                DatePicker_Fecha.SelectedDate = ruta_selec.FechaRealizacion.Date;
                txt_descripcion_ruta.Text = ruta_selec.Descripcion;
                cargarExcursionistaRutasListBox(ruta_selec);
                cargarGuiasRutasListBox(ruta_selec);
            }
            else
            {
                ListBox_rutas.SelectedIndex = 0;
            }
        }

        private void Eliminar_Pdi_Click(object sender, RoutedEventArgs e)
        {
            if (ListBox_pdi.Items[0] != null)
            {
                PuntoInteres pdi_sel = ListBox_pdi.SelectedItem as PuntoInteres;
                Ruta ruta_selec = ListBox_rutas.SelectedItem as Ruta;
                int indice = -1;
                for (int i = 0; i < MainWindow.Rutas.Count; i++)
                {
                    if (MainWindow.Rutas[i] == ruta_selec)
                    {
                        indice = i;
                    }
                }
                MessageBoxResult result = MessageBox.Show("¿Estás seguro de que quieres borrar el punto de interés: " + pdi_sel.Nombre + "?", "Confirmación", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        List <PuntoInteres> pdis_ruta = MainWindow.Rutas[indice].PuntosInteres.ToList();
                        pdis_ruta.Remove(pdi_sel);
                        MainWindow.Rutas[indice].PuntosInteres = pdis_ruta.ToArray();
                        ListBox_pdi.ItemsSource = null;
                        ListBox_pdi.ItemsSource = MainWindow.Rutas[indice].PuntosInteres;
                        MessageBox.Show("Eliminación exitosa.", "Punto de Interés borrado");
                        break;
                }
                if (MainWindow.Rutas[indice].PuntosInteres[0] == null)
                {
                    vaciarCampos_Pdis();
                }
            }
            else
            {
                MessageBox.Show("No hay un punto de interés para borrar.", "Lista vacia");
            }
        }

        private void vaciarCampos_Pdis()
        {
            //List<PuntoInteres> vacia = new List<PuntoInteres> { };
            txt_nombre_pdi.Text = "";
            txt_descripcion_pdi.Text = "";
            txt_tipologia_pdi.Text = "";
            //ListBox_pdi.ItemsSource = vacia;
            Uri imgDefecto = new Uri("Imagenes/usuario.png", UriKind.Relative);
            var bitmap = new BitmapImage(imgDefecto);
            ImagenPdi.Source = bitmap;
        }

        private void Button_Anadir_Pdi_Click(object sender, RoutedEventArgs e)
        {
            vaciarCampos_Pdis();
            ListBox_pdi.IsEnabled = false;
            txt_nombre_pdi.IsEnabled = true;
            txt_descripcion_pdi.IsEnabled = true;
            txt_tipologia_pdi.IsEnabled = true;
            Button_Cargar_Imagen_Pdi.Visibility = Visibility.Visible;
            Button_Anadir_PuntosInteres.Visibility = Visibility.Collapsed;
            Button_Eliminar_PuntosInteres.Visibility = Visibility.Collapsed;
            Button_Cargar_Imagen_Pdi.Visibility = Visibility.Collapsed;
            Cerrar_Pdi.Visibility = Visibility.Collapsed;
            Aceptar_Anadir_Pdi.Visibility = Visibility.Visible;
            Cancelar_Anadir_Pdi.Visibility = Visibility.Visible;

        }

        private void Aceptar_Pdi_Anadir_Click(object sender, RoutedEventArgs e)
        {
            ListBox_pdi.IsEnabled = true;
            txt_nombre_pdi.IsEnabled = false;
            txt_descripcion_pdi.IsEnabled = false;
            txt_tipologia_pdi.IsEnabled = false;
            Button_Cargar_Imagen_Pdi.Visibility = Visibility.Visible;
            Button_Anadir_PuntosInteres.Visibility = Visibility.Visible;
            Button_Eliminar_PuntosInteres.Visibility = Visibility.Visible;
            Cerrar_Pdi.Visibility = Visibility.Visible;
            Aceptar_Anadir_Pdi.Visibility = Visibility.Collapsed;
            Cancelar_Anadir_Pdi.Visibility = Visibility.Collapsed;
            PuntoInteres pdi_creado = new PuntoInteres(txt_nombre_pdi.Text, txt_descripcion_pdi.Text,
                txt_tipologia_pdi.Text);
            Ruta ruta_sel = ListBox_rutas.SelectedItem as Ruta;
            for (int x = 0; x < ruta_sel.PuntosInteres.Length; x++)
            {
                if (ruta_sel.PuntosInteres[x] == null)
                {
                    ruta_sel.PuntosInteres[x] = pdi_creado;
                    break;
                }
            }
            for (int i = 0; i < MainWindow.Rutas.Count; i++)
            {
                if (ruta_sel.Nombre == MainWindow.Rutas[i].Nombre)
                {
                    MainWindow.Rutas[i] = ruta_sel;
                    break;
                }
            }
            ListBox_pdi.ItemsSource = null;
            ListBox_pdi.ItemsSource = ruta_sel.PuntosInteres;
        }

        private void Cancelar_Pdi_Anadir_Click(object sender, RoutedEventArgs e)
        {
            ListBox_pdi.IsEnabled = true;
            txt_nombre_pdi.IsEnabled = false;
            txt_descripcion_pdi.IsEnabled = false;
            txt_tipologia_pdi.IsEnabled = false;
            Button_Cargar_Imagen_Pdi.Visibility = Visibility.Visible;
            Button_Anadir_PuntosInteres.Visibility = Visibility.Visible;
            Button_Eliminar_PuntosInteres.Visibility = Visibility.Visible;
            Cerrar_Pdi.Visibility = Visibility.Visible;
            Aceptar_Anadir_Pdi.Visibility = Visibility.Collapsed;
            Cancelar_Anadir_Pdi.Visibility = Visibility.Collapsed;
            PuntoInteres pdi_selec = ListBox_pdi.SelectedItem as PuntoInteres;
            Ruta ruta_sel = ListBox_rutas.SelectedItem as Ruta;
            ListBox_pdi.ItemsSource = null;
            ListBox_pdi.ItemsSource = ruta_sel.PuntosInteres;
            if (pdi_selec != null)
            {
                var bitmap = new BitmapImage(pdi_selec.Fotos[0]);
                ImagenPdi.Source = bitmap;
                txt_nombre_pdi.Text = pdi_selec.Nombre;
                txt_descripcion_pdi.Text = pdi_selec.Descripcion;
                txt_tipologia_pdi.Text = pdi_selec.Tipologia;
            }
            else
            {
                ListBox_pdi.SelectedIndex = 0;
            }
        }

        private void Button_Cargar_Imagen_Pdi_Click(object sender, RoutedEventArgs e)
        {
            cargarImagen(ImagenPdi);
            Siguiente_Pdi.IsEnabled = true;
            Atras_Pdi.IsEnabled = true;
            PuntoInteres pdi_selec = ListBox_pdi.SelectedItem as PuntoInteres;
            pdi_selec.anadirFoto((ImagenPdi.Source as BitmapImage).UriSource);
        }

        private void txtSoloNumeros_KeyPress(object sender, KeyEventArgs e)
        {
            //Para obligar a que sólo se introduzcan números
            if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 ||
                e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 ||
                e.Key == Key.NumPad8 || e.Key == Key.NumPad9)
            {
                e.Handled = false;
            }
            else
              if (e.Key == Key.Back) //permitir teclas de control como retroceso
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan
                e.Handled = true;
            }
        }

        private void TextBoxSoloNumeros_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void TextBoxSoloLetras_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }
    }
}
