using System;
using System.Collections.Generic;
using System.Text;

namespace Patrones_Estructurales.Composite.OrganigramaEmpresa
{
    public class Empleado : IEmpleado
    {
        public string Nombre { get; }
        public string Puesto { get; }
        private decimal _salario;

        public Empleado(string nombre, string puesto, decimal salario)
        {
            Nombre = nombre;
            Puesto = puesto;
            _salario = salario;
        }

        public decimal GetSalario() => _salario;

        public string Mostrar(int nivel)
        {
            return $"{new string(' ', nivel * 4)}👤 {Nombre} ({Puesto}) - ${GetSalario():N2}\n";
        }
    }
}
