using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Xml;

using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static Examen2Grupo3.Datos;
using static TheArtOfDevHtmlRenderer.Adapters.RGraphicsPath;


namespace Examen2Grupo3
{
    public partial class IngresarDatosEmpresa : Form
    {
        public Empresa datos;
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int IParam);

        public IngresarDatosEmpresa()
        {
            InitializeComponent();
        }

        public void setDatos(string Razonsocial, string Numero, string DireccionFisica, string Correo, string Website)//si se edita la empresa
        {
            guna2TextBox1.Text = Razonsocial;
            guna2TextBox2.Text = DireccionFisica;
            guna2TextBox3.Text = Numero;
            guna2TextBox4.Text = Website;
            guna2TextBox5.Text = Correo;

        }

        public void ObtenerDatos()
        {
            try
            {


                //verifica si los campos estan vacios
                if (string.IsNullOrWhiteSpace(guna2TextBox1.Text) ||
                    string.IsNullOrWhiteSpace(guna2TextBox2.Text) ||
                    string.IsNullOrWhiteSpace(guna2TextBox3.Text) ||
                    string.IsNullOrWhiteSpace(guna2TextBox4.Text) ||
                    string.IsNullOrWhiteSpace(guna2TextBox5.Text))
                {
                    MessageBox.Show("Campos vacíos. Intente nuevamente","Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                datos = new Empresa
                {
                    RazonSocial = guna2TextBox1.Text,
                    Direccion = guna2TextBox2.Text,
                    Numero = guna2TextBox3.Text,
                    Correo = guna2TextBox5.Text,
                    Website = guna2TextBox4.Text,
                };

                MessageBox.Show("Operacion exitosa!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GuardarEmpresa(datos);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void GuardarEmpresa(Empresa empresactual)
        {
            string rutaarchivo = "Empresa.Json";
            try
            {
                // Corrected the use of Newtonsoft.Json's SerializeObject method  
                string json = JsonConvert.SerializeObject(empresactual, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(rutaarchivo, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            ObtenerDatos();
        }

        private void pictureBox3_Click(object sender, EventArgs e)//btn de cerrar
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void guna2CustomGradientPanel1_MouseDown(object sender, MouseEventArgs e)//para mover el formulario 
        {

            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void guna2TextBox3_KeyPress(object sender, KeyPressEventArgs e)// prohibe la entrada de caracteres al campo de Telefono
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Bloquea la entrada de caracteres no numéricos
            }

        }

        private void guna2TextBox3_Leave(object sender, EventArgs e)//si el usuario ingresa un numero invalido
        {
            if (!EsTelefonoValido(guna2TextBox3.Text))
            {
                MessageBox.Show("Número de teléfono inválido. Debe contener 10 dígitos.","Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                guna2TextBox3.Focus();
            }


        }
        // Método para validar el teléfono
        bool EsTelefonoValido(string telefono)
        {
            string patron = @"^\d{10}$"; // Ejemplo: 10 dígitos sin espacios
            return Regex.IsMatch(telefono, patron);
        }

        private void guna2TextBox4_Leave(object sender, EventArgs e)//si el usuario ingresa una direccion invalida
        {
            if (!ValidacionURL.EsURLValida(guna2TextBox4.Text))
            {
                MessageBox.Show("Ingrese una URL válida. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                guna2TextBox4.Focus();
            }
        }
        public class ValidacionURL
        {
            public static bool EsURLValida(string url)
            {
                string patron = @"^www\.[\da-z.-]+\.[a-z.]{2,6}([\/\w .-]*)*\/?$";
                return Regex.IsMatch(url, patron, RegexOptions.IgnoreCase);
            }
        }

        private void guna2TextBox5_Leave_1(object sender, EventArgs e)
        {
            // Expresión regular para validar correos electrónicos  
            string patronCorreo = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(guna2TextBox5.Text, patronCorreo))//validar correo
            {
                MessageBox.Show("El correo ingresado no es válido. Por favor, Verifique haber ingresado correctamente el campo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                guna2TextBox5.Focus();
            }
        }
    }
}
