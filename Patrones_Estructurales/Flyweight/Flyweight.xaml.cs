using Patrones_Estructurales.Flyweight.BosqueDigital;
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

namespace Patrones_Estructurales.Flyweight
{
    /// <summary>
    /// Interaction logic for Flyweight.xaml
    /// </summary>
    public partial class Flyweight : UserControl
    {
        public Flyweight()
        {
            InitializeComponent();
            ContenedorFlyweight.Content = new BosqueView();
        }

        private void BtnBosque_Click(object sender, RoutedEventArgs e)
        {
            //Ejemplo 1
            ContenedorFlyweight.Content = new BosqueView();
        }

        private void BtnParticulas_Click(object sender, RoutedEventArgs e)
        {
            //Ejemplo 2
            ContenedorFlyweight.Content = new Particulas.ParticulasView();
        }

        private void BtnDocumentos_Click(object sender, RoutedEventArgs e)
        {
            //Ejemplo 3 
            ContenedorFlyweight.Content = new EditorTexto.EditorView();
        }
    }
}
