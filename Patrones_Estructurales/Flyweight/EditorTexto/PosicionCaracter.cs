using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace Patrones_Estructurales.Flyweight.EditorTexto
{
    public class PosicionCaracter
    {
        private readonly double _x, _y;
        private readonly ICaracterFlyweight _flyweight;

        public PosicionCaracter(double x, double y, ICaracterFlyweight flyweight)
        {
            _x = x;
            _y = y;
            _flyweight = flyweight;
        }

        public void Renderizar(DrawingContext dc)
        {
            _flyweight.Dibujar(dc, _x, _y);
        }
    }
}
