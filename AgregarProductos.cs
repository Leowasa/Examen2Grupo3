using ejemplo;
using Guna.UI2.WinForms.Suite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Examen2Grupo3.Datos;
using static iTextSharp.tool.xml.html.HTML;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Examen2Grupo3
{

    public partial class Agregar_Productos : Form
    {
        public Producto producto { get; set; } = new Producto(); // Initialize to avoid nullability issues  
        public List<Producto> Productos = new List<Producto>();
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int IParam);

        public Agregar_Productos(List<Producto> productos)
        {
            Productos = productos; 
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                producto = new Producto();
                producto.ID = int.Parse(guna2TextBox1.Text);
                producto.Nombre = guna2TextBox2.Text;
                producto.Descripcion = guna2TextBox3.Text;
                producto.Categoria = guna2TextBox4.Text;
                producto.PrecioUnitario = decimal.Parse(guna2TextBox6.Text);
                producto.Cantidad = int.Parse(guna2TextBox5.Text);
                // Aquí puedes agregar una validación extra si el número debe estar en cierto rango
                if (producto.ID < 0 || producto.ID ==0)
                {
                    MessageBox.Show("El ID no puede ser negativo o igual a 0.");
                    return;
                }
                else
                {
                    // Validación normal de ID repetido
                    if (Productos.Any(c => c.ID == producto.ID))
                    {
                        MessageBox.Show("No pueden haber más de un ID idéntico.");
                        return;
                    }
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Campos Erroneos. Asegurese de haber ingresado correctamente los campos y vuelva a intentar", ex.Message);
                return;
            }
            catch (OverflowException ex)
            {
                MessageBox.Show("El número ingresado es demasiado grande o pequeño para el ID.", ex.Message);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Campos vacios. Verifique de haber LLenado todos los campos e intente nuevamente",ex.Message); // Captura la excepción del campo vacío
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public void SetDatosProducto(string id, string nombre, string categoria, string Cantidad, string descripcion, string precio)
        {
            try
            {
                guna2TextBox1.Text = id;
                guna2TextBox2.Text = nombre;
                guna2TextBox4.Text = categoria;
                guna2TextBox3.Text = descripcion;
                guna2TextBox5.Text = Cantidad;
                guna2TextBox6.Text = precio;
            }
            catch
            {
            }
        }

        public Producto ObtenerProductoEditado()
        {
            Producto productoEditado = new Producto
            {
                ID = int.Parse(guna2TextBox1.Text),
                Nombre = guna2TextBox2.Text,
                Categoria = guna2TextBox4.Text,
                Descripcion = guna2TextBox3.Text,
                PrecioUnitario = decimal.Parse(guna2TextBox6.Text),
                Cantidad = int.Parse(guna2TextBox5.Text)
            };
            return productoEditado;
        }

        private void Agregar_Productos_Load(object sender, EventArgs e)
        {
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void guna2TextBox6_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void guna2CustomGradientPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }


}


