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
        private INotificacion notificador;

        public LibreriaExterna()
        {
            InitializeComponent();
            notificador = new AdaptadorNotificacion();
        }

        private void BtnExito_Click(object sender, RoutedEventArgs e)
        {
            notificador.MostrarExito(TxtMensaje.Text);
            TxtResultado.Text = "Adapter llamó a ShowSuccess de la librería externa.";
        }

        private void BtnError_Click(object sender, RoutedEventArgs e)
        {
            notificador.MostrarError(TxtMensaje.Text);
            TxtResultado.Text = "Adapter llamó a ShowError de la librería externa.";
        }

        private void BtnInfo_Click(object sender, RoutedEventArgs e)
        {
            notificador.MostrarInfo(TxtMensaje.Text);
            TxtResultado.Text = "Adapter llamó a ShowInformation de la librería externa.";
        }
    }
}
