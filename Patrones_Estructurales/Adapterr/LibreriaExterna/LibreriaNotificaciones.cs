using System.Windows;

namespace Patrones_Estructurales.Adapterr.LibreriaExterna
{
    public class LibreriaNotificaciones
    {
        private string Mensaje;
        private string Tipo;

        public LibreriaNotificaciones(string mensaje, string tipo)
        {
            Mensaje = mensaje;
            Tipo = tipo;
        }

        public void MostrarNotificacionExterna()
        {
            MessageBoxImage icono = MessageBoxImage.Information;

            if (Tipo == "Error")
                icono = MessageBoxImage.Error;
            else if (Tipo == "Información")
                icono = MessageBoxImage.Warning;

            MessageBox.Show(Mensaje, Tipo, MessageBoxButton.OK, icono);
        }
    }
}