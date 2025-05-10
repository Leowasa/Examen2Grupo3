using System.Runtime.InteropServices;
using static Examen2Grupo3.RegistroPedidos;

namespace Examen2Grupo3
{
    public partial class SuperUsuario : Form
    {
        private int opcion;
        private static Usuarios Admin = new Usuarios();


        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int IParam);

        public SuperUsuario()
        {
            InitializeComponent();
            cargarAdmin();
        }

        private void SuperUsuario_Load(object sender, EventArgs e)
        {
            cargarAdmin();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            opcion = 0;
            OpcionesSuperUsuario form2 = new OpcionesSuperUsuario(opcion, Admin);
            form2.Show();

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            opcion = 1;
            OpcionesSuperUsuario form1 = new OpcionesSuperUsuario(opcion, Admin);
            form1.label1.Text = "Cambiar Codigo Especial";
            form1.label2.Text = "Confirmar Codigo Especial";
            form1.Show();

        }
        private void cargarAdmin()
        {
            string rutarchivo = "usuarios.json";
            if (File.Exists(rutarchivo))
            {
                string json = File.ReadAllText(rutarchivo);
                List<Usuarios>? listaUsuarios = System.Text.Json.JsonSerializer.Deserialize<List<Usuarios>>(json) ?? new List<Usuarios>();
                foreach (var lista in listaUsuarios)
                {
                    if (lista.Tipo == "Administrador")
                    {
                        Admin = lista;
                    }

                }



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
