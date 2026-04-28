using System.Windows.Media;

namespace Patrones_Estructurales.Bridge.Ejercicio1_Dispositivos
{
    public interface IEstiloVisual
    {
        Brush ColorFondo { get; }
        Brush ColorTexto { get; }
        Brush ColorBorde { get; }
        Brush ColorBoton { get; }
        Brush ColorBotonHover { get; }
        string Nombre { get; }
    }

    // Tema AZUL - Basado en tu paleta
    public class TemaAzul : IEstiloVisual
    {
        public Brush ColorFondo => new SolidColorBrush(Color.FromRgb(38, 34, 51));     // #262233
        public Brush ColorTexto => new SolidColorBrush(Color.FromRgb(232, 226, 242));  // #E8E2F2
        public Brush ColorBorde => new SolidColorBrush(Color.FromRgb(127, 115, 161));  // #7F73A1
        public Brush ColorBoton => new SolidColorBrush(Color.FromRgb(127, 115, 161));  // #7F73A1
        public Brush ColorBotonHover => new SolidColorBrush(Color.FromRgb(155, 141, 190)); // #9B8DBE
        public string Nombre => "TEMA AZUL";
    }

    // Tema GRIS - Neutro y clásico
    public class TemaGris : IEstiloVisual
    {
        public Brush ColorFondo => new SolidColorBrush(Color.FromRgb(80, 80, 95));
        public Brush ColorTexto => new SolidColorBrush(Color.FromRgb(232, 226, 242));
        public Brush ColorBorde => new SolidColorBrush(Color.FromRgb(114, 108, 128));
        public Brush ColorBoton => new SolidColorBrush(Color.FromRgb(100, 95, 115));
        public Brush ColorBotonHover => new SolidColorBrush(Color.FromRgb(120, 115, 135));
        public string Nombre => "TEMA GRIS";
    }

    // Tema OSCURO - Minimalista
    public class TemaOscuro : IEstiloVisual
    {
        public Brush ColorFondo => new SolidColorBrush(Color.FromRgb(15, 15, 20));
        public Brush ColorTexto => Brushes.WhiteSmoke;
        public Brush ColorBorde => new SolidColorBrush(Color.FromRgb(80, 75, 95));
        public Brush ColorBoton => new SolidColorBrush(Color.FromRgb(50, 50, 65));
        public Brush ColorBotonHover => new SolidColorBrush(Color.FromRgb(75, 75, 95));
        public string Nombre => "TEMA OSCURO";
    }
}