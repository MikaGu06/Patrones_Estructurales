using Patrones_Estructurales.Facade.GestorCentral;
using System.Windows;
using System.Windows.Controls;

namespace Patrones_Estructurales.Facade
{
    public partial class Facade : UserControl
    {
        public Facade()
        {
            InitializeComponent();
            ContenedorFacade.Content = new AccesoServicios.AccesoServicios();
        }

        private void BtnAcceso_Click(object sender, RoutedEventArgs e)
        {
            ContenedorFacade.Content = new AccesoServicios.AccesoServicios();
        }

        private void BtnMultiples_Click(object sender, RoutedEventArgs e)
        {
            ContenedorFacade.Content = new MultiplesServicios.MultiplesServicios();
        }

        private void BtnGestor_Click(object sender, RoutedEventArgs e)
        {
            ContenedorFacade.Content = new GestorCentral.GestorCentral();
        }
    }
}