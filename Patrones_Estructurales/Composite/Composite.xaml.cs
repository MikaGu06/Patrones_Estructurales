using Patrones_Estructurales.Composite.Sistema_de_Archivos;
using System;
using System.Collections.Generic;
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

namespace Patrones_Estructurales.Composite
{
    /// <summary>
    /// Interaction logic for Composite.xaml
    /// </summary>
    public partial class Composite : UserControl
    {
        public Composite()
        {
            InitializeComponent();
            ContenedorComposite.Content = new SistemaArchivosView();
        }

        private void BtnSistemaArchivos_Click(object sender, RoutedEventArgs e)
        {
            // Ejemplo 1: Sistema de Archivos
            ContenedorComposite.Content = new SistemaArchivosView();
        }

        private void BtnOrganigrama_Click(object sender, RoutedEventArgs e)
        {
            // Ejemplo 2: Organigrama de Empresa
            ContenedorComposite.Content = new OrganigramaEmpresa.OrganigramaView();
        }

        private void BtnMenus_Click(object sender, RoutedEventArgs e)
        {
            // Ejemplo 3: Menús de Interfaz
            ContenedorComposite.Content = new MenusUI.MenusUIView();
        }
    }
}
