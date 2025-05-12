using System.Runtime.InteropServices;
using Newtonsoft.Json;
using static Examen2Grupo3.Datos;

namespace Examen2Grupo3
{
    public partial class Codigo_especial : Form
    {
        public Empresa empresactual = new Empresa();
        string codigoIngresado;
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int IParam);

        public Codigo_especial()
        {
            InitializeComponent();
        }

        private void Codigo_especial_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            leerEmpresa();
            codigoIngresado = guna2TextBox1.Text;
            if (empresactual.Codigo == null)
            {
                MessageBox.Show("No hay codigo de autorizacion establecido. Por favor solicite al superusuario ingresar un codigo al programa");
                this.DialogResult = DialogResult.Cancel;
                this.Close();

            }
            else if (codigoIngresado == empresactual.Codigo)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else if (codigoIngresado != empresactual.Codigo)
            {
                MessageBox.Show("Codigo invalido. Intente nuevamente");
                return;
            }

        }
        public void leerEmpresa()
        {
            string rutaArchivo = "Empresa.Json";
            if (File.Exists(rutaArchivo))
            {
                string jsonString = File.ReadAllText(rutaArchivo);
                empresactual = JsonConvert.DeserializeObject<Empresa>(jsonString);

            }
            else
            {
                MessageBox.Show("Los campos de su empresa estan vacios. Asegurese de rellenarlos correctamente");
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

