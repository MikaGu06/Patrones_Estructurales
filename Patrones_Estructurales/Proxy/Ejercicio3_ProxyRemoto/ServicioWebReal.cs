using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Patrones_Estructurales.Proxy.Ejercicio3_ProxyRemoto
{
    public interface IServicioWeb
    {
        Task<string> ObtenerClima(string ciudad);
        Task<Dictionary<string, decimal>> ObtenerCotizaciones();
        string EstadoConexion { get; }
    }

    public class ServicioWebReal : IServicioWeb
    {
        private readonly HttpClient _httpClient;
        private string _estadoConexion = "Listo";

        public ServicioWebReal()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(15);
        }

        public string EstadoConexion => _estadoConexion;

        public async Task<string> ObtenerClima(string ciudad)
        {
            try
            {
                _estadoConexion = "Consultando clima real...";

                string ciudadLimpia = ciudad.Trim();
                string ciudadUrl = ciudadLimpia.Replace(" ", "+");

                string[] urls = new string[]
                {
                    $"https://wttr.in/{ciudadUrl}?format=%C+%t+%w&lang=es",
                    $"https://wttr.in/{ciudadLimpia}?format=%C+%t+%w&lang=es",
                    $"https://wttr.in/{System.Net.WebUtility.UrlEncode(ciudadLimpia)}?format=%C+%t+%w&lang=es"
                };

                string respuesta = null;

                foreach (string url in urls)
                {
                    try
                    {
                        respuesta = await _httpClient.GetStringAsync(url);
                        if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("Unknown") && !respuesta.Contains("We did not find"))
                        {
                            break;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }

                _estadoConexion = "Conectado";

                if (string.IsNullOrEmpty(respuesta) || respuesta.Contains("Unknown") || respuesta.Contains("We did not find"))
                {
                    return $"No se encontro la ciudad: '{ciudad}'\nSugerencias: La Paz, Santa Cruz, Cochabamba, Sucre, Potosi, Tarija, Oruro";
                }

                respuesta = respuesta.Trim();

                return $"{ciudadLimpia}: {respuesta}";
            }
            catch (Exception ex)
            {
                _estadoConexion = "Error de conexion";
                return $"Error al obtener clima: {ex.Message}";
            }
        }

        public async Task<Dictionary<string, decimal>> ObtenerCotizaciones()
        {
            var cotizaciones = new Dictionary<string, decimal>();

            try
            {
                _estadoConexion = "Consultando cotizaciones reales...";

                string url = "https://api.exchangerate-api.com/v4/latest/USD";
                string respuesta = await _httpClient.GetStringAsync(url);

                using JsonDocument doc = JsonDocument.Parse(respuesta);
                var rates = doc.RootElement.GetProperty("rates");

                cotizaciones["BOB"] = rates.GetProperty("BOB").GetDecimal();
                cotizaciones["ARS"] = rates.GetProperty("ARS").GetDecimal();
                cotizaciones["CLP"] = rates.GetProperty("CLP").GetDecimal();
                cotizaciones["PEN"] = rates.GetProperty("PEN").GetDecimal();
                cotizaciones["BRL"] = rates.GetProperty("BRL").GetDecimal();
                cotizaciones["EUR"] = rates.GetProperty("EUR").GetDecimal();

                _estadoConexion = "Conectado";
            }
            catch (Exception ex)
            {
                _estadoConexion = "Error de conexion";
                cotizaciones["Error"] = 0;
            }

            return cotizaciones;
        }
    }
}