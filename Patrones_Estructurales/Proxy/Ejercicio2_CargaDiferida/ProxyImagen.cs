using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Patrones_Estructurales.Proxy.Ejercicio2_CargaDiferida
{
    public interface IImagen
    {
        string Nombre { get; }
        ImageSource Imagen { get; }
        string Tamaño { get; }
        string EstadoTexto { get; }
        Visibility CargandoVisible { get; }
        int Ancho { get; }
        int Alto { get; }
    }

    public class ProxyImagen : IImagen, INotifyPropertyChanged
    {
        private string _nombre;
        private string _urlImagen;
        private int _tamañoKB;
        private int _ancho;
        private int _alto;
        private bool _cargando = false;
        private bool _cargada = false;
        private ImageSource _imagen;
        private string _estadoTexto = "No cargada";

        public event PropertyChangedEventHandler PropertyChanged;

        public ProxyImagen(string nombre, string urlImagen, int tamañoKB, int ancho, int alto)
        {
            _nombre = nombre;
            _urlImagen = urlImagen;
            _tamañoKB = tamañoKB;
            _ancho = ancho;
            _alto = alto;
        }

        public string Nombre => _nombre;
        public string Tamaño => $"{_tamañoKB} KB";
        public int Ancho => _ancho;
        public int Alto => _alto;

        public ImageSource Imagen
        {
            get => _imagen;
            set { _imagen = value; OnPropertyChanged(); }
        }

        public string EstadoTexto
        {
            get => _estadoTexto;
            set { _estadoTexto = value; OnPropertyChanged(); }
        }

        public Visibility CargandoVisible => _cargando ? Visibility.Visible : Visibility.Collapsed;

        public void CargarImagen()
        {
            if (!_cargada && !_cargando)
            {
                _cargando = true;
                EstadoTexto = "Cargando...";
                OnPropertyChanged(nameof(CargandoVisible));

                Task.Run(async () =>
                {
                    // Pequeño delay para simular carga de red
                    await Task.Delay(800);

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        try
                        {
                            var bitmap = new BitmapImage();
                            bitmap.BeginInit();
                            bitmap.UriSource = new Uri(_urlImagen, UriKind.Absolute);
                            bitmap.DecodePixelWidth = _ancho;
                            bitmap.DecodePixelHeight = _alto;
                            bitmap.CacheOption = BitmapCacheOption.OnDemand;
                            bitmap.EndInit();

                            Imagen = bitmap;
                            _cargada = true;
                            _cargando = false;
                            EstadoTexto = "Cargada";
                            OnPropertyChanged(nameof(CargandoVisible));
                        }
                        catch (Exception ex)
                        {
                            _cargando = false;
                            EstadoTexto = "Error al cargar";
                            OnPropertyChanged(nameof(CargandoVisible));
                            System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
                        }
                    });
                });
            }
        }

        public void Resetear()
        {
            _cargada = false;
            _cargando = false;
            Imagen = null;
            EstadoTexto = "No cargada";
            OnPropertyChanged(nameof(CargandoVisible));
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}