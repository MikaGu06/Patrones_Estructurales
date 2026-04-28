using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace Patrones_Estructurales.Proxy.Ejercicio2_CargaDiferida
{
    public partial class CargaDiferidaProxy : UserControl
    {
        private CargaDiferidaViewModel _viewModel;

        public CargaDiferidaProxy()
        {
            InitializeComponent();
            _viewModel = new CargaDiferidaViewModel();
            DataContext = _viewModel;
        }

        private void BtnCargarTodo_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.CargarTodas();
        }

        private void BtnReset_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.Resetear();
        }
    }

    public class CargaDiferidaViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ProxyImagen> _imagenes;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ProxyImagen> Imagenes
        {
            get => _imagenes;
            set { _imagenes = value; OnPropertyChanged(); }
        }

        public CargaDiferidaViewModel()
        {
            // Tus 6 imágenes de gatos con títulos "Gato 1" al "Gato 6"
            Imagenes = new ObservableCollection<ProxyImagen>
            {
                new ProxyImagen("Gato 1", "https://i.pinimg.com/1200x/36/ea/b6/36eab65d3d9268a81051eb4bf6487cc8.jpg", 2048, 250, 200),
                new ProxyImagen("Gato 2", "https://i.pinimg.com/1200x/a3/d6/c0/a3d6c0caf1fa0317bb7e8fa02819d6ff.jpg", 1850, 250, 200),
                new ProxyImagen("Gato 3", "https://i.pinimg.com/736x/cc/18/a1/cc18a1b03421840dcadff9c707e86ec1.jpg", 3200, 250, 200),
                new ProxyImagen("Gato 4", "https://i.pinimg.com/736x/10/bc/bd/10bcbdc51fdacda178fbf70267e19251.jpg", 2750, 250, 200),
                new ProxyImagen("Gato 5", "https://i.pinimg.com/1200x/bb/00/fb/bb00fbabd0a58d0bc918cb8bd5664837.jpg", 1650, 250, 200),
                new ProxyImagen("Gato 6", "https://i.pinimg.com/736x/14/20/fd/1420fdb2c1b84a55bc9a61e3050b0fa5.jpg", 1950, 250, 200)
            };
        }

        public void CargarTodas()
        {
            foreach (var imagen in Imagenes)
            {
                imagen.CargarImagen();
            }
        }

        public void Resetear()
        {
            foreach (var imagen in Imagenes)
            {
                imagen.Resetear();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}