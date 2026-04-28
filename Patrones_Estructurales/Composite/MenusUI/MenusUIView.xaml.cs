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

namespace Patrones_Estructurales.Composite.MenusUI
{
    /// <summary>
    /// Interaction logic for MenusUIView.xaml
    /// </summary>
    public partial class MenusUIView : UserControl
    {
        private SubMenu _menuPrincipal;
        private List<SubMenu> _subMenusDisponibles;
        public MenusUIView()
        {
            InitializeComponent();
            InicializarMenu();
        }
        private void InicializarMenu()
        {
            _menuPrincipal = new SubMenu("Barra de Navegación Principal");
            _subMenusDisponibles = new List<SubMenu> { _menuPrincipal };
            ActualizarPantalla();
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            string nombre = TxtNombre.Text.Trim();
            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("Ingresa un nombre para la opción.", "Aviso");
                return;
            }

            SubMenu padre = CmbSubMenus.SelectedItem as SubMenu;
            if (padre == null) return;

            if (RdbSubMenu.IsChecked == true)
            {
                SubMenu nuevoSubMenu = new SubMenu(nombre);
                padre.Agregar(nuevoSubMenu);
                _subMenusDisponibles.Add(nuevoSubMenu);
            }
            else
            {
                ItemMenu nuevoItem = new ItemMenu(nombre);
                padre.Agregar(nuevoItem);
            }

            TxtNombre.Clear();
            ActualizarPantalla();
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("¿Quieres borrar todo el menú?", "Limpiar", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                InicializarMenu();
                TxtNombre.Clear();
            }
        }
        private void ActualizarPantalla()
        {
            TxtResultado.Text = _menuPrincipal.Renderizar(0);

            CmbSubMenus.ItemsSource = null;
            CmbSubMenus.ItemsSource = _subMenusDisponibles;

            if (CmbSubMenus.SelectedIndex == -1 && _subMenusDisponibles.Count > 0)
            {
                CmbSubMenus.SelectedIndex = 0;
            }
        }
    }
}
