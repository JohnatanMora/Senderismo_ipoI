using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Senderismo.Clases
{
    public class Ruta
    {
        public string Nombre { get; set; }
        public string Distancia { get; set; }
        public string Provincia { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public DateTime FechaRealizacion { get; set; }
        public string Duracion { get; set; }
        public Excursionista[] Excursionistas { get; set; }
        public Guia[] Guias { get; set; }
        public string Descripcion { get; set; }
        public Uri Foto { get; set; }
        public string Dificultad { get; set; }
        public string Estado { get; set; }
        public PuntoInteres[] PuntosInteres { get; set; }

        /*public Ruta(string nombre, string distancia, string provincia, string origen, string destino, string fecha_realizacion, string duracion, string descripcion)
        {
            Nombre = nombre;
            Distancia = distancia;
            Provincia = provincia;
            Origen = origen;
            Destino = destino;
            FechaRealizacion = fecha_realizacion;
            Duracion = duracion;
            Descripcion = descripcion;
        }*/

        public Ruta(string nombre, string distancia, string provincia, string origen, string destino, DateTime fecha_realizacion, string duracion, string dificultad, string descripcion, Excursionista[] excursionistas, string estado, Uri foto, Guia[] guias, PuntoInteres[] pdis)
        {
            Nombre = nombre;
            Distancia = distancia;
            Provincia = provincia;
            Origen = origen;
            Destino = destino;
            FechaRealizacion = fecha_realizacion.Date;
            Duracion = duracion;
            Dificultad = dificultad;
            Descripcion = descripcion;
            Excursionistas = new Excursionista[20];
            Guias = new Guia[10];
            PuntosInteres = new PuntoInteres[10];
            for (int i = 0; i<excursionistas.Length;i++)
            {
                Excursionistas[i] = excursionistas[i];
            }
            for (int i = 0; i < guias.Length; i++)
            {
               Guias[i] = guias[i];
            }
            for (int i = 0; i < pdis.Length; i++)
            {
                PuntosInteres[i] = pdis[i];
            }
            Estado = estado;
            Foto = foto;
            //Guias = guias;
        }

        public Ruta(string nombre, string distancia, string provincia, string origen, string destino, DateTime fecha_realizacion, string descripcion, string estado, Uri foto)
        {
            Nombre = nombre;
            Distancia = distancia;
            Provincia = provincia;
            Origen = origen;
            Destino = destino;
            FechaRealizacion = fecha_realizacion.Date;
            Descripcion = descripcion;
            Excursionistas = new Excursionista[20];
            Guias = new Guia[10];
            PuntosInteres = new PuntoInteres[10];
            Estado = estado;
            Foto = foto;
        }

        public void actualizar(string nombre, string distancia, Uri foto, string provincia, string origen, string destino, DateTime fecha_realizacion, string descripcion)
        {
            Nombre = nombre;
            Distancia = distancia;
            Foto = foto;
            Provincia = provincia;
            Origen = origen;
            Destino = destino;
            FechaRealizacion = fecha_realizacion.Date;
            Descripcion = descripcion;
        }

        public void anadirExcursionista(Excursionista e)
        {
            for (int i = 0; i < Excursionistas.Length; i++)
            {
                if (Excursionistas[i] == null)
                {
                    Excursionistas[i] = e;
                    break;
                }
            }
        }

        public void anadirGuia(Guia g)
        {
            for (int i = 0; i < Guias.Length; i++)
            {
                if (Guias[i] == null)
                {
                    Guias[i] = g;
                    break;
                }
            }
        }
    }



}
