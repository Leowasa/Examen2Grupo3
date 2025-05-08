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
using ejemplo;
using static Examen2Grupo3.Login;
using static Examen2Grupo3.GenerarPedido;

namespace Examen2Grupo3
{
    public partial class GenerarOrden : Form
    {
        public static Pedido seleccionado = new Pedido();
        public static List<Pedido> Lista = new List<Pedido>();
        public  RegistroPedidos.Usuarios UsuarioActual = new RegistroPedidos.Usuarios();
        // private bool _cargandoDatos = false;
        Form1 form = new Form1();
        public GenerarOrden(RegistroPedidos.Usuarios UsuarioActual)
        {
            this.UsuarioActual = UsuarioActual;
            InitializeComponent();
            CargarDatosDesdeJson();
        }
        public List<Pedido> LeerPedidos()
        {
            string rutaArchivo = "pedidos.json";
            if (File.Exists(rutaArchivo))
            {
                string contenido = File.ReadAllText(rutaArchivo);
                return JsonConvert.DeserializeObject<List<Pedido>>(contenido) ?? new List<Pedido>();
            }
            return new List<Pedido>();
        }


        private void CargarDatosDesdeJson()
        {
            // _cargandoDatos = true;
            string rutaArchivoOriginal = "pedidos.json";


            // Verificar si el archivo original existe
            if (File.Exists(rutaArchivoOriginal))
            {

                var pedidos = LeerPedidos();

                if (pedidos != null) // Evitar CS8601
                {
                    dataGridView1.Rows.Clear();
                    foreach (var datos in pedidos)
                    {

                        // Agregar una nueva fila con los datos
                        int rowIndex = dataGridView1.Rows.Add(datos.ID, datos.Cliente.Nombre, datos.Fecha.ToString("dd/MM/yyyy"), datos.Total);

                        // Obtener la celda ComboBox correctamente

                        var comboCell = dataGridView1.Rows[rowIndex].Cells["Estado"] as DataGridViewComboBoxCell;
                        if (comboCell.Value == null)
                        {

                            comboCell.Value = "Pendiente"; // Estado por defecto
                        }
                        if (comboCell != null && comboCell.Items != null && comboCell.Value != null)
                        {

                            comboCell.Value = datos.Estado;
                            if (comboCell.Items.Contains(datos.Estado))
                            {

                                comboCell.Value = datos.Estado; // Mantener el estado
                            }
                        }

                    }
                    Lista = pedidos;
                }
            }
            else
            {
                MessageBox.Show("El archivo 'datos.json' no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // _cargandoDatos = true;
        }

        private void GuardarCambios(List<Pedido> orden)
        {
            string rutaArchivo = "Pedidos.json";

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
        private void GenerarOrden_Load(object sender, EventArgs e)
        {

        }

        private void Cliente_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // Verificar si la celda editada pertenece a la columna de estado
            // Verifica si estamos en la columna correcta y si el control es un ComboBox
            if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.Columns["Estado"].Index &&
                e.Control is ComboBox comboBox)
            {
                // Eliminar manejador previo para evitar duplicaciones
                comboBox.SelectedIndexChanged -= ComboBox_SelectedIndexChanged;

                // Agregar nuevo manejador para limitar opciones
                comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            }
        }

        public void AbrirOtroFormulario(Pedido seleccionadot, int opcion)
        {
            Form1 principal = (Form1)Application.OpenForms["Form1"];
            if (principal != null)
            {
                switch (opcion) 
                {
                    case 1:
                        principal.AbrirFormularioEnPanel(new Factura(seleccionadot, opcion)); // Reemplaza con el formulario que desees abrir
                        break;
                    case 2:
                        principal.AbrirFormularioEnPanel(new Factura(seleccionadot, opcion, UsuarioActual)); // Reemplaza con el formulario que desees abrir
                        break;
                }
               
            }
        }
        private void BuscarElemento(string textoBusqueda)
        {
            // Verificar que el texto de búsqueda tenga al menos 4 caracteres
            if (textoBusqueda.Length < 4)
            {
                // Si tiene menos de 4 caracteres, mostrar todas las filas
                foreach (DataGridViewRow fila in dataGridView1.Rows)
                {
                    fila.Visible = true;
                }
                return;
            }

            // Convertir el texto de búsqueda a minúsculas para una comparación insensible a mayúsculas/minúsculas
            string filtro = textoBusqueda.ToLower();

            // Iterar sobre las filas del DataGridView
            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                // Verificar si la celda de ID o Nombre contiene el texto de búsqueda
                bool coincide = (fila.Cells["Numero"].Value != null && fila.Cells["Numero"].Value.ToString().ToLower().Contains(filtro)) ||
                                (fila.Cells["Nombre"].Value != null && fila.Cells["Nombre"].Value.ToString().ToLower().Contains(filtro));

                // Mostrar u ocultar la fila según si coincide con el filtro
                fila.Visible = coincide;
            }
        }


        private void ComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // Obtener el ComboBox que disparó el evento  
            var comboBox = sender as ComboBox;

            if (comboBox != null)
            {
                // Obtener la fila actual del DataGridView  
                var fila = dataGridView1.CurrentRow;
                string? estadoActual = comboBox.SelectedItem?.ToString();

                if (estadoActual == null)
                {
                    MessageBox.Show("El estado seleccionado no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                comboBox.Items.Clear();
                if (fila != null)
                {
                    // Extraer los datos de la fila  
                    if (int.TryParse(fila.Cells["Numero"].Value?.ToString(), out int idSeleccionado))
                    {
                        seleccionado.ID = idSeleccionado;
                        foreach (var lista in Lista)
                        {
                            if (lista.ID == seleccionado.ID)
                            {
                                if (estadoActual == "Entregado" || estadoActual == "Rechazado")
                                {
                                    lista.Estado = estadoActual;
                                    GuardarCambios(Lista);
                                   
                                    CargarDatosDesdeJson();
                                    return;
                                }
                                seleccionado = lista;
                                seleccionado.Estado = estadoActual ?? string.Empty;
                                lista.Estado = estadoActual ?? string.Empty;
                                GuardarCambios(Lista);
                                AbrirOtroFormulario(seleccionado, 2);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("El ID del pedido no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Codigo_especial Form = new Codigo_especial();
            if (UsuarioActual.Tipo == "Aprobador" || UsuarioActual.Tipo == "Registrador")
            {
                Form.ShowDialog();
                if (Form.DialogResult == DialogResult.OK)
                {
                    editar(e);
                }
            }
            editar(e);
        }
        private Pedido ObtenerPedidoSeleccionado()
        {
            // Verificar si hay una fila seleccionada en el DataGridView
            if (dataGridView1.CurrentRow != null)
            {
                // Obtener el valor del ID de la fila seleccionada
                var idSeleccionado = dataGridView1.CurrentRow.Cells["Numero"].Value?.ToString();

                if (!string.IsNullOrEmpty(idSeleccionado))
                {
                    // Buscar el pedido en la lista por el ID
                    var pedidoSeleccionado = Lista.FirstOrDefault(p => p.ID.ToString() == idSeleccionado);

                    if (pedidoSeleccionado != null)
                    {
                        return pedidoSeleccionado;

                    }
                }
            }

            // Si no se encuentra el pedido, devolver null o manejar el caso
            MessageBox.Show("No se pudo encontrar el pedido seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return null;
        }

        public void editar(DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == dataGridView1.Columns["Ver"].Index && e.RowIndex >= 0)
            {
                var pedido = ObtenerPedidoSeleccionado();
                if (pedido != null) 
                {
                    AbrirOtroFormulario(pedido, 1);
                }
                
            }
            else if (e.ColumnIndex == dataGridView1.Columns["Eliminar"].Index && e.RowIndex >= 0)
            {
                // Verificar que la celda pertenece a la columna de botones y no es el encabezado

                DialogResult result = MessageBox.Show("¿Deseas eliminar este producto?", "Confirmar eliminación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    Lista.RemoveAt(e.RowIndex);
                    dataGridView1.Rows.RemoveAt(e.RowIndex); // Eliminar la fila seleccionada
                    GuardarCambios(Lista);
                }



            }
        }
        private void dataGridView1_EditModeChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Estado" &&
        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                string estadoActual = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                DataGridViewComboBoxCell comboBoxCell = (DataGridViewComboBoxCell)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];

                comboBoxCell.Items.Clear(); // Limpiar las opciones antes de asignar las nuevas

                switch (estadoActual)
                {
                    case "Pendiente":
                        comboBoxCell.Items.AddRange(new string[] { "Pendiente", "Aprobado", "Rechazado" });
                        break;
                    case "Aprobado":
                        comboBoxCell.Items.AddRange(new string[] { "Entregado" });
                        break;
                    case "Entregado":
                        comboBoxCell.ReadOnly = true;
                        break;
                    case "Rechazado":
                        comboBoxCell.Items.AddRange(new string[] { "Rechazado" });
                        break;
                }
            }
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {
            BuscarElemento(guna2TextBox2.Text);
        }
    }
}