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
            if (!decimal.TryParse(TxtPrecio.Text, out decimal precio))
            {
                TxtResultado.Text = "Error: ingrese un precio válido.";
                return;
            }

            IProducto producto = new ProductoBase(TxtProducto.Text, precio);
            producto = new ImpuestoDecorator(producto);
            producto = new DescuentoDecorator(producto);

            TxtResultado.Text =
                "Producto: " + producto.ObtenerNombre() +
                "\nPrecio final: " + producto.ObtenerPrecio() + " Bs";
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
        public ImpuestoDecorator(IProducto producto) : base(producto) { }

        public override decimal ObtenerPrecio()
        {
            return base.ObtenerPrecio() * 1.13m;
        }
    }

    public class DescuentoDecorator : ProductoDecorator
    {
        public DescuentoDecorator(IProducto producto) : base(producto) { }

        public override decimal ObtenerPrecio()
        {
            return base.ObtenerPrecio() - 10;
        }
    }
}