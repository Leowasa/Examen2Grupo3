using System.Runtime.InteropServices;
using ejemplo;
using Newtonsoft.Json;
using static Examen2Grupo3.Datos;

namespace Examen2Grupo3
{
    public partial class OpcionesSuperUsuario : Form
    {
        public int opcion;
        private static Empresa empresactual = new Empresa();
        List<Datos.Usuarios> lista = new List<Datos.Usuarios>();
        public Datos.Usuarios Usuarios { get; set; }


        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int IParam);

        public OpcionesSuperUsuario(int opcion, Datos.Usuarios usuarioActual)
        {
            Usuarios = usuarioActual;
            InitializeComponent();
            CargarCodigo();
            this.opcion = opcion;
        }

        public void CargarCodigo()
        {
            if (File.Exists("Empresa.json")) // Corrected file name
            {
                var json = File.ReadAllText("Empresa.json");
                Empresa? empresa = JsonConvert.DeserializeObject<Empresa>(json); // Fixed syntax and variable name

                if (empresa != null) // Check for null to avoid CS8602
                {
                    empresactual = empresa;
                    return; // Return the matched Empresa object           
                }
            } // Return null if no match is found or file doesn't exist
        }
        public void GuardarCodigo()
        {
            string rutaArchivo = "CodigoEspecial.json";
            string codigo = guna2TextBox1.Text;
            // Obtener la ruta de la carpeta "Data" en la raíz del proyecto
            string directorio = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Codigo");

            // Crear la carpeta "Data" si no existe
            if (!Directory.Exists(directorio))
            {
                Directory.CreateDirectory(directorio);
            }

            // Combinar la ruta del directorio con el nombre del archivo
            string rutaCompleta = Path.Combine(directorio, rutaArchivo);

            // Guardar el archivo JSON en la ruta especificada
            string json = JsonConvert.SerializeObject(codigo, Formatting.Indented);
            File.WriteAllText(rutaCompleta, json);

        }
        private List<Datos.Usuarios> LeerUsuarios()
        {
            // Obtener la ruta de la carpeta "Data" en la raíz del proyecto
            string directorio = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Usuario");
            // Combinar la ruta del directorio con el nombre del archivo
            string rutaCompleta = Path.Combine(directorio, "usuarios.json");


            // Verificar si el archivo existe
            if (!File.Exists(rutaCompleta))
            {
                return new List<Datos.Usuarios>(); // Retornar una lista vacía si no existe
            }

            // Leer el contenido del archivo JSON
            string json = File.ReadAllText(rutaCompleta);

            // Deserializar el contenido JSON en una lista de usuarios
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Datos.Usuarios>>(json) ?? new List<Datos.Usuarios>();
        }


        public void GuardarUsuarios(string rutaArchivo)
        {
            // Obtener la ruta de la carpeta "Data" en la raíz del proyecto
            string directorio = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Usuario");

            // Crear la carpeta "Data" si no existe
            if (!Directory.Exists(directorio))
            {
                Directory.CreateDirectory(directorio);
            }
            List<Datos.Usuarios>? listaUsuarios = LeerUsuarios();
            if (listaUsuarios != null) // Check for null to avoid CS8602
            {
                foreach (var usuario in listaUsuarios)
                {
                    if (usuario.ID == Usuarios.ID) // Example condition
                    {
                        usuario.Password = guna2TextBox1.Text; // Update the password

                    }

                }
            }
            // Combinar la ruta del directorio con el nombre del archivo
            string rutaCompleta = Path.Combine(directorio, rutaArchivo);

            // Guardar el archivo JSON en la ruta especificada
            string json = JsonConvert.SerializeObject(listaUsuarios, Formatting.Indented);
            File.WriteAllText(rutaCompleta, json);
        }
        public void GuardarClave()
        {
            if (File.Exists("usuarios.json"))
            {
                var jsonContent = File.ReadAllText("usuarios.json"); // Renamed variable to avoid conflict
                List<Datos.Usuarios>? listaUsuarios = JsonConvert.DeserializeObject<List<Datos.Usuarios>>(jsonContent);

                if (listaUsuarios != null) // Check for null to avoid CS8602
                {
                    foreach (var usuario in listaUsuarios)
                    {
                        if (usuario.ID == Usuarios.ID) // Example condition
                        {
                            usuario.Password = guna2TextBox1.Text; // Update the password

                        }

                    }
                    var updatedJson = JsonConvert.SerializeObject(listaUsuarios, Formatting.Indented); // Renamed variable to avoid conflict
                    File.WriteAllText("usuarios.json", updatedJson);
                }
            }
        }
        private void LeerCodigo()
        {
            
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text != guna2TextBox2.Text || (!int.TryParse(guna2TextBox1.Text, out _)) || (!int.TryParse(guna2TextBox2.Text, out _)) || guna2TextBox1.Text == "" || guna2TextBox2.Text == "")
            {
                MessageBox.Show("Texto Erroneo o incompleto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string dato = guna2Button1.Text;
            switch (opcion)
            {
                case 0://Para guardar contrasenia del admin
                    GuardarClave();
                    MessageBox.Show("Operacion realizada exitosamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 1://Para guardar codigo especial
                    GuardarCodigo();
                    MessageBox.Show("Operacion realizada exitosamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
            this.Close();
        }

        private void lael1_Click(object sender, EventArgs e)
        {

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
