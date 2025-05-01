using Newtonsoft.Json;
using static Examen2Grupo3.RegistroPedidos;

namespace Examen2Grupo3
{
    public partial class Codigo_especial : Form
    {
        public Empresa empresactual = new Empresa();
        string codigoIngresado;
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
    }
}
