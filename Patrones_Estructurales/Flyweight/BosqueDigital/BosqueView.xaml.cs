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
using Patrones_Estructurales.Flyweight;

namespace Patrones_Estructurales.Flyweight.BosqueDigital
{
    /// <summary>
    /// Interaction logic for BosqueView.xaml
    /// </summary>
    public partial class BosqueView : UserControl
    {
        private List<Arbol> _bosque = new List<Arbol>();
        private Random _random = new Random();
        public BosqueView()
        {
            InitializeComponent();
        }
        

        private void PlantarArboles(int cantidad)
        {
            string nombre = (CmbTipos.SelectedItem as ComboBoxItem).Content.ToString();
            Color color = nombre switch
            {
                "Roble" => Colors.ForestGreen,
                "Pino" => Colors.DarkGreen,
                "Cerezo" => Colors.Pink,
                _ => Colors.Green
            };

            // Obtenemos el tipo (Flyweight) desde la fábrica
            TipoArbol tipo = ArbolFactory.GetTipoArbol(nombre, color);

            for (int i = 0; i < cantidad; i++)
            {
                double x = _random.Next(20, (int)CanvasBosque.ActualWidth - 20);
                double y = _random.Next(20, (int)CanvasBosque.ActualHeight - 20);

                _bosque.Add(new Arbol(x, y, tipo));
            }

            ActualizarDibujo();
        }
        private void ActualizarDibujo()
        {
            // Creamos un Visual para dibujar de forma eficiente
            DrawingVisual visual = new DrawingVisual();
            using (DrawingContext dc = visual.RenderOpen())
            {
                foreach (var arbol in _bosque)
                {
                    arbol.Renderizar(dc);
                }
            }

            // Mostramos el dibujo en el Canvas
            CanvasBosque.Children.Clear();
            CanvasBosque.Children.Add(new VisualHost { Visual = visual });

            TxtStats.Text = $"Árboles: {_bosque.Count:N0} | Tipos en Memoria: {ArbolFactory.GetCantidadTipos()}";
        }

        private void BtnPlantarUno_Click(object sender, RoutedEventArgs e) => PlantarArboles(1);
        private void BtnPlantarMil_Click(object sender, RoutedEventArgs e) => PlantarArboles(1000);

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            _bosque.Clear();
            ArbolFactory.Reiniciar();
            CanvasBosque.Children.Clear();
            ActualizarDibujo();
        }

    }
}
