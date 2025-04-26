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
            GenerarNuevoArchivoConNumeracion();
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

        private void GenerarNuevoArchivoConNumeracion()
        {
            string rutaArchivoOriginal = "datos.json";
            string rutaNuevoArchivo = "DatosGenerarOrden.json";

            if (File.Exists(rutaArchivoOriginal))
            {
                string jsonString = File.ReadAllText(rutaArchivoOriginal);
                var pedidos = JsonConvert.DeserializeObject<List<Pedido>>(jsonString);

                if (pedidos != null) // Check for null to avoid CS8601
                {
                    // Leer el archivo nuevo si existe para obtener los números ya asignados
                    List<dynamic> pedidosConNumeracionExistentes = new List<dynamic>();
                    HashSet<int> numerosExistentes = new HashSet<int>();

                    if (File.Exists(rutaNuevoArchivo))
                    {
                        string nuevoJsonString = File.ReadAllText(rutaNuevoArchivo);
                        pedidosConNumeracionExistentes = JsonConvert.DeserializeObject<List<dynamic>>(nuevoJsonString) ?? new List<dynamic>();

                        foreach (var item in pedidosConNumeracionExistentes)
                        {
                            numerosExistentes.Add((int)item.Numero);
                        }
                    }

                    // Generar numeración continua desde el número faltante
                    int numeroActual = numerosExistentes.Count > 0 ? numerosExistentes.Max() + 1 : 1;
                    var nuevosPedidosConNumeracion = pedidos.Select(pedido =>
                    {
                        while (numerosExistentes.Contains(numeroActual))
                        {
                            numeroActual++;
                        }
                        var resultado = new
                        {
                            Numero = numeroActual,
                            Pedido = pedido
                        };
                        numerosExistentes.Add(numeroActual);
                        numeroActual++;
                        return resultado;
                    }).ToList();

                    // Combinar los datos existentes con los nuevos
                    pedidosConNumeracionExistentes.AddRange(nuevosPedidosConNumeracion);

                    string nuevoJsonStringFinal = JsonConvert.SerializeObject(pedidosConNumeracionExistentes, Formatting.Indented);
                    File.WriteAllText(rutaNuevoArchivo, nuevoJsonStringFinal);
                }
            }
        }

        private void GenerarOrden_Load(object sender, EventArgs e)
        {

        }


        private void Cliente_Click(object sender, EventArgs e)
        {

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MostrarDatosDesdeNuevoJson();
        }

        private void MostrarDatosDesdeNuevoJson()
        {
            string rutaNuevoArchivo = "DatosGenerarOrden.json";
            if (File.Exists(rutaNuevoArchivo))
            {
                string jsonString = File.ReadAllText(rutaNuevoArchivo);
                var pedidosConNumeracion = JsonConvert.DeserializeObject<List<dynamic>>(jsonString);

                if (pedidosConNumeracion != null) // Check for null to avoid CS8601
                {
                    dataGridView1.Rows.Clear(); // Clear existing rows
                    foreach (var item in pedidosConNumeracion)
                    {
                        var pedido = item.Pedido;
                        dataGridView1.Rows.Add(item.Numero, pedido.ID, pedido.Cliente.Nombre, pedido.Fecha.ToString("dd/MM/yyyy"), pedido.Total, pedido.Estado);
                    }
                }
            }
        }
    }
}
