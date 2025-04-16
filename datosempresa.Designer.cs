namespace Examen2Grupo3
{
    partial class datosempresa
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(datosempresa));
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.Gainsboro;
            label5.Location = new Point(657, 71);
            label5.Name = "label5";
            label5.Size = new Size(162, 15);
            label5.TabIndex = 18;
            label5.Text = "Website: www.company.com";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.Gainsboro;
            label4.Location = new Point(305, 107);
            label4.Name = "label4";
            label4.Size = new Size(274, 15);
            label4.TabIndex = 17;
            label4.Text = "Dirección Física de la empresa: Calle Principal #123";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.Gainsboro;
            label3.Location = new Point(618, 39);
            label3.Name = "label3";
            label3.Size = new Size(251, 15);
            label3.TabIndex = 16;
            label3.Text = "Dirección de correo: contacto@company.com";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.Gainsboro;
            label2.Location = new Point(310, 39);
            label2.Name = "label2";
            label2.Size = new Size(154, 15);
            label2.TabIndex = 15;
            label2.Text = "Razón Social: Company S.A.";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.Gainsboro;
            label1.Location = new Point(310, 71);
            label1.Name = "label1";
            label1.Size = new Size(269, 15);
            label1.TabIndex = 14;
            label1.Text = "Número de teléfono de la empresa:  414-123-4567";
            // 
            // guna2Button1
            // 
            guna2Button1.Anchor = AnchorStyles.Top;
            guna2Button1.BorderRadius = 8;
            guna2Button1.CustomizableEdges = customizableEdges1;
            guna2Button1.DisabledState.BorderColor = Color.DarkGray;
            guna2Button1.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button1.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2Button1.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button1.FillColor = SystemColors.HotTrack;
            guna2Button1.Font = new Font("Segoe UI", 9F);
            guna2Button1.ForeColor = Color.White;
            guna2Button1.Location = new Point(559, 203);
            guna2Button1.Name = "guna2Button1";
            guna2Button1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Button1.Size = new Size(74, 23);
            guna2Button1.TabIndex = 130;
            guna2Button1.Text = "Editar";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(213, 170);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 131;
            pictureBox1.TabStop = false;
            // 
            // datosempresa
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(45, 66, 91);
            ClientSize = new Size(965, 808);
            Controls.Add(pictureBox1);
            Controls.Add(guna2Button1);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "datosempresa";
            Text = "datosempresa";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private PictureBox pictureBox1;
    }
}