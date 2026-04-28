using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace Patrones_Estructurales.Bridge.Ejercicio2_Reproductor
{
    // Abstracción del Bridge - Reproductor base
    public abstract class Reproductor : INotifyPropertyChanged
    {
        protected ITemaVisual _tema;
        private bool _reproduciendo;
        private double _volumen;
        private double _progreso;
        private string _cancionActual;

        public event PropertyChangedEventHandler PropertyChanged;

        protected Reproductor(ITemaVisual tema)
        {
            _tema = tema;
            _reproduciendo = false;
            _volumen = 0.7;
            _progreso = 0;
            _cancionActual = "Ninguna canción seleccionada";
        }

        public ITemaVisual Tema
        {
            get => _tema;
            set
            {
                _tema = value;
                OnPropertyChanged(nameof(Tema));
                OnPropertyChanged(nameof(ColorFondoPrincipal));
                OnPropertyChanged(nameof(ColorFondoSecundario));
                OnPropertyChanged(nameof(ColorTexto));
                OnPropertyChanged(nameof(ColorTextoSubtitulo));
                OnPropertyChanged(nameof(ColorBoton));
                OnPropertyChanged(nameof(ColorBorde));
                OnPropertyChanged(nameof(ColorProgreso));
            }
        }

        public bool Reproduciendo
        {
            get => _reproduciendo;
            set
            {
                _reproduciendo = value;
                OnPropertyChanged(nameof(Reproduciendo));
                OnPropertyChanged(nameof(EstadoIcono));
                OnPropertyChanged(nameof(EstadoTexto));
            }
        }

        public double Volumen
        {
            get => _volumen;
            set
            {
                _volumen = Math.Max(0, Math.Min(1, value));
                OnPropertyChanged(nameof(Volumen));
                OnPropertyChanged(nameof(VolumenPorcentaje));
            }
        }

        public double Progreso
        {
            get => _progreso;
            set
            {
                _progreso = Math.Max(0, Math.Min(1, value));
                OnPropertyChanged(nameof(Progreso));
                OnPropertyChanged(nameof(TiempoActual));
            }
        }

        public string CancionActual
        {
            get => _cancionActual;
            set
            {
                _cancionActual = value;
                OnPropertyChanged(nameof(CancionActual));
            }
        }

        // Propiedades helper para binding
        public Brush ColorFondoPrincipal => Tema?.ColorFondoPrincipal;
        public Brush ColorFondoSecundario => Tema?.ColorFondoSecundario;
        public Brush ColorTexto => Tema?.ColorTexto;
        public Brush ColorTextoSubtitulo => Tema?.ColorTextoSubtitulo;
        public Brush ColorBoton => Tema?.ColorBoton;
        public Brush ColorBorde => Tema?.ColorBorde;
        public Brush ColorProgreso => Tema?.ColorProgreso;

        public string EstadoIcono => Reproduciendo ? "⏸" : "▶";
        public string EstadoTexto => Reproduciendo ? "REPRODUCIENDO" : "DETENIDO";
        public string VolumenPorcentaje => $"{(int)(Volumen * 100)}%";
        public string TiempoActual => $"{Math.Floor(Progreso * 3.5):mm\\:ss} / 03:30";

        public abstract void PlayPause();
        public abstract void Siguiente();
        public abstract void Anterior();
        public abstract void SubirVolumen();
        public abstract void BajarVolumen();

        public void CambiarTema(ITemaVisual nuevoTema)
        {
            Tema = nuevoTema;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Abstracción extendida - Reproductor de música
    public class ReproductorMusica : Reproductor
    {
        private string[] _listaCanciones = new string[]
        {
            "Cancion 1 - Artista A",
            "Cancion 2 - Artista B",
            "Cancion 3 - Artista C",
            "Cancion 4 - Artista D"
        };
        private int _indiceActual = -1;

        public ReproductorMusica(ITemaVisual tema) : base(tema)
        {
        }

        public string[] ListaCanciones => _listaCanciones;

        public void SeleccionarCancion(int indice)
        {
            if (indice >= 0 && indice < _listaCanciones.Length)
            {
                _indiceActual = indice;
                CancionActual = _listaCanciones[_indiceActual];
                Progreso = 0;
            }
        }

        public override void PlayPause()
        {
            if (CancionActual == "Ninguna canción seleccionada" && _indiceActual == -1)
            {
                SeleccionarCancion(0);
            }
            Reproduciendo = !Reproduciendo;
        }

        public override void Siguiente()
        {
            if (_listaCanciones.Length > 0)
            {
                _indiceActual = (_indiceActual + 1) % _listaCanciones.Length;
                CancionActual = _listaCanciones[_indiceActual];
                Progreso = 0;
                Reproduciendo = true;
            }
        }

        public override void Anterior()
        {
            if (_listaCanciones.Length > 0)
            {
                _indiceActual = (_indiceActual - 1 + _listaCanciones.Length) % _listaCanciones.Length;
                CancionActual = _listaCanciones[_indiceActual];
                Progreso = 0;
                Reproduciendo = true;
            }
        }

        public override void SubirVolumen()
        {
            Volumen += 0.1;
        }

        public override void BajarVolumen()
        {
            Volumen -= 0.1;
        }
    }

    // View Model
    public class ReproductorViewModel
    {
        public ReproductorMusica Reproductor { get; set; }
        public ITemaVisual TemaClaro { get; }
        public ITemaVisual TemaOscuro { get; }

        public ReproductorViewModel()
        {
            TemaClaro = new TemaClaro();
            TemaOscuro = new TemaOscuro();
            Reproductor = new ReproductorMusica(TemaClaro);
        }

        public void CambiarATemaClaro()
        {
            Reproductor.CambiarTema(TemaClaro);
        }

        public void CambiarATemaOscuro()
        {
            Reproductor.CambiarTema(TemaOscuro);
        }
    }
}