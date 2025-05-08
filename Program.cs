using Examen2Grupo3;

namespace ejemplo
{
    internal static class Program
    {

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            try
            {
                RegistroPedidos.Usuarios usuario = new RegistroPedidos.Usuarios();
                Application.Run(new Form1());
            }
            catch (System.NullReferenceException ex)
            {
                MessageBox.Show($"Se produjo un error: {ex.Message}\n\nPila de llamadas:\n{ex.StackTrace}",
                                "Error de inicialización",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Se produjo un error inesperado: {ex.Message}\n\nPila de llamadas:\n{ex.StackTrace}",
                                "Error inesperado",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
    }
}