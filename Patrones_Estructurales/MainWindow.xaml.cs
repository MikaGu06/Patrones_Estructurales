using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Patrones_Estructurales
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnAdapter_Click(object sender, RoutedEventArgs e)
        {
            ContenedorContenido.Content = new Adapterr.Adapter();
        }

        private void BtnDecorator_Click(object sender, RoutedEventArgs e)
        {
            ContenedorContenido.Content = new Decorator.Decorator();
        }

        private void BtnFacade_Click(object sender, RoutedEventArgs e)
        {
            ContenedorContenido.Content = new Facade.Facade();
        }
    }
}