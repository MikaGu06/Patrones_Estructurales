using System;
using System.Windows;
using System.Windows.Controls;

namespace Patrones_Estructurales.Decorator.ComportamientoAdicional
{
    public partial class ComportamientoAdicional : UserControl
    {
        public ComportamientoAdicional()
        {
            InitializeComponent();
        }

        private void BtnCalcular_Click(object sender, RoutedEventArgs e)
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

            IProducto producto = new ProductoBase(TxtProducto.Text, precio);

            if (ChkImpuesto.IsChecked == true)
            {
                if (!decimal.TryParse(TxtImpuesto.Text, out decimal impuesto))
                {
                    TxtResultado.Text = "Error: ingrese un impuesto válido.";
                    return;
                }

                producto = new ImpuestoDecorator(producto, impuesto);
            }

            if (ChkDescuento.IsChecked == true)
            {
                if (!decimal.TryParse(TxtDescuento.Text, out decimal descuento))
                {
                    TxtResultado.Text = "Error: ingrese un descuento válido.";
                    return;
                }

                producto = new DescuentoDecorator(producto, descuento);
            }

            TxtResultado.Text =
                "Producto: " + producto.ObtenerNombre() +
                "\nPrecio final: " + producto.ObtenerPrecio().ToString("0.00") + " Bs";
        }
    }

    public interface IProducto
    {
        string ObtenerNombre();
        decimal ObtenerPrecio();
    }

    public class ProductoBase : IProducto
    {
        private readonly string nombre;
        private readonly decimal precio;

        public ProductoBase(string nombre, decimal precio)
        {
            this.nombre = nombre;
            this.precio = precio;
        }

        public string ObtenerNombre()
        {
            return nombre;
        }

        public decimal ObtenerPrecio()
        {
            return precio;
        }
    }

    public abstract class ProductoDecorator : IProducto
    {
        protected IProducto producto;

        protected ProductoDecorator(IProducto producto)
        {
            this.producto = producto;
        }

        public virtual string ObtenerNombre()
        {
            return producto.ObtenerNombre();
        }

        public virtual decimal ObtenerPrecio()
        {
            return producto.ObtenerPrecio();
        }
    }

    public class ImpuestoDecorator : ProductoDecorator
    {
        private readonly decimal porcentaje;

        public ImpuestoDecorator(IProducto producto, decimal porcentaje) : base(producto)
        {
            this.porcentaje = porcentaje;
        }

        public override decimal ObtenerPrecio()
        {
            return base.ObtenerPrecio() + (base.ObtenerPrecio() * porcentaje / 100);
        }
    }

    public class DescuentoDecorator : ProductoDecorator
    {
        private readonly decimal descuento;

        public DescuentoDecorator(IProducto producto, decimal descuento) : base(producto)
        {
            this.descuento = descuento;
        }

        public override decimal ObtenerPrecio()
        {
            return base.ObtenerPrecio() - descuento;
        }
    }
}