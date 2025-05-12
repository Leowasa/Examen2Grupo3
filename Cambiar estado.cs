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
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace Examen2Grupo3
{
    public partial class Cambiar_estado : Form
    {
        Pedido Ordenes;
        Datos.Usuarios UsuarioActual = new Datos.Usuarios();
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int IParam);

        public Cambiar_estado(Pedido Orden, Datos.Usuarios usuarioActual)
        {
            Ordenes = new Pedido();
            this.Ordenes = Orden;
            InitializeComponent();
            UsuarioActual = usuarioActual;
            cargarestado();
        }
        public void AbrirOtroFormulario(Pedido seleccionadot)
        {
            Form1 principal = (Form1)Application.OpenForms["Form1"];
            if (principal != null)
            {

                principal.AbrirFormularioEnPanel(new Factura(seleccionadot, 2, UsuarioActual, guna2ComboBox1.SelectedItem.ToString())); // Reemplaza con el formulario que desees abrir

            }
            this.Close();
        }
        private void GuardarOrdenEnJson(Pedido orden)
        {
            string rutaArchivo = "Ordenes.json";

            try
            {
                // Cargar los pedidos existentes
                List<Pedido> OrdenesExistentes = LeerOrdenes();

                foreach (var lista in OrdenesExistentes)
                {
                    if (lista.ID == orden.ID)
                    {
                        lista.Estado = orden.Estado;

                    }
                }

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
        private void GuardarPedidoEnJson(Pedido orden)
        {
            string rutaArchivo = "pedidos.json";

            try
            {
                // Cargar los pedidos existentes
                List<Pedido> PedidosExistentes = LeerPedidos();

                foreach (var lista in PedidosExistentes)
                {
                    if (lista.ID == orden.ID)
                    {
                        lista.Estado = orden.Estado;
                        // Serializar la lista actualizada de pedidos
                        var json = JsonConvert.SerializeObject(PedidosExistentes, Formatting.Indented);

                        // Escribir el JSON en el archivo
                        File.WriteAllText(rutaArchivo, json);

                    }
                }

           
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
        private List<Pedido> LeerPedidos()
        {
            string rutaArchivo = "Ordenes.json";
            if (File.Exists(rutaArchivo))
            {
                string contenido = File.ReadAllText(rutaArchivo);
                return JsonConvert.DeserializeObject<List<Pedido>>(contenido) ?? new List<Pedido>();
            }
            return new List<Pedido>();


        }
        private void cargarestado()
        {
            List<string> nuevosEstados = new List<string>();
            // Cargar el estado actual de la orden en el ComboBox
            guna2ComboBox1.SelectedItem = Ordenes.Estado;
            switch (Ordenes.Estado)
            {
                case "Pendiente":
                    nuevosEstados = new List<string> { "Aprobado", "Rechazado" };
                    break;
                case "Aprobado":
                    nuevosEstados = new List<string> { "Entregado" };
                    break;
                case "Rechazado":
                    nuevosEstados = new List<string> { "Pendiente" };
                    break;
                case "Entregado":
                    nuevosEstados = new List<string> { "Entregado" }; // Solo se mantiene entregado
                    guna2ComboBox1.Enabled = false;
                    break;

            }
            guna2ComboBox1.Items.Clear();
            guna2ComboBox1.Items.AddRange(nuevosEstados.ToArray());

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {

                if (guna2ComboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Por favor escoja un estado valido");
                    return;

                }
                else if (guna2ComboBox1.SelectedItem == "Rechazado" || guna2ComboBox1.SelectedItem == "Entregado")
                {
                    Ordenes.Estado = guna2ComboBox1.SelectedItem.ToString();
                    GuardarOrdenEnJson(Ordenes);
                    GuardarPedidoEnJson(Ordenes);

                    this.Close();
                }
                else 
                {
                    Ordenes.Estado = guna2ComboBox1.SelectedItem.ToString(); 
                    AbrirOtroFormulario(Ordenes); 
                }


            }
            catch (Exception ex) { }

        }

        private void Cambiar_estado_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void guna2CustomGradientPanel1_MouseDown(object sender, MouseEventArgs e)
        {
           
        }

        private void guna2CustomGradientPanel1_MouseDown_1(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);

        }
    }
}
