using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Examen2Grupo3 
{
    public class RegistroPedidos
    {

        public class Pedido
        {
            public DateTime Fecha { get; set; }
            public int ID { get; set; }
            public Cliente Cliente { get; set; }    
            public List<Producto> Productos { get; set; }
            public decimal Total => Productos.Sum(p => p.Total);
        }

        public class Cliente
        {
            public string Nombre { get; set; }
            public string Direccion { get; set; }
            public string Correo { get; set; }
            public int ID { get; set; } 
            public string direccion { get; set; }
        }

        public class Producto
        {
            public int ID { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public string Categoria { get; set; }
            public int Cantidad { get; set; }
            public decimal PrecioUnitario { get; set; }
            public decimal Total => Cantidad * PrecioUnitario;
        }
    }




}



