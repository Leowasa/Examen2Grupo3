using Newtonsoft.Json; // Agregar esta directiva para usar JsonSerializerOptions
using System.Drawing.Drawing2D;
using static Examen2Grupo3.RegistroPedidos;


namespace Examen2Grupo3
{
    public partial class GenerarPedido : Form
    {
        private Pedido pedido = new Pedido();//se almacenan distintos miembros del pedido en distintas funciones para despues guardarlos en una lista 
        private List<Pedido> ListaPedidos = new List<Pedido>();
        private static int NumeroPedido;
        public GenerarPedido()
        {
            InitializeComponent();
            label14.Text = CargarNum().ToString("D6");
        }
        private int CargarNum()
        {
            string rutaArchivo = "datos.json";
            if (File.Exists(rutaArchivo))
            {
                string jsonString = File.ReadAllText(rutaArchivo);
                var pedidos = JsonConvert.DeserializeObject<List<Pedido>>(jsonString);

                if (pedidos.Count > 0)
                {
                    return NumeroPedido = pedidos.Max(p => p.ID) + 1; // Encuentra el mayor número de pedido
                }
            }
            return 1; // Si no hay pedidos, empieza desde 0
        }

        public class RoundButton : Button
        {
            protected override void OnPaint(PaintEventArgs pevent)
            {
                GraphicsPath path = new GraphicsPath();
                path.AddEllipse(0, 0, Width, Height);
                this.Region = new Region(path);
                base.OnPaint(pevent);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Agregar_Productos formProductos = new Agregar_Productos();
            if (formProductos.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Producto ProductoNuevo = new Producto
                    {
                        Nombre = formProductos.Nombre,
                        ID = formProductos.Id,
                        Categoria = formProductos.Categoria,
                        PrecioUnitario = formProductos.PrecioUnitario,
                        Cantidad = formProductos.Cantidad,
                        Descripcion = formProductos.Descripcion
                    };
                    //  listaProductos.Add(ProductoNuevo);
                    if (pedido.Productos == null)
                    {
                        pedido.Productos = new List<Producto>();

                    }
                    pedido.Estado = "Pendiente";
                    pedido.Productos.Add(ProductoNuevo); //codigo anterior  pedido.Productos = ListaProductos
                    label18.Text = pedido.SubtTotal.ToString();
                    Descuento();
                    dataGridView1.Rows.Add(ProductoNuevo.ID, ProductoNuevo.Nombre, ProductoNuevo.Categoria, ProductoNuevo.Descripcion, ProductoNuevo.Cantidad, ProductoNuevo.PrecioUnitario, ProductoNuevo.Cantidad * ProductoNuevo.PrecioUnitario);

                }
                catch
                {
                    MessageBox.Show("Error al ingresar los datos. Verifique que haya ingresado correctamente los datos");
                }

            }
            else
            {
                //nada xd
            }
        }
        public void Descuento()
        {
            if (pedido.Productos.Count > 3)
            {
                decimal descuento = 0.20m;
                descuento = pedido.SubtTotal * descuento;
                pedido.Total = pedido.SubtTotal - descuento;

                label20.Text = pedido.Total.ToString();
                label19.Text = "20%";

            }
            else
            {
                pedido.Total = pedido.SubtTotal;
                label20.Text = pedido.SubtTotal.ToString();
            }


        }
        public string? GuardarCliente()
        {
            try
            {
                pedido.Cliente = new Cliente
                {
                    Nombre = guna2TextBox1.Text,
                    ID = int.Parse(guna2TextBox2.Text),
                    Correo = guna2TextBox3.Text,
                    Direccion = guna2TextBox4.Text,

                };
                pedido.Cliente = pedido.Cliente;
                return "";
            }
            catch
            {
                return null;
            }

        }
        private void GuardarDatosEnJson(Pedido nuevoPedido)
        {
            string rutaArchivo = "datos.json";

            try
            {
                // Cargar los pedidos existentes
                List<Pedido> pedidosExistentes = LeerPedidos();

                // Agregar el nuevo pedido a la lista
                pedidosExistentes.Add(nuevoPedido);

                // Serializar la lista actualizada de pedidos
                var json = JsonConvert.SerializeObject(pedidosExistentes, Formatting.Indented);

                // Escribir el JSON en el archivo
                File.WriteAllText(rutaArchivo, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        // Guardar el pedido cuando el usuario presione el botón
        private void guna2Button3_Click_1(object sender, EventArgs e)
        {
            if (pedido.Productos == null || GuardarCliente() == null)
            {
                MessageBox.Show("Faltan Datos por rellenar");
                return;
            }
            pedido.ID = int.Parse(label14.Text);
            pedido.Fecha = guna2DateTimePicker1.Value;
            GuardarDatosEnJson(pedido);
            dataGridView1.Rows.Clear();
            LimpiarTextBox();
            label14.Text = CargarNum().ToString("D6");
            label18.Text = ": ";
            label19.Text = ": ";
            label20.Text = ": ";
            MessageBox.Show("Pedido generado exitosamente!");



        }
        private void LimpiarTextBox()
        {
            foreach (Control control in this.Controls)
            {
                if (control is Guna.UI2.WinForms.Guna2TextBox gunaTextBox)
                {
                    gunaTextBox.Clear();
                }
            }
        }
        private void GenerarPedido_Load(object sender, EventArgs e)
        {

        }
        public List<Pedido> LeerPedidos()
        {
            string rutaArchivo = "datos.json";
            if (File.Exists(rutaArchivo))
            {
                string contenido = File.ReadAllText(rutaArchivo);
                return JsonConvert.DeserializeObject<List<Pedido>>(contenido) ?? new List<Pedido>();
            }
            return new List<Pedido>();
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            label18.Text = ": ";
            label19.Text = ": ";
            label20.Text = ": ";
            Pedido borrar = new Pedido();
            pedido = borrar;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            LimpiarTextBox();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}