using Newtonsoft.Json; // Agregar esta directiva para usar JsonSerializerOptions
using System.Drawing.Drawing2D;
using static Examen2Grupo3.RegistroPedidos;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using System.Text;
using static System.Text.CodePagesEncodingProvider;
using System.Net.Http;
using System.Collections.Immutable;



namespace Examen2Grupo3
{
    public partial class GenerarPedido : Form
    {
        private static Pedido pedido = new Pedido();//se almacenan distintos miembros del pedido en distintas funciones para despues guardarlos en una lista 
        private List<Pedido> ListaPedidos = new List<Pedido>();
        private static int NumeroPedido;
        private Producto ProductoNuevo;
        private Usuarios usuarioActual = new Usuarios();
        //validar cuando se ingrese 0 como cantidad
        public GenerarPedido(Usuarios usuarioActual)
        {
            InitializeComponent();
            Cliente.Text = "Pedido Nº: " + CargarNum().ToString("D6");
            this.usuarioActual = usuarioActual;
            lblEncargado.Text += usuarioActual.Username;
            lblNombre.Text += usuarioActual.Nombre;
            lblID.Text += usuarioActual.ID.ToString();
        }
        private int CargarNum()
        {
            string rutaArchivo = "pedidos.json";
            if (File.Exists(rutaArchivo))
            {
                string jsonString = File.ReadAllText(rutaArchivo);
                var pedidos = JsonConvert.DeserializeObject<List<Pedido>>(jsonString);

                if (pedidos.Count > 0)
                {
                    return NumeroPedido = pedidos.Max(p => p.ID) + 1; // Encuentra el mayor número de pedido
                }
                return NumeroPedido= 1;
            }
            return NumeroPedido= 1; // Si no hay pedidos, empieza desde 1
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
               
                return"";

            }
            catch
            {
                return null;
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



        // Guardar el pedido cuando el usuario presione el botón
        private void guna2Button3_Click_1(object sender, EventArgs e)
        {
            if (pedido.Productos == null || GuardarCliente() == null)
            {
                MessageBox.Show("Faltan Datos por rellenar");
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
            Cliente.Text = "Pedido Nº: " + CargarNum().ToString("D6");
            
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
            string rutaArchivo = "pedidos.json";
            if (File.Exists(rutaArchivo))
            {
                string contenido = File.ReadAllText(rutaArchivo);
                return JsonConvert.DeserializeObject<List<Pedido>>(contenido) ?? new List<Pedido>();
            }
            return new List<Pedido>();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            lblSubtotal.Text = "Subtotal: ";
            lblDescuento.Text = "Descuento: ";
            lblTotal.Text = "Total: ";
            pedido.Productos.Clear(); // Limpiar la lista de productos del pedido actual
            ;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            LimpiarTextBox();
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            BuscarClientes frmClientes = new BuscarClientes();

            frmClientes.ClienteSeleccionado += RellenarTextBoxes;
            frmClientes.ShowDialog();
            // Suscribirse al evento y actualizar los TextBox cuando se seleccione un client


        }
        private void RellenarTextBoxes(Cliente Clientes)
        {
            guna2TextBox1.Text = Clientes.Nombre;
            guna2TextBox2.Text = Clientes.ID.ToString();
            guna2TextBox4.Text = Clientes.Direccion;
            guna2TextBox3.Text = Clientes.Correo;
        }


        private void guna2Button6_Click(object sender, EventArgs e)
        {
     
        }
        private string rellenarHtml(string htmlContent)
        {
            htmlContent = htmlContent.Replace("@Razon", "si");
            htmlContent = htmlContent.Replace("@Telefono", "042655");
            htmlContent = htmlContent.Replace("@Direccion", "por ahi");
            htmlContent = htmlContent.Replace("@Correo", "si");
            htmlContent = htmlContent.Replace("@Website", "si");

            string filas = string.Empty;
            foreach (var row in pedido.Productos)
            {
                filas += "<tr>";
                filas += "<td>" + row.ID + "</td>";
                filas += "<td>" + row.Nombre + "</td>";
                filas += "<td>" + row.Descripcion + "</td>";
                filas += "<td>" + row.Categoria + "</td>";
                filas += "<td>" + row.Cantidad + "</td>";
                filas += "<td>" + row.PrecioUnitario + "</td>";
                filas += "<td>" + row.Total + "</td>";
                filas += "</tr>";
            }
            htmlContent = htmlContent.Replace("@FILAS", filas);
            return htmlContent;

        }

        private void btnagregarProductoClick(object sender, EventArgs e)
        {

            BuscarProducto formProductos = new BuscarProducto();
            if (formProductos.ShowDialog() == DialogResult.OK)
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
                    //  listaProductos.Add(ProductoNuevo);
                    if (pedido.Productos == null)
                    {
                        pedido.Productos = new List<Producto>();

                    }
                    pedido.Estado = "Pendiente";
                
                    pedido.Productos.Add(ProductoNuevo); //codigo anterior  pedido.Productos = ListaProductos
        

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
            lblSubtotal.Text ="Subtotal: " +pedido.SubtTotal.ToString();
            var cantidad  = pedido.Productos.Sum(p => p.Cantidad);
            if (cantidad > 3)
            {
                decimal descuento = 0.20m;
                descuento = pedido.SubtTotal * descuento;
                pedido.Total = pedido.SubtTotal - descuento;

                lblTotal.Text = "total: "+ pedido.Total.ToString();
                lblDescuento.Text ="Descuento: "+ "20%";

            }
            else
            {
                pedido.Total = pedido.SubtTotal;
                lblTotal.Text ="Total: "+ pedido.SubtTotal.ToString();
            }


        }
    }
}
