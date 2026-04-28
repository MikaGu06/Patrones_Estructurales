using System.Windows.Media;
using System.Windows.Shapes;

namespace Patrones_Estructurales.Bridge.Ejercicio3_VisorFiguras
{
    // Interfaz del Bridge para motores de renderizado
    public interface IRenderizador
    {
        Shape RenderizarFigura(string tipoFigura, double size);
        string Nombre { get; }
        string Descripcion { get; }
    }

    // Renderizado SUAVE - Con sombras y bordes redondeados
    public class RenderSuave : IRenderizador
    {
        public Shape RenderizarFigura(string tipoFigura, double size)
        {
            Shape figura = null;

            switch (tipoFigura)
            {
                case "Circulo":
                    figura = new Ellipse
                    {
                        Width = size,
                        Height = size,
                        Fill = new SolidColorBrush(Color.FromRgb(0, 150, 255)),
                        Stroke = new SolidColorBrush(Color.FromRgb(0, 100, 200)),
                        StrokeThickness = 3,
                        Effect = new System.Windows.Media.Effects.DropShadowEffect
                        {
                            BlurRadius = 15,
                            ShadowDepth = 5,
                            Opacity = 0.5
                        }
                    };
                    break;

                case "Rectangulo":
                    figura = new Rectangle
                    {
                        Width = size,
                        Height = size * 0.7,
                        Fill = new SolidColorBrush(Color.FromRgb(75, 180, 75)),
                        Stroke = new SolidColorBrush(Color.FromRgb(50, 140, 50)),
                        StrokeThickness = 3,
                        RadiusX = 10,
                        RadiusY = 10,
                        Effect = new System.Windows.Media.Effects.DropShadowEffect
                        {
                            BlurRadius = 15,
                            ShadowDepth = 5,
                            Opacity = 0.5
                        }
                    };
                    break;

                case "Triangulo":
                    figura = new Polygon
                    {
                        Points = new PointCollection
                        {
                            new System.Windows.Point(size / 2, 0),
                            new System.Windows.Point(0, size),
                            new System.Windows.Point(size, size)
                        },
                        Fill = new SolidColorBrush(Color.FromRgb(255, 100, 100)),
                        Stroke = new SolidColorBrush(Color.FromRgb(200, 60, 60)),
                        StrokeThickness = 3,
                        Effect = new System.Windows.Media.Effects.DropShadowEffect
                        {
                            BlurRadius = 15,
                            ShadowDepth = 5,
                            Opacity = 0.5
                        }
                    };
                    break;
            }

            return figura;
        }

        public string Nombre => "Renderizado Suave";
        public string Descripcion => "Bordes redondeados, sombras suaves, colores vibrantes";
    }

    // Renderizado PIXELADO - Estilo retro 8-bits
    public class RenderPixelado : IRenderizador
    {
        public Shape RenderizarFigura(string tipoFigura, double size)
        {
            Shape figura = null;

            switch (tipoFigura)
            {
                case "Circulo":
                    figura = new Ellipse
                    {
                        Width = size,
                        Height = size,
                        Fill = new SolidColorBrush(Color.FromRgb(0, 0, 255)),
                        Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                        StrokeThickness = 4,
                        StrokeDashArray = new DoubleCollection { 2, 2 }
                    };
                    break;

                case "Rectangulo":
                    figura = new Rectangle
                    {
                        Width = size,
                        Height = size * 0.7,
                        Fill = new SolidColorBrush(Color.FromRgb(0, 255, 0)),
                        Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                        StrokeThickness = 4,
                        StrokeDashArray = new DoubleCollection { 2, 2 }
                    };
                    break;

                case "Triangulo":
                    figura = new Polygon
                    {
                        Points = new PointCollection
                        {
                            new System.Windows.Point(size / 2, 0),
                            new System.Windows.Point(0, size),
                            new System.Windows.Point(size, size)
                        },
                        Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0)),
                        Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                        StrokeThickness = 4,
                        StrokeDashArray = new DoubleCollection { 2, 2 }
                    };
                    break;
            }

            // Forzar renderizado pixelado (sin anti-aliasing)
            if (figura != null)
            {
                RenderOptions.SetEdgeMode(figura, EdgeMode.Aliased);
                RenderOptions.SetBitmapScalingMode(figura, BitmapScalingMode.NearestNeighbor);
            }

            return figura;
        }

        public string Nombre => "Renderizado Pixelado";
        public string Descripcion => "Estilo retro 8-bits, bordes pixelados, sin suavizado";
    }

    // Renderizado ALTO CONTRASTE - Para accesibilidad
    public class RenderAltoContraste : IRenderizador
    {
        public Shape RenderizarFigura(string tipoFigura, double size)
        {
            Shape figura = null;

            switch (tipoFigura)
            {
                case "Circulo":
                    figura = new Ellipse
                    {
                        Width = size,
                        Height = size,
                        Fill = Brushes.White,
                        Stroke = Brushes.Black,
                        StrokeThickness = 5
                    };
                    break;

                case "Rectangulo":
                    figura = new Rectangle
                    {
                        Width = size,
                        Height = size * 0.7,
                        Fill = Brushes.Black,
                        Stroke = Brushes.White,
                        StrokeThickness = 5
                    };
                    break;

                case "Triangulo":
                    figura = new Polygon
                    {
                        Points = new PointCollection
                        {
                            new System.Windows.Point(size / 2, 0),
                            new System.Windows.Point(0, size),
                            new System.Windows.Point(size, size)
                        },
                        Fill = Brushes.White,
                        Stroke = Brushes.Black,
                        StrokeThickness = 5
                    };
                    break;
            }

            return figura;
        }

        public string Nombre => "Alto Contraste";
        public string Descripcion => "Máximo contraste blanco/negro, bordes gruesos para accesibilidad";
    }
}