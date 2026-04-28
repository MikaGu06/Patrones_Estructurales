using System;
using System.Collections.Generic;
using System.Text;

namespace Patrones_Estructurales.Flyweight.EditorTexto
{
    public class CaracterFactory
    {
        private static readonly Dictionary<string, CaracterFlyweight> _cache = new Dictionary<string, CaracterFlyweight>();

        public static CaracterFlyweight GetCaracter(char simbolo, string fuente, double tamaño)
        {
            // La clave única es la combinación de letra, fuente y tamaño
            string clave = $"{simbolo}_{fuente}_{tamaño}";

            if (!_cache.ContainsKey(clave))
            {
                _cache.Add(clave, new CaracterFlyweight(simbolo, fuente, tamaño));
            }
            return _cache[clave];
        }

        public static int ObtenerTotalFlyweights() => _cache.Count;
        public static void Reiniciar() => _cache.Clear();
    }
}
