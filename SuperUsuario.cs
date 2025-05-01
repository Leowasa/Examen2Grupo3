namespace Examen2Grupo3
{
    public partial class SuperUsuario : Form
    {
        private int opcion;
        public SuperUsuario()
        {
            InitializeComponent();
        }

        private void SuperUsuario_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            opcion = 0;
            OpcionesSuperUsuario form2 = new OpcionesSuperUsuario(opcion);
            form2.Show();

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            opcion = 1;
            OpcionesSuperUsuario form1 = new OpcionesSuperUsuario(opcion);
            form1.label1.Text = "Cambiar Codigo Especial";
            form1.label2.Text = "Confirmar Codigo Especial";
            form1.Show();

        }
    }
}
