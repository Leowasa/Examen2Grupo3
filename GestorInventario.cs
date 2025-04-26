using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen2Grupo3
{
    class GestorInventario
    {
        public class producto
        {
            public int ID { get; set; }
            public string Nombre { get; set; }
            public string Categoria { get; set; }
            public string Descripcion { get; set; }
            public int Stock { get; set; }
            public decimal PrecioUnitario { get; set; }
            public producto() { }
            public producto(int id, string nombre, string categoria, string descripcion, int stock, decimal precio)
            {
                ID = id;
                Nombre = nombre;
                Categoria = categoria;
                Descripcion = descripcion;
                Stock = stock;
                PrecioUnitario = precio;
            }



        }
    }
}
