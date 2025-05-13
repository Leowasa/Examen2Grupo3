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

            this.opcion = opcion;
        }
        public void GuardarCodigo()
        {
            string rutaArchivo = "CodigoEspecial.json";
            string codigo = guna2TextBox1.Text;
            // Obtener la ruta de la carpeta "usuario" en la raíz del proyecto
            string directorio = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Codigo");

            // Crear la carpeta "Usuario" si no existe
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
            // Obtener la ruta de la carpeta "Usuario" en la raíz del proyecto
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


        public void GuardarUsuarios()
        {
            string rutaArchivo = "usuarios.json";
            // Obtener la ruta de la carpeta "Usuario" en la raíz del proyecto
            string directorio = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Usuario");

            // Crear la carpeta "Usuario" si no existe
            if (!Directory.Exists(directorio))
            {
                Directory.CreateDirectory(directorio);
            }
            List<Datos.Usuarios>? listaUsuarios = LeerUsuarios();
            if (listaUsuarios != null)
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
                case 0://Para guardar contraseña del admin
                    GuardarUsuarios();
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

        private void pictureBox2_Click(object sender, EventArgs e)//para cerrar 
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();

        }

        private void guna2CustomGradientPanel1_MouseDown(object sender, MouseEventArgs e)//para poder mover el programa
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void guna2TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Bloquea la entrada de caracteres no numéricos
            }
        }

        private void guna2TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Bloquea la entrada de caracteres no numéricos
            }
        }
    }
}
