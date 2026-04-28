#nullable disable

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace Patrones_Estructurales.Bridge.Ejercicio1_Dispositivos
{
    // ========== ABSTRACCIÓN DEL BRIDGE ==========
    public abstract class Dispositivo : INotifyPropertyChanged
    {
        protected IEstiloVisual _estiloVisual;
        private bool _encendido;
        private string _nombre;

        public event PropertyChangedEventHandler PropertyChanged;

        protected Dispositivo(IEstiloVisual estiloVisual, string nombre)
        {
            _estiloVisual = estiloVisual;
            _nombre = nombre;
            _encendido = false;
        }

        public IEstiloVisual EstiloVisual
        {
            get => _estiloVisual;
            set
            {
                _estiloVisual = value;
                OnPropertyChanged(nameof(EstiloVisual));
                OnPropertyChanged(nameof(ColorFondo));
                OnPropertyChanged(nameof(ColorTexto));
                OnPropertyChanged(nameof(ColorBoton));
            }
        }

        public string Nombre
        {
            get => _nombre;
            set { _nombre = value; OnPropertyChanged(nameof(Nombre)); }
        }

        public bool Encendido
        {
            get => _encendido;
            set
            {
                _encendido = value;
                OnPropertyChanged(nameof(Encendido));
                OnPropertyChanged(nameof(EstadoTexto));
            }
        }

        public string EstadoTexto => Encendido ? "ENCENDIDO" : "APAGADO";

        public Brush ColorFondo => EstiloVisual?.ColorFondo;
        public Brush ColorTexto => EstiloVisual?.ColorTexto;
        public Brush ColorBoton => EstiloVisual?.ColorBoton;

        public abstract string Icono { get; }
        public abstract string TipoDispositivo { get; }
        public abstract void AccionPrincipal();

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void CambiarEstilo(IEstiloVisual nuevoEstilo)
        {
            EstiloVisual = nuevoEstilo;
        }
    }

    // ========== ABSTRACCIONES EXTENDIDAS ==========
    public class Luz : Dispositivo
    {
        public Luz(IEstiloVisual estiloVisual) : base(estiloVisual, "Luz Inteligente")
        {
        }

        public override string Icono => "💡";
        public override string TipoDispositivo => "Luz";

        public override void AccionPrincipal()
        {
            Encendido = !Encendido;
        }
    }

    public class Termostato : Dispositivo
    {
        private int _temperatura = 22;

        public Termostato(IEstiloVisual estiloVisual) : base(estiloVisual, "Termostato")
        {
        }

        public override string Icono => "🌡️";
        public override string TipoDispositivo => "Termostato";

        public int Temperatura
        {
            get => _temperatura;
            set
            {
                _temperatura = value;
                OnPropertyChanged(nameof(Temperatura));
            }
        }

        public override void AccionPrincipal()
        {
            Encendido = !Encendido;
        }

        public void SubirTemperatura() => Temperatura++;
        public void BajarTemperatura() => Temperatura--;
    }

    public class Cerradura : Dispositivo
    {
        public Cerradura(IEstiloVisual estiloVisual) : base(estiloVisual, "Cerradura Inteligente")
        {
        }

        public override string Icono => "🔒";
        public override string TipoDispositivo => "Cerradura";

        public override void AccionPrincipal()
        {
            Encendido = !Encendido;
        }

        public string EstadoBloqueo => Encendido ? "BLOQUEADA" : "DESBLOQUEADA";
    }

    // ========== VIEW MODEL ==========
    public class DispositivoViewModel
    {
        public ObservableCollection<Dispositivo> Dispositivos { get; set; }
        public IEstiloVisual EstiloActual { get; set; }
        public IEstiloVisual TemaAzul { get; }
        public IEstiloVisual TemaGris { get; }
        public IEstiloVisual TemaOscuro { get; }

        public DispositivoViewModel()
        {
            TemaAzul = new TemaAzul();
            TemaGris = new TemaGris();
            TemaOscuro = new TemaOscuro();
            EstiloActual = TemaAzul;

            Dispositivos = new ObservableCollection<Dispositivo>
            {
                new Luz(EstiloActual),
                new Termostato(EstiloActual),
                new Cerradura(EstiloActual)
            };
        }

        public void CambiarEstilo(IEstiloVisual nuevoEstilo)
        {
            EstiloActual = nuevoEstilo;
            foreach (var dispositivo in Dispositivos)
            {
                dispositivo.CambiarEstilo(nuevoEstilo);
            }
        }
    }
}