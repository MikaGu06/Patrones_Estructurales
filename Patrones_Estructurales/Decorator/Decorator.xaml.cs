using System.Windows;
using System.Windows.Controls;

namespace Patrones_Estructurales.Decorator
{
    public partial class Decorator : UserControl
    {
        public Decorator()
        {
            InitializeComponent();
            ContenedorDecorator.Content = new Validaciones.Validaciones();
        }

        private void BtnValidaciones_Click(object sender, RoutedEventArgs e)
        {
            ContenedorDecorator.Content = new Validaciones.Validaciones();
        }

        private void BtnControles_Click(object sender, RoutedEventArgs e)
        {
            ContenedorDecorator.Content = new ExtenderControles.ExtenderControles();
        }

        private void BtnBase_Click(object sender, RoutedEventArgs e)
        {
            ContenedorDecorator.Content = new ComportamientoAdicional.ComportamientoAdicional();
        }
    }
}