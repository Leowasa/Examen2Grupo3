namespace Examen2Grupo3
{
    partial class Verperfil
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Verperfil));
            lblRol = new Label();
            lblUsuario = new Label();
            lblID = new Label();
            lblNombre = new Label();
            guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // lblRol
            // 
            lblRol.AutoSize = true;
            lblRol.ForeColor = Color.Gainsboro;
            lblRol.Location = new Point(524, 139);
            lblRol.Margin = new Padding(4, 0, 4, 0);
            lblRol.Name = "lblRol";
            lblRol.Size = new Size(83, 15);
            lblRol.TabIndex = 112;
            lblRol.Text = "Administrador";
            lblRol.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblUsuario
            // 
            lblUsuario.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUsuario.ForeColor = Color.Gainsboro;
            lblUsuario.Location = new Point(515, 109);
            lblUsuario.Margin = new Padding(4, 0, 4, 0);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(94, 30);
            lblUsuario.TabIndex = 111;
            lblUsuario.Text = "Usuario1";
            lblUsuario.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblID
            // 
            lblID.AutoSize = true;
            lblID.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold);
            lblID.ForeColor = Color.Gainsboro;
            lblID.Location = new Point(486, 218);
            lblID.Margin = new Padding(4, 0, 4, 0);
            lblID.Name = "lblID";
            lblID.Size = new Size(26, 16);
            lblID.TabIndex = 110;
            lblID.Text = "ID:";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold);
            lblNombre.ForeColor = Color.Gainsboro;
            lblNombre.Location = new Point(485, 193);
            lblNombre.Margin = new Padding(4, 0, 4, 0);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(66, 16);
            lblNombre.TabIndex = 109;
            lblNombre.Text = "Nombre:";
            // 
            // guna2Button1
            // 
            guna2Button1.BorderRadius = 8;
            guna2Button1.CustomizableEdges = customizableEdges3;
            guna2Button1.DisabledState.BorderColor = Color.DarkGray;
            guna2Button1.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button1.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2Button1.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button1.FillColor = SystemColors.HotTrack;
            guna2Button1.Font = new Font("Segoe UI", 9F);
            guna2Button1.ForeColor = Color.White;
            guna2Button1.Location = new Point(486, 257);
            guna2Button1.Name = "guna2Button1";
            guna2Button1.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2Button1.Size = new Size(151, 23);
            guna2Button1.TabIndex = 129;
            guna2Button1.Text = "Cambiar contraseña";
            guna2Button1.Click += guna2Button1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(515, 25);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(92, 81);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 130;
            pictureBox1.TabStop = false;
            // 
            // Verperfil
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 66, 91);
            ClientSize = new Size(1094, 872);
            Controls.Add(pictureBox1);
            Controls.Add(guna2Button1);
            Controls.Add(lblRol);
            Controls.Add(lblUsuario);
            Controls.Add(lblID);
            Controls.Add(lblNombre);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Verperfil";
            Text = "Verperfil";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblRol;
        private Label lblUsuario;
        private Label lblID;
        private Label lblNombre;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private PictureBox pictureBox1;
    }
}