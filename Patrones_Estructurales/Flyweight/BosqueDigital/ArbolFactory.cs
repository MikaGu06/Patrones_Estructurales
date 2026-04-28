using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace Patrones_Estructurales.Flyweight.BosqueDigital
{
    public class ArbolFactory
    {
        private static Dictionary<string, TipoArbol> _tiposArbol = new Dictionary<string, TipoArbol>();

        public static TipoArbol GetTipoArbol(string nombre, Color color)
        {
            if (!_tiposArbol.ContainsKey(nombre))
            {
                _tiposArbol.Add(nombre, new TipoArbol(nombre, new SolidColorBrush(color)));
            }
            return _tiposArbol[nombre];
        }

        public static int GetCantidadTipos() => _tiposArbol.Count;
        public static void Reiniciar() => _tiposArbol.Clear();
    }
}
