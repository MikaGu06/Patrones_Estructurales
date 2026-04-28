using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace Patrones_Estructurales.Flyweight.Particulas
{
    public class ParticulaFactory
    {
        private static readonly Dictionary<string, ParticulaFlyweight> _cache = new Dictionary<string, ParticulaFlyweight>();

        public static ParticulaFlyweight GetParticula(Color color, double tamaño)
        {
            string clave = $"{color}_{tamaño}";

            if (!_cache.ContainsKey(clave))
            {
                _cache.Add(clave, new ParticulaFlyweight(color, tamaño));
            }
            return _cache[clave];
        }

        public static int CantidadFlyweights() => _cache.Count;
        public static void Reiniciar() => _cache.Clear();
    }
}
