using System.Windows;
using System.Windows.Controls;

namespace Patrones_Estructurales.Decorator.ExtenderControles
{
    public partial class ExtenderControles : UserControl
    {
        public ExtenderControles()
        {
            InitializeComponent();
        }

        private void BtnEjecutar_Click(object sender, RoutedEventArgs e)
        {
            IAccion accion = new BotonBase(TxtTexto.Text);
            accion = new RegistroDecorator(accion);
            accion = new MayusculaDecorator(accion);

            TxtResultado.Text = accion.Ejecutar();
        }
    }

    public interface IAccion
    {
        string Ejecutar();
    }

    public class BotonBase : IAccion
    {
        private readonly string texto;

        public BotonBase(string texto)
        {
            this.texto = texto;
        }

        public string Ejecutar()
        {
            return "Texto ingresado: " + texto;
        }
    }

    public abstract class AccionDecorator : IAccion
    {
        protected IAccion accion;

        protected AccionDecorator(IAccion accion)
        {
            this.accion = accion;
        }

        public virtual string Ejecutar()
        {
            return accion.Ejecutar();
        }
    }

    public class RegistroDecorator : AccionDecorator
    {
        public RegistroDecorator(IAccion accion) : base(accion) { }

        public override string Ejecutar()
        {
            return "Registro: el botón fue presionado.\n" + base.Ejecutar();
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
}