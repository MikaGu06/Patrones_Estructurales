using System;
using System.Collections.Generic;
using System.Text;

namespace Patrones_Estructurales.Composite.Sistema_de_Archivos
{
    public interface IComponenteArchivo
    {
        string Nombre { get; }
        string Mostrar(int nivel);
    }
}
