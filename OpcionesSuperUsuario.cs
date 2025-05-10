using System.Runtime.InteropServices;
using Newtonsoft.Json;
using static Examen2Grupo3.RegistroPedidos;

namespace Examen2Grupo3
{
    public partial class OpcionesSuperUsuario : Form
    {
        public int opcion;
        private static Empresa empresactual = new Empresa();
        List<Usuarios> lista = new List<Usuarios>();
        public Usuarios Usuarios { get; set; }


        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int IParam);

        public OpcionesSuperUsuario(int opcion, Usuarios usuarioActual)
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
            try
            {
                empresactual.Codigo = guna2TextBox1.Text;
                string json = JsonConvert.SerializeObject(empresactual, Formatting.Indented);

                File.WriteAllText("Empresa.json", json);
            }
            catch
            {
                // Fixed the MessageBox.Show call by correcting the third argument
                MessageBox.Show("Error al guardar los cambios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void GuardarClave()
        {
            if (File.Exists("usuarios.json"))
            {
                var jsonContent = File.ReadAllText("usuarios.json"); // Renamed variable to avoid conflict
                List<Usuarios>? listaUsuarios = JsonConvert.DeserializeObject<List<Usuarios>>(jsonContent);

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
                case 0:
                    GuardarClave();
                    MessageBox.Show("Operacion realizada exitosamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 1:
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
