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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            dataGridView1 = new DataGridView();
            btnAgregarUsuario = new Guna.UI2.WinForms.Guna2Button();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            textBox1 = new Guna.UI2.WinForms.Guna2TextBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.BackgroundColor = Color.FromArgb(45, 66, 91);
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.HotTrack;
            dataGridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeight = 30;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.GridColor = Color.SteelBlue;
            dataGridView1.Location = new Point(12, 161);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(45, 66, 91);
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.SteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.Size = new Size(860, 615);
            dataGridView1.TabIndex = 95;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // btnAgregarUsuario
            // 
            btnAgregarUsuario.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAgregarUsuario.BorderRadius = 8;
            btnAgregarUsuario.CustomizableEdges = customizableEdges1;
            btnAgregarUsuario.DisabledState.BorderColor = Color.DarkGray;
            btnAgregarUsuario.DisabledState.CustomBorderColor = Color.DarkGray;
            btnAgregarUsuario.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnAgregarUsuario.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnAgregarUsuario.FillColor = SystemColors.HotTrack;
            btnAgregarUsuario.Font = new Font("Segoe UI", 9F);
            btnAgregarUsuario.ForeColor = Color.White;
            btnAgregarUsuario.Location = new Point(871, 132);
            btnAgregarUsuario.Name = "btnAgregarUsuario";
            btnAgregarUsuario.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnAgregarUsuario.Size = new Size(82, 23);
            btnAgregarUsuario.TabIndex = 130;
            btnAgregarUsuario.Text = "Agregar";
            btnAgregarUsuario.Click += guna2Button1_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(819, 113);
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
            pictureBox1.Location = new Point(771, 113);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(42, 42);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 152;
            pictureBox1.TabStop = false;
            // 
            // textBox1
            // 
            textBox1.BorderRadius = 7;
            textBox1.CustomizableEdges = customizableEdges3;
            textBox1.DefaultText = "";
            textBox1.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            textBox1.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            textBox1.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            textBox1.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            textBox1.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            textBox1.Font = new Font("Segoe UI", 9F);
            textBox1.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            textBox1.Location = new Point(12, 132);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "";
            textBox1.SelectedText = "";
            textBox1.ShadowDecoration.CustomizableEdges = customizableEdges4;
            textBox1.Size = new Size(154, 23);
            textBox1.TabIndex = 154;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Century Gothic", 21.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Gainsboro;
            label1.Location = new Point(378, 9);
            label1.Name = "label1";
            label1.Size = new Size(234, 70);
            label1.TabIndex = 155;
            label1.Text = "Usuarios";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // Usuarios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 66, 91);
            ClientSize = new Size(965, 788);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(btnAgregarUsuario);
            Controls.Add(dataGridView1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Usuarios";
            Text = "Usuarios";
            Load += Usuarios_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label Cliente;
        private DataGridView dataGridView1;
        private Guna.UI2.WinForms.Guna2Button btnAgregarUsuario;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private Guna.UI2.WinForms.Guna2TextBox textBox1;
        private Label label1;
    }
}