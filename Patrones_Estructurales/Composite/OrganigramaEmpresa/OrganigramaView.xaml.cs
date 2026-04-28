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

namespace Patrones_Estructurales.Composite.OrganigramaEmpresa
{
    /// <summary>
    /// Interaction logic for OrganigramaView.xaml
    /// </summary>
    public partial class OrganigramaView : UserControl
    {
        private Jefe _ceo;
        private List<Jefe> _listaDeJefes;
        public OrganigramaView()
        {
            InitializeComponent();
            _ceo = new Jefe("Director General (CEO)", "CEO", 5000);
            _listaDeJefes = new List<Jefe> { _ceo };
            ActualizarPantalla();
        }

        private void BtnContratar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtNombre.Text) || !decimal.TryParse(TxtSalario.Text, out decimal salario))
            {
                MessageBox.Show("Datos inválidos.");
                return;
            }

            Jefe superior = CmbJefes.SelectedItem as Jefe;
            IEmpleado nuevoEmpleado;

            if (ChkEsJefe.IsChecked == true)
            {
                var nuevoJefe = new Jefe(TxtNombre.Text, TxtPuesto.Text, salario);
                nuevoEmpleado = nuevoJefe;
                _listaDeJefes.Add(nuevoJefe);
            }
            else
            {
                nuevoEmpleado = new Empleado(TxtNombre.Text, TxtPuesto.Text, salario);
            }

            superior.AgregarSubordinado(nuevoEmpleado);
            LimpiarCampos();
            ActualizarPantalla();
        }
        private void ActualizarPantalla()
        {
            TxtOrganigrama.Text = _ceo.Mostrar(0);
            TxtCostoTotal.Text = $"Costo Total Nómina: ${_ceo.GetSalario():N2}";

            CmbJefes.ItemsSource = null;
            CmbJefes.ItemsSource = _listaDeJefes;
            CmbJefes.SelectedIndex = 0;
        }

        private void LimpiarCampos()
        {
            TxtNombre.Clear();
            TxtPuesto.Clear();
            TxtSalario.Clear();
            ChkEsJefe.IsChecked = false;
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            var resultado = MessageBox.Show("¿Deseas despedir a todo el personal y reiniciar el organigrama? Solo quedará el CEO.",
                                   "Reiniciar Empresa",
                                   MessageBoxButton.YesNo,
                                   MessageBoxImage.Warning);

            if (resultado == MessageBoxResult.Yes)
            {
                // Reiniciamos el CEO (Raíz)
                _ceo = new Jefe("Director General (CEO)", "CEO", 5000);

                // Reiniciamos la lista que alimenta al ComboBox
                _listaDeJefes = new List<Jefe> { _ceo };

                // Limpiamos los campos de entrada
                LimpiarCampos();

                // Refrescamos la pantalla
                ActualizarPantalla();

                MessageBox.Show("El organigrama ha sido reiniciado.", "Éxito");
            }
        }
    }
}
