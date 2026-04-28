using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Patrones_Estructurales.Proxy.Ejercicio3_ProxyRemoto
{
    // Proxy Remoto - Controla acceso al servicio web real con caché
    public class ProxyServicioWeb : IServicioWeb
    {
        private ServicioWebReal _servicioReal;
        private Dictionary<string, Tuple<string, DateTime>> _cacheClima;
        private Tuple<Dictionary<string, decimal>, DateTime> _cacheCotizaciones;
        private TimeSpan _tiempoCache = TimeSpan.FromSeconds(30); // Caché de 30 segundos

        public ProxyServicioWeb()
        {
            _servicioReal = new ServicioWebReal();
            _cacheClima = new Dictionary<string, Tuple<string, DateTime>>();
        }

        public string EstadoConexion => _servicioReal?.EstadoConexion ?? "Proxy listo";

        public async Task<string> ObtenerClima(string ciudad)
        {
            // Normalizar nombre de ciudad (minúsculas, sin espacios extras)
            string ciudadKey = ciudad.Trim().ToLower();

            // Verificar caché
            if (_cacheClima.ContainsKey(ciudadKey))
            {
                var cache = _cacheClima[ciudadKey];
                if (DateTime.Now - cache.Item2 < _tiempoCache)
                {
                    return $"📦 [CACHE] {cache.Item1}";
                }
            }

            // Llamar al servicio real
            string resultado = await _servicioReal.ObtenerClima(ciudad);

            // Guardar en caché solo si no es error
            if (!resultado.Contains("Error") && !resultado.Contains("No se encontró"))
            {
                _cacheClima[ciudadKey] = Tuple.Create(resultado, DateTime.Now);
            }

            return resultado;
        }

        public async Task<Dictionary<string, decimal>> ObtenerCotizaciones()
        {
            // Verificar caché
            if (_cacheCotizaciones != null)
            {
                if (DateTime.Now - _cacheCotizaciones.Item2 < _tiempoCache)
                {
                    var cotizacionesCache = new Dictionary<string, decimal>();
                    foreach (var kvp in _cacheCotizaciones.Item1)
                    {
                        cotizacionesCache[kvp.Key] = kvp.Value;
                    }
                    return cotizacionesCache;
                }
            }

            // Llamar al servicio real
            var resultados = await _servicioReal.ObtenerCotizaciones();

            // Guardar en caché
            _cacheCotizaciones = Tuple.Create(resultados, DateTime.Now);

            return resultados;
        }

        public void LimpiarCache()
        {
            _cacheClima.Clear();
            _cacheCotizaciones = null;
        }
    }
}