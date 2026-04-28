using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace Patrones_Estructurales.Flyweight.Particulas
{
    public class Particula
    {
        private double _x, _y;
        private readonly IParticulaFlyweight _flyweight;

        public Particula(double x, double y, IParticulaFlyweight flyweight)
        {
            _x = x;
            _y = y;
            _flyweight = flyweight;
        }

        public void Dibujar(DrawingContext dc)
        {
            _flyweight.Renderizar(dc, _x, _y);
        }
    }
}
