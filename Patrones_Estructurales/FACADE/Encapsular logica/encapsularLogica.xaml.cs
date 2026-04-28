using System;
using System.Windows;
using System.Windows.Controls;

namespace Patrones_Estructurales.Facade.MultiplesServicios
{
    public partial class MultiplesServicios : UserControl
    {
        public MultiplesServicios()
        {
            InitializeComponent();
        }

        private void BtnProcesar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtProducto.Text))
            {
                TxtResultado.Text = "Error: ingrese el nombre del producto.";
                return;
            }

            if (!decimal.TryParse(TxtPrecio.Text, out decimal precio))
            {
                TxtResultado.Text = "Error: ingrese un precio válido.";
                return;
            }

            VentaFacade facade = new VentaFacade();
            TxtResultado.Text = facade.ProcesarVenta(TxtProducto.Text, precio);
        }
    }

    public class VentaFacade
    {
        private readonly InventarioService inventario;
        private readonly PagoService pago;
        private readonly FacturaService factura;

        public VentaFacade()
        {
            inventario = new InventarioService();
            pago = new PagoService();
            factura = new FacturaService();
        }

        public string ProcesarVenta(string producto, decimal precio)
        {
            string resultado = "";
            resultado += inventario.VerificarStock(producto) + "\n";
            resultado += pago.ProcesarPago(precio) + "\n";
            resultado += factura.GenerarFactura(producto, precio);

            return resultado;
        }
    }

    public class InventarioService
    {
        public string VerificarStock(string producto)
        {
            return "Stock disponible para: " + producto;
        }
    }

    public class PagoService
    {
        public string ProcesarPago(decimal monto)
        {
            return "Pago procesado por: " + monto.ToString("0.00") + " Bs";
        }
    }

    public class FacturaService
    {
        public string GenerarFactura(string producto, decimal monto)
        {
            return "Factura generada por " + producto + " con total " + monto.ToString("0.00") + " Bs";
        }
    }
}