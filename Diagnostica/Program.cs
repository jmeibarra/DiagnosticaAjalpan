using DAjalpan.Forms;
using System;
using System.Windows.Forms;
using DAjalpan.Clases.Seguridad;
using DAjalpan.Negocio;
using DAjalpan.Clases;

namespace DAjalpan
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                ParametrosSistema.Inicializar(new ParametrosRepositorio(Conexion.ConexionDB()));
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo leer la cadena de conexión:\n" + ex.Message,
                                "Error de configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Application.Run(new fPrincipal());
            //Application.Run(new frmVenta());
            Application.Run(new frmMain());
            //Application.Run(new frmLogin(false));
        }
    }
}
