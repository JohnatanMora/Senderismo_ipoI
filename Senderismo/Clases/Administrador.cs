using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Senderismo.Clases
{
    public class Administrador
    {
        public string Usuario { set; get; }
        public string Contrasenia { set; get; }
        public string Nombre { set; get; }
        public string Apellidos { set; get; }
        public string Fecha_Nacimiento { set; get; }
        public string Email { set; get; }
        public string N_Telefono { set; get; }
        public string Pais { set; get; }
        public Uri Imagen { set; get; }
        public DateTime UltimoAcceso { set; get; }

        public Administrador(string usuario, string contrasenia, string nombre, string apellidos, string fecha_nacimiento, string email, string n_telef, string pais, Uri imagen, DateTime ultimoAcceso)
        {
            Usuario = usuario;
            Contrasenia = contrasenia;
            Nombre = nombre;
            Apellidos = apellidos;
            Fecha_Nacimiento = fecha_nacimiento;
            Email = email;
            N_Telefono = n_telef;
            Pais = pais;
            Imagen = imagen;
            UltimoAcceso = ultimoAcceso;
        }
        public Administrador(string usuario, string contrasenia, string nombre, string apellidos, string fecha_nacimiento, string email, string n_telef, string pais, Uri imagen)
        {
            Usuario = usuario;
            Contrasenia = contrasenia;
            Nombre = nombre;
            Apellidos = apellidos;
            Fecha_Nacimiento = fecha_nacimiento;
            Email = email;
            N_Telefono = n_telef;
            Pais = pais;
            Imagen = imagen;
            UltimoAcceso = DateTime.Now;
        }
    }
}
