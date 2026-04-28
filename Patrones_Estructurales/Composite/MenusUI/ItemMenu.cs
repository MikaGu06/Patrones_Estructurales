using System;
using System.Collections.Generic;
using System.Text;

namespace Patrones_Estructurales.Composite.MenusUI
{
    public class ItemMenu : IMenuElemento
    {
        public string Nombre { get; }

        public ItemMenu(string nombre)
        {
            Nombre = nombre;
        }

        public string Renderizar(int nivel)
        {
            return $"{new string(' ', nivel * 4)}🔘 {Nombre}\n";
        }
    }
}
