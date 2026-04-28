using System;
using System.Collections.Generic;
using System.Text;

namespace Patrones_Estructurales.Composite.OrganigramaEmpresa
{
    public class Jefe : IEmpleado
    {
        public string Nombre { get; }
        public string Puesto { get; }
        private decimal _salario;
        private List<IEmpleado> _subordinados = new List<IEmpleado>();

        public Jefe(string nombre, string puesto, decimal salario)
        {
            Nombre = nombre;
            Puesto = puesto;
            _salario = salario;
        }

        public void AgregarSubordinado(IEmpleado empleado) => _subordinados.Add(empleado);

        public decimal GetSalario()
        {
            // El costo de un jefe es su salario + el de todos sus subordinados
            return _salario + _subordinados.Sum(s => s.GetSalario());
        }

        public string Mostrar(int nivel)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{new string(' ', nivel * 4)}👑 {Nombre} ({Puesto}) - Jefe\n");

            foreach (var subordinado in _subordinados)
            {
                sb.Append(subordinado.Mostrar(nivel + 1));
            }
            return sb.ToString();
        }
    }
}
