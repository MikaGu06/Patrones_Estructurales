using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Patrones_Estructurales.Proxy.Ejercicio3_ProxyRemoto
{
    public partial class ProxyRemoto : UserControl
    {
        private ProxyServicioWeb _proxy;

        public ProxyRemoto()
        {
            InitializeComponent();
            _proxy = new ProxyServicioWeb();
            DataContext = _proxy;
        }

        private async void BtnObtenerClima_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtCiudad.Text))
            {
                MessageBox.Show("Ingresa una ciudad", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            TxtResultadoClima.Text = "🌐 Consultando clima real...";
            TxtResultadoClima.Foreground = Brushes.Yellow;

            string resultado = await _proxy.ObtenerClima(TxtCiudad.Text);

            TxtResultadoClima.Text = resultado;
            TxtResultadoClima.Foreground = resultado.StartsWith("📦 [CACHE]") ? Brushes.LightGreen : Brushes.White;
        }

        private async void BtnObtenerCotizaciones_Click(object sender, RoutedEventArgs e)
        {
            // Mostrar carga
            var textos = new[] { TxtCotizacionBOB, TxtCotizacionARS, TxtCotizacionCLP,
                                 TxtCotizacionPEN, TxtCotizacionBRL, TxtCotizacionEUR };
            foreach (var txt in textos)
            {
                txt.Text = "Cargando...";
                txt.Foreground = Brushes.Yellow;
            }

            var cotizaciones = await _proxy.ObtenerCotizaciones();

            // Actualizar UI
            if (cotizaciones.ContainsKey("BOB"))
            {
                TxtCotizacionBOB.Text = $"🇧🇴 Boliviano (BOB): {cotizaciones["BOB"]:F2} Bs";
                TxtCotizacionBOB.Foreground = cotizaciones["BOB"] > 0 ? Brushes.LightGreen : Brushes.Red;
            }

            if (cotizaciones.ContainsKey("ARS"))
            {
                TxtCotizacionARS.Text = $"🇦🇷 Peso Argentino (ARS): {cotizaciones["ARS"]:F2} $";
                TxtCotizacionARS.Foreground = Brushes.White;
            }

            if (cotizaciones.ContainsKey("CLP"))
            {
                TxtCotizacionCLP.Text = $"🇨🇱 Peso Chileno (CLP): {cotizaciones["CLP"]:F0} $";
                TxtCotizacionCLP.Foreground = Brushes.White;
            }

            if (cotizaciones.ContainsKey("PEN"))
            {
                TxtCotizacionPEN.Text = $"🇵🇪 Sol Peruano (PEN): {cotizaciones["PEN"]:F2} S/";
                TxtCotizacionPEN.Foreground = Brushes.White;
            }

            if (cotizaciones.ContainsKey("BRL"))
            {
                TxtCotizacionBRL.Text = $"🇧🇷 Real Brasileño (BRL): {cotizaciones["BRL"]:F2} R$";
                TxtCotizacionBRL.Foreground = Brushes.White;
            }

            if (cotizaciones.ContainsKey("EUR"))
            {
                TxtCotizacionEUR.Text = $"🇪🇺 Euro (EUR): {cotizaciones["EUR"]:F2} €";
                TxtCotizacionEUR.Foreground = Brushes.White;
            }
        }

        private void BtnLimpiarCache_Click(object sender, RoutedEventArgs e)
        {
            _proxy.LimpiarCache();
            MessageBox.Show("Cache limpiado correctamente.\nLas próximas consultas irán a las APIs reales.",
                "Proxy Remoto", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}