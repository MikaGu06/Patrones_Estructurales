using System.Windows;
using System.Windows.Controls;

namespace Patrones_Estructurales.Facade.AccesoServicios
{
    public partial class AccesoServicios : UserControl
    {
        public AccesoServicios()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginFacade facade = new LoginFacade();
            TxtResultado.Text = facade.IniciarSesion(TxtUsuario.Text, TxtPassword.Text);
        }
    }

    public class LoginFacade
    {
        private readonly ServicioConexion conexion;
        private readonly ServicioValidacion validacion;
        private readonly ServicioSesion sesion;

        public LoginFacade()
        {
            conexion = new ServicioConexion();
            validacion = new ServicioValidacion();
            sesion = new ServicioSesion();
        }

        public string IniciarSesion(string usuario, string password)
        {
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(password))
                return "Error: complete todos los campos.";

            string resultado = "";
            resultado += conexion.Conectar() + "\n";
            resultado += validacion.Validar(usuario, password) + "\n";
            resultado += sesion.CrearSesion(usuario);

            return resultado;
        }
    }

    public class ServicioConexion
    {
        public string Conectar()
        {
            return "Servicio de conexión iniciado.";
        }
    }

    public class ServicioValidacion
    {
        public string Validar(string usuario, string password)
        {
            return "Credenciales validadas para: " + usuario;
        }
    }

    public class ServicioSesion
    {
        public string CrearSesion(string usuario)
        {
            return "Sesión creada correctamente para: " + usuario;
        }
    }
}