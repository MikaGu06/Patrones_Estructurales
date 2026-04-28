using System.Windows;
using System.Windows.Controls;

namespace Patrones_Estructurales.Bridge.Ejercicio3_VisorFiguras
{
    public partial class VisorFigurasBridge : UserControl
    {
        private VisorFigurasViewModel _viewModel;

        public VisorFigurasBridge()
        {
            InitializeComponent();
            _viewModel = new VisorFigurasViewModel();
            DataContext = _viewModel;
        }

        private void BtnRenderSuave_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.CambiarRenderizador(_viewModel.RenderSuave);
        }

        private void BtnRenderPixelado_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.CambiarRenderizador(_viewModel.RenderPixelado);
        }

        private void BtnRenderAltoContraste_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.CambiarRenderizador(_viewModel.RenderAltoContraste);
        }
    }
}