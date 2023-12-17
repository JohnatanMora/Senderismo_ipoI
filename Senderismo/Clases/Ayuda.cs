using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senderismo.Clases
{
    public class Ayuda
    {
        public string Titulo { set; get; }
        public string Contenido { set; get; }

        public Ayuda(string titulo, string contenido)
        {
            Titulo = titulo;
            Contenido = contenido;
        }
    }
}
