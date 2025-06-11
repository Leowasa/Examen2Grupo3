using Newtonsoft.Json; // Agregar esta directiva para usar JsonSerializerOptions
using System.Drawing.Drawing2D;
using static Examen2Grupo3.Datos;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using System.Text;
using static System.Text.CodePagesEncodingProvider;
using System.Net.Http;
using System.Collections.Immutable;
using static Examen2Grupo3.BuscarProducto;
using System.Text.Json;



namespace Examen2Grupo3
{
    public partial class GenerarPedido : Form
    {
        private Pedido pedido = new Pedido();//se almacenan distintos miembros del pedido en distintas funciones para despues guardarlos en una lista 
        private static int NumeroPedido;
        private Producto ProductoNuevo;
        List<Producto> listaProductos = new List<Producto>();
        private Usuarios usuarioActual = new Usuarios();
        int opcion = 0;
        private List<Producto> inventarioOriginal = new List<Producto>();
        public GenerarPedido(Usuarios usuarioActual)
        {
            InitializeComponent();
            dataGridView1.Columns["PrecioUnit"].DefaultCellStyle.Format = "C2";
            dataGridView1.Columns["Total"].DefaultCellStyle.Format = "C2";
            dataGridView1.Columns["PrecioUnit"].DefaultCellStyle.FormatProvider = new System.Globalization.CultureInfo("en-US");
            dataGridView1.Columns["Total"].DefaultCellStyle.FormatProvider = new System.Globalization.CultureInfo("en-US");
            CargarInventario("Inventario.json");
            Cliente.Text = "Venta Nº: " + CargarNum();//actualiza el numero del pedido conforme se generen mas
            this.usuarioActual = usuarioActual;//carga el usuario actual para registrarlo como encargado
            lblEncargado.Text += usuarioActual.Username;
            lblNombre.Text += usuarioActual.Nombre;
            lblID.Text += usuarioActual.ID.ToString();
        }
        private string CargarNum()
        {
            string rutaArchivo = "pedidos.json";
            if (File.Exists(rutaArchivo))
            {
                string jsonString = File.ReadAllText(rutaArchivo);
                var pedidos = JsonConvert.DeserializeObject<List<Pedido>>(jsonString);

                if (pedidos.Count > 0)
                {
                    NumeroPedido = pedidos.Max(p => p.ID) + 1; // Encuentra el mayor número de pedido  
                    return NumeroPedido.ToString("D6");
                }
                NumeroPedido = 1;
                return NumeroPedido.ToString("D6");
            }
            NumeroPedido = 1;
            return NumeroPedido.ToString("D6"); // Si no hay pedidos, empieza desde 1  
        }
        public void CargarInventario(string rutaArchivo)
        {
            if (File.Exists(rutaArchivo))
            {
                string json = File.ReadAllText(rutaArchivo);
                var listaProductos = JsonConvert.DeserializeObject<List<Producto>>(json) ?? new List<Producto>();

                if (listaProductos != null)
                {
                    // Guardar una copia del inventario original  
                    inventarioOriginal = listaProductos;
                }
            }
        }
        public void GuardarInventario(string rutaArchivo)
        {
            if (File.Exists(rutaArchivo))
            {
                // Corrected the syntax for SerializeObject and added proper Formatting.Indented  
                string json = JsonConvert.SerializeObject(inventarioOriginal, Formatting.Indented);
                File.WriteAllText(rutaArchivo, json);
            }
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

        public string? GuardarCliente()//Guarda los datos del Cliente
        {
            try
            {
                pedido.Cliente = new Cliente
                {
                    Nombre = guna2TextBox1.Text,
                    ID = int.Parse(guna2TextBox2.Text),
                 
                    Direccion = guna2TextBox4.Text,

                };
                return "";
            }
            catch
            {
                return null;//returna null si la operacion resulto fallida
            }

        }
        private void GuardarDatosEnJson(Pedido nuevoPedido)
        {
            string rutaArchivo = "pedidos.json";

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


        private void LimpiarTextBox()//si el usuario presiona el btn para limpiar todos los campos del cliente 
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
            string rutaArchivo = "pedidos.json";
            if (File.Exists(rutaArchivo))
            {
                string contenido = File.ReadAllText(rutaArchivo);
                return JsonConvert.DeserializeObject<List<Pedido>>(contenido) ?? new List<Pedido>();
            }
            return new List<Pedido>();
        }

        private void guna2Button5_Click(object sender, EventArgs e)//btn para limpiar la lista de pedidos
        {
            dataGridView1.Rows.Clear();
            lblSubtotal.Text = "Subtotal: ---";
            lblDescuento.Text = "Descuento: ---";
            lblTotal.Text = "Total: ---";
            GuardarInventario("Inventario.json");
            if (pedido.Productos != null)
                pedido.Productos.Clear(); // Limpiar la lista de productos del pedido actual

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            LimpiarTextBox();
        }
        private void guna2Button1_Click(object sender, EventArgs e)//btn para seleccion rapida de clientes
        {
            BuscarClientes frmClientes = new BuscarClientes();

            frmClientes.ClienteSeleccionado += RellenarTextBoxes;
            frmClientes.ShowDialog();
            // Suscribirse al evento y actualizar los TextBox cuando se seleccione un client


        }
        private void RellenarTextBoxes(Cliente Clientes)// rellena los campos al terminar la selecccion
        {
            guna2TextBox1.Text = Clientes.Nombre;
            guna2TextBox2.Text = Clientes.ID.ToString();
            guna2TextBox4.Text = Clientes.Direccion;
     
        }


        private void guna2Button6_Click(object sender, EventArgs e)
        {

        }

        private void btnagregarProductoClick(object sender, EventArgs e)//btn para agregar productos
        {

            BuscarProducto formProductos = new BuscarProducto();
            if (formProductos.ShowDialog() == DialogResult.OK)//si la operacion culmino de manera correcta, ejecutar el cuerpo
            {
                try
                {
                    ProductoNuevo = new Producto
                    {
                        Nombre = formProductos.Producto.Nombre,
                        ID = formProductos.Producto.ID,
                        Categoria = formProductos.Producto.Categoria,
                        PrecioUnitario = formProductos.Producto.PrecioUnitario,
                        Cantidad = formProductos.Producto.Cantidad,
                        Descripcion = formProductos.Producto.Descripcion
                    };
                    if (pedido.Productos == null)
                    {
                        pedido.Productos = new List<Producto>();

                    }
                    pedido.Estado = "Pendiente";//estado predeterminado

                    pedido.Productos.Add(ProductoNuevo); //codigo anterior  pedido.Productos = ListaProductos


                    Descuento();
                    dataGridView1.Rows.Add(ProductoNuevo.ID, ProductoNuevo.Nombre, ProductoNuevo.Categoria, ProductoNuevo.Descripcion, ProductoNuevo.Cantidad, ProductoNuevo.PrecioUnitario, ProductoNuevo.Cantidad * ProductoNuevo.PrecioUnitario);

                }
                catch
                {
                    MessageBox.Show("Error al ingresar los datos. Verifique que haya ingresado correctamente los campos requeridos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }
        public void Descuento()//en esta funcion se aplican los montos
        {
            var culture = new System.Globalization.CultureInfo("en-US"); // Cultura con símbolo "$"

            lblSubtotal.Text = "Subtotal: " + pedido.SubtTotal.ToString("C2", culture);
            var cantidad = pedido.Productos.Sum(p => p.Cantidad);
            if (cantidad > 3)
            {

                pedido.Descuento = pedido.SubtTotal * 0.31M;//Descuento del 31%
                pedido.Total = pedido.SubtTotal - pedido.Descuento;

                lblTotal.Text = "total: " + pedido.Total.ToString("C2", culture);
                lblDescuento.Text = "Descuento(20%): " + pedido.Descuento.ToString("C2", culture);

            }
            else
            {
                pedido.Total = pedido.SubtTotal;
                lblTotal.Text = "Total: " + pedido.SubtTotal.ToString("C2", culture);
            }


        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {

            if (pedido.Productos == null || GuardarCliente() == null)
            {
                MessageBox.Show("Faltan Datos por rellenar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            pedido.Encargado = usuarioActual;
            pedido.ID = NumeroPedido;
            pedido.Fecha = guna2DateTimePicker1.Value;
            GuardarDatosEnJson(pedido);
            dataGridView1.Rows.Clear();
            LimpiarTextBox();
            lblDescuento.Text = "Descuento: ";
            lblSubtotal.Text = "Subtotal: ";
            lblTotal.Text = "Total: ";
            Cliente.Text = "Venta Nº: " + CargarNum();

            MessageBox.Show("Venta generado exitosamente!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

            opcion++;
            if (pedido.Productos == null && moneda.bolivares == 0) return;

            if (opcion == 1 && moneda.bolivares != 0)//cambia de dolares a bs
            {
                foreach (var producto in pedido.Productos)
                {
                    producto.PrecioUnitario = producto.PrecioUnitario / moneda.bolivares; // Cambia aquí la lógica según lo que necesites
                }
            }

            if (opcion == 2 && moneda.bolivares != 0)
            {
                foreach (var producto in pedido.Productos)
                {
                    producto.PrecioUnitario = producto.PrecioUnitario * moneda.bolivares; // Cambia aquí la lógica según lo que necesites
                }
                opcion = 0;
            }


            // Refresca el DataGridView
            dataGridView1.Rows.Clear();
            foreach (var producto in pedido.Productos)
            {
                dataGridView1.Rows.Add(
                    producto.ID,
                    producto.Nombre,
                    producto.Categoria,
                    producto.Descripcion,
                    producto.Cantidad,
                    producto.PrecioUnitario,
                    producto.Cantidad * producto.PrecioUnitario
                );
            }

            // Si tienes cálculos de totales, actualízalos
            Descuento();
        }
        private int ObtenerIndice()
        {
            int indice=0;
            if (dataGridView1.CurrentRow != null)
            {
                 indice = dataGridView1.CurrentRow.Index;
                return indice;
                // Usar 'indice'
            }
            return 2;
           
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            int indice = ObtenerIndice();
            if (indice >= 0) 
            {
                try { dataGridView1.Rows.RemoveAt(indice); } catch { }
              
            }
        }
    }
}
