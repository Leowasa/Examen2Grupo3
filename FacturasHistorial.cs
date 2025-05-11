using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using Newtonsoft.Json;
using static Examen2Grupo3.RegistroPedidos;
using System.Text.Json;
using System.Collections;

namespace Examen2Grupo3
{
    public partial class FacturasHistorial : Form
    {
        static Pedido Actual = new Pedido();
        List<Pedido>? pedidos = new List<Pedido>();
        static Empresa Empresa = new Empresa();
        public FacturasHistorial()
        {
            InitializeComponent();
            dataGridView1.Columns["Total"].DefaultCellStyle.Format = "C2";
            dataGridView1.Columns["Total"].DefaultCellStyle.FormatProvider = new System.Globalization.CultureInfo("en-US");
            CargarFacturas("Ordenes.json");
            cargarEmpresa();
        }
        public void CargarFacturas(string rutaArchivo)
        {
            if (File.Exists(rutaArchivo))
            {
                string json = File.ReadAllText(rutaArchivo);
                pedidos = JsonConvert.DeserializeObject<List<Pedido>>(json); // Cambiar JsonSerializer por JsonConvert  

                if (pedidos != null) // Verificar que la lista no sea nula  
                {
                    dataGridView1.Rows.Clear(); // Limpiar la tabla antes de cargar nuevos datos  

                    foreach (var producto in pedidos)
                    {
                        dataGridView1.Rows.Add(producto.ID.ToString("D6"), producto.Cliente.Nombre, producto.Fecha.ToString("dd/MM/yy"), producto.Total);
                    }
                }
            }
        }
        public void cargarEmpresa()
        {
            if (File.Exists("Empresa.json"))
            {
                try
                {
                    string json = File.ReadAllText("Empresa.json");
                    Empresa = JsonConvert.DeserializeObject<Empresa>(json) ?? new Empresa(); // Cambiar JsonSerializer por JsonConvert  


                }
                catch (Exception ex)
                { }

            }
        }

        private void GenerarFacturaPDF(string pdfPath)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // **Primer pase: calcular el número total de páginas**
            int totalPages;
            using (MemoryStream ms = new MemoryStream())
            {
                Document tempDocument = new Document(PageSize.A4, 45, 45, 45, 45);
                PdfWriter tempWriter = PdfWriter.GetInstance(tempDocument, ms);
                PageNumberHelper tempPageEvent = new PageNumberHelper();
                tempWriter.PageEvent = tempPageEvent;

                tempDocument.Open();
                string htmlContent = Properties.Resources.plantilla_factura != null
                    ? Encoding.UTF8.GetString(Properties.Resources.plantilla_factura)
                    : string.Empty;
                htmlContent = rellenarHtml(htmlContent, tempDocument);

                using (var reader = new StringReader(htmlContent))
                {
                    XMLWorkerHelper.GetInstance().ParseXHtml(tempWriter, tempDocument, reader);
                }

                tempDocument.Close();
                totalPages = tempWriter.PageNumber; // Calcula el número total de páginas
            }

            try
            {
                // **Segundo pase: generar el documento final**
                using (FileStream fs = new FileStream(pdfPath, FileMode.Create))
                {
                    Document finalDocument = new Document(PageSize.A4, 45, 45, 45, 45);
                    PdfWriter finalWriter = PdfWriter.GetInstance(finalDocument, fs);
                    PageNumberHelper finalPageEvent = new PageNumberHelper();
                    finalPageEvent.TotalPages = totalPages; // Usa el total calculado en el primer pase
                    finalWriter.PageEvent = finalPageEvent;

                    finalDocument.Open();
                    string htmlContent = Properties.Resources.plantilla_factura != null
                        ? Encoding.UTF8.GetString(Properties.Resources.plantilla_factura)
                        : string.Empty;
                    htmlContent = rellenarHtml(htmlContent, finalDocument);

                    using (var reader = new StringReader(htmlContent))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(finalWriter, finalDocument, reader);
                    }

                    finalDocument.Close();
                }

                MessageBox.Show("✅ PDF generado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Error inesperado: {ex.Message}");
            }
        }


        private string rellenarHtml(string htmlContent, Document document)
        {
            //Datos de la empresa
            htmlContent = htmlContent.Replace("@Razon", Empresa.RazonSocial);
            htmlContent = htmlContent.Replace("@Telefono", Empresa.Numero);
            htmlContent = htmlContent.Replace("@Direccion", Empresa.Direccion);
            htmlContent = htmlContent.Replace("@Correo", Empresa.Correo);
            htmlContent = htmlContent.Replace("@Website", Empresa.Website);

            //Datos del cliente
            htmlContent = htmlContent.Replace("@p", Actual.Cliente.ID.ToString());
            htmlContent = htmlContent.Replace("@4", Actual.Cliente.Nombre);
            htmlContent = htmlContent.Replace("@otrodato", Actual.Cliente.Direccion);
            htmlContent = htmlContent.Replace("@si", Actual.Cliente.Correo);

            //Datos del usuario
            htmlContent = htmlContent.Replace("@IDusuario", Actual.Encargado.ID.ToString());
            htmlContent = htmlContent.Replace("@Nombreusuario", Actual.Encargado.Nombre);
            htmlContent = htmlContent.Replace("@Username", Actual.Encargado.Username);

            //Numero y fecha de factura
            htmlContent = htmlContent.Replace("@Numero", Actual.ID.ToString("D6"));
            htmlContent = htmlContent.Replace("@Validacion", Actual.Fecha.ToString("dd/MM/yy"));
            htmlContent = htmlContent.Replace("@Emision", Actual.Fecha.ToString("dd/MM/yy"));

            //Datos de precio
            htmlContent = htmlContent.Replace("@SUBTOTAL", "$"+Actual.SubtTotal.ToString("F2") );
            htmlContent = htmlContent.Replace("@IVA", "$" + Actual.IVA.ToString("F2") );
            htmlContent = htmlContent.Replace("@TOTAL","$"+Actual.Total.ToString("F2"));

            //Observaciones
            htmlContent = htmlContent.Replace("@Observaciones", Actual.Observaciones);

            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Properties.Resources.icons8_company_48, System.Drawing.Imaging.ImageFormat.Png);
            img.ScaleToFit(120, 85);
            img.Alignment = iTextSharp.text.Image.UNDERLYING;

            //img.SetAbsolutePosition(10,100);
            img.SetAbsolutePosition(document.RightMargin, document.Top - 60);
            document.Add(img);


            string filas = string.Empty;
            foreach (var row in Actual.Productos)
            {
                filas += "<tr>";
                filas += "<td>" + row.ID + "</td>";
                filas += "<td>" + row.Nombre + "</td>";
                filas += "<td>" + row.Descripcion + "</td>";
                filas += "<td>" + row.Categoria + "</td>";
                filas += "<td>" + row.Cantidad + "</td>";
                filas += "<td>" +"$"+ row.PrecioUnitario+ "</td>";
                filas += "<td>" +"$"+ row.Total +"</td>";
                filas += "</tr>";
            }
            htmlContent = htmlContent.Replace("@FILAS", filas);
            return htmlContent;

        }
        public class PageNumberHelper : PdfPageEventHelper
        {
            public int TotalPages { get; set; } // Se actualizará después de la creación del documento
            public string Observaciones { get; set; } // Propiedad para recibir datos dinámicos
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                int pageNumber = writer.PageNumber; // Obtiene el número de la página actual
                PdfContentByte cb = writer.DirectContent;

                // Define la fuente
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb.SetFontAndSize(bf, 10);

                // Posición de la numeración (abajo a la derecha)
                float x = document.PageSize.Width - 50;
                float y = document.PageSize.GetBottom(30);

                // Posición de las observaciones (abajo a la izquierda)
                float obsX = document.LeftMargin;
                float obsY = document.PageSize.GetBottom(30); // Misma altura que la numeración

                cb.BeginText();
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, $"Página {pageNumber} de {TotalPages}", x, y, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, $"Observaciones: {Observaciones}", obsX, obsY, 0);
                cb.EndText();
            }

        }



        public void GuardarFacturas()
        {
            // Fix for CS1503: Use Newtonsoft.Json.Formatting instead of System.Text.Json.JsonSerializerOptions  
            string json = JsonConvert.SerializeObject(pedidos, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("Ordenes.Json", json);
            CargarFacturas("Ordenes.Json");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns.Contains("Exportar") && e.ColumnIndex == dataGridView1.Columns["Exportar"].Index && e.RowIndex >= 0)
            {
                string pdfPath;
                DataGridViewRow filaSeleccionada = dataGridView1.Rows[e.RowIndex];

                // Ensure 'pedidos' is not null and the index is within bounds  
                if (pedidos != null && e.RowIndex < pedidos.Count && pedidos[e.RowIndex] != null)
                {
                    Actual = pedidos[e.RowIndex];
                    using (SaveFileDialog sfd = new SaveFileDialog())
                    {
                        sfd.Filter = "Archivos PDF (*.PDf)|*.PDf";
                        sfd.Title = "Selecciona dónde guardar el archivo PDF";

                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            pdfPath = sfd.FileName;
                            GenerarFacturaPDF(pdfPath);
                        }
                        else return;
                    }
                }
            }
            else if (e.ColumnIndex == dataGridView1.Columns["Eliminar"].Index && e.RowIndex >= 0)
            {
                DialogResult result = MessageBox.Show("¿Deseas eliminar este producto?", "Confirmar eliminación",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Ensure 'pedidos' is not null and the index is within bounds  
                    if (pedidos != null && e.RowIndex < pedidos.Count)
                    {
                        pedidos.RemoveAt(e.RowIndex);
                        dataGridView1.Rows.RemoveAt(e.RowIndex);
                        GuardarFacturas();
                    }
                }
            }
        }
        private void BuscarElemento(string textoBusqueda)
        {
            // Verificar que el texto de búsqueda tenga al menos 4 caracteres
            if (textoBusqueda.Length >= 3)
            {
                // Si tiene menos de 4 caracteres, mostrar todas las filas
                foreach (DataGridViewRow fila in dataGridView1.Rows)
                {
                    fila.Visible = true;
                }
                return;
            }

            // Convertir el texto de búsqueda a minúsculas para una comparación insensible a mayúsculas/minúsculas
            string filtro = textoBusqueda.ToLower();

            // Iterar sobre las filas del DataGridView
            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                // Verificar si la celda de ID o Nombre contiene el texto de búsqueda
                bool coincide = (fila.Cells["Numero"].Value != null && fila.Cells["Numero"].Value.ToString().Contains(filtro, StringComparison.CurrentCultureIgnoreCase)) ||
                                (fila.Cells["Nombre"].Value != null && fila.Cells["Nombre"].Value.ToString().ToLower().Contains(filtro));

                // Mostrar u ocultar la fila según si coincide con el filtro
                fila.Visible = coincide;
            }
        }
        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            BuscarElemento(guna2TextBox1.Text);
        }
    }
}