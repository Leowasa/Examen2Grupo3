using System.IO; // Ensure this is included for File operations
using System.Windows.Forms;
using Examen2Grupo3;
using Newtonsoft.Json;
using System.Collections.Generic; // Ensure this is included for List<T>
using static Examen2Grupo3.RegistroPedidos;


namespace ejemplo
{

    public partial class PedidosHistorial : Form
    {
        private List<Producto> listaPersonas = new List<Producto>();
        private List<Pedido> listaPedido = new List<Pedido>();
        public PedidosHistorial()
        {
            InitializeComponent();
            CargarDatosDesdeJson();
        }

        // Fix for CS1503: Use JsonConvert from Newtonsoft.Json instead of JsonSerializer
        private void CargarDatosDesdeJson()
        {
            string rutaArchivo = "datos.json";
            if (File.Exists(rutaArchivo))
            {
                string jsonString = File.ReadAllText(rutaArchivo);
                var pedidos = JsonConvert.DeserializeObject<List<Pedido>>(jsonString);

                if (pedidos != null) // Check for null to avoid CS8601
                {
                    foreach (var datos in pedidos)
                    {
                        dataGridView1.Rows.Add(datos.ID, datos.Cliente.Nombre, datos.Fecha.ToString("dd/MM/yyyy"), datos.Total, datos.Estado);

                    }
                }
            }
        }

        private void PedidosHistorial_Load(object sender, EventArgs e)
        {

        }
    }
}


