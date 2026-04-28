using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Shapes;

namespace Patrones_Estructurales.Bridge.Ejercicio3_VisorFiguras
{
    // Abstracción del Bridge - Figura base
    public abstract class Figura : INotifyPropertyChanged
    {
        protected IRenderizador _renderizador;
        protected double _tamaño;
        private Shape _figuraRenderizada;

        public event PropertyChangedEventHandler PropertyChanged;

        protected Figura(IRenderizador renderizador, double tamaño)
        {
            _renderizador = renderizador;
            _tamaño = tamaño;
        }

        public IRenderizador Renderizador
        {
            get => _renderizador;
            set
            {
                _renderizador = value;
                OnPropertyChanged(nameof(Renderizador));
                ActualizarFigura();
            }
        }

        public double Tamaño
        {
            get => _tamaño;
            set
            {
                _tamaño = value;
                OnPropertyChanged(nameof(Tamaño));
                ActualizarFigura();
            }
        }

        public Shape FiguraRenderizada
        {
            get => _figuraRenderizada;
            set
            {
                _figuraRenderizada = value;
                OnPropertyChanged(nameof(FiguraRenderizada));
            }
        }

        public abstract string Tipo { get; }
        public abstract string Icono { get; }

        protected void ActualizarFigura()
        {
            FiguraRenderizada = _renderizador?.RenderizarFigura(Tipo, _tamaño);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Abstracciones extendidas
    public class FiguraCirculo : Figura
    {
        public FiguraCirculo(IRenderizador renderizador, double tamaño) : base(renderizador, tamaño)
        {
            ActualizarFigura();
        }

        public override string Tipo => "Circulo";
        public override string Icono => "●";
    }

    public class FiguraRectangulo : Figura
    {
        public FiguraRectangulo(IRenderizador renderizador, double tamaño) : base(renderizador, tamaño)
        {
            ActualizarFigura();
        }

        public override string Tipo => "Rectangulo";
        public override string Icono => "■";
    }

    public class FiguraTriangulo : Figura
    {
        public FiguraTriangulo(IRenderizador renderizador, double tamaño) : base(renderizador, tamaño)
        {
            ActualizarFigura();
        }

        public override string Tipo => "Triangulo";
        public override string Icono => "▲";
    }

    // View Model
    public class VisorFigurasViewModel
    {
        public ObservableCollection<Figura> Figuras { get; set; }
        public IRenderizador RenderizadorActual { get; set; }
        public IRenderizador RenderSuave { get; }
        public IRenderizador RenderPixelado { get; }
        public IRenderizador RenderAltoContraste { get; }

        public VisorFigurasViewModel()
        {
            RenderSuave = new RenderSuave();
            RenderPixelado = new RenderPixelado();
            RenderAltoContraste = new RenderAltoContraste();
            RenderizadorActual = RenderSuave;

            Figuras = new ObservableCollection<Figura>
            {
                new FiguraCirculo(RenderizadorActual, 120),
                new FiguraRectangulo(RenderizadorActual, 120),
                new FiguraTriangulo(RenderizadorActual, 120)
            };
        }

        public void CambiarRenderizador(IRenderizador nuevoRenderizador)
        {
            RenderizadorActual = nuevoRenderizador;
            foreach (var figura in Figuras)
            {
                figura.Renderizador = nuevoRenderizador;
            }
        }
    }
}