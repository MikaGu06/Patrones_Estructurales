using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Patrones_Estructurales.Flyweight.BosqueDigital
{
    public class TipoArbol : ITipoArbol
    {
        public string Nombre { get; }
        public Brush Color { get; }

        public TipoArbol(string nombre, Brush color)
        {
            Nombre = nombre;
            Color = color;
        }

        public void Dibujar(DrawingContext dc, double x, double y)
        {
            // Dibujamos un árbol simple usando vectores (para no cargar imágenes externas)
            // Tronco
            dc.DrawRectangle(new SolidColorBrush(Colors.SaddleBrown), null, new Rect(x - 5, y, 10, 20));
            // Copa
            dc.DrawEllipse(Color, null, new Point(x, y - 10), 15, 20);
        }
    }
}
