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
                empresactual = JsonConvert.DeserializeObject<Empresa>(jsonString);

                if (empresactual == null)
                {
                    MessageBox.Show("El archivo JSON contiene datos inválidos o está vacío. Asegúrese de que los datos sean correctos.");
                    empresactual = new Empresa(); // Crear una instancia vacía para evitar errores posteriores
                }
                else
                {
                    label2.Text = empresactual.RazonSocial;
                    label1.Text = empresactual.Numero?.ToString() ?? string.Empty;
                    label4.Text = empresactual.Direccion;
                    label3.Text = empresactual.Correo;
                    label5.Text = empresactual.Website;
                }

            }
            else
            {
                MessageBox.Show("Los campos de su empresa estan vacios. Asegurese de rellenarlos correctamente");
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
                label2.Text = datos.datos.RazonSocial;
                label1.Text = datos.datos.Numero.ToString();
                label4.Text = datos.datos.Direccion;
                label3.Text = datos.datos.Correo;
                label5.Text = datos.datos.Website;
            }
            empresactual = datos.datos;
            GuardarEmpresa(empresactual);
        }
        public void GuardarEmpresa(Empresa empresactual)
        {
            string rutaarchivo = "Empresa.json";
            try
            {
                // Corrected the use of Newtonsoft.Json's SerializeObject method  
                string json = JsonConvert.SerializeObject(empresactual, Formatting.Indented);
                File.WriteAllText(rutaarchivo, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
