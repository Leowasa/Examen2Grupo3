using System.Runtime.InteropServices;
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

        public void setDatos(string Razonsocial, string Numero, string DireccionFisica, string Correo, string Website)
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
                // Corrected the if condition to properly check for empty or whitespace values in all text boxes  
                if (string.IsNullOrWhiteSpace(guna2TextBox1.Text) ||
                    string.IsNullOrWhiteSpace(guna2TextBox2.Text) ||
                    string.IsNullOrWhiteSpace(guna2TextBox3.Text) ||
                    string.IsNullOrWhiteSpace(guna2TextBox4.Text) ||
                    string.IsNullOrWhiteSpace(guna2TextBox5.Text))
                {
                    MessageBox.Show("Campos vacíos. Intente nuevamente");
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

                MessageBox.Show("Operacion exitosa!");
                GuardarEmpresa(datos);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                // Added exception parameter to catch block for better debugging  
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void GuardarEmpresa(Empresa empresactual)
        {
            string rutaarchivo = "Empresa.json";
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void guna2CustomGradientPanel1_MouseDown(object sender, MouseEventArgs e)
        {

            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
