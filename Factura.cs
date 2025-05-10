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
        private static Pedido orden;
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
            dataGridView1.Rows.Clear();
            Pedido = pedido;
            configurar();
           
        }
        
        private void configurar()
        {
            cargarEmpresa();
            dataGridView1.Rows.Clear();
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
            label10.Text = "Correo Electronico: " + orden.Cliente.Correo;

            switch (Opcion)
            {
                case 1://ver pedido
                    lblFactura.Text = "Pedido";
                    guna2Button2.Text = "Volver";
                    guna2Button2.Location = new Point(19, 750);
                    guna2TextBox1.Text = orden.Observaciones;
                    lblEncargado.Text = "Encargado Del Pedido: " + orden.Encargado.Username;
                    lblID.Text = "ID: " + orden.Encargado.ID.ToString(); ;
                    lblNombre.Text = "Nombre: " + orden.Encargado.Nombre;
                    guna2TextBox1.Visible = false;
                    lblObservaciones.Visible = false;
                    FechaValidacion.Enabled = false;

                    try 
                    {
      
                    } catch(System.NullReferenceException)
                    {
                        
                        MessageBox.Show("Error");
                    
                    }
                    break;
                case 2://Generar Orden
                    lblFactura.Text = "Orden de entrega";
                    guna2Button2.Text = "Generar orden";
                    lblEncargado.Text = "Encargado de la Orden: "+usuarioActual.Username;
                    lblID.Text = "ID: "+usuarioActual.ID.ToString(); ;
                    lblNombre.Text ="Nombre: " +usuarioActual.Nombre;
                    orden.IVA = Pedido.Total * 0.21M;
                    orden.Total = orden.Total + Pedido.IVA;
                    lblIVa.Text = "IVA(21%): " + orden.IVA.ToString("F2");
                    break;
                case 3://ver orden
                    lblFactura.Text = "Orden de entrega";
                    guna2Button2.Text = "Volver";
                    guna2TextBox1.Text = orden.Observaciones;
                    lblEncargado.Text = "Encargado De la Orden: " + orden.Encargado.Username;
                    lblID.Text = "ID: " + orden.Encargado.ID.ToString(); ;
                    lblNombre.Text = "Nombre: " + orden.Encargado.Nombre;
                    lblIVa.Text = "IVA(21%): " + orden.IVA.ToString("F2");
                    guna2TextBox1.Enabled = true;
                    FechaValidacion.Enabled = false;
                    FechaValidacion.Value = orden.FechaValidado;

                    try
                    {

                    }
                    catch (System.NullReferenceException)
                    {

                        MessageBox.Show("Error");

                    }
                    break;
            }
          

            foreach (var lista in Pedido.Productos)
            {
                dataGridView1.Rows.Add(lista.ID, lista.Nombre, lista.Categoria, lista.Descripcion, lista.Cantidad, lista.PrecioUnitario, lista.Cantidad * lista.PrecioUnitario);


            }
            label21.Text = "Subtotal: " + orden.SubtTotal.ToString("F2");
            lblDescuento.Text = "Descuento: " + (orden.Descuento * 100M).ToString("0")+ "%";
         
            label20.Text = "Total: " + orden.Total.ToString("F2");
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
                case 2://Generar Orden

                    string fechaEntrega = label5.Text.Replace("Fecha: ", "").Trim();
                    if (FechaValidacion.Value.ToString("dd/MM/yyyy") == fechaEntrega)
                    {
                        MessageBox.Show("La fecha de validación no puede ser la misma que la fecha de entrega", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else if (string.IsNullOrWhiteSpace(guna2TextBox1.Text))
                    {
                        MessageBox.Show("El campo no puede estar vacío.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    orden.Observaciones = guna2TextBox1.Text;
                    orden.FechaValidado = FechaValidacion.Value;
                    GuardarPedidosEnJson(orden);
                    
                    GuardarOrdenEnJson(orden);
                    if (principal != null)
                    {
                        principal.AbrirFormularioEnPanel(new GenerarOrden(usuarioActual)); // Reemplaza con el formulario que desees abrir
                        MessageBox.Show("Operacion generada Satisfactoriamente");
                    }
                    break;
                case 1://Ver pedido
                    if (principal != null)
                    {
                        principal.AbrirFormularioEnPanel(new PedidosHistorial()); // Reemplaza con el formulario que desees abrir  
                    }
                    break;
                case 3://Ver Orden
                    if (principal != null)
                    {
                        principal.AbrirFormularioEnPanel(new OrdenesHistorial()); // Reemplaza con el formulario que desees abrir  
                    }
                    break;
            }
        }








        private void guna2Button1_Click(object sender, EventArgs e)
        {
         
        }




       



    }
}
