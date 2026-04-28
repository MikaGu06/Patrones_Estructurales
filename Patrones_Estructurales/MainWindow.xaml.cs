using Patrones_Estructurales.Proxy;
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

        private void BtnBridge_Click(object sender, RoutedEventArgs e)
        {
            var menuBridge = new Patrones_Estructurales.Bridge.MenuBridge();
            ContenedorContenido.Content = menuBridge;
        }

        private void BtnProxy_Click(object sender, RoutedEventArgs e)
        {
            var menuProxy = new Patrones_Estructurales.Proxy.MenuProxy();
            ContenedorContenido.Content = menuProxy;
        }

        private void BtnFacade_Click(object sender, RoutedEventArgs e)
        {
            ContenedorContenido.Content = new Facade.Facade();
        }

        private void BtnAdapter_Click(object sender, RoutedEventArgs e)
        {
            ContenedorContenido.Content = new Adapterr.Adapter();
        }

        private void BtnComposite_Click(object sender, RoutedEventArgs e)
        {
            ContenedorContenido.Content = new Composite.Composite();
        }

        private void BtnDecorator_Click(object sender, RoutedEventArgs e)
        {
            ContenedorContenido.Content = new Decorator.Decorator();
        }

        private void BtnFacade_Click_1(object sender, RoutedEventArgs e)
        {
            ContenedorContenido.Content = new Facade.Facade();
        }

        private void BtnFlyweight_Click(object sender, RoutedEventArgs e)
        {
            ContenedorContenido.Content = new Flyweight.Flyweight();
        }
    }
}