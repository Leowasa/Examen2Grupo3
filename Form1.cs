using Examen2Grupo3;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace ejemplo
{
    [SupportedOSPlatform("windows6.1")]
    public partial class Form1 : Form
    {
        RegistroPedidos.Usuarios UsuarioActual = new RegistroPedidos.Usuarios();
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int IParam);

        public Form1(RegistroPedidos.Usuarios UsuarioActual)
        {
            InitializeComponent();
            // En el constructor de Form1 o en el diseñador
            PanelPrincipal.AutoScroll = true;
            this.UsuarioActual = UsuarioActual;
            customizemenu();
            lblRol.Text = UsuarioActual.Tipo;
            lblUsuario.Text = UsuarioActual.Username;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;
            // this.FormBorderStyle = FormBorderStyle.None; // Elimina el borde del formulario
            // this.Region = CrearRegionRedondeada(30);
        }
        public Form1()
        {
            InitializeComponent();
            customizemenu();
        }
        public void AbrirFormularioEnPanel(Form formulario)
        {
            PanelPrincipal.Controls.Clear();

            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;

            // Ajustar el tamaño del formulario al contenido
            formulario.AutoSize = true;
            formulario.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            PanelPrincipal.Controls.Add(formulario);
            formulario.Show();
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

        private void btncerrarr_Click(object sender, EventArgs e)
        {
            Application.Exit();
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



        private void guna2Button9_Click(object sender, EventArgs e)
        {
            customizemenu();
            showmenu(panelVentas);
        }


        //RESIZE METODO PARA REDIMENCIONAR/CAMBIAR TAMAÑO A FORMULARIO EN TIEMPO DE EJECUCION ----------------------------------------------------------
        private int tolerance = 12;
        private const int WM_NCHITTEST = 132;
        private const int HTBOTTOMRIGHT = 17;
        private Rectangle sizeGripRectangle;
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    var hitPoint = this.PointToClient(new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16));
                    if (sizeGripRectangle.Contains(hitPoint))
                        m.Result = new IntPtr(HTBOTTOMRIGHT);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        //----------------DIBUJAR RECTANGULO / EXCLUIR ESQUINA PANEL 
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            var region = new Region(new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));
            sizeGripRectangle = new Rectangle(this.ClientRectangle.Width - tolerance, this.ClientRectangle.Height - tolerance, tolerance, tolerance);
            region.Exclude(sizeGripRectangle);
            this.panel1.Region = region;
            this.panel1.Region = region;
            this.Invalidate();
        }
        //----------------COLOR Y GRIP DE RECTANGULO INFERIOR
        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush blueBrush = new SolidBrush(Color.FromArgb(244, 244, 244));
            e.Graphics.FillRectangle(blueBrush, sizeGripRectangle);
            base.OnPaint(e);
            ControlPaint.DrawSizeGrip(e.Graphics, Color.Transparent, sizeGripRectangle);
        }

        private void guna2Button15_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new GenerarPedido(UsuarioActual));
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            showmenu(panelhistorial);
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            customizemenu();
            AbrirFormulario(new Usuarios(this.UsuarioActual));
        }

        private void PanelPrincipal_Click(object sender, EventArgs e)
        {
            customizemenu();
            AbrirFormulario(new datosempresa(UsuarioActual));
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {

        }

        private void btnUsuarios_Click_1(object sender, EventArgs e)
        {

            customizemenu();
            AbrirFormulario(new Usuarios(this.UsuarioActual));
        }

        private void btnDistribuidora_Click(object sender, EventArgs e)
        {
            customizemenu();
            AbrirFormulario(new datosempresa(UsuarioActual));
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            customizemenu();
            AbrirFormulario(new Inventario(this.UsuarioActual));
        }

        private void btnClientes_Click_1(object sender, EventArgs e)
        {
            customizemenu();
            AbrirFormulario(new Clientes(this.UsuarioActual));
        }

        private void btnHistorial_Click_1(object sender, EventArgs e)
        {
            showmenu(panelhistorial);
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            showmenu(panelVentas);
        }

        private void btnPedidosHistorial_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new PedidosHistorial());
        }

        private void btnOrdenesHistorial_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new OrdenesHistorial());
        }

        private void btnGenerarPedido_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new GenerarPedido(UsuarioActual));
        }

        private void btnGeneraOrden_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new GenerarOrden(UsuarioActual));
        }

        private void btnVerPerfil_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new Verperfil(UsuarioActual));
        }

        private void btnFactura_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FacturasHistorial());
        }

        private void bntMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private Rectangle originalBounds; // Para guardar el tamaño y posición originales
        private bool isMaximized = false; // Para rastrear si el formulario está maximizado

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            if (!isMaximized)
            {
                originalBounds = this.Bounds; // Guarda el tamaño y posición originales
                isMaximized = true;
            }

            this.WindowState = FormWindowState.Normal; // Asegúrate de que no esté minimizado
            this.Bounds = Screen.FromHandle(this.Handle).WorkingArea; // Ajusta al área de trabajo de la pantalla
            btnMaximizar.Visible = false;
            btnRestaurar.Visible = true;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            if (isMaximized)
            {
                this.Bounds = originalBounds; // Restaura el tamaño y posición originales
                isMaximized = false;
            }

            btnRestaurar.Visible = false;
            btnMaximizar.Visible = true;
        }

        private void guna2CustomGradientPanel1_MouseDown_1(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);


        }

        private void PanelPrincipal_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
