namespace ejemplo
{
    partial class Clientes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Clientes));
            dataGridView1 = new DataGridView();
            Cedula = new DataGridViewTextBoxColumn();
            Nombre = new DataGridViewTextBoxColumn();
            Usuariosd = new DataGridViewButtonColumn();
            Rol = new DataGridViewTextBoxColumn();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Cliente = new Label();
            guna2TextBox2 = new Guna.UI2.WinForms.Guna2TextBox();
            guna2Button2 = new Guna.UI2.WinForms.Guna2Button();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AccessibleRole = AccessibleRole.TitleBar;
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
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Cedula, Nombre, Usuariosd, Rol, Column1, Column2, Column3 });
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.GridColor = Color.SteelBlue;
            dataGridView1.Location = new Point(12, 345);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(45, 66, 91);
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.SteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.Size = new Size(941, 405);
            dataGridView1.TabIndex = 99;
            // 
            // Cedula
            // 
            Cedula.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Cedula.HeaderText = "ID";
            Cedula.Name = "Cedula";
            Cedula.ReadOnly = true;
            // 
            // Nombre
            // 
            Nombre.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Nombre.HeaderText = "Nombre";
            Nombre.Name = "Nombre";
            // 
            // Usuariosd
            // 
            Usuariosd.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Usuariosd.FlatStyle = FlatStyle.Flat;
            Usuariosd.HeaderText = "Correo Electronico";
            Usuariosd.Name = "Usuariosd";
            Usuariosd.ReadOnly = true;
            // 
            // Rol
            // 
            Rol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Rol.HeaderText = "Direccion";
            Rol.Name = "Rol";
            // 
            // Column1
            // 
            Column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column1.HeaderText = "Tipo de cliente ";
            Column1.Name = "Column1";
            // 
            // Column2
            // 
            Column2.HeaderText = "Editar";
            Column2.Name = "Column2";
            // 
            // Column3
            // 
            Column3.HeaderText = "Eliminar";
            Column3.Name = "Column3";
            // 
            // Cliente
            // 
            Cliente.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Cliente.BackColor = Color.Transparent;
            Cliente.Font = new Font("Century Gothic", 21.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
            Cliente.ForeColor = Color.Gainsboro;
            Cliente.Location = new Point(409, 9);
            Cliente.Name = "Cliente";
            Cliente.Size = new Size(234, 70);
            Cliente.TabIndex = 101;
            Cliente.Text = "Clientes";
            Cliente.TextAlign = ContentAlignment.TopCenter;
            // 
            // guna2TextBox2
            // 
            guna2TextBox2.BorderRadius = 6;
            guna2TextBox2.CustomizableEdges = customizableEdges1;
            guna2TextBox2.DefaultText = "Ingresar Nombre o ID...";
            guna2TextBox2.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            guna2TextBox2.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            guna2TextBox2.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            guna2TextBox2.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            guna2TextBox2.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            guna2TextBox2.Font = new Font("Segoe UI", 9F);
            guna2TextBox2.ForeColor = SystemColors.ControlDarkDark;
            guna2TextBox2.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            guna2TextBox2.Location = new Point(12, 316);
            guna2TextBox2.Name = "guna2TextBox2";
            guna2TextBox2.PlaceholderText = "";
            guna2TextBox2.SelectedText = "";
            guna2TextBox2.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2TextBox2.Size = new Size(139, 23);
            guna2TextBox2.TabIndex = 148;
            guna2TextBox2.TextChanged += guna2TextBox2_TextChanged;
            // 
            // guna2Button2
            // 
            guna2Button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            guna2Button2.BorderRadius = 8;
            guna2Button2.CustomizableEdges = customizableEdges3;
            guna2Button2.DisabledState.BorderColor = Color.DarkGray;
            guna2Button2.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button2.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2Button2.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button2.FillColor = SystemColors.HotTrack;
            guna2Button2.Font = new Font("Segoe UI", 9F);
            guna2Button2.ForeColor = Color.White;
            guna2Button2.Location = new Point(842, 316);
            guna2Button2.Name = "guna2Button2";
            guna2Button2.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2Button2.Size = new Size(111, 23);
            guna2Button2.TabIndex = 149;
            guna2Button2.Text = "Agregar";
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(746, 297);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(42, 42);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 150;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(794, 297);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(42, 42);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 151;
            pictureBox2.TabStop = false;
            // 
            // Clientes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 66, 91);
            ClientSize = new Size(965, 808);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(guna2Button2);
            Controls.Add(guna2TextBox2);
            Controls.Add(Cliente);
            Controls.Add(dataGridView1);
            Cursor = Cursors.IBeam;
            FormBorderStyle = FormBorderStyle.None;
            Name = "Clientes";
            Text = "Clientes";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private DataGridView dataGridView1;
        private Label Cliente;
        private DataGridViewTextBoxColumn Cedula;
        private DataGridViewTextBoxColumn Nombre;
        private DataGridViewButtonColumn Usuariosd;
        private DataGridViewTextBoxColumn Rol;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox2;
        private Guna.UI2.WinForms.Guna2Button guna2Button2;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
    }
}