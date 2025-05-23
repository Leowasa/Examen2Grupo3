using System.Windows.Forms;
using ejemplo;
using Newtonsoft.Json;
using static Examen2Grupo3.Datos;

namespace Examen2Grupo3
{
    public partial class GenerarOrden : Form
    {
        public static Pedido seleccionado = new Pedido();
        public static List<Pedido> Lista = new List<Pedido>();
        public  List<Pedido> ListaOriginal = new List<Pedido>();
        public Datos.Usuarios UsuarioActual = new Datos.Usuarios();
        private BindingSource bindingSource = new BindingSource();

        Form1 form = new Form1();
        public GenerarOrden(Datos.Usuarios UsuarioActual)
        {
            this.UsuarioActual = UsuarioActual;
            InitializeComponent();
            dataGridView1.Columns["Total"].DefaultCellStyle.Format = "C2";//para Mostrar el monto con decimales
            dataGridView1.Columns["Total"].DefaultCellStyle.FormatProvider = new System.Globalization.CultureInfo("en-US");//y mostrarlo en dolares
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = bindingSource;

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
          
            string rutaArchivoOriginal = "pedidos.json";


            // Verificar si el archivo original existe
            if (File.Exists(rutaArchivoOriginal))
            {

                Lista  = LeerPedidos();

                ListaOriginal = Lista;
                bindingSource.DataSource =Lista;
                bindingSource.ResetBindings(false);
            }
          

        }

        private void GuardarCambios(List<Pedido> orden)
        {
            string rutaArchivo = "pedidos.json";

            try
            {
                // Cargar los pedidos existentes
                List<Pedido> PedidosExistentes = LeerPedidos();

                // Serializar la lista actualizada de pedidos
                var json = JsonConvert.SerializeObject(PedidosExistentes, Formatting.Indented);

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

    
        public void AbrirOtroFormulario(Pedido seleccionadot, int opcion)
        {
            Form1 principal = (Form1)Application.OpenForms["Form1"];
            if (principal != null)
            {

                principal.AbrirFormulario(new Factura(seleccionadot, 1, UsuarioActual)); // Abre el formulario Factura Con los detalles del pedido



            }
        }
        private void BuscarElemento(string textoBusqueda)
        {
            if (string.IsNullOrWhiteSpace(textoBusqueda) || textoBusqueda.Length < 3)
            {
                // Mostrar todas las órdenes
                bindingSource.DataSource = new List<Pedido>(ListaOriginal);
            }
            else
            {
                string filtro = textoBusqueda.ToLower();
                var filtrados = Lista.Where(p =>
                    p.ID.ToString().Contains(filtro) ||
                    (p.Cliente != null && p.Cliente.Nombre != null && p.Cliente.Nombre.ToLower().Contains(filtro))
                ).ToList();

                bindingSource.DataSource = filtrados;
            }
            bindingSource.ResetBindings(false);
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)//barra de busqueda
        {
            BuscarElemento(guna2TextBox2.Text);
        }

        private void ComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)//celda seleccionada del datagrid
        {

            if (e.ColumnIndex == dataGridView1.Columns["Ver"].Index && e.RowIndex >= 0)
            {
                if (Lista[e.RowIndex] != null)
                {
                    AbrirOtroFormulario(Lista[e.RowIndex], 1);
                }

            }
            else if (e.ColumnIndex == dataGridView1.Columns["Eliminar2"].Index && e.RowIndex >= 0)//solicitara una clave especial en caso de eliminar un pedido preventivo siendo aprobador o registrador
            {
                Codigo_especial Form = new Codigo_especial();
                if (UsuarioActual.Tipo == "Aprobador" || UsuarioActual.Tipo == "Registrador")
                {
                    Form.ShowDialog();
                    if (Form.DialogResult == DialogResult.OK)
                    {
                        eliminar(e);
                        return;
                    }
                }
                else eliminar(e); return;


            }
            else if (e.ColumnIndex == dataGridView1.Columns["Estado"].Index && e.RowIndex >= 0)
            {
                Cambiar_estado estado = new Cambiar_estado(Lista[e.RowIndex], UsuarioActual);
                estado.ShowDialog();
                CargarDatosDesdeJson();

            }
        }
        private void eliminar(DataGridViewCellEventArgs e)
        {
            //Preguntar si en verdad desea eliminar el pedido
            DialogResult result = MessageBox.Show("¿Deseas eliminar este pedido?", "Confirmar eliminación",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                Lista.RemoveAt(e.RowIndex);//elimina el pedido preventivo seleccionado
                 GuardarCambios(Lista);
            }
        }
      
    }
}
