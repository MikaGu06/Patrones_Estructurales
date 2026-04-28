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

namespace Patrones_Estructurales.Flyweight.EditorTexto
{
    /// <summary>
    /// Interaction logic for EditorView.xaml
    /// </summary>
    public partial class EditorView : UserControl
    {
        private List<PosicionCaracter> _documento = new List<PosicionCaracter>();

        private double _cursorX = 10;
        private double _cursorY = 10;
        private Random _random = new Random();

        public EditorView()
        {
            InitializeComponent();
        }

        
        private void ProcesarTexto(string texto)
        {
            string fuente = (CmbFuente.SelectedItem as ComboBoxItem).Content.ToString();
            double tamaño = double.Parse((CmbTamaño.SelectedItem as ComboBoxItem).Content.ToString());
            double avanceX = tamaño * 0.6;

            foreach (char c in texto)
            {
                // Si llegamos al borde derecho (usamos un ancho fijo o el del scroll)
                if (_cursorX > 800)
                {
                    _cursorX = 10;
                    _cursorY += tamaño * 1.5;
                }

                var flyweight = CaracterFactory.GetCaracter(c, fuente, tamaño);
                _documento.Add(new PosicionCaracter(_cursorX, _cursorY, flyweight));

                _cursorX += avanceX;
            }

            if (_cursorY + 100 > CanvasEditor.Height)
            {
                CanvasEditor.Height = _cursorY + 100;
            }

            ActualizarPantalla();
        }
     

        private void ActualizarPantalla()
        {
            DrawingVisual visual = new DrawingVisual();
            using (DrawingContext dc = visual.RenderOpen())
            {
                foreach (var caracter in _documento)
                {
                    caracter.Renderizar(dc);
                }
            }

            CanvasEditor.Children.Clear();
            // Utilizamos el VisualHost que creamos en el archivo independiente
            CanvasEditor.Children.Add(new VisualHost { Visual = visual });

            TxtStats.Text = $"Letras: {_documento.Count:N0} | Tipos en Memoria: {CaracterFactory.ObtenerTotalFlyweights()}";
        }

        private void BtnEscribir_Click(object sender, RoutedEventArgs e)
        {
            string texto = TxtEntrada.Text;
            if (string.IsNullOrEmpty(texto)) return;

            ProcesarTexto(texto);
            TxtEntrada.Clear();
        }

        private void BtnGenerar10k_Click(object sender, RoutedEventArgs e)
        {
            char[] letras = new char[10000];
            string caracteresPosibles = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789 ";

            for (int i = 0; i < 10000; i++)
            {
                letras[i] = caracteresPosibles[_random.Next(caracteresPosibles.Length)];
            }

            ProcesarTexto(new string(letras));
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            _documento.Clear();
            CaracterFactory.Reiniciar();
            CanvasEditor.Children.Clear();

            _cursorX = 10;
            _cursorY = 10;
            CanvasEditor.Height = 500;

            ActualizarPantalla();
        }
    }
}
