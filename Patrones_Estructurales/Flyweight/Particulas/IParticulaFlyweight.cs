using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace Patrones_Estructurales.Flyweight.Particulas
{
    public interface IParticulaFlyweight
    {
        void Renderizar(DrawingContext dc, double x, double y);
    }
}
