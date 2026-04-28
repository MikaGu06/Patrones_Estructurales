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
using Patrones_Estructurales.Flyweight.BosqueDigital;

namespace Patrones_Estructurales.Flyweight.Particulas
{
    /// <summary>
    /// Interaction logic for ParticulasView.xaml
    /// </summary>
    public partial class ParticulasView : UserControl
    {
        private List<Particula> _particulas = new List<Particula>();
        private Random _random = new Random();
        public ParticulasView()
        {
            InitializeComponent();
        }

        private void BtnGenerar_Click(object sender, RoutedEventArgs e)
        {
            Color colorSeleccionado = (CmbColor.Text) switch
            {
                "Rojo" => Colors.Red,
                "Azul" => Colors.Cyan,
                "Amarillo" => Colors.Yellow,
                _ => Colors.White
            };

            double tamaño = SldTamaño.Value;

            // 1. Obtenemos el objeto compartido (Flyweight)
            var flyweight = ParticulaFactory.GetParticula(colorSeleccionado, tamaño);

            // 2. Creamos 5000 partículas usando ese único objeto visual
            for (int i = 0; i < 5000; i++)
            {
                double x = _random.NextDouble() * CanvasParticulas.ActualWidth;
                double y = _random.NextDouble() * CanvasParticulas.ActualHeight;

                _particulas.Add(new Particula(x, y, flyweight));
            }

            ActualizarPantalla();
        }

        private void ActualizarPantalla()
        {
            CanvasParticulas.Children.Clear();
            DrawingVisual visual = new DrawingVisual();
            using (DrawingContext dc = visual.RenderOpen())
            {
                foreach (var p in _particulas) p.Dibujar(dc);
            }

            CanvasParticulas.Children.Clear();
            // Usamos la misma clase VisualHost que creamos en el ejemplo anterior
            CanvasParticulas.Children.Add(new VisualHost { Visual = visual });

            TxtStats.Text = $"Partículas: {_particulas.Count:N0} | Flyweights: {ParticulaFactory.CantidadFlyweights()}";
        }


        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            _particulas.Clear();
            ParticulaFactory.Reiniciar();
            CanvasParticulas.Children.Clear();
            ActualizarPantalla();
        }
    }
}
