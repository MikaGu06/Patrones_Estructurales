using System.Windows;
using System.Windows.Controls;
using Patrones_Estructurales.Proxy.Ejercicio1_ControlAcceso;
using Patrones_Estructurales.Proxy.Ejercicio2_CargaDiferida;
using Patrones_Estructurales.Proxy.Ejercicio3_ProxyRemoto;

namespace Patrones_Estructurales.Proxy
{
    public partial class MenuProxy : UserControl
    {
        public MenuProxy()
        {
            InitializeComponent();
            ContenidoEjercicio.Content = new ControlAccesoProxy();
        }

        private void BtnProxyEjercicio1_Click(object sender, RoutedEventArgs e)
        {
            ContenidoEjercicio.Content = new ControlAccesoProxy();
        }

        private void BtnProxyEjercicio2_Click(object sender, RoutedEventArgs e)
        {
            ContenidoEjercicio.Content = new CargaDiferidaProxy();
        }

        private void BtnProxyEjercicio3_Click(object sender, RoutedEventArgs e)
        {
            ContenidoEjercicio.Content = new ProxyRemoto();
        }
    }
}