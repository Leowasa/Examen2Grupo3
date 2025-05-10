using Newtonsoft.Json;
namespace Examen2Grupo3;
using ejemplo;
using Examen2Grupo3.Properties;
using System;
using System.Runtime.InteropServices;
using ejemplo;

public partial class Login : Form
{
    List<RegistroPedidos.Usuarios> listaUsuarios = new List<RegistroPedidos.Usuarios>();

    public RegistroPedidos.Usuarios usuario = new RegistroPedidos.Usuarios();
    [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
    private extern static void ReleaseCapture();
    [DllImport("user32.DLL", EntryPoint = "SendMessage")]
    private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int IParam);
    public Login()
    {
        InitializeComponent();

    }
    private void primeraEjecucion()
    {
        string rutaArchivo = "configuracion.dat";
        IngresarDatosEmpresa Nuevo = new IngresarDatosEmpresa();
        if (File.Exists(rutaArchivo))
        {
            Validar();

        }
        else
        {

            if (Nuevo.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(rutaArchivo, "Ejecutado"); // Crear el archivo para futuras ejecuciones
                Validar();

            }
        }
    }
    private void guna2Button2_Click(object sender, EventArgs e)
    {

        primeraEjecucion();
    }
    private void ProcesarUsuario()
    {


    }
    public bool Validar()
    {
        cargarCargarUsuario();

        // Validar si los campos están vacíos
        if (string.IsNullOrWhiteSpace(guna2TextBox1.Text) || string.IsNullOrWhiteSpace(guna2TextBox3.Text))
        {
            MessageBox.Show("Por favor, complete todos los campos antes de iniciar sesión.", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        try
        {
            usuario.Username = guna2TextBox1.Text;
            usuario.Password = guna2TextBox3.Text;

            if (listaUsuarios != null)
            {
                bool usuarioEncontrado = false;
                foreach (var lista in listaUsuarios)
                {
                    if (lista.Username == usuario.Username && lista.Password == usuario.Password)
                    {

                        usuarioEncontrado = true;
                        personalizar(lista);
                        return true;
                    }
                }

                if (!usuarioEncontrado)
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Error de inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Error al cargar los usuarios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al iniciar sesión: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        return false;




    }
    public void cargarCargarUsuario()
    {
        string rutarchivo = "usuarios.json";
        if (File.Exists(rutarchivo))
        {
            string usuarios = File.ReadAllText(rutarchivo);
            try
            {
                listaUsuarios = JsonConvert.DeserializeObject<List<RegistroPedidos.Usuarios>>(usuarios);
            }
            catch
            {
                MessageBox.Show("error");
            }

        }

    }
    public void personalizar(RegistroPedidos.Usuarios usuarios)
    {
        switch (usuarios.Tipo)
        {
            case "Administrador":
                Form1 form1 = new Form1(usuarios);
                this.Hide();
                form1.Show();
                break;
            case "Aprobador":
                Form1 form2 = new Form1(usuarios);
                form2.btnGenerarPedido.Visible = false;
                this.Hide();
                // Assuming "Mibotn" is a control or property, it must be accessed correctly.
                // Replace "Mibotn" with the correct property or method name.
                form2.Show();
                break;
            case "Registrador":
                Form1 form3 = new Form1(usuarios);
                form3.btnGeneraOrden.Visible = false;
                this.Hide();
                form3.Show();
                break;
            case "SuperUsuario":
                SuperUsuario form = new SuperUsuario();
                this.Hide();
                form.Show();
                break;
        }
    }

    private void Login_Load(object sender, EventArgs e)
    {

    }

    private void pictureBox3_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void guna2CustomGradientPanel1_MouseDown(object sender, MouseEventArgs e)
    {
        ReleaseCapture();
        SendMessage(this.Handle, 0x112, 0xf012, 0);
    }
}
