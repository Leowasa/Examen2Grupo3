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
        private int Opcion;
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int IParam);

        public Agregar_Productos(List<Producto> productos, int opcion)
        {
            this.Opcion = opcion;
            Productos = productos;
            InitializeComponent();
        }
        public bool EsIdProductoUnico(int id, List<Producto> productos, int? idActual = null)
        {
            // Excluir el ID actual de la validación si se está editando
            return !productos.Any(p => p.ID == id && p.ID != idActual);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                int nuevoId = int.Parse(guna2TextBox1.Text); // Suponiendo que el ID se ingresa en `guna2TextBox1`
                //validacion para que el ID sea mayor o igual a 3
                if (guna2TextBox1.Text.Trim().Length < 3)
                {
                    MessageBox.Show("El ID no puede ser menor a 3.");
                    return;
                }
                else
                {
                    //si los campos son correctos, agregar
                    producto = new Producto();
                    producto.ID = int.Parse(guna2TextBox1.Text);
                    producto.Nombre = guna2TextBox2.Text;
                    producto.Descripcion = guna2TextBox3.Text;
                    producto.Categoria = guna2TextBox4.Text;
                    producto.PrecioUnitario = decimal.Parse(guna2TextBox6.Text);
                    producto.Cantidad = int.Parse(guna2TextBox5.Text);
                    // Validación normal de ID repetido (si se agrega)
                    if (!EsIdProductoUnico(nuevoId, Productos, producto?.ID))

                    {
                        MessageBox.Show("El ID ya existe. Por favor, elige otro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    MessageBox.Show("Operacion exitosa!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                MessageBox.Show("Campos vacios. Verifique de haber LLenado todos los campos e intente nuevamente", ex.Message); 
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public void SetDatosProducto(string id, string nombre, string categoria, string Cantidad, string descripcion, string precio)
        {
            //Rellenar los textboxes al editar un producto
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

        public Producto ObtenerProductoEditado()//Obtener los cambios
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

        private void pictureBox2_Click(object sender, EventArgs e)//para cerrar
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void guna2CustomGradientPanel1_MouseDown(object sender, MouseEventArgs e)//para mover el formulario por la pantalla
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void guna2TextBox1_KeyPress(object sender, KeyPressEventArgs e)//para prohibir al usuario ingresar caracteres en el ID
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Bloquea la entrada de caracteres no numéricos
            }
        }
    }


}


