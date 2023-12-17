using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senderismo.Clases
{
    public class Excursionista
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public int Edad { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public Ruta[] Rutas { get; set; }
        public Uri Foto { get; set; }

        /*public Excursionista(string nombre, string apellidos, int edad, string telefono, string email, Ruta[] rutas, Uri foto)
        {
            Nombre = nombre;
            Apellidos = apellidos;
            Edad = edad;
            Telefono = telefono;
            Email = email;
            Rutas = rutas;
            Foto = foto;
        }*/
        public Excursionista(string nombre, string apellidos, int edad, string telefono, string email, Uri foto)
        {
            Nombre = nombre;
            Apellidos = apellidos;
            Edad = edad;
            Telefono = telefono;
            Email = email;
            Foto = foto;
            Rutas = new Ruta[20];
        }

        public void actualizar(string nombre, string apellidos, int edad, string telefono, string email, Uri foto)
        {
            Nombre = nombre;
            Apellidos = apellidos;
            Edad = edad;
            Telefono = telefono;
            Email = email;
            Foto = foto;
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
    }
}
