namespace Patrones_Estructurales.Adapterr.LibreriaExterna
{
    public class AdaptadorNotificacion : INotificacion
    {
        private LibreriaNotificacionesReal notificador = new LibreriaNotificacionesReal();

        public void MostrarExito(string mensaje)
        {
            notificador.MostrarSuccess(mensaje);
        }

        public void MostrarError(string mensaje)
        {
            notificador.MostrarError(mensaje);
        }

        public void MostrarInfo(string mensaje)
        {
            notificador.MostrarInfo(mensaje);
        }
    }
}