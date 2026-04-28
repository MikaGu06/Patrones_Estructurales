using System;
using System.Collections.Generic;
using System.Text;

namespace Patrones_Estructurales.Adapterr.LibreriaExterna
{
    public interface INotificacion
    {
        void MostrarExito(string mensaje);
        void MostrarError(string mensaje);
        void MostrarInfo(string mensaje);
    }
}