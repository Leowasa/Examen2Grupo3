using Newtonsoft.Json;
using static Examen2Grupo3.RegistroPedidos;
namespace Examen2Grupo3
{
    public partial class datosempresa : Form
    {
        public Empresa empresactual = new Empresa();
        public datosempresa(Usuarios UsuarioActual)
        {
            InitializeComponent();
            leerEmpresa();
            ControlUsuario(UsuarioActual);
        }

        public void ControlUsuario(Usuarios UsuarioActual)
        {
            if (UsuarioActual.Tipo == "Aprobador" || UsuarioActual.Tipo == "Registrador")
            {
                guna2Button1.Visible = false;

            }

        }
        public void leerEmpresa()
        {
            string rutaArchivo = "Empresa.Json";
            if (File.Exists(rutaArchivo))
            {
                string jsonString = File.ReadAllText(rutaArchivo);
                empresactual = JsonConvert.DeserializeObject<Empresa>(jsonString) ?? new Empresa();

                if (empresactual == null)
                {
                    MessageBox.Show("El archivo JSON contiene datos inválidos o está vacío. Asegúrese de que los datos sean correctos.");
                    empresactual = new Empresa(); // Crear una instancia vacía para evitar errores posteriores
                }
                else
                {
                    label2.Text ="Razon Social: "+ empresactual.RazonSocial;
                    label1.Text = "Numero de Telefono: "+empresactual.Numero?.ToString() ?? string.Empty;
                    label4.Text = "Direccion Fisica: "+empresactual.Direccion;
                    label3.Text = "Correo Electronico: "+empresactual.Correo;
                    label5.Text = "Website: "+empresactual.Website;
                }

            }
            else
            {
               // MessageBox.Show("Los campos de su empresa estan vacios. Asegurese de rellenarlos correctamente");
            }


        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            IngresarDatosEmpresa datos = new IngresarDatosEmpresa();

            datos.setDatos
            (
                label2.Text,
                label1.Text,
                label4.Text,
                label3.Text,
                label5.Text
            );

            datos.ShowDialog();
            if (datos.DialogResult == DialogResult.OK)
            {
                leerEmpresa();
            }
          
         
        }
    
    }
}
