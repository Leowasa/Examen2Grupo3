using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text.RegularExpressions;
using Guna.UI2.WinForms;
using static Examen2Grupo3.Datos;
using static TheArtOfDevHtmlRenderer.Adapters.RGraphicsPath;

namespace Examen2Grupo3
{
    [SupportedOSPlatform("windows6.1")]
    public partial class AgregarCliente : Form
    {
        public  Cliente DatosClientes;//datos por si se agregara un nuevo al json
        public  Datos.Usuarios DatosUsuario;
        private List<Cliente> Clientes = new List<Cliente>();//listas para cargar el json
        private List<Datos.Usuarios> Usuarios = new List<Datos.Usuarios>();
        private int opcion;
        public int IdOriginal { get; set; }//por si se edita un usuario o cliente

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]//DllImport para poder move el formulario por la pantalla
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int IParam);

        public AgregarCliente(int Opcion, List<Datos.Usuarios> usuarios)//Opcion representa la operacion que hara el formulario
        {
            this.Usuarios = usuarios;
            this.opcion = Opcion;
            InitializeComponent();
            Configurar();

        }
        public AgregarCliente(int Opcion, List<Datos.Cliente> clientes)
        {
            Clientes = clientes;
            this.opcion = Opcion;
            InitializeComponent();
            Configurar();

        }
        private void Configurar()
        {
            if (opcion == 1 || opcion == 3)//para agegar o editar un usuario
            {
                this.Text = "Agregar Usuario";
                lbl1.Text = "ID: ";
                label2.Text = "Nombre: ";
                label3.Text = "Username: ";
                label4.Text = "contraseña: ";
                label5.Text = "Confirmar contraseña: ";

            
                guna2TextBox4.PasswordChar = '*';//para ocultar la contraseña
                guna2ComboBox1.Items.Clear(); // limpia los items existentes antes de añadir nuevos
                                             
                guna2ComboBox1.Items.AddRange(new object[] { "Aprobador", "Registrador" });


            }
            else if (opcion == 2 || opcion == 4)//para agregar o editar un cliente
            {
                this.Text = "Agregar Cliente";
                label5.Visible = false;
                guna2TextBox5.Visible = false;
                guna2ComboBox1.Location = new Point(11, 274);
                label6.Location = new Point(11, 254);
                guna2Button1.Location = new Point(11, 314);

            }

        }
        public bool EsIdUsuarioUnico(int id, List<Datos.Usuarios> usuarios, int? idActual = null)
        {
            // Excluir el ID actual de la validación si se está editando
            return !usuarios.Any(u => u.ID == id && u.ID != idActual);
        }

        public bool ObtenerUsuario()
        {
            try
            {
                int nuevoId = int.Parse(guna2TextBox1.Text); // Suponiendo que el ID se ingresa en `guna2TextBox1`
                if (!EsIdUsuarioUnico(nuevoId, Usuarios, IdOriginal))
                {
                    MessageBox.Show("El ID ya existe. Por favor, elige otro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // validar si el numero ingresado es mayor o igual a 3
                if (guna2TextBox1.Text.Trim().Length < 3)
                {
                    MessageBox.Show("El ID no puede ser menor a 3. ");
                    return false;
                }
                //validar si el espacio del combobox esta vacio
                else if (string.IsNullOrWhiteSpace(guna2ComboBox1.Text) || guna2ComboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Por favor seleccione un valor en el ComboBox.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                //Verificar la contraseñas 
                else if (guna2TextBox4.Text != guna2TextBox5.Text)
                {
                    MessageBox.Show("Las contraseñas no coinciden.");
                    return false;

                }
                else
                {
                    DatosUsuario = new Datos.Usuarios();
                    DatosUsuario.ID = int.Parse(guna2TextBox1.Text);
                    DatosUsuario.Nombre = guna2TextBox2.Text;
                    DatosUsuario.Username = guna2TextBox3.Text;
                    DatosUsuario.Password = guna2TextBox4.Text;
                    DatosUsuario.Tipo = guna2ComboBox1.Text;
                }
            }
            catch (FormatException )
            {
                MessageBox.Show("Campos Erroneos. Asegurese de haber ingresado correctamente los campos y vuelva a intentar", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);// Captura la excepción de campos erroneos
                return false;
            }
            catch (OverflowException)
            {
                MessageBox.Show("El número ingresado es demasiado grande o pequeño para el ID.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);// Captura la excepción de un numero mayor al que puede soportar un int
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("Campos vacios. Verifique de haber LLenado todos los campos e intente nuevamente", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); // Captura la excepción de los demas campos vacíos
                return false;
            }

            MessageBox.Show("Operacion exitosa!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }

        public bool ObtenerCliente()//Agregar un cliente
        {

            try
            {

                // Expresión regular para validar correos electrónicos  
                string patronCorreo = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

                int nuevoId = int.Parse(guna2TextBox1.Text); // Suponiendo que el ID se ingresa en `guna2TextBox1`
                if (!EsIdUnico(nuevoId, Clientes, IdOriginal))
                {
                    MessageBox.Show("El ID ya existe. Por favor, eliga otro diferente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // validación extra para que el número este en cierto rango
                if (guna2TextBox1.Text.Trim().Length < 3)
                {
                    MessageBox.Show("El ID no puede ser menor a 3.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                //validar que el campo Tipo este lleno
                else if (string.IsNullOrWhiteSpace(guna2ComboBox1.Text) || guna2ComboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Por favor seleccione un valor en el ComboBox.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                //validar el correo ingresado
                else if (!Regex.IsMatch(guna2TextBox3.Text, patronCorreo))
                {
                    MessageBox.Show("El correo ingresado no es válido. Por favor, ingrese un correo válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                //los datos son correctos, guardar en DatosClientes
                else
                {

                    DatosClientes = new Cliente
                    {
                        ID = int.Parse(guna2TextBox1.Text), 
                        Nombre = guna2TextBox2.Text,
                        Direccion = guna2TextBox4.Text,
                        Correo = guna2TextBox3.Text,
                        Tipo = guna2ComboBox1.SelectedItem?.ToString() ?? string.Empty 

                    };
                }
            }
            catch (FormatException )//campos erroneos
            {
                MessageBox.Show("Campos Erroneos. Asegurese de haber ingresado correctamente los campos y vuelva a intentar", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            catch (OverflowException)//ID ingresado que exceda el tamaño soportado por el int
            {
                MessageBox.Show("El número ingresado es demasiado grande o pequeño para el ID.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            catch (Exception)//Demas campos en blanco
            {
                MessageBox.Show("Campos vacios. Verifique de haber LLenado todos los campos e intente nuevamente", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); // Captura la excepción del campo vacío
                return false;
            }

            MessageBox.Show("Operacion exitosa!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }
        public void SetDatosCiente(Datos.Cliente clientes)
        {
            //rellenar los campos al editar un cliente
            try
            {
                guna2TextBox1.Text = clientes.ID.ToString();
                guna2TextBox2.Text = clientes.Nombre;
                guna2TextBox4.Text = clientes.Direccion;
                guna2TextBox3.Text = clientes.Correo;
                guna2ComboBox1.Text = clientes.Tipo;
            }
            catch
            {
                MessageBox.Show("Error al cargar los datos del cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public bool EsIdUnico(int id, List<Cliente> clientes, int? idActual = null)
        {
            // Excluir el ID actual de la validación si se está editando
            return !clientes.Any(c => c.ID == id && c.ID != idActual);
        }

        public void SetDatosUsuarios(Datos.Usuarios Usuario)
        {
            try
            {
                //rellenar los campos al editar un usuario
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


        private void guna2Button1_Click_1(object sender, EventArgs e)//btn confirmar
        {
            switch (opcion)
            {
                case 1: //Agregar Usuario


                    if (Usuarios.Any(c => c.ID == int.Parse(guna2TextBox1.Text))) // Validación normal de ID repetido
                    {
                        MessageBox.Show("No pueden haber más de un ID idéntico.");
                        return;
                    }
                    else if (ObtenerUsuario() == true)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    break;
                case 2: //Agregar Cliente

                    if (Clientes.Any(c => c.ID == int.Parse(guna2TextBox1.Text))) // Validación normal de ID repetido
                    {
                        MessageBox.Show("No pueden haber más de un ID idéntico.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (ObtenerCliente())
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    break;
                case 3://Editar Usuario
                    if (ObtenerUsuario() == true)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    break;
                case 4://Editar Cliente
                    if (ObtenerCliente())
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }

                    break;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)//btn cerrar
        {

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void guna2CustomGradientPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);//Asegura poder mover el formulario por la pantalla
        }

        private void guna2TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Bloquea la entrada de caracteres no numéricos
            }
        }
    }
}
