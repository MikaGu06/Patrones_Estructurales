using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Patrones_Estructurales.Adapterr.FormatoJsonXML
{
    /// <summary>
    /// Lógica de interacción para FormatoJsonXML.xaml
    /// </summary>
    public partial class FormatoJsonXML : UserControl
    {
        private List<ProductoApi> productosApi = new List<ProductoApi>();

        public FormatoJsonXML()
        {
            InitializeComponent();
        }

        // 🔹 BOTÓN AGREGAR PRODUCTO
        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            string nombre = TxtNombre.Text.Trim();
            string precio = TxtPrecio.Text.Trim();
            string stock = TxtStock.Text.Trim();

            // Validación nombre
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("El nombre no puede estar vacío.");
                TxtNombre.Focus();
                return;
            }

            // Validación precio
            if (!decimal.TryParse(precio, out decimal precioValido))
            {
                MessageBox.Show("El precio debe ser un número válido.");
                TxtPrecio.Focus();
                return;
            }

            // Validación stock
            if (!int.TryParse(stock, out int stockValido))
            {
                MessageBox.Show("El stock debe ser un número entero.");
                TxtStock.Focus();
                return;
            }

            // Crear producto
            ProductoApi producto = new ProductoApi
            {
                nombre = nombre,
                precio = precio,
                stock = stock
            };

            productosApi.Add(producto);

            MostrarProductos();

            // Limpiar campos
            TxtNombre.Clear();
            TxtPrecio.Clear();
            TxtStock.Clear();

            TxtNombre.Focus();
        }

        // 🔹 BOTÓN CONVERTIR
        private void BtnConvertir_Click(object sender, RoutedEventArgs e)
        {
            if (productosApi.Count == 0)
            {
                MessageBox.Show("Agrega al menos un producto.");
                return;
            }

            ArchivoXml archivoXml = new ArchivoXml(productosApi);

            IConvertidorFormato convertidor = new AdaptadorXmlJson(archivoXml);

            TxtXmlGenerado.Text = archivoXml.GenerarXml();
            TxtJsonResultado.Text = convertidor.Convertir();
        }

        // 🔹 MOSTRAR PRODUCTOS AGREGADOS
        private void MostrarProductos()
        {
            string texto = "";

            for (int i = 0; i < productosApi.Count; i++)
            {
                texto += "Producto " + (i + 1) +
                         "\nNombre: " + productosApi[i].nombre +
                         "\nPrecio: " + productosApi[i].precio +
                         "\nStock: " + productosApi[i].stock +
                         "\n\n";
            }

            TxtProductosAgregados.Text = texto;
        }

        // 🔹 SOLO NÚMEROS ENTEROS (STOCK)
        private void SoloNumeros_Entero(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        // 🔹 SOLO NÚMEROS DECIMALES (PRECIO)
        private void SoloNumeros_Decimal(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
