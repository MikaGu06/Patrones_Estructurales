using System;
using System.Collections.Generic;
using System.Text;

namespace Patrones_Estructurales.Composite.MenusUI
{
    public interface IMenuElemento
    {
        string Nombre { get; }
        string Renderizar(int nivel);
    }
}
