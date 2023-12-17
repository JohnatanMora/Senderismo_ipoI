using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Senderismo.Clases
{
    public class Guia
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public Uri Foto { get; set; }
        public string Idiomas { get; set; }
        public string Telefono { get; set; }
        public string Disponibilidad { get; set; }
        public string Email { get; set; }
        public double Valoracion { get; set; }
        public Ruta[] Rutas { get; set; }

        public Guia(string nombre, string apellidos, Uri foto, string idiomas, string telefono, string disponibilidad, string email, double valoracion)
        {
            Nombre = nombre;
            Apellidos = apellidos;
            Foto = foto;
            Idiomas = idiomas;
            Telefono = telefono;
            Disponibilidad = disponibilidad;
            Email = email;
            Valoracion = valoracion;
            Rutas = new Ruta[20];
        }

        public void anadirRuta(Ruta r)
        {
            for (int i = 0; i < Rutas.Length; i++)
            {
                if (Rutas[i] == null)
                {
                    Rutas[i] = r;
                    break;
                }
            }
        }

        public void actualizar(string nombre, string apellidos, Uri foto,string idiomas, string telefono, string disponibilidad, string email, double valoracion)
        {
            Nombre = nombre;
            Apellidos = apellidos;
            Foto = foto;
            Idiomas = idiomas;
            Telefono = telefono;
            Disponibilidad = disponibilidad;
            Email = email;
            Valoracion = valoracion;
        }

    }
}
