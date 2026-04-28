using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Patrones_Estructurales.Bridge.Ejercicio1_Dispositivos
{
    public partial class DispositivoBridge : UserControl
    {
        private DispositivoViewModel _viewModel;

        public DispositivoBridge()
        {
            InitializeComponent();
            _viewModel = new DispositivoViewModel();
            DataContext = _viewModel;
        }

        private void BtnTemaAzul_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.CambiarEstilo(_viewModel.TemaAzul);
        }

        private void BtnTemaGris_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.CambiarEstilo(_viewModel.TemaGris);
        }

        private void BtnTemaOscuro_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.CambiarEstilo(_viewModel.TemaOscuro);
        }

        private void BtnAccion_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var dispositivo = button?.Tag as Dispositivo;
            dispositivo?.AccionPrincipal();
        }
    }

    // Convertidor para el color del texto del estado
    public class EstadoColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? Brushes.LightGreen : Brushes.OrangeRed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // Convertidor para el fondo del estado
    public class EstadoFondoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? new SolidColorBrush(Color.FromRgb(40, 167, 69)) : new SolidColorBrush(Color.FromRgb(220, 53, 69));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}