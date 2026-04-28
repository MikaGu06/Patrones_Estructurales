using System.Windows.Media;

namespace Patrones_Estructurales.Bridge.Ejercicio2_Reproductor
{
    // Interfaz del Bridge para temas visuales
    public interface ITemaVisual
    {
        Brush ColorFondoPrincipal { get; }
        Brush ColorFondoSecundario { get; }
        Brush ColorTexto { get; }
        Brush ColorTextoSubtitulo { get; }
        Brush ColorBoton { get; }
        Brush ColorBotonHover { get; }
        Brush ColorBorde { get; }
        Brush ColorProgreso { get; }
        string Nombre { get; }
    }

    // Tema CLARO
    public class TemaClaro : ITemaVisual
    {
        public Brush ColorFondoPrincipal => new SolidColorBrush(Color.FromRgb(240, 240, 245));
        public Brush ColorFondoSecundario => new SolidColorBrush(Color.FromRgb(255, 255, 255));
        public Brush ColorTexto => new SolidColorBrush(Color.FromRgb(30, 30, 40));
        public Brush ColorTextoSubtitulo => new SolidColorBrush(Color.FromRgb(100, 100, 120));
        public Brush ColorBoton => new SolidColorBrush(Color.FromRgb(200, 200, 210));
        public Brush ColorBotonHover => new SolidColorBrush(Color.FromRgb(170, 170, 185));
        public Brush ColorBorde => new SolidColorBrush(Color.FromRgb(180, 180, 195));
        public Brush ColorProgreso => new SolidColorBrush(Color.FromRgb(127, 115, 161));
        public string Nombre => "TEMA CLARO";
    }

    // Tema OSCURO
    public class TemaOscuro : ITemaVisual
    {
        public Brush ColorFondoPrincipal => new SolidColorBrush(Color.FromRgb(25, 25, 35));
        public Brush ColorFondoSecundario => new SolidColorBrush(Color.FromRgb(40, 40, 50));
        public Brush ColorTexto => new SolidColorBrush(Color.FromRgb(230, 230, 240));
        public Brush ColorTextoSubtitulo => new SolidColorBrush(Color.FromRgb(150, 150, 170));
        public Brush ColorBoton => new SolidColorBrush(Color.FromRgb(60, 60, 75));
        public Brush ColorBotonHover => new SolidColorBrush(Color.FromRgb(85, 85, 105));
        public Brush ColorBorde => new SolidColorBrush(Color.FromRgb(70, 70, 90));
        public Brush ColorProgreso => new SolidColorBrush(Color.FromRgb(127, 115, 161));
        public string Nombre => "TEMA OSCURO";
    }
}