namespace Examen2Grupo3
{
    partial class OrdenesHistorial
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
            Cliente = new Label();
            dataGridView1 = new DataGridView();
            guna2TextBox2 = new Guna.UI2.WinForms.Guna2TextBox();
            Cedula = new DataGridViewTextBoxColumn();
            Usuariosd = new DataGridViewButtonColumn();
            Nombre = new DataGridViewTextBoxColumn();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewImageColumn();
            Column5 = new DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // Cliente
            // 
            Cliente.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Cliente.BackColor = Color.Transparent;
            Cliente.Font = new Font("Century Gothic", 21.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
            Cliente.ForeColor = Color.Gainsboro;
            Cliente.Location = new Point(113, 9);
            Cliente.Name = "Cliente";
            Cliente.Size = new Size(785, 70);
            Cliente.TabIndex = 112;
            Cliente.Text = "Historial de Ordenes de Entrega";
            Cliente.TextAlign = ContentAlignment.TopCenter;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
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
            dataGridView1.ColumnHeadersHeight = 30;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Cedula, Usuariosd, Nombre, Column1, Column2, Column3, Column5 });
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.GridColor = Color.SteelBlue;
            dataGridView1.Location = new Point(12, 161);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(45, 66, 91);
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.SteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.Size = new Size(931, 385);
            dataGridView1.TabIndex = 111;
            // 
            // guna2TextBox2
            // 
            guna2TextBox2.BorderRadius = 6;
            guna2TextBox2.CustomizableEdges = customizableEdges1;
            guna2TextBox2.DefaultText = "Ingresar ID o Nombre del cliente...";
            guna2TextBox2.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            guna2TextBox2.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            guna2TextBox2.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            guna2TextBox2.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            guna2TextBox2.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            guna2TextBox2.Font = new Font("Segoe UI", 9F);
            guna2TextBox2.ForeColor = SystemColors.ControlDarkDark;
            guna2TextBox2.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            guna2TextBox2.Location = new Point(12, 132);
            guna2TextBox2.Name = "guna2TextBox2";
            guna2TextBox2.PlaceholderText = "";
            guna2TextBox2.SelectedText = "";
            guna2TextBox2.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2TextBox2.Size = new Size(202, 23);
            guna2TextBox2.TabIndex = 149;
            // 
            // Cedula
            // 
            Cedula.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Cedula.HeaderText = "Numero";
            Cedula.Name = "Cedula";
            Cedula.ReadOnly = true;
            // 
            // Usuariosd
            // 
            Usuariosd.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Usuariosd.FlatStyle = FlatStyle.Flat;
            Usuariosd.HeaderText = "Cliente";
            Usuariosd.Name = "Usuariosd";
            Usuariosd.ReadOnly = true;
            // 
            // Nombre
            // 
            Nombre.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Nombre.HeaderText = "Fecha de creacion";
            Nombre.Name = "Nombre";
            // 
            // Column1
            // 
            Column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column1.HeaderText = "Monto total";
            Column1.Name = "Column1";
            // 
            // Column2
            // 
            Column2.HeaderText = "Estado";
            Column2.Name = "Column2";
            // 
            // Column3
            // 
            Column3.HeaderText = "ver detalles";
            Column3.Name = "Column3";
            Column3.Resizable = DataGridViewTriState.True;
            Column3.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // Column5
            // 
            Column5.HeaderText = "Eliminar";
            Column5.Name = "Column5";
            Column5.Resizable = DataGridViewTriState.True;
            Column5.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // OrdenesHistorial
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(45, 66, 91);
            ClientSize = new Size(965, 788);
            Controls.Add(guna2TextBox2);
            Controls.Add(Cliente);
            Controls.Add(dataGridView1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "OrdenesHistorial";
            Text = "OrdenesHistorial";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label Cliente;
        private DataGridView dataGridView1;
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox2;
        private DataGridViewTextBoxColumn Cedula;
        private DataGridViewButtonColumn Usuariosd;
        private DataGridViewTextBoxColumn Nombre;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewImageColumn Column3;
        private DataGridViewImageColumn Column5;
    }
}