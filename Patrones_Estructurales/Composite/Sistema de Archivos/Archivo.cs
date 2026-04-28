using System;
using System.Collections.Generic;
using System.Text;

namespace Patrones_Estructurales.Composite.Sistema_de_Archivos
{
    public class Archivo : IComponenteArchivo
    {
        public string Nombre { get; }

        public Archivo(string nombre)
        {
            Nombre = nombre;
        }

        public string Mostrar(int nivel)
        {
            // Retorna el nombre con sangría según su profundidad
            return $"{new string(' ', nivel * 4)}📄 {Nombre}\n";
        }
    }
}
