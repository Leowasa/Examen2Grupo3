using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Examen2Grupo3
{
    public partial class GenerarPedido : Form
    {
        public GenerarPedido()
        {
            InitializeComponent();
        }

        private void GenerarPedido_Load(object sender, EventArgs e)
        {

        }
        public class RoundButton : Button
        {
            protected override void OnPaint(PaintEventArgs pevent)
            {
                GraphicsPath path = new GraphicsPath();
                path.AddEllipse(0, 0, Width, Height);
                this.Region = new Region(path);
                base.OnPaint(pevent);
            }
        }
    }
}
