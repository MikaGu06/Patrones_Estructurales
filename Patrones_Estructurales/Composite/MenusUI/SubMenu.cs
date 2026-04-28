using System;
using System.Collections.Generic;
using System.Text;

namespace Patrones_Estructurales.Composite.MenusUI
{
    public class SubMenu : IMenuElemento
    {
        public string Nombre { get; }
        private List<IMenuElemento> _elementos = new List<IMenuElemento>();

        public SubMenu(string nombre)
        {
            Nombre = nombre;
        }

        public void Agregar(IMenuElemento elemento)
        {
            _elementos.Add(elemento);
        }

        public string Renderizar(int nivel)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{new string(' ', nivel * 4)}🗂️ {Nombre} (Desplegable)\n");

            foreach (var elemento in _elementos)
            {
                sb.Append(elemento.Renderizar(nivel + 1));
            }

            return sb.ToString();
        }
    }
}
