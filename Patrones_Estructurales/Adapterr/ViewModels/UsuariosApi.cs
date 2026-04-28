using System;
using System.Collections.Generic;
using System.Text;

namespace Patrones_Estructurales.Adapterr.ViewModels
{
    public class UsuarioApi
    {
        public int id { get; set; }
        public string full_name { get; set; }
        public string email_address { get; set; }
        public string role_name { get; set; }
    }
}