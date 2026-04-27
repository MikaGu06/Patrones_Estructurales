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

namespace Patrones_Estructurales.Adapterr.LibreriaExterna
{
    /// <summary>
    /// Lógica de interacción para LibreriaExterna.xaml
    /// </summary>
    public partial class LibreriaExterna : UserControl
    {
        public LibreriaExterna()
        {
            InitializeComponent();
        }

        private void BtnExito_Click(object sender, RoutedEventArgs e)
        {
            INotificacion notificacion = new AdaptadorNotificacion(TxtMensaje.Text, "Éxito");
            notificacion.Mostrar();

            TxtResultado.Text = "WPF usó INotificacion.Mostrar(), y el Adapter llamó a MostrarNotificacionExterna().";
        }

        private void BtnError_Click(object sender, RoutedEventArgs e)
        {
            INotificacion notificacion = new AdaptadorNotificacion(TxtMensaje.Text, "Error");
            notificacion.Mostrar();

            TxtResultado.Text = "WPF usó INotificacion.Mostrar(), y el Adapter llamó a MostrarNotificacionExterna().";
        }

        private void BtnInfo_Click(object sender, RoutedEventArgs e)
        {
            INotificacion notificacion = new AdaptadorNotificacion(TxtMensaje.Text, "Información");
            notificacion.Mostrar();

            TxtResultado.Text = "WPF usó INotificacion.Mostrar(), y el Adapter llamó a MostrarNotificacionExterna().";
        }
    }
}
