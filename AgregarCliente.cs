using System.Runtime.Versioning;
using static Examen2Grupo3.RegistroPedidos;

namespace Examen2Grupo3
{
    [SupportedOSPlatform("windows6.1")]
    public partial class AgregarCliente : Form
    {
        public Cliente Datos = new Cliente();

        public AgregarCliente(int Opcion)//Opcion representa la operacion que hara el formulario
        {
            
            InitializeComponent();
            Configurar(Opcion);

        }
        private void Configurar(int Opcion)
        {
            switch(Opcion)
            {
                case 1: //Para Agregar Usuario  
                    this.Text = "Agregar Cliente";
                    guna2Button1.Text = "Agregar";
                    label3.Text = "Username: ";
                    label4.Text = "contraseña: ";
                    label5.Text = "Confirmar contraseña: ";
                    guna2ComboBox1.Items.Clear(); // Clear existing items before adding new ones
                    // Use AddRange to populate the Items collection instead of assigning directly  
                    guna2ComboBox1.Items.AddRange(new object[] { "Aprobador", "Registrador" });
                    break;
                case 2: //Para Agregar Cliente
                    this.Text = "Agregar Cliente";
                    guna2Button1.Text = "Agregar";
                    break;
            }
            
        }
        public Cliente ObtenerClienteEditado()
        {
            if (guna2ComboBox1.SelectedItem == null)
            {
                throw new InvalidOperationException("El tipo de cliente no puede ser nulo. Por favor, seleccione un tipo.");
            }

            Cliente clienteEditado = new Cliente
            {
                ID = int.Parse(guna2TextBox1.Text), // Ensure 'ID' is a property of 'cliente' and not a Label
                Nombre = guna2TextBox2.Text,
                Direccion = guna2TextBox4.Text,
                Correo = guna2TextBox3.Text,
                Tipo = guna2ComboBox1.SelectedItem?.ToString() ?? string.Empty // Safeguard against null
            };
            return clienteEditado;
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

            }
        }
        public void SetDatosUsuarios(string ID, string Nombre, string Username, string Password, string Tipo)
        {
            try
            {
                guna2TextBox1.Text = ID;
                guna2TextBox2.Text = Nombre;
                guna2TextBox4.Text =Username;
                guna2TextBox3.Text = Password;
                guna2ComboBox1.Text = Tipo;
            }
            catch
            {

            }
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            
            try
            {
                Datos.ID = int.Parse(guna2TextBox1.Text);
                Datos.Nombre = guna2TextBox2.Text;
                Datos.Direccion = guna2TextBox3.Text;
                Datos.Correo = guna2TextBox4.Text;

                if (guna2ComboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Por favor seleccione un tipo de cliente.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Datos.Tipo = guna2ComboBox1.SelectedItem?.ToString() ?? string.Empty; // Safeguard against null
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
