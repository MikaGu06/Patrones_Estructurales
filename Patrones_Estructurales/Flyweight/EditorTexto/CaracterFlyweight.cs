using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Patrones_Estructurales.Flyweight.EditorTexto
{
    public class CaracterFlyweight : ICaracterFlyweight
    {
        private readonly char _simbolo;
        private readonly string _fuente;
        private readonly double _tamaño;

        public CaracterFlyweight(char simbolo, string fuente, double tamaño)
        {
            _simbolo = simbolo;
            _fuente = fuente;
            _tamaño = tamaño;
        }

        public void Dibujar(DrawingContext dc, double x, double y)
        {
            // Canvas
            FormattedText textoFormat = new FormattedText(
                _simbolo.ToString(),
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface(_fuente),
                _tamaño,
                Brushes.Black,
                1.25 
            );

            dc.DrawText(textoFormat, new Point(x, y));
        }
    }
}
