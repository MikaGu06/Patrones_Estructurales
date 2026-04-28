using System;
using System.Collections.Generic;
using System.Text;

namespace Patrones_Estructurales.Composite.Sistema_de_Archivos
{
    public class Carpeta : IComponenteArchivo
    {
        public string Nombre { get; }
        private List<IComponenteArchivo> _hijos = new List<IComponenteArchivo>();

        public Carpeta(string nombre)
        {
            Nombre = nombre;
        }

        public void Agregar(IComponenteArchivo componente)
        {
            _hijos.Add(componente);
        }

        public string Mostrar(int nivel)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{new string(' ', nivel * 4)}📁 {Nombre}\n");

            // Recursividad: la carpeta le pide a sus hijos que se muestren
            foreach (var hijo in _hijos)
            {
                sb.Append(hijo.Mostrar(nivel + 1));
            }

            return sb.ToString();
        }
    }
}
