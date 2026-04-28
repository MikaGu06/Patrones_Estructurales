using System;
using System.Windows;
using System.Windows.Controls;

namespace Patrones_Estructurales.Decorator
{
    public partial class Decorator : UserControl
    {
        public Decorator()
        {
            InitializeComponent();
            cmbEjemplos.SelectedIndex = 0;
        }

        private void cmbEjemplos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtResultado.Text = "";
            txtEntrada.Text = "";

            if (cmbEjemplos.SelectedIndex == 0)
            {
                txtTitulo.Text = "Validaciones dinámicas a controles";
                txtDescripcion.Text = "Usa Decorator para agregar validaciones a un TextBox sin modificar su clase base.";
                txtEntrada.Visibility = Visibility.Visible;
                txtEntrada.ToolTip = "Ingrese solo números";
            }
            else if (cmbEjemplos.SelectedIndex == 1)
            {
                txtTitulo.Text = "Extender funcionalidades de botones o textbox";
                txtDescripcion.Text = "Usa Decorator para agregar registro de acción y transformación de texto sin modificar el botón.";
                txtEntrada.Visibility = Visibility.Visible;
                txtEntrada.ToolTip = "Ingrese un texto para procesar";
            }
            else
            {
                txtTitulo.Text = "Comportamiento adicional sin modificar la clase base";
                txtDescripcion.Text = "Usa Decorator para agregar impuesto y descuento a un producto sin modificar la clase Producto.";
                txtEntrada.Visibility = Visibility.Collapsed;
            }
        }

        private void btnEjecutar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbEjemplos.SelectedIndex == 0)
                {
                    IControlEntrada entrada = new TextBoxEntrada(txtEntrada.Text);
                    entrada = new ValidacionVacioDecorator(entrada);
                    entrada = new ValidacionNumericaDecorator(entrada);

                    txtResultado.Text = entrada.ObtenerValor();
                }
                else if (cmbEjemplos.SelectedIndex == 1)
                {
                    IAccion accion = new BotonBasico(txtEntrada.Text);
                    accion = new LogDecorator(accion);
                    accion = new MayusculaDecorator(accion);

                    txtResultado.Text = accion.Ejecutar();
                }
                else
                {
                    IProducto producto = new ProductoBase();
                    producto = new ImpuestoDecorator(producto);
                    producto = new DescuentoDecorator(producto);

                    txtResultado.Text = $"Precio final del producto: {producto.ObtenerPrecio()} Bs";
                }
            }
            catch (Exception ex)
            {
                txtResultado.Text = ex.Message;
            }
        }
    }

    public interface IControlEntrada
    {
        string ObtenerValor();
    }

    public class TextBoxEntrada : IControlEntrada
    {
        private readonly string _valor;

        public TextBoxEntrada(string valor)
        {
            _valor = valor;
        }

        public string ObtenerValor()
        {
            return _valor;
        }
    }

    public abstract class ControlEntradaDecorator : IControlEntrada
    {
        protected IControlEntrada _control;

        public ControlEntradaDecorator(IControlEntrada control)
        {
            _control = control;
        }

        public virtual string ObtenerValor()
        {
            return _control.ObtenerValor();
        }
    }

    public class ValidacionVacioDecorator : ControlEntradaDecorator
    {
        public ValidacionVacioDecorator(IControlEntrada control) : base(control) { }

        public override string ObtenerValor()
        {
            string valor = base.ObtenerValor();

            if (string.IsNullOrWhiteSpace(valor))
                throw new Exception("El campo no puede estar vacío.");

            return valor;
        }
    }

    public class ValidacionNumericaDecorator : ControlEntradaDecorator
    {
        public ValidacionNumericaDecorator(IControlEntrada control) : base(control) { }

        public override string ObtenerValor()
        {
            string valor = base.ObtenerValor();

            if (!int.TryParse(valor, out _))
                throw new Exception("El campo solo acepta números.");

            return $"Valor válido ingresado: {valor}";
        }
    }

    public interface IAccion
    {
        string Ejecutar();
    }

    public class BotonBasico : IAccion
    {
        private readonly string _texto;

        public BotonBasico(string texto)
        {
            _texto = texto;
        }

        public string Ejecutar()
        {
            return $"Botón ejecutado con el texto: {_texto}";
        }
    }

    public abstract class AccionDecorator : IAccion
    {
        protected IAccion _accion;

        public AccionDecorator(IAccion accion)
        {
            _accion = accion;
        }

        public virtual string Ejecutar()
        {
            return _accion.Ejecutar();
        }
    }

    public class LogDecorator : AccionDecorator
    {
        public LogDecorator(IAccion accion) : base(accion) { }

        public override string Ejecutar()
        {
            return "Log agregado: la acción fue registrada.\n" + base.Ejecutar();
        }
    }

    public class MayusculaDecorator : AccionDecorator
    {
        public MayusculaDecorator(IAccion accion) : base(accion) { }

        public override string Ejecutar()
        {
            return base.Ejecutar().ToUpper();
        }
    }

    public interface IProducto
    {
        decimal ObtenerPrecio();
    }

    public class ProductoBase : IProducto
    {
        public decimal ObtenerPrecio()
        {
            return 100;
        }
    }

    public abstract class ProductoDecorator : IProducto
    {
        protected IProducto _producto;

        public ProductoDecorator(IProducto producto)
        {
            _producto = producto;
        }

        public virtual decimal ObtenerPrecio()
        {
            return _producto.ObtenerPrecio();
        }
    }

    public class ImpuestoDecorator : ProductoDecorator
    {
        public ImpuestoDecorator(IProducto producto) : base(producto) { }

        public override decimal ObtenerPrecio()
        {
            return base.ObtenerPrecio() + 13;
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