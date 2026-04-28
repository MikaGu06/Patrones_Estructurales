using Notifications.Wpf;
using System;

namespace Patrones_Estructurales.Adapterr.LibreriaExterna
{
    public class LibreriaNotificacionesReal
    {
        private NotificationManager notificador = new NotificationManager();

        public void MostrarSuccess(string mensaje)
        {
            notificador.Show(new NotificationContent
            {
                Title = "Éxito",
                Message = mensaje,
                Type = NotificationType.Success
            }, expirationTime: TimeSpan.FromSeconds(3));
        }

        public void MostrarError(string mensaje)
        {
            notificador.Show(new NotificationContent
            {
                Title = "Error",
                Message = mensaje,
                Type = NotificationType.Error
            }, expirationTime: TimeSpan.FromSeconds(3));
        }

        public void MostrarInfo(string mensaje)
        {
            notificador.Show(new NotificationContent
            {
                Title = "Información",
                Message = mensaje,
                Type = NotificationType.Information
            }, expirationTime: TimeSpan.FromSeconds(3));
        }
    }
}