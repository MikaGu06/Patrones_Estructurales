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

namespace Patrones_Estructurales.Composite.Sistema_de_Archivos
{
    /// <summary>
    /// Interaction logic for SistemaArchivosView.xaml
    /// </summary>
    public partial class SistemaArchivosView : UserControl
    {
        // El estado de nuestro sistema
        private Carpeta _raiz;
        // Mantenemos una lista plana de solo las carpetas para el ComboBox
        private List<Carpeta> _todasLasCarpetasDisponibles; 
        public SistemaArchivosView()
        {
            InitializeComponent();
            InicializarSistema();
        }

        private void InicializarSistema()
        {
            // Empezamos con el disco C:
            _raiz = new Carpeta("C:");

            _todasLasCarpetasDisponibles = new List<Carpeta>();
            _todasLasCarpetasDisponibles.Add(_raiz);

            ActualizarUI();
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            string nombreElemento = TxtNombreElemento.Text.Trim();

            // Validaciones básicas
            if (string.IsNullOrEmpty(nombreElemento))
            {
                MessageBox.Show("Por favor, ingresa un nombre para el elemento.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Obtenemos la carpeta destino seleccionada en el ComboBox
            Carpeta carpetaDestino = CmbCarpetasPadre.SelectedItem as Carpeta;
            if (carpetaDestino == null) return;

            // Decidimos si creamos un archivo o una carpeta (¡AQUÍ ACTÚA EL PATRÓN!)
            if (RdbCarpeta.IsChecked == true)
            {
                Carpeta nuevaCarpeta = new Carpeta(nombreElemento);
                carpetaDestino.Agregar(nuevaCarpeta); // Agregamos al árbol
                _todasLasCarpetasDisponibles.Add(nuevaCarpeta); // Agregamos a la lista del ComboBox
            }
            else
            {
                Archivo nuevoArchivo = new Archivo(nombreElemento);
                carpetaDestino.Agregar(nuevoArchivo); // Agregamos al árbol
                // Un archivo no se agrega al ComboBox porque no puede contener cosas
            }

            // Limpiamos la caja de texto y actualizamos pantalla
            TxtNombreElemento.Clear();
            ActualizarUI();
        }

        private void ActualizarUI()
        {
            // 1. Refrescar el árbol visual usando la recursividad del Composite
            TxtResultado.Text = _raiz.Mostrar(0);

            // 2. Refrescar el ComboBox de carpetas disponibles
            CmbCarpetasPadre.ItemsSource = null;
            CmbCarpetasPadre.ItemsSource = _todasLasCarpetasDisponibles;

            // Si no hay nada seleccionado, seleccionar el primero (la raíz)
            if (CmbCarpetasPadre.SelectedIndex == -1 && _todasLasCarpetasDisponibles.Count > 0)
            {
                CmbCarpetasPadre.SelectedIndex = 0;
            }
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            var resultado = MessageBox.Show("¿Estás seguro de que quieres borrar todos los archivos y carpetas? Solo se mantendrá C:/",
                                   "Confirmar Limpieza",
                                   MessageBoxButton.YesNo,
                                   MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                //Llamamos de nuevo a la inicialización para resetear el sistema
                InicializarSistema();

                // Limpiamos los campos de texto
                TxtNombreElemento.Clear();

                MessageBox.Show("Sistema reiniciado correctamente.", "Limpieza", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
