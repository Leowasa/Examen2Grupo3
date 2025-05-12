using System.Runtime.InteropServices;
using static Examen2Grupo3.Datos;

namespace Examen2Grupo3
{
    public partial class SuperUsuario : Form
    {
        private int opcion;//Si se quiere cambiar clave especial o contrasenia del admin
        private static Usuarios Admin = new Usuarios();


        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int IParam);

        public SuperUsuario()
        {
            InitializeComponent();
           Admin = cargarAdmin();//obtengo los datos del admin
        }

        private void SuperUsuario_Load(object sender, EventArgs e)
        {
          
        }

        private void guna2Button2_Click(object sender, EventArgs e)//para cambiar clave del admin
        {
            opcion = 0;
            OpcionesSuperUsuario form2 = new OpcionesSuperUsuario(opcion, Admin);
            form2.Show();

        }

        private void guna2Button1_Click(object sender, EventArgs e)//para cambiar codigo especial
        {
            opcion = 1;
            OpcionesSuperUsuario form1 = new OpcionesSuperUsuario(opcion, Admin);
            form1.label1.Text = "Cambiar Codigo Especial";
            form1.label2.Text = "Confirmar Codigo Especial";
            form1.Show();

        }
        private Datos.Usuarios cargarAdmin()
        {
            // Obtener la ruta de la carpeta "Usuario" en la raíz del proyecto
            string directorio = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Usuario");
            // Combinar la ruta del directorio con el nombre del archivo
            string rutaCompleta = Path.Combine(directorio, "usuarios.json");

            // Leer el contenido del archivo JSON
            string json = File.ReadAllText(rutaCompleta);

            // Deserializar el contenido JSON en una lista de usuarios
           List<Datos.Usuarios> usuarios = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Datos.Usuarios>>(json) ?? new List<Datos.Usuarios>();
           foreach (var lista in usuarios)
           {
               if (lista.Tipo == "Administrador")//buscar el admin y retornarlo
               {
                 return lista;                    
               }
           }
            return new Datos.Usuarios(); // Retornar un usuario vacío si no existe
        }


        private void pictureBox2_Click(object sender, EventArgs e)//btn cerrar
        {
            Application.Exit();
        }

        private void guna2CustomGradientPanel1_MouseDown(object sender, MouseEventArgs e)//para poder mover el programa por la pantalla
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
