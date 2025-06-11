using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Examen2Grupo3.Datos;

namespace Examen2Grupo3
{
    public partial class Moneda : Form
    {
        public Moneda()
        {
            InitializeComponent();
            txtBolivares.Text = moneda.bolivares.ToString("F2"); // Formatear a dos decimales   
        }

        private void btnConfirmar(object sender, EventArgs e)
        {
            try 
            {
                moneda.bolivares = decimal.Parse(txtBolivares.Text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
                MessageBox.Show("Moneda actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                guardar();
            } catch
            {
                MessageBox.Show("Formato no valido. Intente nuevamente","Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void guardar() 
        {

            if (moneda.bolivares!= 0) // Validamos que la fila tenga datos
            {
                string json = JsonSerializer.Serialize(moneda.bolivares, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText("moneda.Json", json);//serializo, lo escribo en el json y luego cargo los cambios
               // CargarInventario("Inventario.Json");
            }
            else MessageBox.Show("Error al guardar los datos. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);



        }
       
    }
}
