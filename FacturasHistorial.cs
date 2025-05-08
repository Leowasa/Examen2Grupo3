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
            CargarInventario("ordenes.json");
            cargarEmpresa();
        }
        public void CargarInventario(string rutaArchivo)
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
                        dataGridView1.Rows.Add(producto.ID, producto.Cliente.Nombre, producto.Fecha.ToString("dd/MM/yy"), producto.Total);
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
        private void BuscarElemento(string textoBusqueda)
        {
            // Verificar que el texto de búsqueda tenga al menos 4 caracteres
            if (textoBusqueda.Length < 4)
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
                bool coincide = (fila.Cells["Numero"].Value != null && fila.Cells["Numero"].Value.ToString().ToLower().Contains(filtro)) ||
                                (fila.Cells["Nombre"].Value != null && fila.Cells["Nombre"].Value.ToString().ToLower().Contains(filtro));

                // Mostrar u ocultar la fila según si coincide con el filtro
                fila.Visible = coincide;
            }
        }
        /* private void GenerarFacturaPDF(string pdfPath)
         {
             string htmlPath = Properties.Resources.plantilla_factura.ToString();

             Document document = new Document(PageSize.A4, 25, 25, 25, 25);
             SaveFileDialog savefile = new SaveFileDialog();

                try
                 {
                     // 1. Configuración inicial  
                     Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                     // 2. Configuración del documento PDF  

                     // 3. Crear el archivo PDF  
                     using (FileStream fs = new FileStream(pdfPath, FileMode.Create))
                     {
                         PdfWriter writer = PdfWriter.GetInstance(document, fs);

                         // 4. Abrir documento  
                         document.Open();
                         document.Add(new Phrase(""));

                         // 5. Leer contenido HTML  
                         string htmlContent = File.ReadAllText("Factura - copia.html", Encoding.UTF8);
                         htmlContent = rellenarHtml(htmlContent, document);
                         if (string.IsNullOrWhiteSpace(htmlContent))
                         {
                             throw new InvalidOperationException("El contenido HTML está vacío o no se pudo leer.");
                         }

                         // 6. Convertir HTML a PDF con configuración robusta  
                         using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(htmlContent)))
                         using (var reader = new StreamReader(ms, Encoding.UTF8)) // Convertir MemoryStream a TextReader  
                         {
                             XMLWorkerHelper.GetInstance().ParseXHtml(
                                 writer,
                                 document,
                                 reader
                             );
                         }

                         // 7. Cerrar documento (importante para evitar corrupción)  
                         document.Close();
                         fs.Close();
                     }

                     MessageBox.Show("✅ PDF generado correctamente.");
                 }
                 catch (FileNotFoundException ex)
                 {
                     MessageBox.Show($"❌ Error: Archivo no encontrado. {ex.Message}");
                 }
                 catch (IOException ex)
                 {
                     MessageBox.Show($"❌ Error de E/S: {ex.Message}");
                 }
                 catch (DocumentException ex)
                 {
                     MessageBox.Show($"❌ Error al generar PDF: {ex.Message}");
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show($"❌ Error inesperado: {ex.Message}");
                 }
                 finally
                 {
                     // Asegurarse de que el documento esté cerrado  
                     if (document != null && document.IsOpen())
                     {
                         document.Close();
                     }
                 }


         }
        */
        private void GenerarFacturaPDF(string pdfPath)
        {
            string htmlPath = Properties.Resources.plantilla_factura.ToString();
            Document document = new Document(PageSize.A4, 45, 45, 45, 45);

            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                using (FileStream fs = new FileStream(pdfPath, FileMode.Create))
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, fs);

                    // **1. Agregar el evento de numeración de páginas**
                    PageNumberHelper pageEvent = new PageNumberHelper();
                    writer.PageEvent = pageEvent;

                    document.Open();
                    document.Add(new Phrase(""));

                    // **2. Leer contenido HTML**
                    string htmlContent = File.ReadAllText("Factura - copia.html", Encoding.UTF8);
                    htmlContent = rellenarHtml(htmlContent, document);

                    using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(htmlContent)))
                    using (var reader = new StreamReader(ms, Encoding.UTF8))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, reader);
                    }

                    // **3. Actualizar el número total de páginas**
                    pageEvent.TotalPages = writer.PageNumber;

                    document.Close();
                    fs.Close();
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
            htmlContent = htmlContent.Replace("@SUBTOTAL", Actual.SubtTotal.ToString());
            htmlContent = htmlContent.Replace("@IVA", Actual.IVA.ToString());
            htmlContent = htmlContent.Replace("@TOTAL", Actual.Total.ToString());

            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Properties.Resources.icons8_company_48, System.Drawing.Imaging.ImageFormat.Png);
            img.ScaleToFit(90, 70);
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
                filas += "<td>" + row.PrecioUnitario + "</td>";
                filas += "<td>" + row.Total + "</td>";
                filas += "</tr>";
            }
            htmlContent = htmlContent.Replace("@FILAS", filas);
            return htmlContent;

        }
        public class PageNumberHelper : PdfPageEventHelper
        {
            public int TotalPages { get; set; } // Se actualizará después de la creación del documento

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

                cb.BeginText();
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, $"Página {pageNumber} de {TotalPages}", x, y, 0);
                cb.EndText();
            }
        }



        public void GuardarInventario()
        {
            // Fix for CS1503: Use Newtonsoft.Json.Formatting instead of System.Text.Json.JsonSerializerOptions  
            string json = JsonConvert.SerializeObject(pedidos, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("Ordenes.Json", json);
            CargarInventario("Ordenes.Json");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns.Contains("Exportar") && e.ColumnIndex == dataGridView1.Columns["Exportar"].Index && e.RowIndex >= 0)
            {
                string pdfPath;
                DataGridViewRow filaSeleccionada = dataGridView1.Rows[e.RowIndex];
                foreach (var lista in pedidos)
                {
                    if (lista.ID.ToString() == filaSeleccionada.Cells["Numero"].Value.ToString())
                    {
                        Actual = lista;

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
               
            }
            else if (e.ColumnIndex == dataGridView1.Columns["Eliminar"].Index && e.RowIndex >= 0)
            {
                DialogResult result = MessageBox.Show("¿Deseas eliminar este producto?", "Confirmar eliminación",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    pedidos.RemoveAt(e.RowIndex);
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                    GuardarInventario();
                }
            }

        }
    }
}