using Guna.UI2.WinForms;
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
using TheArtOfDevHtmlRenderer.Adapters;
using static Examen2Grupo3.RegistroPedidos;
using static System.Windows.Forms.Design.AxImporter;
using ejemplo;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace Examen2Grupo3
{
    public partial class Factura : Form
    {
        private static Pedido Pedido = new Pedido();//se almacenan distintos miembros del pedido en distintas funciones para despues guardarlos en una lista 
        private static List<Pedido> ListaPedidos = new List<Pedido>();
        private static int NumeroPedido;
        private int Opcion;
        private Pedido orden;
        private RegistroPedidos.Usuarios usuarioActual = new RegistroPedidos.Usuarios();
        public Factura(Pedido pedido, int opcion)
        {
            InitializeComponent();
             Opcion = opcion;
            ListaPedidos = LeerPedidos();
            Pedido = pedido;
            configurar();
           
        }
        public Factura()
        {
            InitializeComponent();
         
        }
        public Factura(Pedido pedido, int opcion, RegistroPedidos.Usuarios usuario)
        {
            usuarioActual = usuario;
            Opcion = opcion;
            InitializeComponent();
            ListaPedidos = LeerPedidos();
            Pedido = pedido;
            configurar();
           
        }
        
        private void configurar()
        {
            cargarEmpresa();
            orden = new Pedido();
            orden = Pedido;
            lblFactura.Text = "Orden de entrega";
            lblNumero.Text = "ID: " + orden.ID.ToString("D6");
            label5.Text = "Fecha: " + orden.Fecha.ToString("dd/MM/yyyy");
            if (orden.Cliente != null)
            {
                label9.Text = "Nombre: " + orden.Cliente.Nombre;
               
            }
            else
            {
                label9.Text = "Nombre: N/A";
            }
            label12.Text = "ID: " + orden.Cliente.ID.ToString();
            label11.Text = "Direccion: " + orden.Cliente.Direccion;
            label10.Text = "Correo Electronico" + orden.Cliente.Correo;

            switch (Opcion)
            {
                case 1:
                    lblFactura.Text = "Pedido";
                    guna2Button2.Text = "Volver";
                    guna2TextBox1.Text = orden.Observaciones;
                    guna2TextBox1.ReadOnly = true;
                    FechaValidacion.Enabled = false;
                    try 
                    {
                        lblEncargado.Text = Pedido.Encargado.Username;
                    } catch(System.NullReferenceException)
                    {
                        
                        MessageBox.Show("Error");
                    
                    }
                      
                    
                    
                    lblNombre.Text = Pedido.Encargado.Nombre.ToString();
                    lblID.Text = Pedido.Encargado.ID.ToString()??"";
                    break;
                case 2:
                    lblFactura.Text = "Orden de entrega";
                    guna2Button2.Text = "Generar orden";
                    lblEncargado.Text = usuarioActual.Username;
                    lblID.Text = usuarioActual.ID.ToString(); ;
                    lblNombre.Text = usuarioActual.Nombre;  
                    break;
            }
          

            foreach (var lista in Pedido.Productos)
            {
                dataGridView1.Rows.Add(lista.ID, lista.Nombre, lista.Categoria, lista.Descripcion, lista.Cantidad, lista.PrecioUnitario, lista.Cantidad * lista.PrecioUnitario);


            }
            label21.Text = "Subtotal: " + Pedido.SubtTotal.ToString();
            lblDescuento.Text = "Descuento: " + (Pedido.Descuento * 100M).ToString("0")+ "%";
            lblIVa.Text = "IVA: 21%";
            label20.Text = "Total: " + Pedido.Total.ToString();
        }
        private void cargarEmpresa()
        {
            if (!File.Exists("Empresa.json"))
            {
                string jsonString = File.ReadAllText("Empresa.json");
                var pedidos = JsonConvert.DeserializeObject<Empresa>(jsonString) ?? new Empresa();
                if (pedidos != null)
                {
                    label16.Text = "Razón Social: " + pedidos.RazonSocial;
                    label17.Text = "Número de teléfono de la empresa: " + pedidos.Direccion;
                    label14.Text = "Dirección Física de la empresa: " + pedidos.Direccion;
                    label15.Text = "Correo Electrónico de la empresa: " + pedidos.Correo;
                    label13.Text = "Página Web de la empresa: " + pedidos.Website;
                }

            }


        }
        private void Factura_Load(object sender, EventArgs e)
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
        private void GuardarPedidosEnJson(Pedido nuevoPedido)
        {
            string rutaArchivo = "pedidos.json";

            try
            {
                // Cargar los pedidos existentes
                List<Pedido> pedidosExistentes = LeerPedidos();
                foreach (var lista in pedidosExistentes) 
                {
                    if (lista.ID == nuevoPedido.ID) 
                    {
                       lista.FechaValidado = nuevoPedido.FechaValidado;
                        lista.Observaciones = nuevoPedido.Observaciones;
                    
                    }
                }

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
        private void GuardarOrdenEnJson(Pedido orden)
        {
            string rutaArchivo = "Ordenes.json";

            try
            {
                // Cargar los pedidos existentes
                List<Pedido> OrdenesExistentes = LeerOrdenes();

                // Agregar el nuevo pedido a la lista
                OrdenesExistentes.Add(orden);

                // Serializar la lista actualizada de pedidos
                var json = JsonConvert.SerializeObject(OrdenesExistentes, Formatting.Indented);

                // Escribir el JSON en el archivo
                File.WriteAllText(rutaArchivo, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //
        }
        private List<Pedido> LeerOrdenes()
        {
            string rutaArchivo = "Ordenes.json";
            if (File.Exists(rutaArchivo))
            {
                string contenido = File.ReadAllText(rutaArchivo);
                return JsonConvert.DeserializeObject<List<Pedido>>(contenido) ?? new List<Pedido>();
            }
            return new List<Pedido>();


        }
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Form1? principal = Application.OpenForms["Form1"] as Form1; // Use nullable reference type and safe casting  

            switch (Opcion)
            {
                case 2:
                    Pedido.Observaciones = guna2TextBox1.Text;
                    Pedido.FechaValidado = FechaValidacion.Value;
                    GuardarPedidosEnJson(Pedido);
                    GuardarOrdenEnJson(Pedido);
                    if (principal != null)
                    {
                        principal.AbrirFormularioEnPanel(new GenerarOrden(usuarioActual)); // Reemplaza con el formulario que desees abrir
                        MessageBox.Show("Operacion generada Satisfactoriamente");
                    }
                    break;
                case 1:
                    if (principal != null)
                    {
                        principal.AbrirFormularioEnPanel(new GenerarOrden(usuarioActual)); // Reemplaza con el formulario que desees abrir  
                    }
                    break;
                case 3:
                    if (principal != null)
                    {
                        principal.AbrirFormularioEnPanel(new Factura()); // Reemplaza con el formulario que desees abrir  
                    }
                    break;
            }
        }








        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string rutaHtml = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Factura.html");
            GenerarFacturaHTML(rutaHtml, Pedido);

            // Convertir el archivo HTML a PDF
            string rutaPdf = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Factura.pdf");
           // ConvertirHtmlAPdfConDinkToPdf(rutaHtml, rutaPdf);
        }




        

    public void GenerarFacturaHTML(string rutaArchivo, Pedido pedido)
        {
            try
            {
                // Cargar los datos de la empresa desde el archivo JSON
                string empresaJsonPath = "Empresa.json";
                Empresa empresa = new Empresa();
                if (File.Exists(empresaJsonPath))
                {
                    string jsonString = File.ReadAllText(empresaJsonPath);
                    empresa = JsonConvert.DeserializeObject<Empresa>(jsonString) ?? new Empresa();
                }
                else
                {
                    throw new FileNotFoundException("El archivo Empresa.json no se encontró.");
                }

                // Construir la tabla de productos
                StringBuilder tablaProductos = new StringBuilder();
                decimal total = 0;

                foreach (var producto in pedido.Productos)
                {
                    decimal totalProducto = producto.Cantidad * producto.PrecioUnitario;
                    total += totalProducto;

                    tablaProductos.AppendLine($@"
            <tr>
                <td>{producto.Nombre}</td>
                <td>{producto.Cantidad}</td>
                <td>${producto.PrecioUnitario}</td>
                <td>${totalProducto}</td>
            </tr>");
                }

                // Generar el contenido HTML
                string html = $@"
    <!DOCTYPE html>
    <html lang='es'>
    <head>
        <meta charset='UTF-8'>
        <title>Factura {pedido.ID}</title>
        <style>
            body {{ font-family: Arial, sans-serif; margin: 20px; padding: 20px; }}
            .factura {{ border: 1px solid #000; padding: 20px; width: 60%; margin: auto; }}
            .titulo {{ text-align: center; font-size: 24px; font-weight: bold; }}
            .info {{ margin-bottom: 10px; }}
            table {{ width: 100%; border-collapse: collapse; margin-top: 10px; }}
            th, td {{ border: 1px solid #000; padding: 8px; text-align: center; }}
            .total {{ text-align: right; font-weight: bold; margin-top: 10px; }}
        </style>
    </head>
    <body>
        <div class='factura'>
            <div class='titulo'>Factura #{pedido.ID}</div>
            <div class='info'>
                <p><strong>Empresa:</strong> {empresa.RazonSocial}</p>
                <p><strong>Dirección:</strong> {empresa.Direccion}</p>
                <p><strong>Contacto:</strong> {empresa.Correo}</p>
                <hr>
                <p><strong>Cliente:</strong> {pedido.Cliente.Nombre} (ID: {pedido.Cliente.ID})</p>
                <p><strong>Fecha:</strong> {pedido.Fecha.ToString("dd/MM/yyyy")}</p>
            </div>
            <table>
                <thead>
                    <tr>
                        <th>Producto</th>
                        <th>Cantidad</th>
                        <th>Precio Unitario</th>
                        <th>Total</th>
                    </tr>
                </thead>
                <tbody>
                    {tablaProductos}
                </tbody>
            </table>
            <p class='total'><strong>Total a pagar:</strong> ${total}</p>
        </div>
    </body>
    </html>";

                // Guardar el archivo HTML
                File.WriteAllText(rutaArchivo, html);

                MessageBox.Show("Factura generada exitosamente en formato HTML.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar la factura: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
