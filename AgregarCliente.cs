using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using static Examen2Grupo3.Datos;

namespace Examen2Grupo3
{
    [SupportedOSPlatform("windows6.1")]
    public partial class AgregarCliente : Form
    {
        public Cliente DatosClientes;
        public Datos.Usuarios DatosUsuario;
        private List<Datos.Usuarios> Usuarios = new List<Datos.Usuarios>();
        private int opcion;

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int IParam);

        public AgregarCliente(int Opcion)//Opcion representa la operacion que hara el formulario
        {
            this.opcion = Opcion;
            InitializeComponent();
            Configurar();

        }
        private void Configurar()
        {
            switch (opcion)
            {
                case 1: //Para Agregar Usuario  
                    this.Text = "Agregar Usuario";
                    lbl1.Text = "ID: ";
                    label2.Text = "Nombre: ";
                    label3.Text = "Username: ";
                    label4.Text = "contraseña: ";
                    label5.Text = "Confirmar contraseña: ";

                    // Fix for the line causing CS1525 and CS1002 errors  
                    guna2TextBox4.PasswordChar = '*';
                    guna2ComboBox1.Items.Clear(); // Clear existing items before adding new ones
                    // Use AddRange to populate the Items collection instead of assigning directly  
                    guna2ComboBox1.Items.AddRange(new object[] { "Aprobador", "Registrador" });
                    break;
                case 2: //Para Agregar Cliente
                    this.Text = "Agregar Cliente";
                    label5.Visible = false;
                    guna2TextBox5.Visible = false;
                    guna2ComboBox1.Location = new Point(11, 274);
                    label6.Location = new Point(11, 254);
                    guna2Button1.Location = new Point(11, 314);
                    break;
            }

        }
        public Datos.Usuarios ObtenerUsuario()
        {
            DatosUsuario = new Datos.Usuarios();


            DatosUsuario.ID = int.Parse(guna2TextBox1.Text);
            DatosUsuario.Nombre = guna2TextBox2.Text;
            DatosUsuario.Username = guna2TextBox3.Text;
            DatosUsuario.Password = guna2TextBox4.Text;
            DatosUsuario.Tipo = guna2ComboBox1.Text;
            MessageBox.Show("Operación realizada satisfactoriamente. ", "Operación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return DatosUsuario;



        }
        public Datos.Cliente ObtenerCliente()
        {
            if (guna2ComboBox1.SelectedItem == null)
            {
                throw new InvalidOperationException("El tipo de cliente no puede ser nulo. Por favor, seleccione un tipo.");
            }
            try 
            {
                DatosClientes = new Cliente
                {
                    ID = int.Parse(guna2TextBox1.Text), // Ensure 'ID' is a property of 'cliente' and not a Label
                    Nombre = guna2TextBox2.Text,
                    Direccion = guna2TextBox4.Text,
                    Correo = guna2TextBox3.Text,
                    Tipo = guna2ComboBox1.SelectedItem?.ToString() ?? string.Empty // Safeguard against null

                };
                return DatosClientes;
            } catch 
            {
                throw new InvalidOperationException("El tipo de cliente no puede ser nulo. Por favor, seleccione un tipo.");
            }
            
        }
        public void SetDatosProducto(string ID, string Nombre, string Direccion, string Correo, string Tipo)
        {
            try
            {
                guna2TextBox1.Text = ID;
                guna2TextBox2.Text = Nombre;
                guna2TextBox4.Text = Direccion;
                guna2TextBox3.Text = Correo;
                guna2ComboBox1.Text = Tipo;
            }
            catch
            {
                MessageBox.Show("Error al cargar los datos del cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void SetDatosUsuarios(Datos.Usuarios Usuario)
        {
            try
            {
                guna2TextBox1.Text = Usuario.ID.ToString();
                guna2TextBox2.Text = Usuario.Nombre;
                guna2TextBox3.Text = Usuario.Username;
                guna2TextBox4.Text = Usuario.Password;
                guna2TextBox5.Text = Usuario.Password;
                guna2ComboBox1.Text = Usuario.Tipo;
            }
            catch
            {
                MessageBox.Show("Error al cargar los datos del usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox1.Text) || string.IsNullOrWhiteSpace(guna2TextBox2.Text) || string.IsNullOrWhiteSpace(guna2TextBox3.Text) || string.IsNullOrWhiteSpace(guna2TextBox4.Text))
            {
                MessageBox.Show("Por favor complete todos los campos.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            switch (opcion)
            {
                case 1: //Agregar Usuario
                    if (guna2TextBox5.Text != guna2TextBox4.Text)
                    {
                        MessageBox.Show("Las contraseñas no coinciden.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    break;
                case 2: //Agregar Cliente
                    ObtenerCliente();
                    break;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
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
