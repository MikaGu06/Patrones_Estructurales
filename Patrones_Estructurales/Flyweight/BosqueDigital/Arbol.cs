using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace Patrones_Estructurales.Flyweight.BosqueDigital
{
    public class Arbol
    {
        private double _x, _y;
        private TipoArbol _tipo;

        public Arbol(double x, double y, TipoArbol tipo)
        {
            _x = x;
            _y = y;
            _tipo = tipo;
        }

        public void Renderizar(DrawingContext dc)
        {
            _tipo.Dibujar(dc, _x, _y);
        }
    }
}
