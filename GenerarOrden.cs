using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Examen2Grupo3.RegistroPedidos;

namespace Examen2Grupo3
{
    public partial class GenerarOrden : Form
    {
        public GenerarOrden()
        {
            InitializeComponent();
            CargarDatosDesdeJson();
        }
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
        private void GenerarOrden_Load(object sender, EventArgs e)
        {

        }

        private void Cliente_Click(object sender, EventArgs e)
        {

        }
    }
}
