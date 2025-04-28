using static Examen2Grupo3.RegistroPedidos;

namespace Examen2Grupo3
{
    public partial class IngresarDatosEmpresa : Form
    {
        public Empresa datos;
        public IngresarDatosEmpresa()
        {
            InitializeComponent();
        }

        public void setDatos(string Razonsocial, string Numero, string DireccionFisica, string Correo, string Website)
        {
            guna2TextBox1.Text = Razonsocial;
            guna2TextBox2.Text = DireccionFisica;
            guna2TextBox3.Text = Numero;
            guna2TextBox4.Text = Website;
            guna2TextBox5.Text = Correo;

        }

        public void ObtenerDatos()
        {

            try
            {
                datos = new Empresa
                {
                    RazonSocial = guna2TextBox1.Text,
                    Direccion = guna2TextBox2.Text,
                    Numero = guna2TextBox3.Text,
                    Correo = guna2TextBox5.Text,
                    Website = guna2TextBox4.Text,

                };
                MessageBox.Show("Cambios aplicados exitosamente!");
                this.DialogResult = DialogResult.OK;
                this.Close();


            }
            catch
            {
                MessageBox.Show("Campos vacios. Intente nuevamente");
            }

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            ObtenerDatos();
        }
    }
}
