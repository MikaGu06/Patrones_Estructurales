using System.Windows;
using System.Windows.Controls;

namespace Patrones_Estructurales.Facade.GestorCentral
{
    public partial class GestorCentral : UserControl
    {
        public GestorCentral()
        {
            InitializeComponent();
        }

        private void BtnEjecutar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtOperador.Text))
            {
                TxtResultado.Text = "Error: ingrese el nombre del operador.";
                return;
            }

            SistemaFacade facade = new SistemaFacade();

            TxtResultado.Text = facade.EjecutarOperaciones(
                TxtOperador.Text,
                ChkBackup.IsChecked == true,
                ChkReporte.IsChecked == true,
                ChkNotificacion.IsChecked == true
            );
        }
    }

    public class SistemaFacade
    {
        private readonly BackupService backup;
        private readonly ReporteService reporte;
        private readonly NotificacionService notificacion;

        public SistemaFacade()
        {
            backup = new BackupService();
            reporte = new ReporteService();
            notificacion = new NotificacionService();
        }

        public string EjecutarOperaciones(string operador, bool hacerBackup, bool generarReporte, bool enviarNotificacion)
        {
            string resultado = "Operador: " + operador + "\n";

            if (hacerBackup)
                resultado += backup.RealizarBackup() + "\n";

            if (generarReporte)
                resultado += reporte.GenerarReporte() + "\n";

            if (enviarNotificacion)
                resultado += notificacion.EnviarNotificacion(operador) + "\n";

            return resultado;
        }
    }

    public class BackupService
    {
        public string RealizarBackup()
        {
            return "Backup realizado correctamente.";
        }
    }

    public class ReporteService
    {
        public string GenerarReporte()
        {
            return "Reporte del sistema generado.";
        }
    }

    public class NotificacionService
    {
        public string EnviarNotificacion(string operador)
        {
            return "Notificación enviada al operador: " + operador;
        }
    }
}