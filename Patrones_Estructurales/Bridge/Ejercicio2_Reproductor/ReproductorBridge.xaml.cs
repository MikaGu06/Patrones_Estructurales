using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Patrones_Estructurales.Bridge.Ejercicio2_Reproductor
{
    public partial class ReproductorBridge : UserControl
    {
        private ReproductorViewModel _viewModel;

        public ReproductorBridge()
        {
            InitializeComponent();
            _viewModel = new ReproductorViewModel();
            DataContext = _viewModel;
        }

        private void BtnTemaClaro_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.CambiarATemaClaro();
        }

        private void BtnTemaOscuro_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.CambiarATemaOscuro();
        }

        private void BtnPlayPause_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Reproductor.PlayPause();
        }

        private void BtnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Reproductor.Siguiente();
        }

        private void BtnAnterior_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Reproductor.Anterior();
        }

        private void BtnSubirVolumen_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Reproductor.SubirVolumen();
        }

        private void BtnBajarVolumen_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Reproductor.BajarVolumen();
        }

        private void CancionSeleccionada(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;
            var cancion = border?.DataContext as string;
            if (cancion != null)
            {
                var indice = _viewModel.Reproductor.ListaCanciones.ToList().IndexOf(cancion);
                _viewModel.Reproductor.SeleccionarCancion(indice);
            }
        }
    }
}