using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senderismo.Clases
{
    public class PuntoInteres
    {
        public string Nombre { get; set; }
        public string Tipologia { get; set; }
        public string Descripcion { get; set; }
        public Uri[] Fotos { get; set; }

        public PuntoInteres(string nombre, string tipologia, string descripcion, Uri[] fotos)
        {
            Nombre = nombre;
            Tipologia = tipologia;
            Descripcion = descripcion;
            Fotos = fotos;
        }
        public PuntoInteres(string nombre, string tipologia, string descripcion)
        {
            Nombre = nombre;
            Tipologia = tipologia;
            Descripcion = descripcion;
        }

        public void anadirFoto (Uri foto)
        {
            if (Fotos != null)
            {
                Uri[] aux_fotos = new Uri[Fotos.Length + 1];
                for (int i = 0; i < Fotos.Length; i++)
                {
                    aux_fotos[i] = Fotos[i];
                }
                aux_fotos[Fotos.Length] = foto;
                Fotos = aux_fotos;
            }
            else
            {
                Fotos = new Uri[1];
                Fotos[0] = foto;
            }
        }

    }
}
