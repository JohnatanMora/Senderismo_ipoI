using Senderismo.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
using System.Xml;
using System.Xml.Linq;


namespace Senderismo
{
    public partial class MainWindow : Window
    {
        public static List<Administrador> Administradores;
        public static List<Ruta> Rutas;
        public static List<Excursionista> Excursionistas;
        public static List<Guia> Guias;
        public static List<PuntoInteres> PuntosInteres;
        public static List<Ayuda> Ayudas;
        public static Preferencia PConfig;
        public static PrincipalWindow wMarco;
        public static Administrador aAux;
        public static Button button_pref;
        public static Button button_ayuda;
        public static TextBox textbox_usuario;
        public static PasswordBox passwordbox_contra;
        public MainWindow()
        {
            InitializeComponent();
            Administradores = cargarAdmins();
            Excursionistas = cargarExcursionistas();
            Guias = cargarGuias();
            PuntosInteres = cargarPuntosInteres();
            Rutas = cargarRutas();
            Ayudas = cargarAyudas();
            wMarco = new PrincipalWindow(this);
            wMarco.Visibility = Visibility.Collapsed;
            button_pref = Button_Preferencias;
            button_ayuda = Button_Ayuda;
            textbox_usuario = TextBox_Usuario;
            passwordbox_contra = PasswordBox_Contrasena;
        }

        private List<Administrador> cargarAdmins()
        {
            List<Administrador> listado = new List<Administrador>();
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(Directory.GetCurrentDirectory() + @"/Administrador.xml");
            }
            catch (Exception)
            {
                var fichero = Application.GetResourceStream(new Uri("Datos/Administrador.xml", UriKind.Relative));
                doc.Load(fichero.Stream);
            }

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                var nuevoAdmin = new Administrador(node.Attributes["Usuario"].Value, node.Attributes["Contrasenia"].Value, node.Attributes["Nombre"].Value,
                    node.Attributes["Apellidos"].Value, node.Attributes["FechaDeNacimiento"].Value, node.Attributes["Email"].Value, node.Attributes["Telefono"].Value,
                    node.Attributes["Pais"].Value, new Uri(node.Attributes["Imagen"].Value, UriKind.RelativeOrAbsolute), generarFecha(node.Attributes["UltimoAcceso"].Value));
                listado.Add(nuevoAdmin);
            }
            return listado;
        }

        private List<Ayuda> cargarAyudas()
        {
            List<Ayuda> listado = new List<Ayuda>();
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(Directory.GetCurrentDirectory() + @"/Ayuda.xml");
            }
            catch (Exception)
            {
                var fichero = Application.GetResourceStream(new Uri("Datos/Ayuda.xml", UriKind.Relative));
                doc.Load(fichero.Stream);
            }

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                var nuevaAyuda = new Ayuda(node.Attributes["Titulo"].Value, node.Attributes["Contenido"].Value);
                listado.Add(nuevaAyuda);
            }
            return listado;
        }

        private List<Excursionista> cargarExcursionistas()
        {
            List<Excursionista> listado = new List<Excursionista>();
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(Directory.GetCurrentDirectory() + @"/Excursionistas.xml");
            }
            catch (Exception)
            {
                var fichero = Application.GetResourceStream(new Uri("Datos/Excursionistas.xml", UriKind.Relative));
                doc.Load(fichero.Stream);
            }

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                //Convert.ToDateTime(node.Attributes["FechaEntrada"].Value)
                /*Ruta[] rutas_metodo = generarRutas(node.Attributes["Rutas"].Value);
                var nuevoExcursionista = new Excursionista(node.Attributes["Nombre"].Value, node.Attributes["Apellidos"].Value,
                    Convert.ToInt32(node.Attributes["Edad"].Value), node.Attributes["Telefono"].Value,
                    node.Attributes["Email"].Value, rutas_metodo, new Uri(node.Attributes["Imagen"].Value, UriKind.RelativeOrAbsolute));
                listado.Add(nuevoExcursionista);*/
                var nuevoExcursionista = new Excursionista(node.Attributes["Nombre"].Value, node.Attributes["Apellidos"].Value,
                    Convert.ToInt32(node.Attributes["Edad"].Value), node.Attributes["Telefono"].Value,
                    node.Attributes["Email"].Value, new Uri(node.Attributes["Imagen"].Value, UriKind.RelativeOrAbsolute));
                listado.Add(nuevoExcursionista);
            }
            return listado;
        }

        private List<PuntoInteres> cargarPuntosInteres()
        {
            List<PuntoInteres> listado = new List<PuntoInteres>();
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(Directory.GetCurrentDirectory() + @"/PuntosInteres.xml");
            }
            catch (Exception)
            {
                var fichero = Application.GetResourceStream(new Uri("Datos/PuntosInteres.xml", UriKind.Relative));
                doc.Load(fichero.Stream);
            }

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                Uri[] imagenes_pdi = generarImagenesPdi(node.Attributes["Imagen"].Value);
                var nuevoPdi = new PuntoInteres(node.Attributes["Nombre"].Value, node.Attributes["Descripcion"].Value,
                    node.Attributes["Tipologia"].Value, imagenes_pdi);
                listado.Add(nuevoPdi);
            }
            return listado;
        }

        private Uri[] generarImagenesPdi(string v)
        {
            v = v.Substring(1, v.Length - 2);
            string[] imagenes_pdi = v.Split(',');
            List<Uri> imagen = new List<Uri>();

            foreach (string imagen_pdi in imagenes_pdi)
            {
                Uri imagen_aux = new Uri(imagen_pdi, UriKind.RelativeOrAbsolute);
                imagen.Add(imagen_aux);
            }
            return imagen.ToArray();
        }

        private List<Ruta> cargarRutas()
        {
            List<Ruta> listado = new List<Ruta>();
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(Directory.GetCurrentDirectory() + @"/Rutas.xml");
            }
            catch (Exception)
            {
                var fichero = Application.GetResourceStream(new Uri("Datos/Rutas.xml", UriKind.Relative));
                doc.Load(fichero.Stream);
            }

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                Excursionista[] excursionistas = generarExcursionistas(node.Attributes["Excursionistas"].Value);
                Guia[] guias = generarGuias(node.Attributes["Guias"].Value);
                PuntoInteres[] pdis = generarPuntosInteres(node.Attributes["PuntosInteres"].Value);
                var nuevaRuta = new Ruta(node.Attributes["Nombre"].Value, node.Attributes["Distancia"].Value,
                    node.Attributes["Provincia"].Value, node.Attributes["Origen"].Value, node.Attributes["Destino"].Value,
                    DateTime.Parse(node.Attributes["FechaRealizacion"].Value), node.Attributes["Duracion"].Value, node.Attributes["Dificultad"].Value,
                    node.Attributes["Descripcion"].Value, excursionistas, comprobarEstadoRuta(DateTime.Parse(node.Attributes["FechaRealizacion"].Value)),
                    new Uri(node.Attributes["Imagen"].Value, UriKind.RelativeOrAbsolute), guias, pdis);
                cargarExcursionistaRutas(nuevaRuta);
                cargarGuiasRutas(nuevaRuta);
                listado.Add(nuevaRuta);
            }
            return listado;
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

        private List<Guia> cargarGuias()
        {
            List<Guia> listado = new List<Guia>();
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(Directory.GetCurrentDirectory() + @"/Guias.xml");
            }
            catch (Exception)
            {
                var fichero = Application.GetResourceStream(new Uri("Datos/Guias.xml", UriKind.Relative));
                doc.Load(fichero.Stream);
            }

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                var nuevoGuia = new Guia(node.Attributes["Nombre"].Value, node.Attributes["Apellidos"].Value,
                    new Uri(node.Attributes["Imagen"].Value, UriKind.RelativeOrAbsolute), node.Attributes["Idiomas"].Value, 
                    node.Attributes["Telefono"].Value, node.Attributes["Disponibilidad"].Value,
                    node.Attributes["Email"].Value, Convert.ToDouble(node.Attributes["Valoracion"].Value));
                listado.Add(nuevoGuia);
            }
            return listado;
        }

        private Excursionista[] generarExcursionistas(string v)
        {
            //List<string> rutas_nombre = new List<string>();
            v = v.Substring(1, v.Length - 2);
            string[] nombres_excursionistas = v.Split(',');
            List<Excursionista> excur = new List<Excursionista>();

            foreach (string nombre_exc in nombres_excursionistas)
            {
                foreach (Excursionista exc_aux in Excursionistas)
                {
                    if (nombre_exc == exc_aux.Nombre + " " + exc_aux.Apellidos)
                    {
                        excur.Add(exc_aux);
                        break;
                    }
                }
            }
            return excur.ToArray();
        }

        private Guia[] generarGuias(string v)
        {
            v = v.Substring(1, v.Length - 2);
            string[] nombres_guias = v.Split(',');
            List<Guia> guia = new List<Guia>();

            foreach (string nombre_guia in nombres_guias)
            {
                foreach (Guia guia_aux in Guias)
                {
                    if (nombre_guia == guia_aux.Nombre + " " + guia_aux.Apellidos)
                    {
                        guia.Add(guia_aux);
                        break;
                    }
                }
            }
            return guia.ToArray();
        }

        private PuntoInteres[] generarPuntosInteres(string v)
        {
            v = v.Substring(1, v.Length - 2);
            string[] nombres_pdi = v.Split(',');
            List<PuntoInteres> pdi = new List<PuntoInteres>();

            foreach (string nombre_pdi in nombres_pdi)
            {
                foreach (PuntoInteres pdi_aux in PuntosInteres)
                {
                    if (nombre_pdi == pdi_aux.Nombre)
                    {
                        pdi.Add(pdi_aux);
                        break;
                    }
                }
            }
            return pdi.ToArray();
        }

        private void cargarExcursionistaRutas(Ruta r_selec)
        {
            List<Excursionista> exc = new List<Excursionista>();
            if (r_selec != null)
            {
                if (r_selec.Excursionistas.Length > 0)
                {
                    for (int i = 0; i < r_selec.Excursionistas.Length; i++)
                    {
                        exc.Add(r_selec.Excursionistas[i]);
                        for (int x = 0; x < Excursionistas.Count; x++)
                        {
                            if (Excursionistas[x] == exc[i])
                            {
                                Excursionistas[x].anadirRuta(r_selec);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void cargarGuiasRutas(Ruta r_selec)
        {
            List<Guia> guias = new List<Guia>();
            if (r_selec != null)
            {
                if (r_selec.Guias.Length > 0)
                {
                    for (int i = 0; i < r_selec.Guias.Length; i++)
                    {
                        guias.Add(r_selec.Guias[i]);
                        for (int x = 0; x < Guias.Count; x++)
                        {
                            if (Guias[x] == guias[i])
                            {
                                Guias[x].anadirRuta(r_selec);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private DateTime generarFecha(string value)
        {
            if (value.Equals("-1"))
            {
                return DateTime.Now;
            }
            else
            {
                return Convert.ToDateTime(value);
            }
        }

        private void Button_Acceder_Click(object sender, RoutedEventArgs e)
        {
            if (comprobarAdmin(TextBox_Usuario.Text, adv_usuario_lo) && comprobarPass(PasswordBox_Contrasena.Password, adv_contra_lo))
            {
                PrincipalWindow.asignarUSuario(aAux);
                this.Visibility = Visibility.Collapsed;
                wMarco.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Corrige los campos con avisos.", "Valores incorrectos");
            }

        }


        private bool comprobarAdmin(string usuario, Image i)
        {
            bool valido = false;
            foreach (Administrador admAux in Administradores)
            {
                if (admAux.Usuario.Equals(usuario))
                {
                    aAux = admAux;
                    valido = true;
                    break;
                }
            }
            if (valido)
            {
                comprobarControl(i, true);
                return true;
            }
            else
            {
                aAux = null;
                comprobarControl(i, false);
                return false;
            }
        }

        private bool comprobarPass(string pass, Image i)
        {
            if (aAux.Contrasenia.Equals(pass))
            {
                comprobarControlContra(i, true);
                return true;
            }
            else
            {
                comprobarControlContra(i, false);
                return false;
            }
        }

        private void comprobarControl(Image c, bool valido)
        {
            if (valido)
            {
                c.Visibility = Visibility.Collapsed;
                textbox_usuario.Background = Brushes.White;

            }
            else
            {
                c.Visibility = Visibility.Visible;
                textbox_usuario.Background = Brushes.IndianRed;



            }
        }

        private void comprobarControlContra(Image c, bool valido)
        {
            if (valido)
            {
                c.Visibility = Visibility.Collapsed;
                passwordbox_contra.Background = Brushes.White;
            }
            else
            {
                c.Visibility = Visibility.Visible;
                passwordbox_contra.Background = Brushes.IndianRed;


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
        private void Window_Closed(object sender, EventArgs e)
        {
            guardar();
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Grid_Login.Visibility != Visibility.Visible)
            {
                MessageBoxResult result = MessageBox.Show("¿Estás seguro de que quieres cerrar la aplicación?", "Confirmación", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        Application.Current.Shutdown();
                        break;
                    case MessageBoxResult.No:
                        e.Cancel = true;
                        break;
                }
            }
            else
            {
                Application.Current.Shutdown();
            }
        }
        private void Enter_App(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Acceder_Click(sender, e);
            }
        }

        public static void escribirXMLAdmins()
        {

            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("Administradores");
            xmlDoc.AppendChild(rootNode);

            XmlNode adminNodo;
            XmlAttribute atributo;

            foreach (Administrador a in Administradores)
            {
                adminNodo = xmlDoc.CreateElement("Administrador");
                atributo = xmlDoc.CreateAttribute("Usuario");
                atributo.Value = a.Usuario;
                adminNodo.Attributes.Append(atributo);
                atributo = xmlDoc.CreateAttribute("Contrasenia");
                atributo.Value = a.Contrasenia;
                adminNodo.Attributes.Append(atributo);
                atributo = xmlDoc.CreateAttribute("Nombre");
                atributo.Value = a.Nombre;
                adminNodo.Attributes.Append(atributo);
                atributo = xmlDoc.CreateAttribute("Apellidos");
                atributo.Value = a.Apellidos;
                adminNodo.Attributes.Append(atributo);
                atributo = xmlDoc.CreateAttribute("FechaDeNacimiento");
                atributo.Value = a.Fecha_Nacimiento;
                adminNodo.Attributes.Append(atributo);
                atributo = xmlDoc.CreateAttribute("Email");
                atributo.Value = a.Email;
                adminNodo.Attributes.Append(atributo);
                atributo = xmlDoc.CreateAttribute("Telefono");
                atributo.Value = a.N_Telefono;
                adminNodo.Attributes.Append(atributo);
                atributo = xmlDoc.CreateAttribute("Pais");
                atributo.Value = a.Pais;
                adminNodo.Attributes.Append(atributo);
                atributo = xmlDoc.CreateAttribute("Imagen");
                atributo.Value = a.Imagen.ToString();
                adminNodo.Attributes.Append(atributo);
                atributo = xmlDoc.CreateAttribute("UltimoAcceso");
                atributo.Value = a.UltimoAcceso.ToString();
                adminNodo.Attributes.Append(atributo);

                rootNode.AppendChild(adminNodo);
            }

            xmlDoc.Save("Administrador.xml");
        }

        public static void guardar()
        {
            escribirXMLAdmins();

        }

        private void Button_Olvide_Contrasenia_Click(object sender, MouseButtonEventArgs e)
        {
            if (String.IsNullOrEmpty(TextBox_Usuario.Text))
            {
                TextBox_Usuario.Background = Brushes.IndianRed;
                MessageBox.Show("Debes rellenar el campo de usuario.", "Ayuda", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                string contrasenia = "";
                TextBox_Usuario.Background = Brushes.White;
                foreach (Administrador admAux in Administradores)
                {
                    if (admAux.Usuario.Equals(TextBox_Usuario.Text))
                    {
                        contrasenia = admAux.Contrasenia;
                        break;
                    }
                }
                MessageBox.Show("Su contraseña es: " + contrasenia, "Contraseña", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Olvide_Contrasenia_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBox_Olvide.TextDecorations = TextDecorations.Underline;
        }
        private void Olvide_Contrasenia_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBox_Olvide.TextDecorations = null;
        }

    }
}