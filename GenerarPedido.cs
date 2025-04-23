using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using static Examen2Grupo3.GenerarPedido;
using static Examen2Grupo3.RegistroPedidos;
using static Examen2Grupo3.PedidoManager;
using System.Text.Json; // Agregar esta directiva para usar JsonSerializerOptions


namespace Examen2Grupo3
{
    public partial class GenerarPedido : Form
    {
        private Cliente clienteActual;
        private Pedido pedido = new Pedido();
        private static List<Pedido> Lista = new List<Pedido>();
        private static int NumeroPedido = 1;
        public GenerarPedido()
        {
            InitializeComponent();
            label14.Text = NumeroPedido.ToString("D6");
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
                    pedido.Productos.Add(ProductoNuevo); //codigo anterior  pedido.Productos = ListaProductos
                    label18.Text += ProductoNuevo.Total;
                    dataGridView1.Rows.Add(ProductoNuevo.ID, ProductoNuevo.Nombre, ProductoNuevo.Categoria, ProductoNuevo.Descripcion, ProductoNuevo.Cantidad, ProductoNuevo.PrecioUnitario, ProductoNuevo.Cantidad * ProductoNuevo.PrecioUnitario);

                }
                catch
                {
                    MessageBox.Show("Error al ingresar los datos. Verifique que haya ingresado correctamente los datos");
                }

            }
        }

        public string? GuardarCliente()
        {
            try
            {
                pedido.Cliente = new Cliente
                {
                    ID = int.Parse(guna2TextBox1.Text),
                    Nombre = guna2TextBox2.Text,
                    Direccion = guna2TextBox3.Text,
                    Correo = guna2TextBox4.Text
                };
                pedido.Cliente = pedido.Cliente;
                return "";
            }
            catch
            {
                MessageBox.Show("error crack");
                return null;
            }

        }
        private void GuardarDatosEnJson(List<Pedido> pedido)
        {
            string rutaArchivo = "datos.json";
            string json = JsonSerializer.Serialize(pedido, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(rutaArchivo, json);
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
            Lista.Add(pedido);
            GuardarDatosEnJson(Lista);
            NumeroPedido++;
            dataGridView1.Rows.Clear();
            label14.Text = NumeroPedido.ToString("D6");
            
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

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }
    }
}
//    dataGridView1.Rows.Clear();
/*  public partial class Form1 : Form
{
    private List<Persona> listaPersonas = new List<Persona>();

    public Form1()
    {
        InitializeComponent();
        CargarDatosDesdeJson(); // Cargar datos previos al iniciar
    }

    private void btnAbrirFormulario_Click(object sender, EventArgs e)
    {
        Form2 form2 = new Form2();
        if (form2.ShowDialog() == DialogResult.OK)
        {
            Persona nuevaPersona = new Persona { Nombre = form2.Nombre, Edad = form2.Edad };
            listaPersonas.Add(nuevaPersona);
            dataGridView1.Rows.Add(nuevaPersona.Nombre, nuevaPersona.Edad);

            GuardarDatosEnJson(); // Guardar datos actualizados
        }
    }

    private void GuardarDatosEnJson()
    {
        string rutaArchivo = "datos.json";
        string json = JsonSerializer.Serialize(listaPersonas, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(rutaArchivo, json);
    }

    private void CargarDatosDesdeJson()
    {
        string rutaArchivo = "datos.json";
        if (File.Exists(rutaArchivo))
        {
            string json = File.ReadAllText(rutaArchivo);
            listaPersonas = JsonSerializer.Deserialize<List<Persona>>(json);

            foreach (var persona in listaPersonas)
            {
                dataGridView1.Rows.Add(persona.Nombre, persona.Edad);
            }
        }
    }
}

public class Persona
{
    public string Nombre { get; set; }
    public int Edad { get; set; }
}






using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

public partial class Form3 : Form
{
    private List<Persona> listaPersonas = new List<Persona>();

    public Form3()
    {
        InitializeComponent();
        CargarDatosDesdeJson();
    }

    private void CargarDatosDesdeJson()
    {
        string rutaArchivo = "datos.json";
        if (File.Exists(rutaArchivo))
        {
            string json = File.ReadAllText(rutaArchivo);
            listaPersonas = JsonSerializer.Deserialize<List<Persona>>(json);

            foreach (var persona in listaPersonas)
            {
                dataGridView2.Rows.Add(persona.Nombre, persona.Edad);
            }
        }
    }
}

*/

/* if (pedido == null || clienteActual == null)
            {
                MessageBox.Show("Faltan datos por rellenar en el pedido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }*/