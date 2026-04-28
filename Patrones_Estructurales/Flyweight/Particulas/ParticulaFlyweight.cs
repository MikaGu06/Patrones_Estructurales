using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Patrones_Estructurales.Flyweight.Particulas
{
    public class ParticulaFlyweight : IParticulaFlyweight
    {
        private readonly Brush _color;
        private readonly double _tamaño;

        public ParticulaFlyweight(Color color, double tamaño)
        {
            _color = new SolidColorBrush(color);
            _tamaño = tamaño;
            _color.Freeze(); // Optimización extra de WPF para objetos compartidos
        }

        public void Renderizar(DrawingContext dc, double x, double y)
        {
            // Dibujamos un círculo simple como partícula
            dc.DrawEllipse(_color, null, new Point(x, y), _tamaño, _tamaño);
        }
    }
}
