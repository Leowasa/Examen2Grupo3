using Examen2Grupo3;
using System.Runtime.InteropServices;

namespace ejemplo
{
    public partial class Form1 : Form

    {
        public Guna.UI2.WinForms.Guna2Button guna2Button14;

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int IParam);
        public Form1()
        {
            InitializeComponent();
            customizemenu();
        }

        private void customizemenu()
        {
            panelhistorial.Visible = false;
            panelVentas.Visible = false;
        }
        private void hidemenu()
        {
            if (panelhistorial.Visible == true)
            {
                panelhistorial.Visible = false;
            }
            if (panelVentas.Visible == true)
            {
                panelVentas.Visible = false;
            }
        }
        private void showmenu(Panel submenu)
        {
            if (submenu.Visible == false)
            {
                hidemenu();
                submenu.Visible = true;
            }
            else submenu.Visible = false;
        }
        private void button10_Click(object sender, EventArgs e)
        {
            showmenu(panelhistorial);
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void btncerrarr_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panelhistorial_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            showmenu(panelhistorial);
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            showmenu(panelVentas);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            customizemenu();
            AbrirFormulario(new Usuarios());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            customizemenu();
            AbrirFormulario(new datosempresa());

        }

        private void button3_Click(object sender, EventArgs e)
        {
            customizemenu();
            AbrirFormulario(new Inventario());
        }

        private void button12_Click(object sender, EventArgs e)
        {
            customizemenu();
            AbrirFormulario(new Clientes());
        }
        public void prueba()
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Btnmaximi_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            Btnmaximi.Visible = false;
            Btnrest.Visible = true;

        }

        private void Btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Btnrest_Click_1(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Normal;
            Btnrest.Visible = false;
            Btnmaximi.Visible = true;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2CustomGradientPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void AbrirFormulario(object? formhija)
        {
            if (this.PanelPrincipal.Controls.Count > 0)
                this.PanelPrincipal.Controls.RemoveAt(0);
            Form? fh = formhija as Form;
            if (fh != null)
            {
                fh.TopLevel = false;
                fh.Dock = DockStyle.Fill;
                this.PanelPrincipal.Controls.Add(fh);
                this.PanelPrincipal.Tag = fh;
                fh.Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new PedidosHistorial());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new OrdenesHistorial());
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new GenerarPedido());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new GenerarOrden());
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new Verperfil());
        }

        private void guna2Button13_Click(object sender, EventArgs e)
        {
            customizemenu();
            AbrirFormulario(new Usuarios());
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            customizemenu();
            AbrirFormulario(new datosempresa());
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            customizemenu();
            AbrirFormulario(new Inventario());
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            customizemenu();
            AbrirFormulario(new Clientes());
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            customizemenu();
            showmenu(panelhistorial);

        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {

            AbrirFormulario(new PedidosHistorial());
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new OrdenesHistorial());
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FacturasHistorial());
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            customizemenu();
            showmenu(panelVentas);
        }

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new GenerarPedido());
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new GenerarOrden());
        }

        private void PanelPrincipal_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
