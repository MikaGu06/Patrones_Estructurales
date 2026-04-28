using System;
using System.Collections.Generic;
using System.Text;
namespace Patrones_Estructurales.Adapterr.ViewModels
{
    public class AdaptadorUsuario : IUsuario
    {
        private UsuarioApi usuarioApi;

        public AdaptadorUsuario(UsuarioApi usuario)
        {
            usuarioApi = usuario;
        }

        public UsuarioViewModel Mostrar()
        {
            return new UsuarioViewModel
            {
                Nombre = usuarioApi.full_name,
                Correo = usuarioApi.email_address,
                Rol = usuarioApi.role_name,
                Resumen = usuarioApi.full_name + " - " + usuarioApi.role_name
            };
        }
    }
}