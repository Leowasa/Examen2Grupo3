using System.Runtime.InteropServices;
using Newtonsoft.Json;
using static Examen2Grupo3.Datos;

namespace Examen2Grupo3
{
    public partial class Codigo_especial : Form
    {
        private string codigoespecial;
        string codigoIngresado;
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int IParam);

        public Codigo_especial()
        {
            InitializeComponent();
        }
        private string LeerCodigo()
        {
            // Obtener la ruta de la carpeta "Data" en la raíz del proyecto
            string directorio = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Codigo");
            // Combinar la ruta del directorio con el nombre del archivo
            string rutaCompleta = Path.Combine(directorio, "CodigoEspecial.json");


            // Verificar si el archivo existe
            if (!File.Exists(rutaCompleta))
            {
                return ""; // Retornar una lista vacía si no existe
            }

            // Leer el contenido del archivo JSON
            string json = File.ReadAllText(rutaCompleta);

            // Deserializar el contenido JSON en una lista de usuarios
            return Newtonsoft.Json.JsonConvert.DeserializeObject<string>(json) ??"";
        }
        private void Codigo_especial_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            codigoespecial = LeerCodigo();
            codigoIngresado = guna2TextBox1.Text;
            if (codigoespecial == null)
            {
                MessageBox.Show("No hay codigo de autorizacion establecido. Por favor solicite al superusuario ingresar un codigo al programa");
                this.DialogResult = DialogResult.Cancel;
                this.Close();

            }
            else if (codigoIngresado == codigoespecial)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else if (codigoIngresado != codigoespecial)
            {
                MessageBox.Show("Codigo invalido. Intente nuevamente");
                return;
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
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

