using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen2Grupo3
{
    public class GestorClientes
    {
        public class cliente 
        { 
            public int ID { get; set; }
            public string? Nombre { get; set; }
            public string Direccion { get; set; }
            public string Correo { get; set; }
            public string Tipo { get; set; }
            public cliente() { }
        }

    }
}
