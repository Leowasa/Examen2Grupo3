namespace Examen2Grupo3
{
    partial class GenerarOrden
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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            Cliente = new Label();
            dataGridView1 = new DataGridView();
            guna2TextBox2 = new Guna.UI2.WinForms.Guna2TextBox();
            Numero = new DataGridViewTextBoxColumn();
            Nombre = new DataGridViewTextBoxColumn();
            Usuariosd = new DataGridViewButtonColumn();
            Total = new DataGridViewTextBoxColumn();
            Estado = new DataGridViewComboBoxColumn();
            Ver = new DataGridViewImageColumn();
            Eliminar = new DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // Cliente
            // 
            Cliente.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Cliente.BackColor = Color.Transparent;
            Cliente.Font = new Font("Microsoft Sans Serif", 21.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
            Cliente.ForeColor = Color.Gainsboro;
            Cliente.Location = new Point(259, 18);
            Cliente.Name = "Cliente";
            Cliente.Size = new Size(534, 92);
            Cliente.TabIndex = 115;
            Cliente.Text = "Generar Orden de Entrega";
            Cliente.TextAlign = ContentAlignment.TopCenter;
            Cliente.Click += Cliente_Click;
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
            dataGridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.Highlight;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeight = 30;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Numero, Nombre, Usuariosd, Total, Estado, Ver, Eliminar });
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.Transparent;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = SystemColors.ActiveCaptionText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.InactiveBorder;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle4;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.GridColor = Color.SteelBlue;
            dataGridView1.Location = new Point(12, 161);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(45, 66, 91);
            dataGridViewCellStyle5.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle5.ForeColor = Color.White;
            dataGridViewCellStyle5.SelectionBackColor = Color.SteelBlue;
            dataGridViewCellStyle5.SelectionForeColor = Color.White;
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle5;
            dataGridView1.Size = new Size(941, 615);
            dataGridView1.TabIndex = 114;
            dataGridView1.EditModeChanged += dataGridView1_EditModeChanged;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.EditingControlShowing += dataGridView1_EditingControlShowing;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            // 
            // guna2TextBox2
            // 
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
            guna2TextBox2.Location = new Point(12, 132);
            guna2TextBox2.Name = "guna2TextBox2";
            guna2TextBox2.PlaceholderText = "Ingresar Numero o Fecha...";
            guna2TextBox2.SelectedText = "";
            guna2TextBox2.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2TextBox2.Size = new Size(164, 23);
            guna2TextBox2.TabIndex = 148;
            guna2TextBox2.TextChanged += guna2TextBox2_TextChanged;
            // 
            // Numero
            // 
            Numero.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.BackColor = Color.Transparent;
            Numero.DefaultCellStyle = dataGridViewCellStyle2;
            Numero.HeaderText = "Numero";
            Numero.Name = "Numero";
            Numero.ReadOnly = true;
            // 
            // Nombre
            // 
            Nombre.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Nombre.HeaderText = "Cliente";
            Nombre.Name = "Nombre";
            // 
            // Usuariosd
            // 
            Usuariosd.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Usuariosd.FlatStyle = FlatStyle.Flat;
            Usuariosd.HeaderText = "Fecha de creacion";
            Usuariosd.Name = "Usuariosd";
            Usuariosd.ReadOnly = true;
            // 
            // Total
            // 
            Total.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Total.HeaderText = "Monto Total";
            Total.Name = "Total";
            // 
            // Estado
            // 
            dataGridViewCellStyle3.BackColor = Color.Transparent;
            dataGridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.HotTrack;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HotTrack;
            Estado.DefaultCellStyle = dataGridViewCellStyle3;
            Estado.DisplayStyleForCurrentCellOnly = true;
            Estado.HeaderText = "Estado";
            Estado.Items.AddRange(new object[] { "Pendiente", "Aprobado", "Rechazado", "Entregado" });
            Estado.Name = "Estado";
            Estado.Resizable = DataGridViewTriState.True;
            Estado.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // Ver
            // 
            Ver.HeaderText = "ver detalles";
            Ver.Image = Properties.Resources.icons8_view_details_241;
            Ver.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Ver.Name = "Ver";
            Ver.Resizable = DataGridViewTriState.True;
            Ver.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // Eliminar
            // 
            Eliminar.HeaderText = "Eliminar";
            Eliminar.Image = Properties.Resources.icons8_trash_can_48;
            Eliminar.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Eliminar.Name = "Eliminar";
            Eliminar.Resizable = DataGridViewTriState.True;
            Eliminar.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // GenerarOrden
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 66, 91);
            ClientSize = new Size(965, 788);
            Controls.Add(guna2TextBox2);
            Controls.Add(Cliente);
            Controls.Add(dataGridView1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "GenerarOrden";
            Text = "GenerarPedido";
            Load += GenerarOrden_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label Cliente;
        private DataGridView dataGridView1;
        private Guna.UI2.WinForms.Guna2TextBox guna2TextBox2;
        private DataGridViewTextBoxColumn Cedula;
        private DataGridViewImageColumn Column3;
        private DataGridViewImageColumn Column5;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Numero;
        private DataGridViewTextBoxColumn Nombre;
        private DataGridViewButtonColumn Usuariosd;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewComboBoxColumn Estado;
        private DataGridViewImageColumn Ver;
        private DataGridViewImageColumn Eliminar;
        private DataGridViewTextBoxColumn Total;
    }
}