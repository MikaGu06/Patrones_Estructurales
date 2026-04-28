using System;
using System.Collections.Generic;
using System.Text;

namespace Patrones_Estructurales.Composite.OrganigramaEmpresa
{
    public interface IEmpleado
    {
        string Nombre { get; }
        string Puesto { get; }
        decimal GetSalario();
        string Mostrar(int nivel);
    }
}
