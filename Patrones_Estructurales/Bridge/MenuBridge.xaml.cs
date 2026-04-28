using Patrones_Estructurales.Bridge.Ejercicio1_Dispositivos;
using Patrones_Estructurales.Bridge.Ejercicio2_Reproductor;
using Patrones_Estructurales.Bridge.Ejercicio3_VisorFiguras;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Patrones_Estructurales.Bridge
{
    public partial class MenuBridge : UserControl
    {
        public MenuBridge()
        {
            InitializeComponent();

            // Cargar el primer ejercicio por defecto
            ContenidoEjercicio.Content = new DispositivoBridge();
        }

        private void BtnBridgeEjercicio1_Click(object sender, RoutedEventArgs e)
        {
            ContenidoEjercicio.Content = new DispositivoBridge();
        }

        private void BtnBridgeEjercicio2_Click(object sender, RoutedEventArgs e)
        {
            ContenidoEjercicio.Content = new ReproductorBridge();
        }

        private void BtnBridgeEjercicio3_Click(object sender, RoutedEventArgs e)
        {
            ContenidoEjercicio.Content = new VisorFigurasBridge();
        }
    }
}