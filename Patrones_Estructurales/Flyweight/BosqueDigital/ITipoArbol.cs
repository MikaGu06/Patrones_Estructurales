using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace Patrones_Estructurales.Flyweight.BosqueDigital
{
    public interface ITipoArbol
    {
        void Dibujar(DrawingContext dc, double x, double y);
    }
}
