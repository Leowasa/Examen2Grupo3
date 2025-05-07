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
            public String Estado { get; set; }
            public decimal Descuento = 0.40M;
            public decimal SubtTotal => Productos.Sum(p => p.Total);
            public decimal Total { get; set; }
            public Usuarios Encargado { get; set; }
            public decimal IVA = 0.21M;
            public DateTime FechaValidado { get; set; }
            public string Observaciones { get; set; }
        }

        public class Cliente
        {
            public string Nombre { get; set; }
            public string Direccion { get; set; }
            public string Correo { get; set; }
            public int ID { get; set; }
            public string direccion { get; set; }
            public string Tipo { get; set; }

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

        public class Usuarios
        {
            public int ID { get; set; }
            public string Username { get; set; }
            public string Nombre { get; set; }
            public string Password { get; set; }
            public string Tipo { get; set; }

        }
        public class Empresa
        {
            public string RazonSocial { get; set; }
            public string Direccion { get; set; }
            public string Numero { get; set; }
            public string Correo { get; set; }
            public string Website { get; set; }
            public string Codigo { get; set; }

        }
        public void CargarUsuarios()
        {

        }
    }

}



