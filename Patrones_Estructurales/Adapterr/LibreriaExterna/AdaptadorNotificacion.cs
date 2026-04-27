using System;
using System.Collections.Generic;
using System.Text;

namespace Patrones_Estructurales.Adapterr.LibreriaExterna
{
    public class AdaptadorNotificacion : INotificacion
    {
        private LibreriaNotificaciones NotificacionExterna;

        public AdaptadorNotificacion(string mensaje, string tipo)
        {
            NotificacionExterna = new LibreriaNotificaciones(mensaje, tipo);
        }

        public void Mostrar()
        {
            NotificacionExterna.MostrarNotificacionExterna();
        }
    }
}