namespace Examen2Grupo3
{
    partial class BuscarClientes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BuscarClientes));
            dataGridView1 = new DataGridView();
            ID = new DataGridViewTextBoxColumn();
            Nombre = new DataGridViewTextBoxColumn();
            Correo = new DataGridViewButtonColumn();
            Direccion = new DataGridViewTextBoxColumn();
            Tipo = new DataGridViewTextBoxColumn();
            guna2TextBox2 = new Guna.UI2.WinForms.Guna2TextBox();
            Cliente = new Label();
            guna2CustomGradientPanel1 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            btnMaximizar = new PictureBox();
            btnRestaurar = new PictureBox();
            bntMinimizar = new PictureBox();
            btnCerrar = new PictureBox();
            Btnrest = new PictureBox();
            Btnmaximi = new PictureBox();
            Btnmini = new PictureBox();
            btncerrarr = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            guna2CustomGradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnMaximizar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnRestaurar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bntMinimizar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnCerrar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Btnrest).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Btnmaximi).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Btnmini).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btncerrarr).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AccessibleRole = AccessibleRole.TitleBar;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.BackgroundColor = Color.FromArgb(45, 66, 91);
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.HotTrack;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeight = 40;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { ID, Nombre, Correo, Direccion, Tipo });
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.GridColor = Color.SteelBlue;
            dataGridView1.Location = new Point(12, 129);
            dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle2.BackColor = Color.FromArgb(45, 66, 91);
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.SteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.Size = new Size(780, 151);
            dataGridView1.TabIndex = 100;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            // 
            // ID
            // 
            ID.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ID.HeaderText = "ID";
            ID.Name = "ID";
            ID.ReadOnly = true;
            // 
            // Nombre
            // 
            Nombre.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Nombre.HeaderText = "Nombre";
            Nombre.Name = "Nombre";
            // 
            // Correo
            // 
            Correo.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Correo.FlatStyle = FlatStyle.Flat;
            Correo.HeaderText = "Correo Electronico";
            Correo.Name = "Correo";
            Correo.ReadOnly = true;
            // 
            // Direccion
            // 
            Direccion.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Direccion.HeaderText = "Direccion";
            Direccion.Name = "Direccion";
            // 
            // Tipo
            // 
            Tipo.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Tipo.HeaderText = "Tipo de cliente ";
            Tipo.Name = "Tipo";
            // 
            // guna2TextBox2
            // 
            guna2TextBox2.BackColor = Color.Transparent;
            guna2TextBox2.BorderRadius = 6;
            guna2TextBox2.CustomizableEdges = customizableEdges1;
            guna2TextBox2.DefaultText = "";
            guna2TextBox2.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            guna2TextBox2.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            guna2TextBox2.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            guna2TextBox2.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            guna2TextBox2.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            guna2TextBox2.Font = new Font("Segoe UI", 9F);
            guna2TextBox2.ForeColor = SystemColors.ControlDarkDark;
            guna2TextBox2.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            guna2TextBox2.Location = new Point(12, 100);
            guna2TextBox2.Name = "guna2TextBox2";
            guna2TextBox2.PlaceholderText = "Ingresar Nombre o ID...";
            guna2TextBox2.SelectedText = "";
            guna2TextBox2.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2TextBox2.Size = new Size(139, 23);
            guna2TextBox2.TabIndex = 149;
            guna2TextBox2.TextChanged += guna2TextBox2_TextChanged;
            // 
            // Cliente
            // 
            Cliente.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Cliente.BackColor = Color.Transparent;
            Cliente.Font = new Font("Microsoft Sans Serif", 21.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
            Cliente.ForeColor = Color.Gainsboro;
            Cliente.Location = new Point(164, 28);
            Cliente.Name = "Cliente";
            Cliente.Size = new Size(496, 58);
            Cliente.TabIndex = 150;
            Cliente.Text = "Escoja un Cliente";
            Cliente.TextAlign = ContentAlignment.TopCenter;
            // 
            // guna2CustomGradientPanel1
            // 
            guna2CustomGradientPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            guna2CustomGradientPanel1.BackColor = Color.Transparent;
            guna2CustomGradientPanel1.Controls.Add(pictureBox2);
            guna2CustomGradientPanel1.Controls.Add(pictureBox1);
            guna2CustomGradientPanel1.Controls.Add(btnMaximizar);
            guna2CustomGradientPanel1.Controls.Add(btnRestaurar);
            guna2CustomGradientPanel1.Controls.Add(bntMinimizar);
            guna2CustomGradientPanel1.Controls.Add(btnCerrar);
            guna2CustomGradientPanel1.Controls.Add(Btnrest);
            guna2CustomGradientPanel1.Controls.Add(Btnmaximi);
            guna2CustomGradientPanel1.Controls.Add(Btnmini);
            guna2CustomGradientPanel1.Controls.Add(btncerrarr);
            guna2CustomGradientPanel1.CustomizableEdges = customizableEdges3;
            guna2CustomGradientPanel1.Dock = DockStyle.Top;
            guna2CustomGradientPanel1.FillColor = Color.FromArgb(26, 32, 40);
            guna2CustomGradientPanel1.FillColor2 = SystemColors.HotTrack;
            guna2CustomGradientPanel1.FillColor3 = Color.FromArgb(0, 80, 200);
            guna2CustomGradientPanel1.FillColor4 = Color.FromArgb(0, 80, 200);
            guna2CustomGradientPanel1.Location = new Point(0, 0);
            guna2CustomGradientPanel1.Name = "guna2CustomGradientPanel1";
            guna2CustomGradientPanel1.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2CustomGradientPanel1.Size = new Size(804, 25);
            guna2CustomGradientPanel1.TabIndex = 158;
            guna2CustomGradientPanel1.MouseDown += guna2CustomGradientPanel1_MouseDown;
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Cursor = Cursors.Hand;
            pictureBox2.ErrorImage = (Image)resources.GetObject("pictureBox2.ErrorImage");
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(775, 0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(26, 25);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 155;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Cursor = Cursors.Hand;
            pictureBox1.ErrorImage = (Image)resources.GetObject("pictureBox1.ErrorImage");
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(804, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(26, 25);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 154;
            pictureBox1.TabStop = false;
            // 
            // btnMaximizar
            // 
            btnMaximizar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMaximizar.BackColor = Color.Transparent;
            btnMaximizar.Cursor = Cursors.Hand;
            btnMaximizar.ErrorImage = (Image)resources.GetObject("btnMaximizar.ErrorImage");
            btnMaximizar.Image = (Image)resources.GetObject("btnMaximizar.Image");
            btnMaximizar.Location = new Point(1762, 10);
            btnMaximizar.Name = "btnMaximizar";
            btnMaximizar.Size = new Size(25, 25);
            btnMaximizar.SizeMode = PictureBoxSizeMode.Zoom;
            btnMaximizar.TabIndex = 153;
            btnMaximizar.TabStop = false;
            // 
            // btnRestaurar
            // 
            btnRestaurar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRestaurar.BackColor = Color.Transparent;
            btnRestaurar.Cursor = Cursors.Hand;
            btnRestaurar.ErrorImage = (Image)resources.GetObject("btnRestaurar.ErrorImage");
            btnRestaurar.Image = (Image)resources.GetObject("btnRestaurar.Image");
            btnRestaurar.Location = new Point(1762, 10);
            btnRestaurar.Name = "btnRestaurar";
            btnRestaurar.Size = new Size(25, 25);
            btnRestaurar.SizeMode = PictureBoxSizeMode.Zoom;
            btnRestaurar.TabIndex = 151;
            btnRestaurar.TabStop = false;
            btnRestaurar.Visible = false;
            // 
            // bntMinimizar
            // 
            bntMinimizar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bntMinimizar.BackColor = Color.Transparent;
            bntMinimizar.Cursor = Cursors.Hand;
            bntMinimizar.ErrorImage = (Image)resources.GetObject("bntMinimizar.ErrorImage");
            bntMinimizar.Image = (Image)resources.GetObject("bntMinimizar.Image");
            bntMinimizar.Location = new Point(1731, 10);
            bntMinimizar.Name = "bntMinimizar";
            bntMinimizar.Size = new Size(25, 25);
            bntMinimizar.SizeMode = PictureBoxSizeMode.Zoom;
            bntMinimizar.TabIndex = 152;
            bntMinimizar.TabStop = false;
            // 
            // btnCerrar
            // 
            btnCerrar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCerrar.BackColor = Color.Transparent;
            btnCerrar.Cursor = Cursors.Hand;
            btnCerrar.ErrorImage = (Image)resources.GetObject("btnCerrar.ErrorImage");
            btnCerrar.Image = (Image)resources.GetObject("btnCerrar.Image");
            btnCerrar.Location = new Point(1793, 10);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(25, 25);
            btnCerrar.SizeMode = PictureBoxSizeMode.Zoom;
            btnCerrar.TabIndex = 150;
            btnCerrar.TabStop = false;
            // 
            // Btnrest
            // 
            Btnrest.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Btnrest.BackColor = Color.Transparent;
            Btnrest.Cursor = Cursors.Hand;
            Btnrest.ErrorImage = null;
            Btnrest.Location = new Point(2762, 7);
            Btnrest.Name = "Btnrest";
            Btnrest.Size = new Size(25, 25);
            Btnrest.SizeMode = PictureBoxSizeMode.Zoom;
            Btnrest.TabIndex = 149;
            Btnrest.TabStop = false;
            Btnrest.Visible = false;
            // 
            // Btnmaximi
            // 
            Btnmaximi.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Btnmaximi.BackColor = Color.Transparent;
            Btnmaximi.Cursor = Cursors.Hand;
            Btnmaximi.ErrorImage = null;
            Btnmaximi.Location = new Point(2762, 7);
            Btnmaximi.Name = "Btnmaximi";
            Btnmaximi.Size = new Size(25, 25);
            Btnmaximi.SizeMode = PictureBoxSizeMode.Zoom;
            Btnmaximi.TabIndex = 147;
            Btnmaximi.TabStop = false;
            // 
            // Btnmini
            // 
            Btnmini.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Btnmini.BackColor = Color.Transparent;
            Btnmini.Cursor = Cursors.Hand;
            Btnmini.ErrorImage = null;
            Btnmini.Location = new Point(2731, 7);
            Btnmini.Name = "Btnmini";
            Btnmini.Size = new Size(25, 25);
            Btnmini.SizeMode = PictureBoxSizeMode.Zoom;
            Btnmini.TabIndex = 148;
            Btnmini.TabStop = false;
            // 
            // btncerrarr
            // 
            btncerrarr.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btncerrarr.BackColor = Color.Transparent;
            btncerrarr.Cursor = Cursors.Hand;
            btncerrarr.ErrorImage = null;
            btncerrarr.Location = new Point(2793, 7);
            btncerrarr.Name = "btncerrarr";
            btncerrarr.Size = new Size(25, 25);
            btncerrarr.SizeMode = PictureBoxSizeMode.Zoom;
            btncerrarr.TabIndex = 146;
            btncerrarr.TabStop = false;
            // 
            // BuscarClientes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Fondo;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(804, 292);
            Controls.Add(guna2CustomGradientPanel1);
            Controls.Add(Cliente);
            Controls.Add(guna2TextBox2);
            Controls.Add(dataGridView1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "BuscarClientes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "BuscarClientes";
            Load += BuscarClientes_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            guna2CustomGradientPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnMaximizar).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnRestaurar).EndInit();
            ((System.ComponentModel.ISupportInitialize)bntMinimizar).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnCerrar).EndInit();
            ((System.ComponentModel.ISupportInitialize)Btnrest).EndInit();
            ((System.ComponentModel.ISupportInitialize)Btnmaximi).EndInit();
            ((System.ComponentModel.ISupportInitialize)Btnmini).EndInit();
            ((System.ComponentModel.ISupportInitialize)btncerrarr).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn Nombre;
        private DataGridViewButtonColumn CorreoElectronico;
        private DataGridViewTextBoxColumn Direccion;
        private DataGridViewTextBoxColumn Tipo;
        private DataGridViewImageColumn Editar;
        private DataGridViewImageColumn Eliminar;
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox2;
        private Label Cliente;
        public DataGridView dataGridView1;
        private DataGridViewButtonColumn Correo;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel guna2CustomGradientPanel1;
        private PictureBox pictureBox1;
        private PictureBox btnMaximizar;
        private PictureBox btnRestaurar;
        private PictureBox bntMinimizar;
        private PictureBox btnCerrar;
        private PictureBox Btnrest;
        private PictureBox Btnmaximi;
        private PictureBox Btnmini;
        private PictureBox btncerrarr;
        private PictureBox pictureBox2;
    }
}