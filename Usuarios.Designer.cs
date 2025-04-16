namespace ejemplo
{
    partial class Usuarios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Usuarios));
            Cliente = new Label();
            dataGridView1 = new DataGridView();
            Cedula = new DataGridViewTextBoxColumn();
            Nombre = new DataGridViewTextBoxColumn();
            Usuariosd = new DataGridViewButtonColumn();
            Rol = new DataGridViewTextBoxColumn();
            textBox1 = new TextBox();
            guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // Cliente
            // 
            Cliente.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Cliente.BackColor = Color.Transparent;
            Cliente.Font = new Font("Century Gothic", 21.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
            Cliente.ForeColor = Color.Gainsboro;
            Cliente.Location = new Point(403, 9);
            Cliente.Name = "Cliente";
            Cliente.Size = new Size(382, 70);
            Cliente.TabIndex = 98;
            Cliente.Text = "Usuarios";
            Cliente.TextAlign = ContentAlignment.TopCenter;
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
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
            dataGridView1.ColumnHeadersHeight = 30;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Cedula, Nombre, Usuariosd, Rol });
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.GridColor = Color.SteelBlue;
            dataGridView1.Location = new Point(54, 345);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(45, 66, 91);
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.SteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.Size = new Size(989, 405);
            dataGridView1.TabIndex = 95;
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
            Usuariosd.HeaderText = "Usuario";
            Usuariosd.Name = "Usuariosd";
            Usuariosd.ReadOnly = true;
            // 
            // Rol
            // 
            Rol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Rol.HeaderText = "Rol";
            Rol.Name = "Rol";
            // 
            // textBox1
            // 
            textBox1.ForeColor = SystemColors.ActiveCaptionText;
            textBox1.Location = new Point(54, 316);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(150, 23);
            textBox1.TabIndex = 94;
            textBox1.Text = "Ingresar Nombre o ID";
            // 
            // guna2Button1
            // 
            guna2Button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            guna2Button1.BorderRadius = 8;
            guna2Button1.CustomizableEdges = customizableEdges1;
            guna2Button1.DisabledState.BorderColor = Color.DarkGray;
            guna2Button1.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button1.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2Button1.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button1.FillColor = SystemColors.HotTrack;
            guna2Button1.Font = new Font("Segoe UI", 9F);
            guna2Button1.ForeColor = Color.White;
            guna2Button1.Location = new Point(961, 316);
            guna2Button1.Name = "guna2Button1";
            guna2Button1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Button1.Size = new Size(82, 23);
            guna2Button1.TabIndex = 130;
            guna2Button1.Text = "Agregar";
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(913, 297);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(42, 42);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 153;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(865, 297);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(42, 42);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 152;
            pictureBox1.TabStop = false;
            // 
            // Usuarios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 66, 91);
            ClientSize = new Size(1094, 872);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(guna2Button1);
            Controls.Add(Cliente);
            Controls.Add(dataGridView1);
            Controls.Add(textBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Usuarios";
            Text = "Usuarios";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Cliente;
        private DataGridView dataGridView1;
        private TextBox textBox1;
        private DataGridViewTextBoxColumn Cedula;
        private DataGridViewTextBoxColumn Nombre;
        private DataGridViewButtonColumn Usuariosd;
        private DataGridViewTextBoxColumn Rol;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
    }
}