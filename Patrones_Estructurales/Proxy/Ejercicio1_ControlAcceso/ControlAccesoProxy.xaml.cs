using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;

namespace Patrones_Estructurales.Proxy.Ejercicio1_ControlAcceso
{
    public partial class ControlAccesoProxy : UserControl
    {
        private ProxySeguridad _proxy;

        public ControlAccesoProxy()
        {
            InitializeComponent();

            _proxy = new ProxySeguridad(
                "Planos de Infraestructura Critica",
                "Contenido confidencial: Los planos detallan ubicaciones de servidores, rutas de cables y sistemas de seguridad del edificio principal.",
                "Ingenieria Segura"
            );

            DataContext = _proxy;
        }

        private void BtnAdmin_Click(object sender, RoutedEventArgs e)
        {
            _proxy.UsuarioActual = "admin";
            ForzarActualizacionUI();
        }

        private void BtnEditor_Click(object sender, RoutedEventArgs e)
        {
            _proxy.UsuarioActual = "editor";
            ForzarActualizacionUI();
        }

        private void BtnLector_Click(object sender, RoutedEventArgs e)
        {
            _proxy.UsuarioActual = "lector";
            ForzarActualizacionUI();
        }

        private void BtnInvitado_Click(object sender, RoutedEventArgs e)
        {
            _proxy.UsuarioActual = "invitado";
            ForzarActualizacionUI();
        }

        private void ForzarActualizacionUI()
        {
            // Forzar actualización de todos los bindings
            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                var bindingExpression = this.GetBindingExpression(DataContextProperty);
                bindingExpression?.UpdateTarget();
            });
        }

        private void BtnCrear_Click(object sender, RoutedEventArgs e)
        {
            if (_proxy.PuedeCrear && !_proxy.DocumentoExiste)
            {
                var dialog = new Window
                {
                    Title = "Crear Nuevo Documento",
                    Width = 500,
                    Height = 400,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    Background = System.Windows.Media.Brushes.White,
                    ResizeMode = ResizeMode.NoResize
                };

                var stackPanel = new StackPanel { Margin = new Thickness(20) };

                stackPanel.Children.Add(new TextBlock { Text = "Titulo:", FontWeight = FontWeights.Bold, Margin = new Thickness(0, 0, 0, 5) });
                var txtTitulo = new TextBox { Margin = new Thickness(0, 0, 0, 15), Height = 30 };
                stackPanel.Children.Add(txtTitulo);

                stackPanel.Children.Add(new TextBlock { Text = "Autor:", FontWeight = FontWeights.Bold, Margin = new Thickness(0, 0, 0, 5) });
                var txtAutor = new TextBox { Margin = new Thickness(0, 0, 0, 15), Height = 30 };
                stackPanel.Children.Add(txtAutor);

                stackPanel.Children.Add(new TextBlock { Text = "Contenido:", FontWeight = FontWeights.Bold, Margin = new Thickness(0, 0, 0, 5) });
                var txtContenido = new TextBox { Margin = new Thickness(0, 0, 0, 15), Height = 150, TextWrapping = TextWrapping.Wrap, AcceptsReturn = true };
                stackPanel.Children.Add(txtContenido);

                var buttonPanel = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Center };

                var btnCrear = new Button { Content = "Crear Documento", Width = 120, Height = 35, Margin = new Thickness(5), Background = System.Windows.Media.Brushes.LightGreen };
                btnCrear.Click += (s, args) =>
                {
                    if (string.IsNullOrWhiteSpace(txtTitulo.Text))
                    {
                        MessageBox.Show("Debes ingresar un titulo.", "Validacion", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    bool exito = _proxy.CrearNuevoDocumento(txtTitulo.Text, txtContenido.Text, txtAutor.Text);
                    if (exito)
                    {
                        MessageBox.Show("Documento creado correctamente!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                        ForzarActualizacionUI();
                        dialog.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error al crear el documento.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                };
                buttonPanel.Children.Add(btnCrear);

                var btnCancelar = new Button { Content = "Cancelar", Width = 120, Height = 35, Margin = new Thickness(5), Background = System.Windows.Media.Brushes.LightCoral };
                btnCancelar.Click += (s, args) => dialog.Close();
                buttonPanel.Children.Add(btnCancelar);

                stackPanel.Children.Add(buttonPanel);
                dialog.Content = stackPanel;
                dialog.Owner = Window.GetWindow(this);
                dialog.ShowDialog();
            }
            else if (_proxy.DocumentoExiste)
            {
                MessageBox.Show("Ya existe un documento. Eliminalo primero si quieres crear uno nuevo.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No tienes permisos para crear documentos. Solo el Admin puede crear.", "Acceso Denegado", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (_proxy.PuedeEditar && _proxy.DocumentoExiste)
            {
                // Guardar valores originales para comparar
                string tituloOriginal = _proxy.Titulo;
                string contenidoOriginal = _proxy.Contenido;
                string autorOriginal = _proxy.Autor;

                var dialog = new Window
                {
                    Title = "Editar Documento",
                    Width = 500,
                    Height = 450,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    Background = System.Windows.Media.Brushes.White,
                    ResizeMode = ResizeMode.NoResize
                };

                var stackPanel = new StackPanel { Margin = new Thickness(20) };

                stackPanel.Children.Add(new TextBlock { Text = "Titulo:", FontWeight = FontWeights.Bold, Margin = new Thickness(0, 0, 0, 5) });
                var txtTitulo = new TextBox { Text = tituloOriginal, Margin = new Thickness(0, 0, 0, 15), Height = 30 };
                stackPanel.Children.Add(txtTitulo);

                stackPanel.Children.Add(new TextBlock { Text = "Autor:", FontWeight = FontWeights.Bold, Margin = new Thickness(0, 0, 0, 5) });
                var txtAutor = new TextBox { Text = autorOriginal, Margin = new Thickness(0, 0, 0, 15), Height = 30 };
                stackPanel.Children.Add(txtAutor);

                stackPanel.Children.Add(new TextBlock { Text = "Contenido:", FontWeight = FontWeights.Bold, Margin = new Thickness(0, 0, 0, 5) });
                var txtContenido = new TextBox { Text = contenidoOriginal, Margin = new Thickness(0, 0, 0, 15), Height = 150, TextWrapping = TextWrapping.Wrap, AcceptsReturn = true };
                stackPanel.Children.Add(txtContenido);

                var buttonPanel = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Center };

                var btnGuardar = new Button { Content = "Guardar Cambios", Width = 120, Height = 35, Margin = new Thickness(5), Background = System.Windows.Media.Brushes.LightGreen };
                btnGuardar.Click += (s, args) =>
                {
                    // Mostrar confirmación de lo que se va a guardar
                    string mensaje = $"Se guardaran los siguientes cambios:\n\n" +
                                     $"Titulo: '{tituloOriginal}' → '{txtTitulo.Text}'\n" +
                                     $"Autor: '{autorOriginal}' → '{txtAutor.Text}'\n" +
                                     $"Contenido: {(contenidoOriginal != txtContenido.Text ? "Modificado" : "Sin cambios")}";

                    var confirmacion = MessageBox.Show(mensaje, "Confirmar cambios", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (confirmacion == MessageBoxResult.Yes)
                    {
                        _proxy.EditarDocumento(txtTitulo.Text, txtContenido.Text, txtAutor.Text);

                        // Verificar que los cambios se aplicaron
                        string mensajeExito = $"Documento actualizado!\n\n" +
                                             $"Nuevo Titulo: {_proxy.Titulo}\n" +
                                             $"Nuevo Autor: {_proxy.Autor}";

                        MessageBox.Show(mensajeExito, "Exito", MessageBoxButton.OK, MessageBoxImage.Information);

                        ForzarActualizacionUI();
                        dialog.Close();
                    }
                };
                buttonPanel.Children.Add(btnGuardar);

                var btnCancelar = new Button { Content = "Cancelar", Width = 120, Height = 35, Margin = new Thickness(5), Background = System.Windows.Media.Brushes.LightCoral };
                btnCancelar.Click += (s, args) => dialog.Close();
                buttonPanel.Children.Add(btnCancelar);

                stackPanel.Children.Add(buttonPanel);
                dialog.Content = stackPanel;
                dialog.Owner = Window.GetWindow(this);
                dialog.ShowDialog();
            }
            else if (!_proxy.DocumentoExiste)
            {
                MessageBox.Show("No se puede editar: El documento fue eliminado. Crea uno nuevo primero.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("No tienes permisos para editar este documento.", "Acceso Denegado", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (_proxy.PuedeEliminar && _proxy.DocumentoExiste)
            {
                var result = MessageBox.Show($"¿Estas SEGURO de eliminar este documento?\n\nTitulo: {_proxy.Titulo}\nAutor: {_proxy.Autor}\n\nEsta accion NO se puede deshacer.",
                    "Confirmar Eliminacion", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    bool exito = _proxy.EliminarDocumento();
                    if (exito)
                    {
                        MessageBox.Show("Documento eliminado correctamente.", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                        ForzarActualizacionUI();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el documento.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else if (!_proxy.DocumentoExiste)
            {
                MessageBox.Show("El documento ya fue eliminado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("No tienes permisos para eliminar este documento.", "Acceso Denegado", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }

    // Convertidores
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value is bool && (bool)value) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class InvertBoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value is bool && (bool)value) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}