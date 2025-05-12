using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Examen2Grupo3.Datos;

namespace Examen2Grupo3
{
    public partial class Verperfil : Form
    {
        Datos.Usuarios usuarioActual = new Datos.Usuarios();
        public Verperfil(Usuarios usuarioActual)
        {
            InitializeComponent();
            //cargo los datos del usuario actual y lo muestro en pantalla
            this.usuarioActual = usuarioActual;
            lblRol.Text = usuarioActual.Tipo;
            lblID.Text ="ID: " +usuarioActual.ID.ToString();
            lblNombre.Text ="Nombre: " +usuarioActual.Nombre;
            lblUsuario.Text = usuarioActual.Username;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

            OpcionesSuperUsuario opcionesSuperUsuario = new OpcionesSuperUsuario(0, usuarioActual);
            opcionesSuperUsuario.ShowDialog();//ejecuto la funcion para cambiar clave
        }
    }
}
