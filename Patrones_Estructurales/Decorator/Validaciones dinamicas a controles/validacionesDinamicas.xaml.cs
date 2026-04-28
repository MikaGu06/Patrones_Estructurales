using System;
using System.Windows;
using System.Windows.Controls;

namespace Patrones_Estructurales.Decorator.Validaciones
{
    public partial class Validaciones : UserControl
    {
        public Validaciones()
        {
            InitializeComponent();
        }

        private void BtnValidar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IEntrada entrada = new EntradaBase(TxtValor.Text);

                if (ChkVacio.IsChecked == true)
                    entrada = new ValidacionVacioDecorator(entrada);

                if (ChkNumerico.IsChecked == true)
                    entrada = new ValidacionNumericaDecorator(entrada);

                if (ChkLongitud.IsChecked == true)
                {
                    if (!int.TryParse(TxtLongitud.Text, out int longitud))
                    {
                        TxtResultado.Text = "Error: ingrese una longitud válida.";
                        return;
                    }

                    entrada = new ValidacionLongitudDecorator(entrada, longitud);
                }

                TxtResultado.Text = entrada.ObtenerValor();
            }
            catch (Exception ex)
            {
                TxtResultado.Text = ex.Message;
            }
        }
    }

    public interface IEntrada
    {
        string ObtenerValor();
    }

    public class EntradaBase : IEntrada
    {
        private readonly string valor;

        public EntradaBase(string valor)
        {
            this.valor = valor;
        }

        public string ObtenerValor()
        {
            return valor;
        }
    }

    public abstract class EntradaDecorator : IEntrada
    {
        protected IEntrada entrada;

        protected EntradaDecorator(IEntrada entrada)
        {
            this.entrada = entrada;
        }

        public virtual string ObtenerValor()
        {
            return entrada.ObtenerValor();
        }
    }

    public class ValidacionVacioDecorator : EntradaDecorator
    {
        public ValidacionVacioDecorator(IEntrada entrada) : base(entrada) { }

        public override string ObtenerValor()
        {
            string valor = base.ObtenerValor();

            if (string.IsNullOrWhiteSpace(valor))
                throw new Exception("Error: el campo no puede estar vacío.");

            return valor;
        }
    }

    public class ValidacionNumericaDecorator : EntradaDecorator
    {
        public ValidacionNumericaDecorator(IEntrada entrada) : base(entrada) { }

        public override string ObtenerValor()
        {
            string valor = base.ObtenerValor();

            if (!int.TryParse(valor, out _))
                throw new Exception("Error: el campo solo acepta números.");

            return valor;
        }
    }

    public class ValidacionLongitudDecorator : EntradaDecorator
    {
        private readonly int longitudMinima;

        public ValidacionLongitudDecorator(IEntrada entrada, int longitudMinima) : base(entrada)
        {
            this.longitudMinima = longitudMinima;
        }

        public override string ObtenerValor()
        {
            string valor = base.ObtenerValor();

            if (valor.Length < longitudMinima)
                throw new Exception("Error: el campo debe tener mínimo " + longitudMinima + " caracteres.");

            return "Valor válido: " + valor;
        }
    }
}