using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Examen2Grupo3.Datos;
using ejemplo;

using System.Text.Json;
using Newtonsoft.Json;

namespace Examen2Grupo3
{
    public partial class OrdenesHistorial : Form
    {
        List<Pedido> listaPedidos = new List<Pedido>();
        Datos.Usuarios usuarioActual = new Datos.Usuarios();
        private BindingSource bindingSource = new BindingSource();
        public OrdenesHistorial(Datos.Usuarios usuarioactual)
        {
           
            InitializeComponent();
            this.usuarioActual = usuarioactual;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = bindingSource;
            dataGridView1.Columns["Total"].DefaultCellStyle.Format = "C2";
            dataGridView1.Columns["Total"].DefaultCellStyle.FormatProvider = new System.Globalization.CultureInfo("en-US");

            CargarOrdenes();
        }
        public void CargarOrdenes()
        {

            if (File.Exists("Ordenes.json"))
            {

                string json = File.ReadAllText("Ordenes.json");
                listaPedidos = System.Text.Json.JsonSerializer.Deserialize<List<Pedido>>(json) ?? new List<Pedido>();
                bindingSource.DataSource = listaPedidos;
                bindingSource.ResetBindings(false);
            }
        }
        private void BuscarElemento(string textoBusqueda)
        {

            // Verificar que el texto de búsqueda tenga al menos 3 caracteres
            if ((string.IsNullOrWhiteSpace(textoBusqueda) || textoBusqueda.Length < 3))
            {
                // Mostrar todas las ordenes
                bindingSource.DataSource = new List<Pedido>(listaPedidos);

            }
            else
            {
                string filtro = textoBusqueda.ToLower();
                var filtrados =listaPedidos.Where(p =>
                    p.ID.ToString().Contains(filtro) ||
                    (p.Cliente != null && p.Cliente.Nombre.ToLower().Contains(filtro))
                ).ToList();

                bindingSource.DataSource = filtrados;
            }
            bindingSource.ResetBindings(false);
        }
        private void GuardarCambios(List<Pedido> orden)
        {
            string rutaArchivo = "Ordenes.json";

            try
            {
                // Serializar la lista actualizada de pedidos
                var json = JsonConvert.SerializeObject(orden, Formatting.Indented);

                // Escribir el JSON en el archivo
                File.WriteAllText(rutaArchivo, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void guna2TextBox2_TextChanged(object sender, EventArgs e)//barra de busqueda
        {
            BuscarElemento(guna2TextBox2.Text);
        }
        public void AbrirOtroFormulario(Pedido seleccionado)
        {
            Form1 principal = (Form1)Application.OpenForms["Form1"];
            if (principal != null)
            {
                principal.AbrirFormulario(new Factura(seleccionado, 3, usuarioActual)); // abre el formulario para visualizar el orden 
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Codigo_especial Form = new Codigo_especial();
            // Si el usuario hizo clic en el botón "Editar"
            if (e.ColumnIndex == dataGridView1.Columns["Ver"].Index && e.RowIndex >= 0)
            {
               int idPedido = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Numero"].Value);
                foreach (var lista in listaPedidos)
                {
                   if (lista.ID == idPedido)
                   AbrirOtroFormulario(lista);
                }
               return;
        
            }

            // Si el usuario hizo clic en el botón "Eliminar"
            if (e.ColumnIndex == dataGridView1.Columns["Eliminar"].Index && e.RowIndex >= 0)
            {
                if (usuarioActual.Tipo == "Aprobador" || usuarioActual.Tipo == "Registrador")
                {
                    Form.ShowDialog();
                    if (Form.DialogResult == DialogResult.OK)
                    {
                        //continuar con el codigo 

                    }
                    else return;

                }


                DialogResult result = MessageBox.Show("¿Deseas eliminar esta orden?", "Confirmar eliminación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    // Obtener el ID del pedido desde la celda correspondiente
                    int idPedido = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Numero"].Value);

                    // Buscar el pedido en la lista por su ID
                    Pedido pedidoAEliminar = listaPedidos.Find(p => p.ID == idPedido);

                    if (pedidoAEliminar != null)
                    {
                        // Eliminar el pedido de la lista
                        listaPedidos.Remove(pedidoAEliminar);

                        // Guardar los cambios en el archivo JSON
                        GuardarCambios(listaPedidos);

                     
                    }
                    else
                    {
                        MessageBox.Show($"No se encontró ningún pedido con ID {idPedido}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }


            }

        }
      
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

}
