using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Patrones_Estructurales.Adapterr.ViewModels
{
    public partial class ViewModels : UserControl
    {
        private List<UsuarioApi> usuariosApi = new List<UsuarioApi>();

        public ViewModels()
        {
            InitializeComponent();
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            string nombre = TxtFullName.Text.Trim();
            string correo = TxtEmail.Text.Trim();
            string rol = TxtRole.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("El campo full_name no puede estar vacío.");
                TxtFullName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(correo))
            {
                MessageBox.Show("El campo email_address no puede estar vacío.");
                TxtEmail.Focus();
                return;
            }

            if (!Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Ingresa un correo válido.");
                TxtEmail.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(rol))
            {
                MessageBox.Show("El campo role_name no puede estar vacío.");
                TxtRole.Focus();
                return;
            }

            UsuarioApi usuario = new UsuarioApi
            {
                id = usuariosApi.Count + 1,
                full_name = nombre,
                email_address = correo,
                role_name = rol
            };

            usuariosApi.Add(usuario);

            MostrarUsuariosApi();

            TxtFullName.Clear();
            TxtEmail.Clear();
            TxtRole.Clear();

            TxtFullName.Focus();
        }

        private void BtnConvertir_Click(object sender, RoutedEventArgs e)
        {
            if (usuariosApi.Count == 0)
            {
                MessageBox.Show("Agrega al menos un usuario.");
                return;
            }

            string resultado = "";

            for (int i = 0; i < usuariosApi.Count; i++)
            {
                IUsuario usuario = new AdaptadorUsuario(usuariosApi[i]);

                UsuarioViewModel vm = usuario.Mostrar();

                resultado += "ViewModel " + (i + 1) +
                             "\nNombre: " + vm.Nombre +
                             "\nCorreo: " + vm.Correo +
                             "\nRol: " + vm.Rol +
                             "\nResumen: " + vm.Resumen +
                             "\n\n";
            }

            TxtResultado.Text = resultado;
        }

        private void MostrarUsuariosApi()
        {
            string texto = "";

            for (int i = 0; i < usuariosApi.Count; i++)
            {
                texto += "UsuarioApi " + (i + 1) +
                         "\nid: " + usuariosApi[i].id +
                         "\nfull_name: " + usuariosApi[i].full_name +
                         "\nemail_address: " + usuariosApi[i].email_address +
                         "\nrole_name: " + usuariosApi[i].role_name +
                         "\n\n";
            }

            TxtUsuariosApi.Text = texto;
        }
    }
}   
