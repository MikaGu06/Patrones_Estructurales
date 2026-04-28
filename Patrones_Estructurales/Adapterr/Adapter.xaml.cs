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

namespace Patrones_Estructurales.Adapterr
{
    /// <summary>
    /// Lógica de interacción para Adapter.xaml
    /// </summary>
    public partial class Adapter : UserControl
    {
        public Adapter()
        {
            InitializeComponent();
        }

        private void BtnLibreriaExterna_Click(object sender, RoutedEventArgs e)
        {
            ContenedorAdapter.Content = new LibreriaExterna.LibreriaExterna();
        }

        private void BtnFormato_Click(object sender, RoutedEventArgs e)
        {
            ContenedorAdapter.Content = new FormatoJsonXML.FormatoJsonXML();
        }

        private void BtnViewModels_Click(object sender, RoutedEventArgs e)
        {
            ContenedorAdapter.Content = new ViewModels.ViewModels();
        }
    }
}
